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
    class HeatBar
    {
        float max = 100f;
        float min = 0f;
        public static float current;

        Texture2D barTex;
        Vector2 position;
        public HeatBar(Vector2 position)
        {
            barTex = ContentManager.Get<Texture2D>("barTex");
            this.position = position;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(barTex, new Rectangle((int)position.X, (int)position.Y, barTex.Width, 44), new Rectangle(0, 45, barTex.Width, 44), Color.Red);

            spriteBatch.Draw(barTex, new Rectangle((int)position.X, (int)position.Y, barTex.Width, 44), new Rectangle(0, 0, barTex.Width, 44), Color.White);
        }
    }
}
