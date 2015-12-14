

namespace GangstaStreetz.Models.Images
{
    using Managers;
    using GangstaStreetz.Models.Images.Proginitor;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Config.StaticClasses;
    using System.Xml.Serialization;

    public class CombinedImage : Image
    {
        [XmlElement("Text")]
        public string Text { get; set; }        
        public string FontPath { get; set; }

        protected SpriteFont Font { get; set; }
        public Vector2 TextPosition { get; set; }
        protected Vector2 TextCenter
        {
            get
            {
                if (Font != null)
                    return new Vector2(Font.MeasureString(Text).X / 2, Font.MeasureString(Text).Y / 2);

                return Vector2.Zero;
            }
        }

        public override Vector2 Center
        {
            get
            {
                if (CollisionRectangle != null)
                    return new Vector2(CollisionRectangle.Width / 2, CollisionRectangle.Height / 2);

                return Vector2.Zero;
            }
        }

        public CombinedImage() : base()
        {
            Text = string.Empty;
            TextPosition = Vector2.Zero;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            if (!string.IsNullOrEmpty(this.FontPath))
                Font = content.Load<SpriteFont>(FontPath);
            else
                Font = content.Load<SpriteFont>(ScreensConfig.PathToGameFont); 

            var collRect = Rectangle.Empty;
            if (Texture != null)
            {
                collRect.Width += Texture.Width;
                collRect.Height += Texture.Height;
            }

            if (Font != null)
            {
                if (TextPosition.X == 0)
                    collRect.Width = Math.Max(collRect.Width, (int)TextCenter.X * 2);
                else
                {
                    var cenDiffX = Math.Abs(TextPosition.X - TextPosition.X);
                    var dx = collRect.Width / 2 - cenDiffX;
                    if (dx > 0 || dx <= TextCenter.X / 2)
                        collRect.Width += (int)(dx + TextCenter.X);
                }

                if (TextPosition.Y == 0)
                    collRect.Height = Math.Max(collRect.Height, (int)TextCenter.Y * 2);
                else
                {
                    var cenDiffY = Math.Abs(TextPosition.Y - TextPosition.Y);
                    var dy = collRect.Height / 2 - cenDiffY;
                    if (dy > 0 || dy <= TextCenter.Y / 2)
                        collRect.Height += (int)(dy + TextCenter.Y);
                }
            }

            if (collRect.Height != 0 && collRect.Width != 0)
            {
                renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice,
                    collRect.Width, collRect.Height);

                this.CollisionRectangle = collRect;

                ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);//initially the screen is the render target, 
                                                                                    //then we swith to the image area to be the only render target
                ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);

                ScreenManager.Instance.Spritebatch.Begin();

                SetRenderTargets();

                ScreenManager.Instance.Spritebatch.End();

                Texture = renderTarget;//we made text and pic 1 texture, so we release the texture

                ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);
            }        
        }

        public override void UnloadContent()
        {
            base.UnloadContent();            
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, PositionUpLeft, CollisionRectangle, Color.White * Alpha,
                Rotation, Center, Scale, SpriteEffects.None, 0.0f);
        }

        protected virtual void SetRenderTargets()
        {
            if (Texture != null)
                ScreenManager.Instance.Spritebatch.Draw(Texture, Vector2.Zero, Color.White);

            if(Text != null)
                ScreenManager.Instance.Spritebatch.DrawString(Font, Text, Vector2.Zero, Color.White);
        }
    }
}
