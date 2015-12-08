namespace Gangstarz.Models.Screens
{
    using Managers;
    using Proginitor;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Config;
    using System;
    using Images.Proginitor;

    public class SplashScreen : GameScreen
    {
        public Image Image { get; set; }

        private SpriteFont GameNameFont { get; set; }        
        public string GameName {get; set;}
        public string Credits { get; set; }
        public string Font_s20 { get; set; }
        public string Font_s45 { get; set; }

        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();
            this.Font = Content.Load<SpriteFont>(Font_s20);
            this.GameNameFont = Content.Load<SpriteFont>(Font_s45);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2[] textPos = ReturnTextPos();

            spriteBatch.DrawString(GameNameFont, GameName, textPos[0], Color.White);
            spriteBatch.DrawString(Font, Credits, textPos[1], Color.White);
            Image.Draw(spriteBatch);

            base.Draw(spriteBatch);            
        }

        private Vector2[] ReturnTextPos()
        {
            var gnBox = GameNameFont.MeasureString(GameConfig.GameName);

            var gameNamePos = new Vector2(ScreensConfig.ScreenWidth / 2 - gnBox.X/2,
            ScreensConfig.ScreenHeight / 2 - gnBox.Y/2);
            var creditsTextPos = new Vector2(gameNamePos.X + 3 * gnBox.X/4, gameNamePos.Y + 4 * gnBox.Y/ 5);

            return new Vector2[]{ gameNamePos, creditsTextPos };
        }
    }
}
