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
    class Shooter : Enemy
    {
        int shootCooldown;
        int firing;
        bool inRange;
        bool shooting;
        bool fleeing;

        Vector2 range;
        Vector2 distance;
        
        List<Bullet> enemyBulletList;
        List<Bullet> enemyRemoveList;

        public Shooter(Vector2 position, int size)
            : base(position, size)
        {
            hitpoints = 1;
            movementSpeed = 2f;
            shootCooldown = 20;
            firing = 0;
            range = new Vector2(200, 200);
            texture = ContentManager.Get<Texture2D>("shooterTex");
            enemyBulletList = new List<Bullet>();
            enemyRemoveList = new List<Bullet>();
        }

        public override void Update()
        {
            if (fleeing)
            {
                position -= direction * movementSpeed;
            }
            else if (!fleeing)
            {
                position += direction * movementSpeed;
            }
            BulletManagement();
            EnemyAngle();
            base.Update();
        }

        public void CalculateDistance(Vector2 playerPosition)
        {
            distance = playerPosition - position;

            if (distance.X < range.X || distance.Y < range.Y)
            {
                fleeing = true;
            }
            else if (distance.X > range.X || distance.Y > range.Y)
            {
                fleeing = false;
               
            }
        }

        void BulletManagement()
        {
            if (!fleeing)
            {
                if (firing <= shootCooldown)
                {
                    CreateBullet();
                }
                firing++;
            }

            foreach (Bullet tempBullet in enemyBulletList)
            {
                tempBullet.Update();
            }

            foreach (Bullet tempBullet in enemyRemoveList)
            {
                enemyBulletList.Remove(tempBullet);
            }
        }

        private void CreateBullet()
        {
            Bullet tempBullet = new Bullet(position, Constants.BulletSize, direction);
            enemyBulletList.Add(tempBullet);
            
        }
    }
}
