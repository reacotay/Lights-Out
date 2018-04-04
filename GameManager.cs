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
    class GameManager
    {
        public Player player;

        EnemyManager enemyManager;

        public static Camera camera;


        public GameManager()
        {
            player = new Player(new Vector2(800, 800));
            enemyManager = new EnemyManager();

            Viewport view = ContentManager.TransferGraphicsDevice().Viewport;
            camera = new Camera(view);

            Game1.penumbra.AmbientColor = Color.Black;
            //Game1.penumbra.Enabled = true;
            //Game1.penumbra.Visible = true;
            Game1.penumbra.Lights.Add(player.viscinity);
            Game1.penumbra.Lights.Add(player.view);
            Game1.penumbra.Initialize();
        }

        public void Update()
        {
            player.Update();
            enemyManager.Update(player.position);
            CheckCollision();

            camera.SetPosition(player.position);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Game1.penumbra.BeginDraw();
            Game1.penumbra.Transform = camera.GetTransform();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());

            spriteBatch.Draw(ContentManager.Get<Texture2D>("Ground"), Vector2.Zero, Color.White);

            player.Draw(spriteBatch);
            enemyManager.Draw(spriteBatch);

            spriteBatch.End();
            Game1.penumbra.Draw(gameTime);
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

                if (tempEnemy.destinationRectangle.Intersects(player.destinationRectangle))
                {
                    enemyManager.removeList.Add(tempEnemy);
                }
            }
        }
    }
}
