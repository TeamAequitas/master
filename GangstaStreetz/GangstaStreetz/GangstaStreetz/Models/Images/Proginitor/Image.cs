namespace GangstaStreetz.Models.Images.Proginitor
{
    using Interfaces.Images;
    using Models.Proginitor;

    using Microsoft.Xna.Framework;
    using System.Xml.Serialization;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Content;
    using Managers;

    public abstract class Image : GameObject, IImage
    {
        public float Alpha { get; set; }
        public string Path { get; set; }
        public bool IsActive { get; set; }
        public float Rotation { get; set; }
        public Vector2 PositionUpLeft { get; set; }
        public Vector2 Scale { get; set; }        
        public Rectangle CollisionRectangle { get; set; }
        [XmlIgnore]
        public virtual Vector2 Center { get; private set; }        

        [XmlIgnore]
        public Texture2D Texture { get; set; }

        protected ContentManager content;
        protected RenderTarget2D renderTarget;

        protected Image()
        {
            Path = string.Empty;
            PositionUpLeft = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0.0f;
            Alpha = 1.0f;
            CollisionRectangle = Rectangle.Empty;
        }

        public virtual void LoadContent()
        {
            content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (!string.IsNullOrEmpty(this.Path))
                Texture = content.Load<Texture2D>(this.Path);
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, PositionUpLeft, CollisionRectangle, Color.White * Alpha,
                Rotation, Center, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
