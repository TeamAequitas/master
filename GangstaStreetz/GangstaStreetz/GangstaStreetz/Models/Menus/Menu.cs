namespace GangstaStreetz.Models.Menus
{
    using Config.StaticClasses;
    using GangstaStreetz.Enums;
    using Images;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Menu
    {
        [XmlElement("Button")]
        public List<Button> Buttons { get; set; }

        [XmlElement("Alignment")]
        public MenuAlignment Alignment { get; set; }       

        public int Margin { get; set; }

        public CombinedImage Background { get; set; }

        [XmlElement("MenuCenter")]
        public Vector2 MenuCenter { get; set; }

        public string MenuFont { get; set; }

        public Menu()
        {
            this.MenuCenter = Vector2.Zero;
            this.Buttons = new List<Button>();
            MenuFont = String.Empty;
        }

        public void LoadContent()
        {
            if (Background != null)
                Background.LoadContent();
            
            foreach (var btn in Buttons)
            {
                btn.FontPath = MenuFont;
                btn.LoadContent();
            }

            switch (Alignment)
            {
                case MenuAlignment.X:
                    AlignBtnsX();
                    break;
                case MenuAlignment.Y:
                    AlignBtnsY();
                    break;
                default:
                    break;
            }
        }

        public void UnloadContent()
        {
            foreach (var btn in Buttons)
                btn.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var btn in Buttons)
                btn.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Background != null)
                Background.Draw(spriteBatch);

            foreach (var btn in Buttons)
                btn.Draw(spriteBatch);                   
        }

        private void AlignBtnsY()
        {
            if (Background != null)
                Background.PositionUpLeft = this.MenuCenter;

            if (Buttons.Any())
            {
                var overallHeight = this.Margin * (this.Buttons.Count - 1);
                foreach (var btn in Buttons)
                    overallHeight += btn.CollisionRectangle.Height;

                var position = overallHeight / 2;
                foreach(var btn in Buttons)
                {
                    var pos = position - btn.CollisionRectangle.Height / 2;
                    btn.PositionUpLeft = new Vector2(this.MenuCenter.X,
                        - pos + this.MenuCenter.Y);      
                    position -= (btn.CollisionRectangle.Height / 2 + this.Margin);                    
                }
            }
        }

        private void AlignBtnsX()
        {
            if (Background != null)
                Background.PositionUpLeft = this.MenuCenter;

            if (Buttons.Any())
            {
                var overallWidth = this.Margin * (this.Buttons.Count - 1);
                foreach (var btn in Buttons)
                    overallWidth += btn.CollisionRectangle.Width;

                var position = overallWidth / 2;
                foreach (var btn in Buttons)
                {
                    var pos = position - btn.CollisionRectangle.Width / 2;
                    btn.PositionUpLeft = new Vector2( - position + btn.CollisionRectangle.Width / 2,//To be revised
                        this.MenuCenter.Y);
                    position -= (btn.CollisionRectangle.Width / 2 + this.Margin);
                }
            }
        }
    }
}
