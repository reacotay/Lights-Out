using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lights_Out
{
    public static class Constants
    {
        public static int WindowWidth { get; private set; }
        public static int WindowHeight { get; private set; }

        public static int PlatfromWidth { get; private set; }
        public static int PlatformHeight { get; private set; }

        public static int CellAmountX { get; private set; }
        public static int CellAmountY { get; private set; }
        public static int CellSize { get; private set; }

        public static Rectangle GameWindow { get; private set; }
        public static Random Randomizer { get; private set; }

        public static KeyboardState keyState, oldKeyState = Keyboard.GetState();
        public static MouseState mouseState, oldMouseState = Mouse.GetState();

        public static void LoadContent()
        {
            WindowWidth = 800;
            WindowHeight = 800;

            PlatfromWidth = 1600;
            PlatformHeight = 1600;

            CellAmountX = 16;
            CellAmountY = 16;
            CellSize = 25;

            GameWindow = new Rectangle(0, 0, WindowWidth, WindowHeight);
            Randomizer = new Random();
        }

        public static void UpdateKeyMouseReader()
        {
            oldKeyState = keyState;
            keyState = Keyboard.GetState();
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
        }

        //----------------------------------------------------------------------------------------------------

        public static bool KeyPressed(Keys key)
        {
            return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
        }

        public static bool LeftClick()
        {
            return mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released;
        }

        public static bool RightClick()
        {
            return mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released;
        }
    }
}
