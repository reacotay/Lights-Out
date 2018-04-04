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
    class Enemy : GameObject
    {
        float angle;
        float movementSpeed;
        string AI;

        Vector2 direction;

        public Enemy(Vector2 position, string AI)
            : base (position)
        {
            this.AI = AI;

            switch (this.AI)
            {
                case "Chaser":
                    movementSpeed = 3f;
                    break;
            }

            texture = ContentManager.Get<Texture2D>("chaserTex");
        }

        public override void Update()
        {
            switch (AI)
            {
                case "Chaser":
                    position += direction * movementSpeed;
                    break;
            }

            EnemyAngle();

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, angle, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
        }

        //----------------------------------------------------------------------------------------------------

        public void SetDirection(Vector2 playerPosition)
        {
            direction = playerPosition - position;
            direction.Normalize();
        }

        void EnemyAngle()
        {
            angle = Convert.ToSingle(Math.Atan2(direction.X, -direction.Y));
        }
    }
}
