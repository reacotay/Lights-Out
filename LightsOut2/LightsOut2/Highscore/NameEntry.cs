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
        private static string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private static string[] letter = new string[] { "_", "_", "_" };
        private static int selected = 0;

        private static SpriteFont spriteFont = ContentManager.Get<SpriteFont>("titleFont");

        enum Letter
        {
            X = 0,
            Y = 1,
            Z = 2,
            Done
        }
        private static Letter currentLetter;

        public static bool Entry()
        {
            switch (currentLetter)
            {
                case Letter.X:
                    if (Choice(0))
                        currentLetter = Letter.Y;
                    break;

                case Letter.Y:
                    if (Choice(1))
                        currentLetter = Letter.Z;
                    break;

                case Letter.Z:
                    if (Choice(2))
                        currentLetter = Letter.Done;
                    break;

                case Letter.Done:
                    string name = string.Join("", letter);
                    Highscore.Record(name);
                    return true;
            }

            return false;
        }

        static bool Choice(int i)
        {
            if ((Constants.keyState.IsKeyDown(Keys.W) && Constants.oldKeyState.IsKeyUp(Keys.W)) || (Constants.gamePadState.IsButtonDown(Buttons.DPadUp) && Constants.oldGamePadState.IsButtonUp(Buttons.DPadUp)))
            {
                if (selected < 33)
                    selected++;
            }

            if (Constants.keyState.IsKeyDown(Keys.S) && Constants.oldKeyState.IsKeyUp(Keys.S) || (Constants.gamePadState.IsButtonDown(Buttons.DPadDown) && Constants.oldGamePadState.IsButtonUp(Buttons.DPadDown)))
            {
                if (selected > 0)
                    selected--;
            }

            if (Constants.keyState.IsKeyDown(Keys.Enter) && Constants.oldKeyState.IsKeyUp(Keys.Enter) || (Constants.gamePadState.IsButtonDown(Buttons.A) && Constants.oldGamePadState.IsButtonUp(Buttons.A)))
            {
                return true;
            }

            if (Constants.keyState.IsKeyDown(Keys.Back) && Constants.oldKeyState.IsKeyUp(Keys.Back) || (Constants.gamePadState.IsButtonDown(Buttons.B) && Constants.oldGamePadState.IsButtonUp(Buttons.B)))
            {
                if (currentLetter != Letter.X)
                    currentLetter = currentLetter - 1;
            }

            letter[i] = alphabet[selected];
            return false;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, "Woupsie! You DIED.\r\nBut you got a new \r\nHighscore, so enter \r\nyour initials:", new Vector2(0, 0), Color.Green);
            for (int i = 0; i < 3; i++)
            {
                if (i == (int)currentLetter)
                    spriteBatch.DrawString(spriteFont, letter[i], new Vector2(100 + i * 50, 500), Color.Red);
                else
                    spriteBatch.DrawString(spriteFont, letter[i], new Vector2(100 + i * 50, 500), Color.White);
            }

            spriteBatch.End();
        }
    }
}
