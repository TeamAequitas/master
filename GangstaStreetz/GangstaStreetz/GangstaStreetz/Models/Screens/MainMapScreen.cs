using GangstaStreetz.Config.StaticClasses;
using GangstaStreetz.Managers;
using GangstaStreetz.Models.Entities.Characters.MainMap;
using GangstaStreetz.Models.Images;
using GangstaStreetz.Models.Screens.Parents;
using GangstaStreetz.Structs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GangstaStreetz.Models.Screens
{
    public class MainMapScreen : SwitchingGameScreen
    {
        public int MapTileDimension { get; set; }

        public string PathToMatrix { get; set; }
        public int TileDimention { get; set; }

        private Player player;

        private Tile[][] backgroundMatrix;//change to class
        private Vector2 mapPosition;

        [XmlElement("Background")]
        public CombinedImage Background { get; set; } 

        public MainMapScreen()
        {
            mapPosition = Vector2.Zero;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            LoadMapMatrix();
            if (Background != null)
                Background.LoadContent();

            XMLManager<Player> playerLoader = new XMLManager<Player>();
            player = playerLoader.Load(Path.Combine(CharactersConfig.MainCharLoadPath, "Beat.xml"));
            player.LoadContent();
            
            //if (Texture != null)
            //    CollisionRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
        }

        private void LoadMapMatrix()
        {
            try
            {
                using (StreamReader file = File.OpenText(PathToMatrix))
                {
                    String picName = file.ReadLine();
                    String dimensions = file.ReadLine();
                    var matrixInfo = file.ReadLine().Trim().ToLower().Split('x').Select(int.Parse).ToArray();
                    backgroundMatrix = new Tile[matrixInfo[0]][];

                    string line;
                    int index = 0;

                    while ((line = file.ReadLine()) != null)
                    {
                        var lineCells = line.Trim().ToCharArray();

                        backgroundMatrix[index] = lineCells
                            .Select(ch => ch == 'O' 
                            ? new Tile(false, new Rectangle(index * TileDimention, lineCells.Length *TileDimention,
                            TileDimention, TileDimention)) 
                            : new Tile(true, new Rectangle(index * TileDimention, lineCells.Length * TileDimention,
                            TileDimention, TileDimention))).ToArray();

                        index++;
                    }
                }
            }
            catch (FileNotFoundException fnf)
            {
                throw new FileNotFoundException($"{fnf.Message} \nUnvalid path: {PathToMatrix}");
            }
            catch (FormatException)
            {
                throw;
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        //public void Update(GameTime gameTime, Player player)
        //{
        //    if (InputManager.Instance.KeyDown(Keys.D))
        //        if (CanMoveInMap(player.Image.Position.X - GamePlayConfig.MainMapOffset,
        //        player.Image.Position.Y - GamePlayConfig.MainMapOffset, Direction.Right))
        //        {
        //            mapPosition.X += player.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        //            if (player.Image.Position.X > ScreensConfig.ScreenWidth / 4
        //            && player.Image.Position.X < ScreensConfig.ScreenWidth * 3 / 4
        //            && Texture.Width - mapPosition.X > ScreensConfig.ScreenWidth / 4)
        //            {
        //                player.Image.Position.X = player.Image.Position.X;
        //                Position.X -= player.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        //            }
        //        }
        //        else
        //        {
        //            player.Image.Position.X = player.Image.Position.X;
        //        }

        //}

        //private bool CanMoveInMap(float pxInMapX, float pxInMapY, Direction direction)
        //{
        //    int tileX = (int)Math.Floor(pxInMapX / TileDimention);
        //    int tileY = (int)Math.Floor(pxInMapY / TileDimention);

        //    switch (direction)
        //    {
        //        case Direction.Right:
        //            {
        //                if (tileX + 1 >= backgroundMatrix[0].Length)
        //                    return false;
        //                var a = backgroundMatrix[tileY][tileX + 1];
        //                if (backgroundMatrix[tileY][tileX + 1])
        //                    return true;
        //            }
        //            break;

        //        case Direction.Left:
        //            {
        //                if (tileX < 0)
        //                    return false;
        //                if (backgroundMatrix[tileY][tileX - 1])
        //                    return true;
        //            }
        //            break;

        //        case Direction.Up:
        //            {
        //                if (tileY + 1 >= backgroundMatrix.Length)
        //                    return false;
        //                if (backgroundMatrix[tileY + 1][tileX])
        //                    return true;
        //            }
        //            break;

        //        case Direction.Down:
        //            {
        //                if (tileY < 0)
        //                    return false;
        //                if (backgroundMatrix[tileY - 1][tileX])
        //                    return true;
        //            }
        //            break;
        //    }

        //    return false;
        //}

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Background != null)
                Background.Draw(spriteBatch);
        }
    }
}
