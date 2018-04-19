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
    class Rager : Enemy
    {
        bool rage;

        public Rager(Vector2 position, int size)
            : base (position, size)
        {
            hitpoints = 5;
            rage = false;
            movementSpeed = 1f;

            texture = ContentManager.Get<Texture2D>("chaserTex");
        }

        public override void Update()
        {
            position += direction * movementSpeed;
            EnemyAngle();
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!rage)
            {
                spriteBatch.Draw(texture, new Vector2(position.X, position.Y), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, angle, new Vector2(texture.Width / 2, texture.Height / 2), 2f, SpriteEffects.None, 0f);
            }
            else if (rage)
            {
                spriteBatch.Draw(texture, new Vector2(position.X, position.Y), new Rectangle(0, 0, texture.Width, texture.Height), Color.Red, angle, new Vector2(texture.Width / 2, texture.Height / 2), 2f, SpriteEffects.None, 0f);
            }
        }

        public override bool TakeDamage()
        {
            hitpoints--;

            if (!rage)
            {
                rage = true;
                movementSpeed = 10;
            }

            if (hitpoints == 0)
                return true;
            else
                return false;
        }
    }
}
