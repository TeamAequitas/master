namespace GangstaStreetz.Config.StaticClasses
{
    using Microsoft.Xna.Framework;

    public static class ScreensConfig
    {
        public const int TransitionDelay = 3;
        public const string FileExtension = "xml";
        public const string PathToFolder = "Config/XML/Screens";      
        public const string PathToImgFolder = "../GangstaStreetzContent/Images/Screens";
        public const string PathToSwitchScreen = "../GangstaStreetzContent/Images/Screens/SwitchBackground";

        public const string PathToGameFont = "Fonts/GameFont";
        public const int TileDimension = 25;
        public static readonly Vector2 ScreenDimensions = new Vector2(1680, 1000);
    }
}
