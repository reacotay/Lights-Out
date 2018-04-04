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
    abstract class GameObject
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 centerPosition;
        public Rectangle destinationRectangle;

        public GameObject(Vector2 position)
        {
            this.position = position;
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, Constants.CellSize, Constants.CellSize);
        }

        public virtual void Update()
        {
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, Constants.CellSize, Constants.CellSize);
            centerPosition = new Vector2(position.X+Constants.CellSize / 2, position.Y+Constants.CellSize / 2);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, Color.White);
        }
    }
}