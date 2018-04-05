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
    static class Highscore
    {
        static string[,] Scores { get; set; } = new string[2, 9];
        //static string file;
        static StreamReader sr;

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
        static void Record()
        {
            //Write Scores to file. ezpz
        }
        static void CheckScore(int newScore)
        {
            GetScore();
            for(int y = 1; y < 8; y++)
            {
                if(Convert.ToInt32(Scores[1,y]) < newScore)
                {
                    Scores[1, y] = newScore.ToString();
                    string name = NameEntry();
                    Scores[0, y] = name;
                    Record();
                }
            }
        }
        static string NameEntry()
        {
            string name = "";

            return name;
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    spriteBatch.DrawString(ContentManager.Get<SpriteFont>("spriteFont"), Scores[x, y], new Vector2(x * 50, y * 20), Color.White);
                }
            }
            spriteBatch.End();
        }
    }
}
