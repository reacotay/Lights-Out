using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lights_Out
{
    class Camera
    {
        private Matrix transform;
        private Vector2 position;
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
                transform = Matrix.CreateTranslation(-position.X + view.Width / 2 - Constants.tempDirection.X * 200,
                    -position.Y + view.Width / 2 - Constants.tempDirection.Y * 150, 0);
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
    }
}
