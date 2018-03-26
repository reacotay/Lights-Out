using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lights_Out
{
    class Player : GameObject
    {
        float movementSpeed;
        bool sprinting;

        List<Bullet> bulletList;
        List<Bullet> removeList;

        public Player(Vector2 position)
            : base (position)
        {
            movementSpeed = 2f;

            texture = ContentManager.Get<Texture2D>("Player");
            bulletList = new List<Bullet>();
            removeList = new List<Bullet>();
        }
        
        public override void Update()
        {
            PlayerMovement();
            BulletManagment();

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet tempBullet in bulletList)
            {
                tempBullet.Draw(spriteBatch);
            }

            base.Draw(spriteBatch);
        }

        //----------------------------------------------------------------------------------------------------

        void BulletManagment()
        {
            Vector2 worldMousePosition = new Vector2(Constants.mouseState.Position.X, Constants.mouseState.Y) + new Vector2(
                        Math.Max(position.X - (Constants.WindowWidth / 2), 0),
                        Math.Max(position.Y - (Constants.WindowHeight / 2), 0));

            Vector2 direction = worldMousePosition - position;
            direction.Normalize();

            if (Constants.mouseState.LeftButton == ButtonState.Pressed && Constants.oldMouseState.LeftButton == ButtonState.Released)
            {
                Bullet tempBullet = new Bullet(position, direction);
                bulletList.Add(tempBullet);
            }

            foreach (Bullet tempBullet in bulletList)
            {
                tempBullet.Update();
            }
        }

        void PlayerMovement()
        {
            if (Constants.keyState.IsKeyDown(Keys.LeftShift))
                sprinting = true;
            else
                sprinting = false;

            PlayerMovementX();
            PlayerMovementY();
        }

        void PlayerMovementX()
        {
            Rectangle tempDestination = destinationRectangle;

            if (Constants.keyState.IsKeyDown(Keys.W) && Constants.keyState.IsKeyUp(Keys.S))
            {
                if (sprinting)
                    tempDestination.Y -= (int)(movementSpeed * 1.5f);
                else
                    tempDestination.Y -= (int)movementSpeed;
            }
            if (Constants.keyState.IsKeyDown(Keys.S) && Constants.keyState.IsKeyUp(Keys.W))
            {
                if (sprinting)
                    tempDestination.Y += (int)(movementSpeed * 1.5f);
                else
                    tempDestination.Y += (int)movementSpeed;
            }

            position.Y = tempDestination.Y;
        }

        void PlayerMovementY()
        {
            Rectangle tempDestination = destinationRectangle;

            if (Constants.keyState.IsKeyDown(Keys.A) && Constants.keyState.IsKeyUp(Keys.D))
            {
                if (sprinting)
                    tempDestination.X -= (int)(movementSpeed * 1.5f);
                else
                    tempDestination.X -= (int)movementSpeed;
            }
            if (Constants.keyState.IsKeyDown(Keys.D) && Constants.keyState.IsKeyUp(Keys.A))
            {
                if (sprinting)
                    tempDestination.X += (int)(movementSpeed * 1.5f);
                else
                    tempDestination.X += (int)movementSpeed;
            }

            position.X = tempDestination.X;
        }
    }
}
