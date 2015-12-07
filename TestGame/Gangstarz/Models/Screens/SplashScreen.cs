namespace Gangstarz.Models.Screens
{
    using Managers;
    using Proginitor;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Config;
    using System;

    class SplashScreen : GameScreen
    {
        private SpriteFont GameNameFont { get; set; }
        public string Path { get; set; }

        public override void LoadContent()
        {
            base.LoadContent();

            this.Font = Content.Load<SpriteFont>(ScreensConfig.SplashScreenFont_s20);
            this.GameNameFont = Content.Load<SpriteFont>(ScreensConfig.SplashScreenFont_s45);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2[] textPos = ReturnTextPos();

            spriteBatch.DrawString(GameNameFont, GameConfig.GameName, textPos[0], Color.White);
            spriteBatch.DrawString(Font, $"by {GameConfig.Credits}", textPos[1], Color.White);

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
