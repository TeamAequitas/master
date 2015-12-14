

namespace GangstaStreetz.Models.Screens
{
    using Images;
    using GangstaStreetz.Models.Screens.Proginitor;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Managers;
    using Microsoft.Xna.Framework.Graphics;
    using Images.Proginitor;
    using System.Xml.Serialization;
    using Parents;

    public class SplashScreen : SwitchingGameScreen
    {
        private bool IsTransitioning { get; set; }

        public override void LoadContent()
        {
            base.LoadContent();
            foreach (var image in Images)
                image.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);            

            if (InputManager.Instance.KeyPressed(Keys.Enter, Keys.Space))
                IsTransitioning = true;            

            if (IsTransitioning)
                Transition(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);                   
        }
    }
}
