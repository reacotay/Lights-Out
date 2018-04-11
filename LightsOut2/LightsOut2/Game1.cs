using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Penumbra;

namespace LightsOut2
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        ContentManager contentManager;
        GameManager gameManager;
        MainMenu mainMenu;

        static public PenumbraComponent penumbra;

        enum GameState
        {
            MainMenu,
            MainGame,
            GameOver,
            NameEntry,
            HighScore
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
            penumbra.AmbientColor = Color.Black;

            gameManager = new GameManager();
            mainMenu = new MainMenu();
            mainMenu.Initialize();

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
                    mainMenu.Update(gameTime);
                    if (Constants.keyState.IsKeyDown(Keys.Enter) && Constants.oldKeyState.IsKeyUp(Keys.Enter))
                    {
                        gameManager.Initialize();
                        currentState = GameState.MainGame;
                    }
                    break;

                case GameState.MainGame:
                    gameManager.Update();
                    if (gameManager.gameOver)
                        currentState = GameState.GameOver;
                    break;

                case GameState.GameOver:
                    if (Highscore.CheckScore(100))
                        currentState = GameState.NameEntry;
                    else
                        currentState = GameState.HighScore;
                    break;

                case GameState.HighScore:
                    if (Constants.keyState.IsKeyDown(Keys.Enter) && Constants.oldKeyState.IsKeyUp(Keys.Enter))
                    {
                        mainMenu.Initialize();
                        currentState = GameState.MainMenu;
                    }
                    break;

                case GameState.NameEntry:
                    NameEntry.Entry();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Vector2 worldMousePosition = Vector2.Transform(new Vector2(Constants.mouseState.Position.X, Constants.mouseState.Position.Y), Matrix.Invert(GameManager.camera.GetTransform()));
            //Vector2 worldMousePosition = new Vector2(Constants.mouseState.Position.X, Constants.mouseState.Position.Y);
            //Window.Title = "" + worldMousePosition;

            switch (currentState)
            {
                case GameState.MainMenu:
                    mainMenu.Draw(spriteBatch, gameTime);
                    break;

                case GameState.MainGame:
                    gameManager.Draw(spriteBatch, gameTime);
                    break;

                case GameState.GameOver:
                    spriteBatch.Begin();

                    spriteBatch.End();
                    break;

                case GameState.HighScore:
                    Highscore.Draw(spriteBatch);
                    break;

                case GameState.NameEntry:
                    NameEntry.Draw(spriteBatch);
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
