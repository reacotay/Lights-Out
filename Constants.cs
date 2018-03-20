using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myGame
{
    public static class Constants
    {
        public static int WindowWidth { get; private set; }
        public static int WindowHeight { get; private set; }

        public static int CellAmountX { get; private set; }
        public static int CellAmountY { get; private set; }
        public static int CellSize { get; private set; }

        public static Rectangle GameWindow { get; private set; }
        public static Random Randomizer { get; private set; }

        public static void LoadContent()
        {
            WindowWidth = 800;
            WindowHeight = 800;

            CellAmountX = 16;
            CellAmountY = 16;
            CellSize = 50;

            GameWindow = new Rectangle(0, 0, WindowWidth, WindowHeight);
            Randomizer = new Random();
        }
    }
}
