using GangstaStreetz.Managers;
using GangstaStreetz.Models.Entities.Characters.MainMap.Proginitor;
using GangstaStreetz.Models.Images;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GangstaStreetz.Models.Entities.Characters.Parents
{
    public abstract class MovingCharacter : Character
    {
        public SpriteImage Image { get; set; }//Should be IImage
        public Vector2 Velocity;
        public float MoveSpeed { get; set; }

        public MovingCharacter(string name) : base(name)
        {
            Velocity = Vector2.Zero;
            MoveSpeed = 0.0f;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            Image.LoadContent();
        }

        public override void UnlaodContent()
        {
            base.UnlaodContent();

            Image.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Image.IsActive = true;

            if (InputManager.Instance.KeyDown(Keys.Up))
                this.MoveSpeed += 5;
            if (InputManager.Instance.KeyDown(Keys.Down))
                this.MoveSpeed -= 5;


            if (Velocity.X == 0)
            {
                if (InputManager.Instance.KeyDown(Keys.S))
                {
                    Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.currentFrame.Y = 2;
                }
                else if (InputManager.Instance.KeyDown(Keys.W))
                {
                    Velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.currentFrame.Y = 3;
                }
                else
                    Velocity.Y = 0;
            }

            if (Velocity.Y == 0)
            {
                if (InputManager.Instance.KeyDown(Keys.D))
                {
                    Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.currentFrame.Y = 4;
                }
                else if (InputManager.Instance.KeyDown(Keys.A))
                {
                    Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.currentFrame.Y = 1;
                }
                else
                    Velocity.X = 0;
            }

            if (Velocity.X == 0 && Velocity.Y == 0)
                Image.IsActive = false;

            Image.Update(gameTime);
            Image.PositionUpLeft += Velocity;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Image.Draw(spriteBatch);
        }
    }
}
