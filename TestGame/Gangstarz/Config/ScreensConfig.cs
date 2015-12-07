namespace Gangstarz.Config
{
    using Microsoft.Xna.Framework.Graphics;

    public static class ScreensConfig
    {
        public static readonly int ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 40;
        public static readonly int ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 20;

        public const string GameFont = "Fonts/MainFont";

        public const string SplashScreenFont_s20 = "Fonts/SplashScreen_s20";
        public const string SplashScreenFont_s45 = "Fonts/SplashScreen_s45";
    }
}
