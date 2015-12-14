namespace GangstaStreetz.Models.Screens.Proginitor
{
    using Config.StaticClasses;
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

        public static readonly string GameFontPath = ScreensConfig.PathToGameFont;
        public static readonly string SwitchScreenPath = ScreensConfig.PathToSwitchScreen;

        protected SpriteFont Font { get; set; }        

        protected ContentManager Content { get; private set; }

        public string XmlPath { get; set; }

        public GameScreen()
        {
            this.Type = this.GetType();
            XmlPath = Path.Combine(ScreensConfig.PathToFolder,
                        this.GetType().Name + "." + ScreensConfig.FileExtension);
        }

        public virtual void LoadContent()
        {
            Content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (!string.IsNullOrEmpty(GameFontPath))
                this.Font = Content.Load<SpriteFont>(GameFontPath);
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
