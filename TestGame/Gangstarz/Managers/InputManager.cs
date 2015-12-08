using Microsoft.Xna.Framework.Input;

namespace Gangstarz.Managers
{
    public class InputManager
    {
        private KeyboardState currentKeyState, previousKeyState;

        private MouseState currentMouseState, previousMauseState;

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

            if (!ScreenManager.Instance.IsTransitioning)
            {
                currentKeyState = Keyboard.GetState();
                currentMouseState = Mouse.GetState();
            }
               
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
            foreach(Keys key in keys)
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

        //public bool AnyKeyPressed()
        //{            
        //    return currentState.GetPressedKeys().Any();            
        //}
    }
}
