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
    class EnemyManager
    {
        private int crawlerCounter;
        private int timePassed;
        private float spawnRate;
        private int number;
        public Enemy tempEnemy;
        public List<Enemy> enemyList;
        public List<Enemy> removeList;

        public EnemyManager()
        {
            enemyList = new List<Enemy>();
            removeList = new List<Enemy>();
        }

        public void Update(Vector2 playerPosition)
        {
            if (timePassed >= spawnRate)
            {
                if (crawlerCounter < 50)
                {
                    number = Constants.Randomizer.Next(1, 6);
                    switch (number)
                    {
                        case 1:
                            tempEnemy = new Chaser(GeneratePosition(), Constants.StandardSize);
                            break;
                        case 2:
                            tempEnemy = new Charger(GeneratePosition(), Constants.StandardSize);
                            break;
                        case 3:
                            tempEnemy = new Shooter(GeneratePosition(), Constants.StandardSize);
                            break;
                        case 4:
                            tempEnemy = new Rager(GeneratePosition(), Constants.BigSize);
                            break;
                        case 5:
                            tempEnemy = new Crawler(GeneratePosition(), Constants.StandardSize);
                            break;
                    }
                    enemyList.Add(tempEnemy);
                    timePassed = 0;
                    crawlerCounter++;
                    spawnRate = Constants.Randomizer.Next(45, 120);
                }
                else
                {
                    tempEnemy = new Crawler(GeneratePosition(), Constants.BigSize);
                    enemyList.Add(tempEnemy);
                    crawlerCounter = 0;
                    timePassed = 0;
                }
            }
            else
            {
                timePassed++;
            }

            foreach (Shooter tempShooter in enemyList.OfType<Shooter>())
            {
                tempShooter.CalculateDistance(playerPosition);
            }

            foreach (Enemy tempEnemy in enemyList)
            {
                tempEnemy.SetDirection(playerPosition);
                tempEnemy.Update();
            }

            foreach (Enemy tempEnemy in removeList)
            {
                enemyList.Remove(tempEnemy);
            }
            removeList.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy tempEnemy in enemyList)
            {
                tempEnemy.Draw(spriteBatch);
            }
        }

        //----------------------------------------------------------------------------------------------------

        Vector2 GeneratePosition()
        {
            int value = Constants.Randomizer.Next(1, 5);
            Vector2 tempPosition = Vector2.Zero;

            switch (value)
            {
                case 1:
                    tempPosition = new Vector2(-800, Constants.Randomizer.Next(0, Constants.PlatformHeight));
                    break;

                case 2:
                    tempPosition = new Vector2(2400, Constants.Randomizer.Next(0, Constants.PlatformHeight));
                    break;

                case 3:
                    tempPosition = new Vector2(Constants.Randomizer.Next(0, Constants.PlatfromWidth), -800);
                    break;

                case 4:
                    tempPosition = new Vector2(Constants.Randomizer.Next(0, Constants.PlatfromWidth), 2400);
                    break;
            }

            return tempPosition;
        }
    }
}
