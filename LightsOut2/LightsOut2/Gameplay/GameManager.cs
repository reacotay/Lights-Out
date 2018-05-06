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

        Texture2D lavaBackground;
        Texture2D brickBackground;

        public Player player;
        HeatBar heatBar;
        Crosshair crosshair;
        EnemyManager enemyManager;
        public static Camera camera;
        ParticleEngine particleEngine;

        public GameManager()
        {
            score = 0;
            gameOver = false;

            lavaBackground = ContentManager.Get<Texture2D>("lavaBackground");
            brickBackground = ContentManager.Get<Texture2D>("brickSeamlessBackground");

            Viewport view = ContentManager.TransferGraphicsDevice().Viewport;
            camera = new Camera(view);
            player = new Player(new Vector2(800, 800), Constants.StandardSize);
            crosshair = new Crosshair(new Vector2(Constants.mouseState.X, Constants.mouseState.Y), 1);
            heatBar = new HeatBar(new Vector2(Constants.GameWindow.Width / 2 - 100, Constants.GameWindow.Height - 44), 1);
            enemyManager = new EnemyManager();
            Game1.penumbra.AmbientColor = Color.Black;
            particleEngine = new ParticleEngine();
        }

        public void Initialize()
        {
            player.extraLife = 3;
            enemyManager.enemyList.Clear();
            enemyManager.removeList.Clear();
            Game1.penumbra.Lights.Clear();
            Game1.penumbra.Hulls.Clear();
            Game1.penumbra.Lights.Add(player.viscinity);
            Game1.penumbra.Lights.Add(player.view);
            Game1.penumbra.Initialize();
            Sfx.Play.BGMStart();
        }

        public void Update()
        {
            particleEngine.Update();
            player.Update();
            CheckMoving();
            crosshair.Update();
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
                spriteBatch.Draw(lavaBackground, new Vector2(-800, -800), Color.White);
                spriteBatch.Draw(brickBackground, Vector2.Zero, Color.White);
                particleEngine.Draw(spriteBatch);
                player.Draw(spriteBatch);
                
                enemyManager.Draw(spriteBatch);
            spriteBatch.End();

            Game1.penumbra.Draw(gameTime);

            //Måste ritas ut separat efter Penumbra för att synas genom skuggorna (Vår GUI, HUD)
            spriteBatch.Begin();
                heatBar.Draw(spriteBatch);
                crosshair.Draw(spriteBatch);
                for (int i = 0; i < player.extraLife; i++)
                {
                    spriteBatch.Draw(ContentManager.Get<Texture2D>("playerTex"), new Vector2(10 + (30 * i), 60), Color.White);
                }
                spriteBatch.DrawString(ContentManager.Get<SpriteFont>("spriteFont"), "Score: " + score, new Vector2(10, 100), Color.White);
            spriteBatch.End();
        }

        //----------------------------------------------------------------------------------------------------

        public void CheckCollision()
        {
            foreach (Enemy tempEnemy in enemyManager.enemyList)
            {
                foreach (Bullet tempBullet in player.bulletList)
                {
                    if (tempEnemy.GetType() == typeof(Crawler))
                    {
                        bool dead = false;
                        Crawler tempCrawler = (Crawler)tempEnemy;
                        foreach(CrawlerPiece x in tempCrawler.BodyPieces)
                        {
                            if (x.piecehitpoints > 0)
                            {
                                if (x.hitbox.Intersects(tempBullet.hitbox))
                                {
                                    x.TakeDamage();
                                    dead = tempCrawler.TakeDamage();
                                    particleEngine.CreateBloodSplatter(tempEnemy.position, tempBullet.direction);
                                    player.removeList.Add(tempBullet);
                                }
                            }
                        }
                        if (dead)
                        {
                            enemyManager.removeList.Add(tempEnemy);
                            particleEngine.CreateBloodSplatter(tempEnemy.position, tempBullet.direction);
                            score += 100;
                        }
                    }
                    else
                    {
                        if (tempEnemy.hitbox.Intersects(tempBullet.hitbox))
                        {
                            bool dead = tempEnemy.TakeDamage();
                            Vector2 direction = tempEnemy.position - player.position;
                            direction.Normalize();
                            particleEngine.CreateBloodSplatter(tempEnemy.position, direction);
                            player.removeList.Add(tempBullet);

                            if (dead)
                            {
                                enemyManager.removeList.Add(tempEnemy);
                                particleEngine.CreateBloodSplatter(tempEnemy.position, tempBullet.direction);
                                score += 100;
                            }
                        }
                    }
                }

                if (player.screenClear != null)
                {
                    if (tempEnemy.hitbox.Intersects(player.screenClear.destinationRectangle))
                    {
                        enemyManager.removeList.Add(tempEnemy);
                        Vector2 direction = tempEnemy.position - player.position;
                        direction.Normalize();
                        particleEngine.CreateBloodSplatter(tempEnemy.position, direction);
                        score += 100;
                    }
                }

                if (tempEnemy.GetType() == typeof(Crawler))
                {
                    Crawler tempCrawler = (Crawler)tempEnemy;
                    foreach (CrawlerPiece x in tempCrawler.BodyPieces)
                    {
                        if (x.hitbox.Intersects(player.hitbox) || tempEnemy.hitbox.Intersects(player.hitbox))
                        {
                            enemyManager.removeList.Add(tempEnemy);
                            if (player.extraLife >= 0)
                                player.TakeDamage();
                            else
                            {
                                gameOver = true;
                            }
                        }
                    }
                }
                else if(tempEnemy.hitbox.Intersects(player.hitbox))
                {
                    enemyManager.removeList.Add(tempEnemy);
                    if (player.extraLife > 0)
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
                    if (tempEnemyBullet.hitbox.Intersects(player.hitbox))
                    {
                        player.TakeDamage();
                        tempShooter.enemyRemoveList.Add(tempEnemyBullet);
                    }
                    if (player.screenClear != null)
                    {
                        if (tempEnemyBullet.hitbox.Intersects(player.screenClear.destinationRectangle))
                        {
                            tempShooter.enemyRemoveList.Add(tempEnemyBullet);
                        }
                    }
                }
            }
        }

        private void CheckMoving()
        {
            if (player.moving)
            particleEngine.CreateRunParticle(player.position);
        }
    }
}
