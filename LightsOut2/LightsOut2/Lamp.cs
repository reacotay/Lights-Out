using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;

namespace LightsOut2
{
    class Lamp
    {
        public Light bulb;
        public Hull shade;
        Vector2 originPosition = new Vector2(400,0);
        int radius = 100;
        float angle = 0f, x = 400, y = 0;
        double idfk = Math.PI / 4, acceleration, velocity = 0, dt = 0.15;
        int length = 500;

        public Lamp()
        {
            bulb = new PointLight()
            {
                Scale = new Vector2(600, 800),
                Position = new Vector2(x, y),
                ShadowType = ShadowType.Solid

            };
            shade = new Hull(new Vector2(-60, -10), new Vector2(60, -10), new Vector2(60, 30), new Vector2(20, 0), new Vector2(-20, 0), new Vector2(-60, 30))
            {
                Position = new Vector2(bulb.Position.X,bulb.Position.Y+200),
            };
        }

        public void Update(GameTime gameTime)
        {
            angle = Convert.ToSingle(-idfk);
            x = Convert.ToSingle(Math.Sin(angle)) * radius + originPosition.X;
            y = Convert.ToSingle(Math.Cos(angle)) * radius + originPosition.Y;
            bulb.Position = new Vector2(x, y);
            bulb.Rotation = Convert.ToSingle(idfk);
            shade.Rotation = Convert.ToSingle(idfk);
            shade.Position = new Vector2(x, y - 15);

            acceleration = -9.81 / length * Math.Sin(idfk);
            velocity += acceleration * dt;
            idfk += velocity * dt;
        }
    }
}
