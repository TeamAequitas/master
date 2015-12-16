namespace GangstaStreetz.Structs
{
    using Microsoft.Xna.Framework;

    public struct Tile // not struct
    {
        public bool IsSolid { get; set; }
        public Rectangle CollRectangle { get; set; }        

        public Tile(bool isSolid, Rectangle collRect)
        {
            this.IsSolid = isSolid;
            this.CollRectangle = collRect;
        }
    }
}
