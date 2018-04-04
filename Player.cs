using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Penumbra;

namespace Lights_Out
{
    class Player : GameObject
    {
        float movementSpeed;
        bool sprinting;
        Vector2 direction;
        float angle;
        public Light viscinity;
        public Light view;

        List<Bullet> bulletList;
        List<Bullet> removeList;

        public Player(Vector2 position)
            : base (position)
        {
            movementSpeed = 2f;
            viscinity = new PointLight();
            view = new Spotlight();
            viscinity.Scale = new Vector2(400,400);
            view.Scale = new Vector2(700, 800);
            texture = ContentManager.Get<Texture2D>("playerTex");
            bulletList = new List<Bullet>();
            removeList = new List<Bullet>();
        }
        
        public override void Update()
        {
            
            PlayerMovement();
            BulletManagment();
            viscinity.Position = position;
            view.Position = position;
            view.Rotation = angle - MathHelper.ToRadians(90f);
            BulletManagment();
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet tempBullet in bulletList)
            {
                tempBullet.Draw(spriteBatch);
            }

            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, angle, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), new Rectangle(0, 0, texture.Width, texture.Height), Color.Black, 0f, new Vector2(texture.Width / 2, texture.Height / 2), 0.1f, SpriteEffects.None, 0f);
        }

        //----------------------------------------------------------------------------------------------------

        void BulletManagment()
        {
            Vector2 worldMousePosition = Vector2.Transform(new Vector2(Constants.mouseState.Position.X, Constants.mouseState.Position.Y), Matrix.Invert(GameManager.camera.GetTransform()));

            direction = worldMousePosition - position;
            direction.Normalize();

            if (Constants.gamePadState.IsConnected)
            {


                if (Constants.tempDirection != Vector2.Zero)
                {
                    direction = Constants.tempDirection;
                    direction.Normalize();
                }


                if (Constants.gamePadState.Triggers.Right >= 0.7f)
                {
                    Bullet tempBullet = new Bullet(position, direction);
                    bulletList.Add(tempBullet);
                }
            }

            else
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
            PlayerAngle();
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

        void PlayerAngle()
        {
            angle = Convert.ToSingle(Math.Atan2(direction.X, -direction.Y));
        }
    }
}
