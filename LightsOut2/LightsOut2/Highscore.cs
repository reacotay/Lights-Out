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
    static class Highscore
    {
        static string[,] Scores { get; set; } = new string[2, 9];
        public static int Index { get; private set; }

        //static string file;
        static StreamReader sr;
        static StreamWriter sw;
        static SpriteFont spriteFont = ContentManager.Get<SpriteFont>("titleFont");

        static void GetScore()
        {
            sr = new StreamReader("Content/Highscore.txt");
            string[] lines;
            lines = Regex.Split(sr.ReadToEnd(),"\r\n| ");
            sr.Close();
            Scores[0, 0] = "Name";
            Scores[1, 0] = "Score";
            int i = 0;

            for(int y = 1; y < 8; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    Scores[x, y] = lines[i];
                    i++;
                }
            }
        }

        public static void Record(string name)
        {
            Scores[0, Index] = name;
            sw = new StreamWriter("Content/Highscore.txt");

            for (int y = 1; y < 8; y++)
            {
                sw.WriteLine(Scores[0, y]+" "+ Scores[1, y]);
            }
            sr.Close();
        }

        public static bool CheckScore(int newScore)
        {
            GetScore();

            for(int y = 1; y < 8; y++)
            {
                if(Convert.ToInt32(Scores[1,y]) < newScore)
                {
                    Scores[1, y] = newScore.ToString();
                    Index = y;
                    return true;
                }
            }
            return false;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    spriteBatch.DrawString(spriteFont, Scores[x, y], new Vector2(200 + x * 200, y * 80), Color.White);
                }
            }

            spriteBatch.End();
        }
    }
}
