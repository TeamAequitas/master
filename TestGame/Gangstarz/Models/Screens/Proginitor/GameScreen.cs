namespace Gangstarz.Models.Screens.Proginitor
{
    using Config;
    using Managers;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Xml.Serialization;

    public class GameScreen
    {
        protected ContentManager Content { get; private set; }
        [XmlIgnore]
        public Type Type;

        protected SpriteFont Font { get; set; }

        public GameScreen()
        {
            this.Type = this.GetType();
        }

        public virtual void LoadContent()
        {
            Content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider, "Content");

            this.Font = Content.Load<SpriteFont>(ScreensConfig.GameFont);
        }

        public virtual void UnloadContent()
        {
            Content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
