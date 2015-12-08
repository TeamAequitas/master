using Gangstarz.Models.Screens.Proginitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Gangstarz.Managers;
using Gangstarz.Config;
using System.IO;

namespace Gangstarz.Models.Screens
{
    public class TitleScreen : GameScreen
    {
        private ManuManager menuManager;

        public TitleScreen()
        {
            menuManager = new ManuManager();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            string path = Path.Combine(MenusConfig.PathToConfigFolder,
                        MenusConfig.TitleScreen + "." + ScreensConfig.FileExtension);
            menuManager.LoadContent(Path.Combine(MenusConfig.PathToConfigFolder,
                        MenusConfig.TitleScreen + "." + ScreensConfig.FileExtension));
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            menuManager.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            menuManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            menuManager.Draw(spriteBatch);
        }
    }
}
