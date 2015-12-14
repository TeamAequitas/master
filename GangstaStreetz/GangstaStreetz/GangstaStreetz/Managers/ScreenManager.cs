namespace GangstaStreetz.Managers
{
    using Models.Screens;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using Models.Screens.Proginitor;
    using Config.StaticClasses;

    public class ScreenManager
    {
        [XmlIgnore]
        public ContentManager Content { get; private set; }

        public Vector2 Dimensions { get; set; }
        public GameScreen CurrentScreen { get; set; }
        public GameScreen NextScreen { get; set; }

        public XMLManager<GameScreen> xmlGameScreenManager;


        [XmlIgnore]
        public GraphicsDevice GraphicsDevice { get; set; }
        [XmlIgnore]
        public SpriteBatch Spritebatch { get; set; }

        //public Image Image { get; set; }

        [XmlIgnore]
        public bool IsTransitioning { get; private set; }

        private static ScreenManager instance;

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    XMLManager<ScreenManager> xml = new XMLManager<ScreenManager>();
                    instance = xml.Load(ManagerConfig.ScreenManagerPath + 
                        "." + ManagerConfig.FileExtension);
                }

                return instance;
            }
        }

        public ScreenManager()
        {
            CurrentScreen = new SplashScreen();//optimization needed
            xmlGameScreenManager = new XMLManager<GameScreen>();
            xmlGameScreenManager.Type = CurrentScreen.Type;
            CurrentScreen = xmlGameScreenManager.Load(Path.Combine(ScreensConfig.PathToFolder,
                CurrentScreen.Type.Name + "." + ScreensConfig.FileExtension));
        }

        public void SwitchScreens(string screenName)//can it be changed to GameScreen NextScreen? 
        {
            switch (screenName.ToLower())
            {
                case "titlescreen":
                        CurrentScreen = new TitleScreen();
                    break;
                case "dialogscreen":
                        CurrentScreen = new DialogScreen();
                    break;
                default:
                    CurrentScreen = new SplashScreen();
                    break;                    
            }
            //NextScreen = (GameScreen)Activator.CreateInstance(Type.GetType("Gangstarz.Models.Screens." + screenName));
            xmlGameScreenManager = new XMLManager<GameScreen>();
            xmlGameScreenManager.Type = CurrentScreen.Type;
            CurrentScreen = xmlGameScreenManager.Load(Path.Combine(ScreensConfig.PathToFolder,
                CurrentScreen.Type.Name + "." + ScreensConfig.FileExtension));
                CurrentScreen.LoadContent();
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            CurrentScreen.LoadContent();
            //Image.LoadContent();
        }

        public void UnloadContent()
        {
            CurrentScreen.UnloadContent();
            //Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
            //Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
            //if (IsTransitioning)
            //    Image.Draw(spriteBatch);
        }
    }
}
