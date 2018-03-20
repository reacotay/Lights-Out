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

            position.Y = tempDestination.Y;
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

            position.X = tempDestination.X;
        }
    }
}
