namespace Gangstarz.Models.Images.Proginitor
{
    using Effects.Proginitor;
    using Effects;
    using Managers;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using Config;

    public class Image
    {
        public float Alpha { get; set; }
        public string Path { get; set; }
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public string FontName = ScreensConfig.MainFont;
        public float Rotation { get; set; }        
        private Vector2 Center { get; set; }//of drawable target(image + text), we need this in order to zoom in accurately
        public bool IsActive;

        public FadeEffect fadeEffect; // whenever we make change to this field the change is reflected in the effect list with the help of the ref(we are tinkering the same variable)

        public Rectangle CollisionRectangle { get; set; }//another class or struct to be added

        Dictionary<string, ImageEffect> effectList { get; set; }//ef
        public string Effects { get; set; }//ef

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
            Effects = string.Empty;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0.0f;
            Alpha = 1.0f;
            CollisionRectangle = Rectangle.Empty;
            effectList = new Dictionary<string, ImageEffect>();
        }

        public void LoadContent()            
        {
            content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (!string.IsNullOrEmpty(this.Path))
                Texture = content.Load<Texture2D>(this.Path);

            if (!string.IsNullOrEmpty(this.FontName))//!!!
                Font = content.Load<SpriteFont>(FontName);//!!!!

            Vector2 dimensions = Vector2.Zero; //to be changed to rectangle or RendedTarget

            if (Texture != null)
                dimensions.X += Texture.Width;

            dimensions.X += Font.MeasureString(Text).X;

            if (Texture != null)
                dimensions.Y = Math.Max(Texture.Height, Font.MeasureString(Text).Y);
            else
                dimensions.Y = Font.MeasureString(Text).Y;

            if (CollisionRectangle == Rectangle.Empty)
                CollisionRectangle = new Rectangle(0,0,(int)dimensions.X, (int)dimensions.Y);

            renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice,
                (int)dimensions.X, (int)dimensions.Y);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);//initial th escreen is the render target, 
            //then we swith to the image area to be the only render target
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);

            ScreenManager.Instance.Spritebatch.Begin();

            if(Texture != null)
                ScreenManager.Instance.Spritebatch.Draw(Texture, Vector2.Zero, Color.White);
            ScreenManager.Instance.Spritebatch.DrawString(Font, Text, Vector2.Zero, Color.White);

            ScreenManager.Instance.Spritebatch.End();

            Texture = renderTarget;//we made text and pic 1 texture, so we release the texture

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            SetEffect(ref fadeEffect);

            if(Effects != string.Empty)
            {
                string[] data = Effects.Split(':');
                foreach (var item in data)
                    ActivateEffect(item);
            }
        }

        public void UnloadContent()
        {
            content.Unload();

            foreach (var effect in effectList)
                DeactivateEffect(effect.Key);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in effectList)
                if (effect.Value.IsActive)
                    effect.Value.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.Center = new Vector2(CollisionRectangle.Width / 2, CollisionRectangle.Height / 2);
            spriteBatch.Draw(Texture, Position + Center, CollisionRectangle, Color.White * Alpha,
                Rotation, Center, Scale, SpriteEffects.None, 0.0f);//rotation?
        }

        private void SetEffect<T>(ref T effect)//eff if effect is blur
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T)); // we create new instance of blur effect
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }

            effectList.Add(effect.GetType().Name, (effect as ImageEffect));
        }

        public void ActivateEffect(string effect)//eff
        {
            if(effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effect) //eff
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = false;
                effectList[effect].UnloadContent();
            }
        }

        public void StoreEffects()
        {
            Effects = String.Empty;
            foreach(var effect in effectList)
            {
                if (effect.Value.IsActive)
                    Effects += effect.Key + ":";
            }
            if (Effects != String.Empty)           
                Effects.Remove(Effects.Length - 1);//removing the last character
        }

        public void RestoreEffects()
        {
            foreach(var effect in effectList)
                DeactivateEffect(effect.Key);

            string[] data = Effects.Split(':');
            foreach (string d in data)
                ActivateEffect(d);
        }
    }
}
