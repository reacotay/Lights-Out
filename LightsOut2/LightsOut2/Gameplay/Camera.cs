﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LightsOut2
{
    class Camera
    {
        private Matrix transform;
        public Vector2 position;
        private Vector2 tempPosition;
        private Viewport view;

        public Camera(Viewport view)
        {
            this.view = view;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;

            if (Constants.gamePadState.IsConnected)
            {
                Boundaries();
                tempPosition += Constants.tempDirection * 8;
                transform = Matrix.CreateTranslation
                    (-position.X + view.Width / 2 - tempPosition.X,
                    -position.Y + view.Height / 2 - tempPosition.Y, 0);
            }
            else
            {
                transform = Matrix.CreateTranslation
                    (-position.X + view.Width / 2 - (Constants.mouseState.Position.X / 2) + 200,
                    -position.Y + view.Height / 2 - (Constants.mouseState.Position.Y / 2) + 200, 0);
            }
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Matrix GetTransform()
        {
            return transform;
        }

        public void Boundaries()
        {
            int boundaryValue = 150;
            if (tempPosition.X >= boundaryValue)
                tempPosition.X = boundaryValue;

            if (tempPosition.X <= -boundaryValue)
                tempPosition.X = -boundaryValue;

            if (tempPosition.Y >= boundaryValue)
                tempPosition.Y = boundaryValue;

            if (tempPosition.Y <= -boundaryValue)
                tempPosition.Y = -boundaryValue;
        }
    }
}
