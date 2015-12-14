

namespace GangstaStreetz.Models.Screens.Parents
{
    using Images.Proginitor;
    using GangstaStreetz.Models.Screens.Proginitor;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Microsoft.Xna.Framework;
    using Managers;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;
    using Config.StaticClasses;
    using Images;

    public abstract class SwitchingGameScreen : GameScreen
    {
        private double SwitchDelay = ScreensConfig.TransitionDelay;
                
        public string NextScreen { get; set; }
        public int FadeSpeed { get; set; }
        [XmlElement("Image")]
        public List<CombinedImage> Images { get; set; }

        public SwitchingGameScreen()
        {
            NextScreen = String.Empty;
            Images = new List<CombinedImage>();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            foreach (var image in Images)
                image.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            foreach (var image in Images)
                image.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (var image in Images)
                image.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (var image in Images)
                image.Draw(spriteBatch);
        }

        protected void Transition(GameTime gameTime)
        {
            foreach (var image in Images)
                if (image.Alpha > 0.0f)
                {
                    image.Alpha -= FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    //image.Update(gameTime);
                }
                    

            if((!Images.Any() || Images.FirstOrDefault().Alpha <= 0.0f) && SwitchDelay > 0)
                SwitchDelay -= gameTime.ElapsedGameTime.TotalSeconds * 10;

            if (SwitchDelay <= 0 && NextScreen != null)
                ScreenManager.Instance.SwitchScreens(NextScreen);
        }
    }
}
