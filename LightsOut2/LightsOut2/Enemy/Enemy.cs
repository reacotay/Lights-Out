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
        protected int hitpoints;
        protected float angle;
        protected float movementSpeed;

        protected Vector2 direction;

        public Enemy(Vector2 position, int size)
            : base (position, size)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y - Constants.ShadowOffset), new Rectangle(0, 0, texture.Width, texture.Height), Color.Black, angle, new Vector2(texture.Width / 2, texture.Height / 2), 1.1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, angle, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
        }

        //----------------------------------------------------------------------------------------------------

        public void SetDirection(Vector2 playerPosition)
        {
            direction = playerPosition - position;
            direction.Normalize();
        }

        protected void EnemyAngle()
        {
            angle = Convert.ToSingle(Math.Atan2(direction.X, -direction.Y));
        }

        public virtual bool TakeDamage()
        {
            hitpoints--;

            if (hitpoints == 0)
                return true;
            else
                return false;
        }
    }
}
