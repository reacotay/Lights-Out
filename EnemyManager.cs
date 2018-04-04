﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lights_Out
{
    class EnemyManager
    {
        int timePassed;
        float spawnRate;

        Enemy tempEnemy;
        public List<Enemy> enemyList;
        public List<Enemy> removeList;

        public EnemyManager()
        {
            timePassed = 0;
            spawnRate = 60;

            enemyList = new List<Enemy>();
            removeList = new List<Enemy>();
        }

        public void Update(Vector2 playerPosition)
        {
            if (timePassed >= spawnRate)
            {
                tempEnemy = new Enemy(GeneratePosition(), "Chaser");
                enemyList.Add(tempEnemy);
                timePassed = 0;
            }
            else
            {
                timePassed++;
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
                    tempPosition = new Vector2(0, Constants.Randomizer.Next(0, Constants.PlatformHeight));
                    break;

                case 2:
                    tempPosition = new Vector2(1600, Constants.Randomizer.Next(0, Constants.PlatformHeight));
                    break;

                case 3:
                    tempPosition = new Vector2(Constants.Randomizer.Next(0, Constants.PlatfromWidth), 0);
                    break;

                case 4:
                    tempPosition = new Vector2(Constants.Randomizer.Next(0, Constants.PlatfromWidth), 1600);
                    break;
            }

            return tempPosition;
        }
    }
}