namespace Gangstarz.Models.Screens.Proginitor
{
    using Config;
    using Managers;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.IO;
    using System.Xml.Serialization;

    public abstract class GameScreen
    {   
        [XmlIgnore]
        public Type Type;

        protected SpriteFont Font { get; set; }

        public string MainFont { get; set; }

        protected ContentManager Content { get; private set; }

        public string XmlPath { get; set; }

        public GameScreen()
        {
            this.Type = this.GetType();
            XmlPath = Path.Combine(ScreensConfig.PathToConfigFolder, 
                        this.GetType().Name + "." + ScreensConfig.FileExtension);
        }

        public virtual void LoadContent()
        {
            Content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (!string.IsNullOrEmpty(MainFont))            
                this.Font = Content.Load<SpriteFont>(MainFont);
        }

        public virtual void UnloadContent()
        {
            Content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
            InputManager.Instance.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
