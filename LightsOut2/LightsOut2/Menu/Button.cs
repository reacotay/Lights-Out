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
    class Button : GameObject
    {
        private string text;

        public Button(Vector2 position, int size, string text)
            : base(position, size)
        {
            texture = ContentManager.Get<Texture2D>(text);
        }

        //----------------------------------------------------------------------------------------------------

        public bool CheckClicked(Point mousePos)
        {
            if (destinationRectangle.Contains(mousePos))
                return true;
            else
                return false;
        }
    }
}
