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
    class Charger : Enemy
    {
        private int chargeDelay;
        private int count;
        private bool charging;

        private Vector2 chargeDirection;

        public Charger(Vector2 position, int size)
            : base(position, size)
        {
            hitpoints = 1;
            movementSpeed = 7f;

            texture = ContentManager.Get<Texture2D>("chargerTex");
        }

        public override void Update()
        {
            if (!charging)
            {
                EnemyAngle();
                chargeDirection = direction;

                if (chargeDelay == 100)
                {
                    charging = true;
                    count = 0;
                }
                else
                {
                    chargeDelay++;
                }
            }
            else if (charging)
            {
                position += chargeDirection * movementSpeed;
                count++;

                if(count == 50)
                {
                    charging = false;
                    chargeDelay = 0;
                }
            }

            base.Update();
        }
    }
}
