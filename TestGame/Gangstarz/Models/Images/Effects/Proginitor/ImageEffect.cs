using Gangstarz.Models.Images.Proginitor;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gangstarz.Models.Images.Effects.Proginitor
{
    public abstract class ImageEffect
    {
        protected Image image; 
        public bool IsActive { get; set; }

        public ImageEffect()
        {
            IsActive = false;
        }

        public virtual void LoadContent(ref Image Image) // merge Image and ImageEffect ???
        {
            this.image = Image;
        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
