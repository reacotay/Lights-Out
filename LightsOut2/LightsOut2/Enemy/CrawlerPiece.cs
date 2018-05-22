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
        public int PieceHitpoints { get; private set; }

        public CrawlerPiece(Vector2 position, int size, Texture2D texture)
            : base (position, size)
        {
            hitpoints = 1;
            PieceHitpoints = 1;
            movementSpeed = 3f;
            this.texture = texture;
        }

        public void Update(Vector2 targetPosition)
        {
            PieceHitpoints = hitpoints;
            SetDirection(targetPosition);
            position = targetPosition - direction * 20;
            EnemyAngle();

            base.Update();
        }
        
    }
}
