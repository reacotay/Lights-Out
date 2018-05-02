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
    class CrawlerPiece : Enemy
    {
        public CrawlerPiece(Vector2 position, int size, Texture2D texture)
            : base (position, size)
        {
            hitpoints = 1;
            movementSpeed = 3f;
            this.texture = texture;
        }

        public override void Update()
        {
            position += direction * movementSpeed;
            EnemyAngle();
            base.Update();
        }
    }
}
