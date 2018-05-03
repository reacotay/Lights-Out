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
        private int radius = 100, length = 500;
        private float x = 400, y = 0;
        private double angle = Math.PI / 5, acceleration, velocity = 0, dt = 0.15;
        private int flicks, flickerTimer = 0;
        private bool lightOut, ttFlicker;
        public Light bulb;
        public Hull shade;
        private Vector2 originPosition = new Vector2(400, 0);

        public Lamp()
        {
            bulb = new PointLight()
            {
                Scale = new Vector2(400, 800),
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
            x = Convert.ToSingle(Math.Sin(-angle)) * radius + originPosition.X;
            y = Convert.ToSingle(Math.Cos(-angle)) * radius + originPosition.Y;
            bulb.Position = new Vector2(x, y);
            bulb.Rotation = Convert.ToSingle(angle);
            shade.Rotation = Convert.ToSingle(angle);
            shade.Position = new Vector2(x, y - 15);

            acceleration = -9.81 / length * Math.Sin(angle);
            velocity += acceleration * dt;
            angle += velocity * dt;
            if(flickerTimer <= 0 && !ttFlicker)
            {
                if (flicks > 0)
                {
                    ttFlicker = true;
                }
                else
                {
                    flickerTimer = Constants.Randomizer.Next(0, 300);
                    flicks = Constants.Randomizer.Next(1, 6);
                }
            }
            else
            {
                flickerTimer--;
            }
            if(ttFlicker)
            {
                if (flicks > 0)
                    Flicker();
                else
                    ttFlicker = false;
            }
        }

        private void Flicker()
        {
            if (bulb.Intensity > 1E-05f && !lightOut)
                bulb.Intensity -= 0.5f;
            else if (bulb.Intensity < 1f && lightOut)
                bulb.Intensity += 0.5f;
            if (bulb.Intensity <= 1E-05f)
                lightOut = true;
            if (bulb.Intensity >= 1f)
            {
                lightOut = false;
                flicks--;
            }
        }
    }
}
