using Gangstarz.Config;
using Gangstarz.Models.Images.Proginitor;
using Gangstarz.Models.Screens;
using Gangstarz.Models.Screens.Proginitor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Gangstarz.Managers
{
    public class ScreenManager
    {
        [XmlIgnore]
        public static readonly Vector2 Dimensions = 
            new Vector2(ScreensConfig.ScreenWidth, ScreensConfig.ScreenHeight);
        [XmlIgnore]
        public ContentManager Content { get; private set; }

        public GameScreen CurrentScreen { get; set; }
        public GameScreen NextScreen { get; set; }

        public XMLManager<GameScreen> xmlGameScreenManager;

        private static ScreenManager instance;
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice { get; set; }
        [XmlIgnore]
        public SpriteBatch Spritebatch { get; set; }

        public Image Image { get; set; }

        [XmlIgnore]
        public bool IsTransitioning { get; private set; }

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    XMLManager<ScreenManager> xml = new XMLManager<ScreenManager>();
                    instance = xml.Load(Path.Combine(ScreensConfig.PathToConfigFolder,
                        typeof(ScreenManager).Name + "." + ScreensConfig.FileExtension));
                    //instance = new ScreenManager();
                }

                return instance;
            }
        }

        public ScreenManager()
        {
            CurrentScreen = new SplashScreen();
            xmlGameScreenManager = new XMLManager<GameScreen>();
            xmlGameScreenManager.Type = CurrentScreen.Type;
            CurrentScreen = xmlGameScreenManager.Load(Path.Combine(ScreensConfig.PathToConfigFolder, 
                CurrentScreen.Type.Name + "." + ScreensConfig.FileExtension));
        }

        public void SwitchScreens(string screenName)//can it be changed to GameScreen NextScreen? 
        {
            NextScreen = (GameScreen)Activator.CreateInstance(Type.GetType("Gangstarz.Models.Screens." + screenName));            
            Image.IsActive = true;
            Image.fadeEffect.Increase = true;
            Image.Alpha = 0.0f;
            IsTransitioning = true;
        }

        private void Transition(GameTime gameTime)
        {
            if (IsTransitioning)
            {
                Image.Update(gameTime);
                if (Image.Alpha == 1.0f)//when the image is fully showing, the effect is fully fadeed out we need to load the next screen
                {
                    CurrentScreen.UnloadContent();//we change screens
                    CurrentScreen = NextScreen;
                    xmlGameScreenManager.Type = CurrentScreen.Type;

                    if (File.Exists(CurrentScreen.XmlPath))
                        CurrentScreen = xmlGameScreenManager.Load(CurrentScreen.XmlPath);

                    CurrentScreen.LoadContent();
                }
                else if (Image.Alpha == 0.0f)
                {
                    Image.IsActive = false;
                    IsTransitioning = false;
                }                
            }
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            CurrentScreen.LoadContent();
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            CurrentScreen.UnloadContent();
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
            if (IsTransitioning)
                Image.Draw(spriteBatch);
        }
    }
}
