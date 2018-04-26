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
    class GameManager
    {
        public int score;
        public bool gameOver;

        public Player player;
        HeatBar heatBar;
        EnemyManager enemyManager;
        public static Camera camera;
        ParticleEngine particleEngine;

        public GameManager()
        {
            score = 0;
            gameOver = false;

            Viewport view = ContentManager.TransferGraphicsDevice().Viewport;
            camera = new Camera(view);
            player = new Player(new Vector2(800, 800), Constants.StandardSize);
            heatBar = new HeatBar(camera.GetPosition(), 1);
            enemyManager = new EnemyManager();
            Game1.penumbra.AmbientColor = Color.Black;
            particleEngine = new ParticleEngine();
        }

        public void Initialize()
        {
            player.lives = 3;
            enemyManager.enemyList.Clear();
            enemyManager.removeList.Clear();
            Game1.penumbra.Lights.Clear();
            Game1.penumbra.Hulls.Clear();
            Game1.penumbra.Lights.Add(player.viscinity);
            Game1.penumbra.Lights.Add(player.view);
            Game1.penumbra.Initialize();
        }

        public void Update()
        {
            particleEngine.Update(player.sprinting, player.position);
            player.Update();
            heatBar.Update();
            enemyManager.Update(player.position);
            CheckCollision();
            camera.SetPosition(player.position);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            //Ritar ut objekt som skall följa med kameran/skärmen
            Game1.penumbra.BeginDraw();
            Game1.penumbra.Transform = camera.GetTransform();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());
                spriteBatch.Draw(ContentManager.Get<Texture2D>("brickBackground"), Vector2.Zero, Color.White);
                particleEngine.Draw(spriteBatch);
                player.Draw(spriteBatch);
                enemyManager.Draw(spriteBatch);
            spriteBatch.End();

            Game1.penumbra.Draw(gameTime);

            //Måste ritas ut separat efter Penumbra för att synas genom skuggorna (Vår GUI, HUD)
            spriteBatch.Begin();
                heatBar.Draw(spriteBatch);
                spriteBatch.DrawString(ContentManager.Get<SpriteFont>("spriteFont"), "Score: " + score, new Vector2(50, 100), Color.White);
            spriteBatch.End();
        }

        //----------------------------------------------------------------------------------------------------

        void CheckCollision()
        {
            foreach (Enemy tempEnemy in enemyManager.enemyList)
            {
                foreach (Bullet tempBullet in player.bulletList)
                {
                    if (tempEnemy.destinationRectangle.Intersects(tempBullet.destinationRectangle))
                    {
                        bool dead = tempEnemy.TakeDamage();
                        player.removeList.Add(tempBullet);

                        if (dead)
                        {
                            enemyManager.removeList.Add(tempEnemy);
                            score += 100;
                        }
                    }
                }

                if (player.screenClear != null)
                {
                    if (tempEnemy.destinationRectangle.Intersects(player.screenClear.destinationRectangle))
                    {
                        enemyManager.removeList.Add(tempEnemy);
                        score += 100;
                    }
                }

                if (tempEnemy.destinationRectangle.Intersects(player.destinationRectangle))
                {
                    enemyManager.removeList.Add(tempEnemy);
                    if (player.lives >= 0)
                        player.TakeDamage();
                    else
                    {
                        gameOver = true;
                    }
                }
            }

            foreach (Shooter tempShooter in enemyManager.enemyList.OfType<Shooter>())
            {
                foreach (Bullet tempEnemyBullet in tempShooter.enemyBulletList)
                {
                    if (tempEnemyBullet.destinationRectangle.Intersects(player.destinationRectangle))
                    {
                        player.TakeDamage();
                        tempShooter.enemyRemoveList.Add(tempEnemyBullet);
                    }
                    if (player.screenClear != null)
                    {
                        if (tempEnemyBullet.destinationRectangle.Intersects(player.screenClear.destinationRectangle))
                        {
                            tempShooter.enemyRemoveList.Add(tempEnemyBullet);
                        }
                    }
                }
            }
        }
    }
}
