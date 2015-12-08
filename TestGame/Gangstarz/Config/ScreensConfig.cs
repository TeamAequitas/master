namespace Gangstarz.Config
{
    using Microsoft.Xna.Framework.Graphics;

    public static class ScreensConfig
    {
        public static readonly int ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 40;
        public static readonly int ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 20;

        public const string PathToConfigFolder = "Config/ScreensConfig";
        public const string FileExtension = "xml";
        public const string MainFont = "Fonts/MainFont";
    }
}
