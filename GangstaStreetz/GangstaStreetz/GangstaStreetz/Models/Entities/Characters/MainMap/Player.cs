using GangstaStreetz.Config.StaticClasses;
using GangstaStreetz.Managers;
using GangstaStreetz.Models.Entities.Characters.MainMap.Proginitor;
using GangstaStreetz.Models.Entities.Characters.Parents;
using GangstaStreetz.Models.Images.Proginitor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GangstaStreetz.Models.Entities.Characters.MainMap
{
    public class Player : MovingCharacter
    {        
        public int RemainingMoves { get; set; }

        public Player() : base("Beat")
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void UnlaodContent()
        {
            base.UnlaodContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
