using Gangstarz.Config;
using Gangstarz.Models.Screens;
using Gangstarz.Models.Screens.Proginitor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Gangstarz.Managers
{
    class ScreenManager
    {
        public static readonly Vector2 Dimensions = 
            new Vector2(ScreensConfig.ScreenWidth, ScreensConfig.ScreenHeight);

        private static ScreenManager instance;

        public ContentManager Content { get; private set; }

        public GameScreen CurrentScreen { get; private set; }

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenManager();
                }

                return instance;
            }
        }

        public ScreenManager()
        {
            CurrentScreen = new SplashScreen();
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            CurrentScreen.LoadContent();
        }

        public void UnloadContent()
        {
            CurrentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
        }
    }
}
