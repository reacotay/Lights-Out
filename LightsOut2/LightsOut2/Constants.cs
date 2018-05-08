using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut2
{
    public static class Constants
    {
        public static int WindowWidth { get; private set; }
        public static int WindowHeight { get; private set; }

        public static int PlatfromWidth { get; private set; }
        public static int PlatformHeight { get; private set; }

        public static int StandardSize { get; private set; }
        public static int BigSize { get; private set; }
        public static int BulletSize { get; private set; }

        public static int ShadowOffset { get; private set; }
        public static float HeatValue { get; set; }

        public static Rectangle GameWindow { get; private set; }
        public static Random Randomizer { get; private set; }

        public static Vector2 tempDirection;
        public static KeyboardState keyState, oldKeyState = Keyboard.GetState();
        public static MouseState mouseState, oldMouseState = Mouse.GetState();

        public static GamePadState gamePadState, oldGamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);

        public static void LoadContent()
        {
            WindowWidth = 800;
            WindowHeight = 800;

            PlatfromWidth = 1600;
            PlatformHeight = 1600;

            StandardSize = 25;
            BigSize = 50;
            BulletSize = 5;

            ShadowOffset = 2;

            GameWindow = new Rectangle(0, 0, WindowWidth, WindowHeight);
            Randomizer = new Random();
        }

        public static void UpdateKeyMouseReader()
        {
            ControllerAngle();

            oldGamePadState = gamePadState;
            gamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);

            oldKeyState = keyState;
            keyState = Keyboard.GetState();
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
        }

        //----------------------------------------------------------------------------------------------------

        public static Vector2 WorldMousePosition()
        {
            return Vector2.Transform(new Vector2(mouseState.Position.X, mouseState.Position.Y), Matrix.Invert(GameManager.camera.GetTransform()));
        }
        
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

        public static void ControllerAngle()
        {
            tempDirection.X = gamePadState.ThumbSticks.Right.X;
            tempDirection.Y = -gamePadState.ThumbSticks.Right.Y;
        }
    }
}
