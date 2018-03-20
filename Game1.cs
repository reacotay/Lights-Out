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
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        ContentManager contentManager;
        GameManager gameManager;

        enum GameState
        {
            MainMenu,
            MainGame,
            GameOver
        }
        GameState currentState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            Constants.LoadContent();

            graphics.PreferredBackBufferWidth = Constants.WindowWidth;
            graphics.PreferredBackBufferHeight = Constants.WindowHeight;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("spriteFont");
            contentManager = new ContentManager(this);

            gameManager = new GameManager();
            currentState = GameState.MainGame;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Constants.UpdateKeyMouseReader();

            switch (currentState)
            {
                case GameState.MainMenu:
                    break;

                case GameState.MainGame:
                    gameManager.Update();
                    break;

                case GameState.GameOver:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            switch (currentState)
            {
                case GameState.MainMenu:
                    spriteBatch.Begin();

                    spriteBatch.End();
                    break;

                case GameState.MainGame:
                    gameManager.Draw(spriteBatch);
                    break;

                case GameState.GameOver:
                    spriteBatch.Begin();

                    spriteBatch.End();
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
