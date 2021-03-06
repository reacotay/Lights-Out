﻿using System;
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
        private Lamp lamp;
        private Texture2D groundTex;
        private Texture2D crosshairTex;
        private SpriteFont titleFont;
        private SpriteFont spriteFont;
        public Button newGameButton;
        public Button newQuitButton;
        public Button newHighscoreButton;
        
        public MainMenu()
        {
            lamp = new Lamp();

            groundTex = ContentManager.Get<Texture2D>("Ground");
            crosshairTex = ContentManager.Get<Texture2D>("Crosshair");
            titleFont = ContentManager.Get<SpriteFont>("titleFont");
            spriteFont = ContentManager.Get<SpriteFont>("spriteFont");

            newGameButton = new Button(new Vector2(683, 500), Constants.StandardSize, "newGameTex");
            newQuitButton = new Button(new Vector2(712, 700), Constants.StandardSize, "quitGameTex");
            newHighscoreButton = new Button(new Vector2(690, 600), Constants.StandardSize, "highscoreButtonTex");
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
                spriteBatch.DrawString(titleFont, "PROJECT: LIGHTS OUT", new Vector2(500,200), Color.Black, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                spriteBatch.DrawString(titleFont, "PROJECT: LIGHTS OUT", new Vector2(503, 203), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            spriteBatch.End();
            Game1.penumbra.Draw(gameTime);

            spriteBatch.Begin();
            if (Constants.gamePadState.IsConnected)
            {
                spriteBatch.DrawString(spriteFont, "'START' TO START THE GAME", new Vector2(600, 600), Color.DimGray, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, "'Y' TO CHECK THE HIGHSCORES", new Vector2(600, 650), Color.DimGray, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, "'SELECT' TO QUIT THE GAME", new Vector2(600, 700), Color.DimGray, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            }
            else
            {
                newGameButton.Draw(spriteBatch);
                newHighscoreButton.Draw(spriteBatch);
                newQuitButton.Draw(spriteBatch);
            }
            if (!Constants.gamePadState.IsConnected)
                spriteBatch.Draw(crosshairTex, Constants.mouseState.Position.ToVector2(), Color.White);
            spriteBatch.Draw(groundTex, new Rectangle((int)lamp.bulb.Position.X, (int)lamp.bulb.Position.Y-20, 140, 35),new Rectangle(0,0,groundTex.Width,groundTex.Height), Color.Black, lamp.bulb.Rotation, new Vector2(800,0), SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
