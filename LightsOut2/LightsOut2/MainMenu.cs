using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;

namespace LightsOut2
{
    class MainMenu
    {
        Light lamp;
        Hull text;
        Camera camera;

        public MainMenu()
        {
            Viewport view = ContentManager.TransferGraphicsDevice().Viewport;
            camera = new Camera(view);
            lamp = new PointLight();
            lamp.Scale = new Vector2(600, 1500);
            lamp.Position = new Vector2(400,0);
            text = new Hull();
            Game1.penumbra.AmbientColor = Color.Black;
            //Game1.penumbra.Enabled = true;
            //Game1.penumbra.Visible = true;
            Game1.penumbra.Lights.RemoveAt(0);
            Game1.penumbra.Lights.RemoveAt(0);
            Game1.penumbra.Lights.Add(lamp);
            Game1.penumbra.Initialize();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            Game1.penumbra.BeginDraw();
            spriteBatch.Draw(ContentManager.Get<Texture2D>("Ground"), Vector2.Zero, Color.White);
            spriteBatch.DrawString(ContentManager.Get<SpriteFont>("titleFont"), "PROJECT: LIGHTS OUT", new Vector2(100,200), Color.Black, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            spriteBatch.End();
            Game1.penumbra.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.DrawString(ContentManager.Get<SpriteFont>("spriteFont"), "Press Enter to start the game!", Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }
}
