using GangstaStreetz.Managers;
using GangstaStreetz.Models.Menus;
using GangstaStreetz.Models.Screens.Parents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GangstaStreetz.Models.Screens
{
    public class TitleScreen : SwitchingGameScreen
    {
        public Menu TitleMenu { get; set; }

        private bool IsTransitioning { get; set; }

        public override void LoadContent()
        {
            base.LoadContent();

            if (TitleMenu != null)
                TitleMenu.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            if (TitleMenu != null)
                TitleMenu.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (TitleMenu != null && TitleMenu.Buttons.Any())
            {
                foreach (var button in TitleMenu.Buttons)
                {
                    var collRec = new Rectangle((int)button.PositionUpLeft.X - (int)button.Center.X, 
                        (int)button.PositionUpLeft.Y - (int)button.Center.Y,
                        button.CollisionRectangle.Width, button.CollisionRectangle.Height);
                    if (collRec.Contains(InputManager.Instance.MousePosition()))//we have at least hover
                    {
                        //Transform
                        if (InputManager.Instance.MouseLeftBtnClicked())
                        {
                            switch (button.LinkTo.ToLower())
                            {
                                case "screen":
                                    {
                                        IsTransitioning = true;
                                        NextScreen = button.LinkName;
                                        //ScreenManager.Instance.SwitchScreens(NextScreen);
                                    }
                                    break;
                                //case "menu":
                                //    //TODO
                                //    break;
                                default: break;
                            }
                        }
                    }

                    TitleMenu.Update(gameTime);
                }

                if (IsTransitioning)
                    Transition(gameTime);
            }            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (TitleMenu != null && TitleMenu.Buttons.Any())
                TitleMenu.Draw(spriteBatch);
        }
    }
}
