using GangstaStreetz.Config.StaticClasses;
using GangstaStreetz.Models.Images.Proginitor;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GangstaStreetz.Models.Images
{
    public class SpriteImage : Image
    {
        private int FrameCounter { get; set; }
        private int SwitchFrame { get; set; }//the lower the value the faster the animation

        public Vector2 currentFrame;
        public int AmountOfFrames { get; set; }

        public Vector2 TilePosition;

        [XmlElement("FrameWidth")]
        public List<int> FrameWidth;//frame width per row

        public int FrameHeight
        {
            get
            {
                if (Texture != null)
                    return Texture.Height / AmountOfFrames;

                return 0;
            }
        }

        public SpriteImage()
        {
            currentFrame = new Vector2(1, 0);
            SwitchFrame = 100;
            FrameCounter = 0;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            this.PositionUpLeft = new Vector2(TilePosition.X * ScreensConfig.TileDimension, 
                TilePosition.Y *ScreensConfig.TileDimension);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            int currFrameWidth = FrameWidth[(int)currentFrame.Y];

            if (IsActive)
            {
                FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (FrameCounter >= SwitchFrame)
                {
                    FrameCounter = 0;
                    currentFrame.X++;

                    if (currentFrame.X  >= AmountOfFrames)
                    {
                        currentFrame.X = 0;//roll back to the beginning
                    }
                }
            }
            else
                currentFrame.X = 1;// standing frame 1,0

            CollisionRectangle = new Rectangle((int)currentFrame.X * currFrameWidth,//????
                (int)currentFrame.Y * FrameHeight, currFrameWidth, FrameHeight); //crop the image // (int)currentFrame.Y * FrameHeight???
        }
    }
}
