using Gangstarz.Models.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gangstarz.Managers
{
    public class ManuManager
    {
        private Menu menu;

        public ManuManager()
        {
            menu = new Menu();
            menu.OnMenuChange += Menu_OnMenuChange;//we are adding this method to the event, like delegat
        }

        private void Menu_OnMenuChange(object sender, EventArgs e)//everything that is added with menuChange will triggure this  method
        {
            XMLManager<Menu> xmlManager = new XMLManager<Menu>();
            menu.UnloadContent();
            menu = xmlManager.Load(menu.Id);
            menu.LoadContent();  
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
            menu.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
