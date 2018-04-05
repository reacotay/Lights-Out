using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Text.RegularExpressions;

namespace LightsOut2
{
    static class NameEntry
    {
        static char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static SpriteFont spriteFont = ContentManager.Get<SpriteFont>("spriteFont");
        static string x="_", y="_", z="_";

        public static bool Entry()
        {

            return true;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, x, new Vector2(100, 100), Color.White);
            spriteBatch.DrawString(spriteFont, y, new Vector2(110, 100), Color.White);
            spriteBatch.DrawString(spriteFont, z, new Vector2(120, 100), Color.White);
            spriteBatch.End();
        }
    }
}
