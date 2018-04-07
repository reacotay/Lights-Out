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
        Vector2 originPosition;
        public Lamp()
        {
            originPosition = new Vector2(400, 0);
            bulb = new PointLight();
            bulb.Scale = new Vector2(600, 1500);
            bulb.Position = new Vector2(400, 0);
        }

        public void Update()
        {
            Vector2 mousePosition = new Vector2(Constants.mouseState.Position.X, Constants.mouseState.Position.Y);
            Vector2 mouseVector = originPosition - mousePosition;
            if (Vector2.Distance(mousePosition, originPosition) > 200)
            {
                mouseVector.Normalize();
                bulb.Position = new Vector2(originPosition.X - mouseVector.X * 200, originPosition.Y - mouseVector.Y*200);
            }
            else
            {
                bulb.Position = mousePosition;
            }
        }
    }
}
