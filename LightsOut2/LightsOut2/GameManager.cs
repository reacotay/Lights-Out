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
        public bool gameOver;

        public Player player;
        HeatBar heatBar;

        EnemyManager enemyManager;

        public static Camera camera;


        public GameManager()
        {
            gameOver = false;

            Viewport view = ContentManager.TransferGraphicsDevice().Viewport;
            camera = new Camera(view);

            player = new Player(new Vector2(800, 800), Constants.CellSize);
            heatBar = new HeatBar(camera.GetPosition());
            enemyManager = new EnemyManager();

            Game1.penumbra.AmbientColor = Color.Black;
        }

        public void Initialize()
        {
            Game1.penumbra.Lights.Clear();
            Game1.penumbra.Hulls.Clear();
            Game1.penumbra.Lights.Add(player.viscinity);
            Game1.penumbra.Lights.Add(player.view);
            Game1.penumbra.Initialize();
        }

        public void Update()
        {
            player.Update();
            heatBar.Update();
            enemyManager.Update(player.position);
            CheckCollision();

            camera.SetPosition(player.position);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            // Ritar ut objekt som skall följa med kameran/skärmen.

            Game1.penumbra.BeginDraw();
            Game1.penumbra.Transform = camera.GetTransform();


            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());

            spriteBatch.Draw(ContentManager.Get<Texture2D>("Ground"), Vector2.Zero, Color.White);

            player.Draw(spriteBatch);
            enemyManager.Draw(spriteBatch);

            spriteBatch.End();

            Game1.penumbra.Draw(gameTime);

            spriteBatch.Begin();
            heatBar.Draw(spriteBatch);
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
                        enemyManager.removeList.Add(tempEnemy);
                        player.removeList.Add(tempBullet);
                    }
                }

                if (player.screenClear != null)
                {
                    if (tempEnemy.destinationRectangle.Intersects(player.screenClear.destinationRectangle))
                    {
                        enemyManager.removeList.Add(tempEnemy);
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
        }
    }
}
