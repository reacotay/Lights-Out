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
    class Bullet : GameObject
    {
        int bulletSpeed;

        Vector2 direction;

        public Bullet(Vector2 position, Vector2 direction)
            : base(position)
        {
            bulletSpeed = 5;

            texture = ContentManager.Get<Texture2D>("bulletTex");
            this.direction = direction;
        }

        public override void Update()
        {
            position += direction * bulletSpeed;
<<<<<<< HEAD
=======

<<<<<<< HEAD
>>>>>>> parent of 415eadf... Fire-rate v01
=======
>>>>>>> master
            base.Update();
        }
    }
}
