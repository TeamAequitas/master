using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gangstarz.Removable
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;

    class AnimatedSprite
    {
        private int currentFrame = 0;     
        
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Texture2D Texture { get; set; }

        private int TotalFrames { get { return this.Rows * this.Columns; } }

        public AnimatedSprite(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
        }

        public void Update()
        {
            currentFrame++;
            if (currentFrame == TotalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)(currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
