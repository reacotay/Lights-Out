﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut2
{
    class HeatBar
    {

        SpriteFont font;
        Texture2D barTex;
        Vector2 position;
        public HeatBar(Vector2 position)
        {
            barTex = ContentManager.Get<Texture2D>("barTex");
            font = ContentManager.Get<SpriteFont>("spriteFont");
            this.position = position;
            Constants.heatValue = 0;
        }
        public void Update()
        {
            Constants.heatValue -= 0.3f;
            Constants.heatValue = MathHelper.Clamp(Constants.heatValue, 0, 100);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(barTex, new Rectangle((int)position.X, (int)position.Y, barTex.Width, 44), new Rectangle(0, 0, barTex.Width, 44), Color.Gray);
            spriteBatch.Draw(barTex, new Rectangle((int)position.X, (int)position.Y, (int)(barTex.Width * ((double)Constants.heatValue / 100)), 44), new Rectangle(0, 45, barTex.Width, 44), Color.Red);
            spriteBatch.Draw(barTex, new Rectangle((int)position.X, (int)position.Y, barTex.Width, 44), new Rectangle(0, 0, barTex.Width, 44), Color.White);
            spriteBatch.DrawString(font, "Heat-Meter", new Vector2(position.X + 5, position.Y + 22), Color.Black);
        }
    }
}