using GangstaStreetz.Config.StaticClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangstaStreetz.Managers
{
    class InputManager
    {
        private KeyboardState currentKeyState, previousKeyState;

        private MouseState currentMouseState, previousMauseState;

        private static double ClickTimer;
        private const double TimerDelay = ManagerConfig.TimerDelay;

        private static InputManager instance;

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputManager();

                return instance;
            }
        }

        public void Update()
        {
            previousKeyState = currentKeyState;
            previousMauseState = currentMouseState;

            //if (!ScreenManager.Instance.IsTransitioning)
            //{
                currentKeyState = Keyboard.GetState();
                currentMouseState = Mouse.GetState();
            //}

        }

        public Point MousePosition()
        {
            return new Point(currentMouseState.X, currentMouseState.Y);
        }

        public bool MouseLeftBtnClicked()
        {
            //if (currentMouseState.LeftButton == ButtonState.Pressed
            //    && previousMauseState.LeftButton == ButtonState.Released)
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                    return true;

            return false;
        }

        public bool MouseLeftBtnDoubleClicked(GameTime gameTime)// out of here?
        {
            ClickTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (this.MouseLeftBtnClicked())
            {
                if (ClickTimer < TimerDelay)
                    return true;
                else
                {
                    ClickTimer = 0;

                    return false;
                }                    
            }

            return false;
        }

        public bool MouseRightBtnClicked()
        {
            //if (currentMouseState.RightButton == ButtonState.Pressed
            //    && previousMauseState.RightButton == ButtonState.Released)
            if (currentMouseState.RightButton == ButtonState.Pressed)
                    return true;

            return false;
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
                if (currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyUp(key))
                    return true;

            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
                if (currentKeyState.IsKeyUp(key) && previousKeyState.IsKeyDown(key))
                    return true;

            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
                if (currentKeyState.IsKeyDown(key))
                    return true;

            return false;
        }
    }
}
