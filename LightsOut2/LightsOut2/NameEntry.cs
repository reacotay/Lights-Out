﻿using System;
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
        static string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        static SpriteFont spriteFont = ContentManager.Get<SpriteFont>("titleFont");
        static string[] letter = new string[] { "_", "_", "_" };
        static int selected = 0;
        enum Letter
        {
            X = 0,
            Y = 1,
            Z = 2,
            Done
        }
        static Letter currentLetter;

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

            if (Constants.keyState.IsKeyDown(Keys.W) && Constants.oldKeyState.IsKeyUp(Keys.W))
            {
                if (selected < 33)
                    selected++;
            }
            if (Constants.keyState.IsKeyDown(Keys.S) && Constants.oldKeyState.IsKeyUp(Keys.S))
            {
                if (selected > 0)
                    selected--;
            }
            if (Constants.keyState.IsKeyDown(Keys.Enter) && Constants.oldKeyState.IsKeyUp(Keys.Enter))
            {
                return true;
            }
            if (Constants.keyState.IsKeyDown(Keys.Back) && Constants.oldKeyState.IsKeyUp(Keys.Back))
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
            for (int i = 0; i < 3; i++)
            {
                if (i == (int)currentLetter)
                    spriteBatch.DrawString(spriteFont, letter[i], new Vector2(100 + i * 50, 100), Color.Red);
                else
                    spriteBatch.DrawString(spriteFont, letter[i], new Vector2(100 + i * 50, 100), Color.White);
            }
            spriteBatch.End();
        }
    }
}