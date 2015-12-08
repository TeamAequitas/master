using Gangstarz.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Gangstarz.Models.Menus
{
    public class Menu
    {
        public string Axis { get; set; }
        public string Effects { get; set; }

        [XmlElement("Item")]
        public List<MenuItem> Items { get; set; }

        private string id;//inherited from GameObject?
        private int itemNumber;

        public int ItemNumber
        {
            get { return itemNumber; }
        }

        public event EventHandler OnMenuChange;

        public Menu()
        {
            id = string.Empty;
            itemNumber = 0;
            Effects = String.Empty;
            Axis = "Y";
            Items = new List<MenuItem>();
        }

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnMenuChange(this, null);//it calls all the methods we want to call
            }
        }

        public void Transition(float alpha)
        {
            foreach (var item in Items)
            {
                item.Image.IsActive = true;
                item.Image.Alpha = alpha;
                if (alpha == 0.0f)
                    item.Image.fadeEffect.Increase = true;
                else
                    item.Image.fadeEffect.Increase = false;
            }
        }

        private void AlignItems()
        {
            Vector2 dimentions = Vector2.Zero;

            foreach (MenuItem item in Items)
                dimentions += new Vector2(item.Image.CollisionRectangle.Width,
                    item.Image.CollisionRectangle.Height);

            dimentions = new Vector2((ScreenManager.Dimensions.X - dimentions.X) / 2,
                (ScreenManager.Dimensions.Y - dimentions.Y) / 2);

            foreach (var item in Items)
            {
                if (Axis == "X")
                    item.Image.Position = new Vector2(dimentions.X,
                        (ScreenManager.Dimensions.Y - item.Image.CollisionRectangle.Height)/2);
                else if(Axis == "Y")
                    item.Image.Position = new Vector2((ScreenManager.Dimensions.X - 
                        item.Image.CollisionRectangle.Width) / 2, dimentions.Y);

                dimentions += new Vector2(item.Image.CollisionRectangle.Width,
                    item.Image.CollisionRectangle.Height);
            }
        }

        
        public void LoadContent()
        {
            string[] split = Effects.Split(':');
            foreach (MenuItem item in Items)
            {
                item.Image.LoadContent();
                foreach (string s in split)
                    item.Image.ActivateEffect(s);
            }

            AlignItems();
        }

        public void UnloadContent()
        {
            foreach (MenuItem item in Items)
                item.Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var item in Items)//not sure if we need this
            {
                if (Axis == "X")
                {
                    if (InputManager.Instance.KeyPressed(Keys.Right))
                    {
                        itemNumber++;
                        break;
                    }                        
                    else if (InputManager.Instance.KeyPressed(Keys.Left))
                    {
                        itemNumber--;
                        break;
                    }                        
                }                   
                else if(Axis == "Y")
                {
                    if (InputManager.Instance.KeyPressed(Keys.Down))
                    {
                        itemNumber++;
                        break;
                    }                       
                    else if (InputManager.Instance.KeyPressed(Keys.Up))
                    {
                        itemNumber--;
                        break;

                    }                        
                }                        
            }

            if (itemNumber < 0)
                itemNumber = 0;
            else if (itemNumber > Items.Count - 1)
                itemNumber = Items.Count - 1;

            for (int i = 0; i < Items.Count; i++)
            {
                if (i == itemNumber)
                    Items[i].Image.IsActive = true;
                else
                    Items[i].Image.IsActive = false;

                Items[i].Image.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in Items)
                item.Image.Draw(spriteBatch);
        }
    }
}
