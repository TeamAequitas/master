using GangstaStreetz.Models.Images.Proginitor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GangstaStreetz.Models.Entities.Characters.MainMap.Proginitor
{
    public abstract class Character
    {
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle CollisionRectangle { get; set; }

        public Character(string name)
        {
            this.Name = name;
            Position = Vector2.Zero;
            CollisionRectangle = Rectangle.Empty;
        }

        public virtual void LoadContent()
        {
        }

        public virtual void UnlaodContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}