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
    class Chaser : Enemy
    {
        public Chaser(Vector2 position, int size)
            : base (position, size)
        {
            hitpoints = 1;
            movementSpeed = 3f;

            texture = ContentManager.Get<Texture2D>("chaserTex");
        }

        public override void Update()
        {
            position += direction * movementSpeed;
            EnemyAngle();

            base.Update();
        }
    }
}
