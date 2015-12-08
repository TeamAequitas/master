namespace Gangstarz.Models.Images.Proginitor
{
    using Managers;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System.Xml.Serialization;
    
    public class Image
    {
        public float Alpha { get; set; }
        public string Path { get; set; }
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public string FontName{ get; set; }
        public float Rotation { get; set; }        
        private Vector2 Center { get; set; }//of drawable target(image + text), we need this in order to zoom in accurately
        public Vector2 Scale { get; set; }

        public Rectangle CollisionRectangle { get; set; }//another class or struct to be added

        [XmlIgnore]
        public Texture2D Texture;
        [XmlIgnore]
        protected SpriteFont Font { get; set; }

        [XmlIgnore]
        private ContentManager content;
        [XmlIgnore]
        private RenderTarget2D renderTarget;

        public Image()
        {
            Path = string.Empty;
            Text = string.Empty;
            Position = Vector2.Zero;
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

            if (!string.IsNullOrEmpty(this.FontName))
                Font = content.Load<SpriteFont>(FontName);

            Vector2 dimensions = Vector2.Zero; //to be changed to rectangle or RendedTarget

            if (Texture != null)
            {
                dimensions.X += Texture.Width;
                dimensions.Y += Texture.Height;
            }
            dimensions.X = MathHelper.Max(dimensions.X, Font.MeasureString(Text).X);
            dimensions.Y += Font.MeasureString(Text).Y;

            if (CollisionRectangle == Rectangle.Empty)
                CollisionRectangle = new Rectangle(0,0,(int)dimensions.X, (int)dimensions.Y);

            renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice,
                (int)dimensions.X, (int)dimensions.Y);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);

            ScreenManager.Instance.Spritebatch.Begin();

            if(Texture != null)
                ScreenManager.Instance.Spritebatch.Draw(Texture, Vector2.Zero, Color.White);
            ScreenManager.Instance.Spritebatch.DrawString(Font, Text, Vector2.Zero, Color.White);

            ScreenManager.Instance.Spritebatch.End();

            Texture = renderTarget;//we made text and pic 1 texture, so we release the texture

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            this.Center = new Vector2(CollisionRectangle.Width / 2, CollisionRectangle.Height / 2);
            spriteBatch.Draw(Texture, Position + Center, CollisionRectangle, Color.White * Alpha,
                Rotation, Center, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
