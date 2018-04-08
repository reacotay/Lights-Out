using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        Vector2 originPosition;
        public Lamp()
        {
            originPosition = new Vector2(400, 1);
            bulb = new PointLight()
            {
                Scale = new Vector2(500, 500),
                Position = originPosition,
                ShadowType = ShadowType.Solid

            };
            shade = new Hull(new Vector2(-60, -10), new Vector2(60, -10), new Vector2(60, 30), new Vector2(20, 0), new Vector2(-20, 0), new Vector2(-60, 30))
            {
                Position = originPosition,
            };
        }

        public void Update()
        {
            Vector2 mousePosition = new Vector2(Constants.mouseState.Position.X, Constants.mouseState.Position.Y);
            Vector2 mouseVector = originPosition - mousePosition;
            if (Vector2.Distance(bulb.Position, originPosition) > 150)
            {
                if(bulb.Position.X > 400)
                {
                    float newPosition = bulb.Position.X - 1f;
                    bulb.Position = new Vector2(newPosition, bulb.Position.Y);
                    shade.Position = new Vector2(newPosition, bulb.Position.Y-10);
                }
                else if(bulb.Position.X < 400)
                {
                    float newPosition = bulb.Position.X + 1f;
                    bulb.Position = new Vector2(newPosition, bulb.Position.Y);
                    shade.Position = new Vector2(newPosition, bulb.Position.Y-10);
                }
                //mouseVector.Normalize();
                //Vector2 bulbPos = bulb.Position;
                //bulbPos.Normalize();
                //bulb.Position = new Vector2(originPosition.X + bulbPos.X, originPosition.Y + bulbPos.Y);
            }
            else
            {
                float newPosition = bulb.Position.Y + 1f;
                bulb.Position = new Vector2(bulb.Position.X, newPosition);
                shade.Position = new Vector2(bulb.Position.X, newPosition-10);
            }
            if (Constants.mouseState.LeftButton == ButtonState.Pressed && Vector2.Distance(mousePosition, originPosition) < 200)
            {
                bulb.Position = mousePosition;
            }
        }
    }
}
