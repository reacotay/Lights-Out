using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Penumbra;

namespace Lights_Out
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        ContentManager contentManager;
        GameManager gameManager;

        static public PenumbraComponent penumbra;

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
            penumbra = new PenumbraComponent(this);

            gameManager = new GameManager();

            currentState = GameState.MainMenu;
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
                    if (Constants.keyState.IsKeyDown(Keys.Enter))
                    {
                        currentState = GameState.MainGame;
                    }
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
            GraphicsDevice.Clear(Color.Black);

            //Vector2 worldMousePosition = Vector2.Transform(new Vector2(Constants.mouseState.Position.X, Constants.mouseState.Position.Y), Matrix.Invert(GameManager.camera.GetTransform()));
            //Window.Title = "" + worldMousePosition;

            switch (currentState)
            {
                case GameState.MainMenu:
                    spriteBatch.Begin();
                    spriteBatch.DrawString(spriteFont, "Press Enter to start the game!", Vector2.Zero, Color.White);
                    spriteBatch.End();
                    break;

                case GameState.MainGame:
                    gameManager.Draw(spriteBatch, gameTime);
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
