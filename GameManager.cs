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
        Player player;

        Camera camera;

        public GameManager()
        {
            player = new Player(new Vector2(100, 100));

            Viewport view = ContentManager.TransferGraphicsDevice().Viewport;
            camera = new Camera(view);
        }

        public void Update()
        {
            player.Update();

            camera.SetPosition(player.position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());

            spriteBatch.Draw(ContentManager.Get<Texture2D>("Player"), Vector2.Zero, Color.Red);
            player.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
