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
        Lamp lamp;
        Texture2D groundTex;
        Texture2D crosshairTex;
        SpriteFont titleFont;
        SpriteFont spriteFont;
        public Button newGameButton;
        public Button newQuitButton;
        

        public MainMenu()
        {
            lamp = new Lamp();
            //Game1.penumbra.Enabled = true;
            //Game1.penumbra.Visible = true;
            groundTex = ContentManager.Get<Texture2D>("Ground");
            crosshairTex = ContentManager.Get<Texture2D>("Crosshair");
            titleFont = ContentManager.Get<SpriteFont>("titleFont");
            spriteFont = ContentManager.Get<SpriteFont>("spriteFont");
            newGameButton = new Button(new Vector2(283, 500), Constants.StandardSize, "newGameTex");
            newQuitButton = new Button(new Vector2(312, 600), Constants.StandardSize, "quitGameTex");
        }

        public void Initialize()
        {
            Matrix screen = Matrix.CreateTranslation(0, 0, 0);
            Game1.penumbra.Transform = screen;
            Game1.penumbra.Lights.Clear();
            Game1.penumbra.Lights.Add(lamp.bulb);
            Game1.penumbra.Hulls.Add(lamp.shade);
            Game1.penumbra.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            lamp.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            Game1.penumbra.BeginDraw();
                spriteBatch.Draw(groundTex, Vector2.Zero, Color.White);
                spriteBatch.DrawString(titleFont, "PROJECT: LIGHTS OUT", new Vector2(100,200), Color.Black, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                spriteBatch.DrawString(titleFont, "PROJECT: LIGHTS OUT", new Vector2(103, 203), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            
            spriteBatch.End();
            Game1.penumbra.Draw(gameTime);

            spriteBatch.Begin();
            newGameButton.Draw(spriteBatch);
            newQuitButton.Draw(spriteBatch);
            if (!Constants.gamePadState.IsConnected)
                spriteBatch.Draw(crosshairTex, Constants.mouseState.Position.ToVector2(), Color.White);

            spriteBatch.Draw(groundTex, new Rectangle((int)lamp.bulb.Position.X, (int)lamp.bulb.Position.Y-20, 140, 35),new Rectangle(0,0,groundTex.Width,groundTex.Height), Color.Black, lamp.bulb.Rotation, new Vector2(800,0), SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
