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
    class HeatBar : GameObject
    {
        private float colorAmount;
        private Texture2D backgroundTexture;
        private Color color;
        private SpriteFont font;

        public HeatBar(Vector2 position, int size) : base(position, size)
        {
            texture = ContentManager.Get<Texture2D>("barTex");
            backgroundTexture = ContentManager.Get<Texture2D>("barBackgroundTex");
            font = ContentManager.Get<SpriteFont>("spriteFont");
            this.position = position;
            Constants.HeatValue = 0;    
        }

        public override void Update()
        {
            colorAmount = Constants.HeatValue / 100;
            Constants.HeatValue -= 0.1f;
            Constants.HeatValue = MathHelper.Clamp(Constants.HeatValue, 0, 100);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var startColor = new Color(0, 255, 0);
            var finalColor = new Color(255, 0, 0);
            color = Color.Lerp(startColor, finalColor, colorAmount);

            spriteBatch.Draw(backgroundTexture, new Rectangle((int)position.X - 100, (int)position.Y, texture.Width, texture.Height), new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
            spriteBatch.Draw(texture, new Rectangle((int)position.X - 100, (int)position.Y, (int)(texture.Width * ((double)Constants.HeatValue / 100)), 44), new Rectangle(0, 45, texture.Width, texture.Height), color);
            spriteBatch.Draw(texture, new Rectangle((int)position.X - 100, (int)position.Y, texture.Width, 44), new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
            
            spriteBatch.DrawString(font, "Heat-Meter", new Vector2(position.X -95, position.Y - 17), Color.White);
        }
    }
}
