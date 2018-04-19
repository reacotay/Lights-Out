﻿using Microsoft.Xna.Framework;
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
        public int lives;
        bool tempDead;
        float angle;
        float movementSpeed;
        bool sprinting;
        bool overheated;
        int fireRate;

        Vector2 direction;
        public ScreenClear screenClear;
        public List<Bullet> bulletList;
        public List<Bullet> removeList;
        public Light viscinity;
        public Light view;

        public Player(Vector2 position, int size)
            : base(position, size)
        {
            lives = 3;
            tempDead = false;
            movementSpeed = 3f;
            fireRate = 10;
            
            direction = new Vector2(1, 0);
            texture = ContentManager.Get<Texture2D>("playerTex");
            screenClear = null;
            bulletList = new List<Bullet>();
            removeList = new List<Bullet>();
            viscinity = new PointLight();
            view = new Spotlight();
            viscinity.Scale = new Vector2(800, 800);
            view.Scale = new Vector2(1600, 1600);
            view.Intensity = 2f;
        }

        public override void Update()
        {
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
            foreach (Bullet tempBullet in bulletList)
            {
                tempBullet.Draw(spriteBatch);
            }

            if (screenClear != null)
            {
                screenClear.Draw(spriteBatch);
            }

            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, angle, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
        }


        //----------------------------------------------------------------------------------------------------

        public void MoveToStartPosition()
        {
            Vector2 tempDirection = new Vector2(800, 800) - position; tempDirection.Normalize();
            position += tempDirection * movementSpeed;
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, size, size);

            if (position.X >= 750 && position.X <= 850 && position.Y >= 750 && position.Y <= 850)
            {
                tempDead = false;
            }
        }

        public void TakeDamage()
        {
            screenClear = new ScreenClear(position, Constants.StandardSize);
            lives--;
            tempDead = true;
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

                if (Constants.gamePadState.Triggers.Right >= 0.7f)
                {
                    CreateBullet();
                }
            }
            else
            {
                Vector2 worldMousePosition = Vector2.Transform(new Vector2(Constants.mouseState.Position.X, Constants.mouseState.Position.Y), Matrix.Invert(GameManager.camera.GetTransform()));

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
            if (Constants.gamePadState.IsConnected)
            {
                if (Constants.gamePadState.IsButtonDown(Buttons.LeftShoulder))
                    sprinting = true;
                else
                    sprinting = false;
            }
            else
            {
                if (Constants.keyState.IsKeyDown(Keys.LeftShift))
                    sprinting = true;
                else
                    sprinting = false;
            }

            PlayerMovementX();
            PlayerMovementY();
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
            }

            position.Y = tempDestination.Y;
        }

        void PlayerMovementX()
        {
            Rectangle tempDestination = destinationRectangle;

            if (Constants.gamePadState.IsConnected)
            {
                tempDestination.X += (int)(Constants.gamePadState.ThumbSticks.Left.X * movementSpeed);
            }
            else
            {
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
            }

            position.X = tempDestination.X;
        }

        void PlayerAngle()
        {
            angle = Convert.ToSingle(Math.Atan2(direction.X, -direction.Y));
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
