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
        int fireRate;
        int frame;
        bool shooting;
        bool fleeing;
        double range;
        double distance;
        
        public List<Bullet> enemyBulletList;
        public List<Bullet> enemyRemoveList;

        public Shooter(Vector2 position, int size)
            : base(position, size)
        {
            hitpoints = 1;
            movementSpeed = 2f;
            fireRate = 20;
            frame = 0;
            range = 200;
            texture = ContentManager.Get<Texture2D>("shooterTex");
            enemyBulletList = new List<Bullet>();
            enemyRemoveList = new List<Bullet>();
        }

        public override void Update()
        {
            if (fleeing)
            {
                position -= direction * movementSpeed;
                shooting = true;
            }
            else if (!fleeing)
            {
                position += direction * movementSpeed;
                shooting = false;
            }
            BulletManagement();
            EnemyAngle();
            base.Update();
        }

        public void CalculateDistance(Vector2 playerPosition)
        {
            Vector2 vectorDistance = playerPosition - position;
            distance = Math.Sqrt((Math.Pow(vectorDistance.X, 2) + Math.Pow(vectorDistance.Y, 2)));

            if (distance < range)
            {
                fleeing = true;
            }
            else if (distance > range)
            {
                fleeing = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet tempBullet in enemyBulletList)
            {
                tempBullet.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }

        void BulletManagement()
        {
            if (shooting)
            {
                if (frame == fireRate)
                {
                    CreateBullet();
                }
                frame++;
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
            frame = 0;
        }
    }
}
