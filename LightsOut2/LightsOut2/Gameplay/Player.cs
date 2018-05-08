using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Penumbra;

namespace LightsOut2
{
    class Player : GameObject
    {
        public int extraLife;
        private bool tempDead;
        private float angle;
        private float movementSpeed;
        private bool overheated;
        public bool moving;
        private int fireRate;

        private Texture2D crosshairTex;
        private Vector2 direction;
        private Vector2 aimingDirection;
        public ScreenClear screenClear;
        public List<Bullet> bulletList;
        public List<Bullet> removeList;
        public Light viscinity;
        public Light view;

        public Player(Vector2 position, int size)
            : base(position, size)
        {
            extraLife = 3;
            tempDead = false;
            movementSpeed = 5f;
            fireRate = 10;
            
            direction = new Vector2(1, 0);
            texture = ContentManager.Get<Texture2D>("playerTex");
            crosshairTex = ContentManager.Get<Texture2D>("Crosshair");

            screenClear = null;
            bulletList = new List<Bullet>();
            removeList = new List<Bullet>();
            viscinity = new PointLight();
            view = new Spotlight();
            viscinity.Scale = new Vector2(1000, 1000);
            view.Scale = new Vector2(1600, 1600);
            view.Intensity = 2f;
        }

        public override void Update()
        {
            moving = false;

            if (!tempDead)
            {
                PlayerMovement();
                BulletManagment();
            }
            else if (tempDead)
            {
                MoveToStartPosition();
            }

            if (Constants.HeatValue >= 100)
                overheated = true;
            else if (Constants.HeatValue <= 0)
                overheated = false;

            if (screenClear != null)
            {
                if (!screenClear.remove)
                    screenClear.Update();
                else if (screenClear.remove)
                    screenClear = null;
            }

            viscinity.Position = position;
            view.Position = position;
            view.Rotation = angle - MathHelper.ToRadians(90f);
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Constants.gamePadState.IsConnected)
                spriteBatch.Draw(crosshairTex, position + (aimingDirection * 300), Color.White);
            else
                spriteBatch.Draw(crosshairTex, Constants.WorldMousePosition(), Color.White);

            foreach (Bullet tempBullet in bulletList)
            {
                tempBullet.Draw(spriteBatch);
            }

            if (screenClear != null)
            {
                screenClear.Draw(spriteBatch);
            }

            spriteBatch.Draw(texture, new Vector2(position.X, position.Y - Constants.ShadowOffset), new Rectangle(0, 0, texture.Width, texture.Height), Color.Black, angle, new Vector2(texture.Width / 2, texture.Height / 2), 1.1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, angle, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
        }


        //----------------------------------------------------------------------------------------------------

        public void MoveToStartPosition()
        {
            if (position.X >= 750 && position.X <= 850 && position.Y >= 750 && position.Y <= 850)
            {
                tempDead = false;
            }

            if (tempDead)
            {
                Vector2 tempDirection = new Vector2(800, 800) - position; tempDirection.Normalize();
                position += tempDirection * movementSpeed;
                destinationRectangle = new Rectangle((int)position.X, (int)position.Y, size, size);
            }
        }

        public void TakeDamage()
        {
            if (!tempDead)
            {
                Sfx.Play.PlayerDeath();
                screenClear = new ScreenClear(position, Constants.StandardSize);
                extraLife--;
                tempDead = true;
            }
        }

        void BulletManagment()
        {
            if (Constants.gamePadState.IsConnected)
            {
                if (Constants.tempDirection != Vector2.Zero)
                {
                    direction = Constants.tempDirection;
                    direction.Normalize();
                }

                if (Constants.gamePadState.Triggers.Right >= 0.7f && overheated == false)
                {
                    CreateBullet();
                }
            }
            else
            {
                Vector2 worldMousePosition = Constants.WorldMousePosition();

                direction = worldMousePosition - position;
                direction.Normalize();
                if (Constants.mouseState.LeftButton == ButtonState.Pressed && overheated == false)
                {
                    CreateBullet();
                }
            }
            
            foreach (Bullet tempBullet in bulletList)
            {
                tempBullet.Update();
            }

            foreach (Bullet tempBullet in removeList)
            {
                bulletList.Remove(tempBullet);
            }

            fireRate--;
        }

        void PlayerMovement()
        {
            PlayerMovementX();
            PlayerMovementY();

            if (Constants.gamePadState.IsConnected)
                PlayerControllerDirection();
            else
                PlayerAngle();
        }

        void PlayerMovementY()
        {
            Rectangle tempDestination = destinationRectangle;

            if (Constants.gamePadState.IsConnected)
            {
                tempDestination.Y -= (int)(Constants.gamePadState.ThumbSticks.Left.Y * movementSpeed);
                
            }
            else
            {
                if (Constants.keyState.IsKeyDown(Keys.W) && Constants.keyState.IsKeyUp(Keys.S))
                {
                    tempDestination.Y -= (int)movementSpeed;
                    moving = true;
                }

                if (Constants.keyState.IsKeyDown(Keys.S) && Constants.keyState.IsKeyUp(Keys.W))
                {
                    tempDestination.Y += (int)movementSpeed;
                    moving = true;
                }
            }

            if (tempDestination.Y >= 0 && tempDestination.Y <= 1600)
                position.Y = tempDestination.Y;
        }

        void PlayerMovementX()
        {
            Rectangle tempDestination = destinationRectangle;

            if (Constants.gamePadState.IsConnected)
            {
                tempDestination.X += (int)(Constants.gamePadState.ThumbSticks.Left.X * movementSpeed);
                moving = true;
            }
            else
            {
                if (Constants.keyState.IsKeyDown(Keys.A) && Constants.keyState.IsKeyUp(Keys.D))
                {
                    tempDestination.X -= (int)movementSpeed;
                    moving = true;
                }

                if (Constants.keyState.IsKeyDown(Keys.D) && Constants.keyState.IsKeyUp(Keys.A))
                {
                    tempDestination.X += (int)movementSpeed;
                    moving = true;
                }
            }

            if (tempDestination.X >= 0 && tempDestination.X <= 1600)
                position.X = tempDestination.X;
        }

        private void PlayerAngle()
        {
            angle = Convert.ToSingle(Math.Atan2(direction.X, -direction.Y));
        }

        private void PlayerControllerDirection()
        {
            if (new Vector2(Constants.gamePadState.ThumbSticks.Right.X,
                            Constants.gamePadState.ThumbSticks.Right.Y) != Vector2.Zero)
            {
                aimingDirection = new Vector2(Constants.gamePadState.ThumbSticks.Right.X,
                                          -Constants.gamePadState.ThumbSticks.Right.Y);
                aimingDirection.Normalize();
            }

            angle = Convert.ToSingle(Math.Atan2(aimingDirection.X, -aimingDirection.Y));
        }

        private void CreateBullet()
        {
            Constants.HeatValue += 0.6f;

            if (fireRate <= 0)
            {
                Bullet tempBullet = new Bullet(position, Constants.BulletSize, direction);
                bulletList.Add(tempBullet);
                fireRate = 10;
            }
        }
    }
}
