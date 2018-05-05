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
        public Button(Vector2 position, int size, string text)
            : base(position, size)
        {
            texture = ContentManager.Get<Texture2D>(text);

            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!destinationRectangle.Contains(Constants.mouseState.Position))
                spriteBatch.Draw(texture, destinationRectangle, Color.DimGray);
            else
                spriteBatch.Draw(texture, destinationRectangle, Color.DarkRed);
        }

        //----------------------------------------------------------------------------------------------------

        public bool CheckClicked()
        {
            if (destinationRectangle.Contains(Constants.mouseState.Position) && Constants.mouseState.LeftButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }
    }
}
