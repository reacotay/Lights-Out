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
    class ScreenClear : GameObject
    {
        private int maxSize;
        public bool remove;

        public ScreenClear(Vector2 position, int size)
            : base (position, size)
        {
            maxSize = 3200;
            remove = false;

            texture = ContentManager.Get<Texture2D>("ScreenClear");
        }

        public override void Update()
        {
            if (size < maxSize)
            {
                position -= new Vector2(10, 10);
                size += 20;
            }
            else
                remove = true;

            base.Update();
        }
    }
}
