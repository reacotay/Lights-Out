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
    abstract class GameObject
    {
        protected int size;
        
        public Texture2D texture;
        public Vector2 position;
        public Vector2 centerPosition;
        public Rectangle destinationRectangle;

        public GameObject(Vector2 position, int size)
        {
            this.size = size;

            this.position = position;
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, size, size);
        }

        public virtual void Update()
        {
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, size, size);
            centerPosition = new Vector2(position.X + (size / 2), position.Y + (size / 2));
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, Color.White);
        }
    }
}
