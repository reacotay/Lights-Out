using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myGame
{
    class Player : GameObject
    {
        float movementSpeed;
        bool sprinting;

        GameObject[,] gameObjectArray;
        KeyboardState keyboardState, previousKeyboardState;

        public Player(Vector2 position)
            : base (position)
        {
            movementSpeed = 2f;

            texture = ContentManager.Get<Texture2D>("Player");
        }
        
        public override void Update()
        {
            previousKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            PlayerMovement();

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        //----------------------------------------------------------------------------------------------------

        public void TransferGameObjectArray(GameObject[,] gameObjectArray)
        {
            this.gameObjectArray = gameObjectArray;
        }

        void PlayerMovement()
        {
            if (keyboardState.IsKeyDown(Keys.LeftShift))
                sprinting = true;
            else
                sprinting = false;

            PlayerMovementX();
            PlayerMovementY();
        }

        void PlayerMovementX()
        {
            Rectangle tempDestination = destinationRectangle;

            if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyUp(Keys.S))
            {
                if (sprinting)
                    tempDestination.Y -= (int)(movementSpeed * 1.5f);
                else
                    tempDestination.Y -= (int)movementSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyUp(Keys.W))
            {
                if (sprinting)
                    tempDestination.Y += (int)(movementSpeed * 1.5f);
                else
                    tempDestination.Y += (int)movementSpeed;
            }

            bool collision = WallCollision(tempDestination);

            if (!collision)
            {
                position.Y = tempDestination.Y;
            }
        }

        void PlayerMovementY()
        {
            Rectangle tempDestination = destinationRectangle;

            if (keyboardState.IsKeyDown(Keys.A) && keyboardState.IsKeyUp(Keys.D))
            {
                if (sprinting)
                    tempDestination.X -= (int)(movementSpeed * 1.5f);
                else
                    tempDestination.X -= (int)movementSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyUp(Keys.A))
            {
                if (sprinting)
                    tempDestination.X += (int)(movementSpeed * 1.5f);
                else
                    tempDestination.X += (int)movementSpeed;
            }

            bool collision = WallCollision(tempDestination);

            if (!collision)
            {
                position.X = tempDestination.X;
            }
        }

        bool WallCollision(Rectangle tempRectangle)
        {
            bool collision = false;

            for (int Y = 0; Y < Constants.CellAmountY; Y++)
            {
                for (int X = 0; X < Constants.CellAmountX; X++)
                {
                    if (gameObjectArray[X, Y] is WallBlock WB)
                    {
                        if (WB.destinationRectangle.Intersects(tempRectangle))
                        {
                            collision = true;
                        }
                    }
                    if (gameObjectArray[X, Y] is Box box)
                    {
                        if (box.destinationRectangle.Intersects(tempRectangle))
                        {
                            collision = true;
                        }
                    }
                }
            }
            return collision;
        }
    }
}
