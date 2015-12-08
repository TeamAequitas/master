using Gangstarz.Models.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gangstarz.Managers
{
    public class ManuManager
    {
        private Menu menu;

        private bool isTransitioning;

        public ManuManager()
        {
            menu = new Menu();
            menu.OnMenuChange += Menu_OnMenuChange;//we are adding this method to the event, like delegat
        }

        private void Menu_OnMenuChange(object sender, EventArgs e)//everything that is added with menuChange will triggure this  method
        {
            XMLManager<Menu> xmlManager = new XMLManager<Menu>();
            menu.UnloadContent();
            menu = xmlManager.Load(menu.Id);//we r loading brand new menu
            menu.LoadContent();
            menu.OnMenuChange += Menu_OnMenuChange;
            menu.Transition(0.0f);

            foreach (MenuItem item in menu.Items)
            {
                item.Image.StoreEffects();
                item.Image.ActivateEffect("FadeEffect");
            }
        }

        private void Transition(GameTime gameTime)//do we need this?
        {
            if (isTransitioning)
            {
                for (int i = 0; i < menu.Items.Count; i++)
                {
                    menu.Items[i].Image.Update(gameTime);
                    float first = menu.Items[0].Image.Alpha;
                    float last = menu.Items[menu.Items.Count - 1].Image.Alpha;
                    if (first == 0.0f && last == 0.0f)
                    {
                        menu.Id = menu.Items[menu.ItemNumber].LinkId;
                    }
                    else if (first == 1.0f && last == 1.0f)
                    {
                        isTransitioning = false;
                        foreach (var item in menu.Items)
                        {
                            item.Image.RestoreEffects();
                        }
                    }
                        
                }
            }
        }

        public void LoadContent(string menuPath)
        {
            if (menuPath != string.Empty)
                menu.Id = menuPath;
        }

        public void UnloadContent()
        {
            menu.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (!isTransitioning)
                menu.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.Enter) && !isTransitioning)
            {
                if (menu.Items[menu.ItemNumber].LinkType == "Screen")
                    ScreenManager.Instance.SwitchScreens(menu.Items[menu.ItemNumber].LinkId);
                else
                {
                    isTransitioning = true;
                    menu.Transition(1.0f);
                    foreach(MenuItem item in menu.Items)
                    {
                        item.Image.StoreEffects();
                        item.Image.ActivateEffect("FadeEffect");
                    }
                }
            }

            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
