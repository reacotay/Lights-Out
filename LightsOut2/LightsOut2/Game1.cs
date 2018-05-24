using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;

namespace LightsOut2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private ContentManager contentManager;
        private GameManager gameManager;
        private MainMenu mainMenu;
        static public PenumbraComponent penumbra;

        enum GameState
        {
            MainMenu,
            MainGame,
            GamePause,
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
            Constants.LoadContent();

            graphics.PreferredBackBufferWidth = Constants.WindowWidth;
            graphics.PreferredBackBufferHeight = Constants.WindowHeight;
            graphics.IsFullScreen = true;
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
            Highscore.GetScore();
            currentState = GameState.MainMenu;
        }

        protected override void Update(GameTime gameTime)
        {
            Constants.UpdateKeyMouseReader();

            switch (currentState)
            {
                case GameState.MainMenu:
                    mainMenu.Update(gameTime);
                    if (mainMenu.newGameButton.CheckClicked() || Constants.gamePadState.IsButtonDown(Buttons.Start) && Constants.oldGamePadState.IsButtonUp(Buttons.Start))
                    {
                        gameManager.Initialize();
                        currentState = GameState.MainGame;
                    }
                    if (mainMenu.newQuitButton.CheckClicked() || Constants.gamePadState.IsButtonDown(Buttons.Back) && Constants.oldGamePadState.IsButtonUp(Buttons.Back))
                    {
                        Exit();
                    }
                    if (mainMenu.newHighscoreButton.CheckClicked() || Constants.gamePadState.IsButtonDown(Buttons.Y))
                    {
                        currentState = GameState.HighScore;
                    }
                        break;

                case GameState.MainGame:
                    gameManager.Update(gameTime);

                    if (gameManager.gameOver)
                        currentState = GameState.GameOver;

                    if (Constants.keyState.IsKeyDown(Keys.P) && Constants.oldKeyState.IsKeyUp(Keys.P) || Constants.gamePadState.IsButtonDown(Buttons.Start) && Constants.oldGamePadState.IsButtonUp(Buttons.Start))
                        currentState = GameState.GamePause;
                    break;

                case GameState.GamePause:
                    if (Constants.keyState.IsKeyDown(Keys.P) && Constants.oldKeyState.IsKeyUp(Keys.P) || Constants.gamePadState.IsButtonDown(Buttons.Start) && Constants.oldGamePadState.IsButtonUp(Buttons.Start))
                        currentState = GameState.MainGame;

                    if (Constants.keyState.IsKeyDown(Keys.Escape) && Constants.oldKeyState.IsKeyUp(Keys.Escape) || Constants.gamePadState.IsButtonDown(Buttons.Back) && Constants.oldGamePadState.IsButtonUp(Buttons.Back))
                    {
                        mainMenu.Initialize();
                        gameManager = new GameManager();
                        currentState = GameState.MainMenu;
                    }
                    break;

                case GameState.GameOver:
                    if (Highscore.CheckScore(gameManager.score))
                        currentState = GameState.NameEntry;
                    else
                        currentState = GameState.HighScore;
                    break;

                case GameState.HighScore:
                    if (Constants.keyState.IsKeyDown(Keys.Enter) && Constants.oldKeyState.IsKeyUp(Keys.Enter) || Constants.gamePadState.IsButtonDown(Buttons.Start))
                    {
                        mainMenu.Initialize();
                        gameManager = new GameManager();
                        currentState = GameState.MainMenu;
                    }
                    break;

                case GameState.NameEntry:
                    if (NameEntry.Entry())
                        currentState = GameState.HighScore;
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            switch (currentState)
            {
                case GameState.MainMenu:
                    mainMenu.Draw(spriteBatch, gameTime);
                    break;

                case GameState.MainGame:
                    gameManager.Draw(spriteBatch, gameTime);
                    break;

                case GameState.GamePause:
                    gameManager.Draw(spriteBatch, gameTime);
                    spriteBatch.Begin();
                        spriteBatch.DrawString(spriteFont, "GAME PAUSED", new Vector2(Constants.WindowWidth / 2, Constants.WindowHeight / 2), Color.White);
                    spriteBatch.End();
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
