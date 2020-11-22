using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Breakout
{
    public class Game1 : Game
    {
        private MoveFunc move = BallOps.move;
        private ReverseFunc reverse = BallOps.reverse;
        private GetInputFunc getInput = PlayerOps.getInput;
        private Ball ball;
        private Player player = new Player(PlayerIndex.One);
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SceneManager sceneManager;
        private TextureManager textureManager;
        private SpriteFont gameFont;
        private Controller gameController = new Controller();
        private Paddle paddle;
        private SoundEffect ballSound;
        private MainMenuScene mainMenuScene;

        public Game1()
        {
            // Set up window
            Window.AllowUserResizing = true;
            Window.Position = new Point(200, 200);
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = Breakout.Window.WIDTH;
            graphics.PreferredBackBufferHeight = Breakout.Window.HEIGHT;
            graphics.ApplyChanges();
            textureManager = new TextureManager();

            ball = new Ball(
                new Vector2(
                    graphics.PreferredBackBufferWidth / 2,
                    graphics.PreferredBackBufferHeight / 2
                ),
                new Vector2(200f, 200f),
                textureManager
            );

            paddle = new Paddle(
                new Vector2(
                    graphics.PreferredBackBufferWidth / 2 - 64,
                    graphics.PreferredBackBufferHeight - 92
                ),
                460,
                textureManager
            );

            // Set up Scenes
            sceneManager = new SceneManager();
            sceneManager.Add(SceneName.Menu, new MainMenuScene(textureManager, Window, sceneManager));
            sceneManager.Add(SceneName.Game, new GameScene(gameController, paddle, ball, Window, ballSound, textureManager, sceneManager));
            sceneManager.Add(SceneName.GameOver, new GameOverScene(Window));
            sceneManager.currentScene = sceneManager.Get(SceneName.Menu);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            textureManager.Add(TextureName.Ball, Content.Load<Texture2D>("ball"));
            textureManager.Add(TextureName.Paddle, Content.Load<Texture2D>("paddle"));
            textureManager.Add(TextureName.TitleScreen, Content.Load<Texture2D>("skull"));
            textureManager.Add(TextureName.Heart, Content.Load<Texture2D>("heart"));

            gameFont = Content.Load<SpriteFont>("gameFont");
            ballSound = Content.Load<SoundEffect>("ballSound");
        }

        public GamePadState GetGamePadState()
        {
            return GamePad.GetState(PlayerIndex.One);
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var gamePadState = getInput(player);
            if (gamePadState.IsButtonDown(Buttons.Back) || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            sceneManager.currentScene.Update(gamePadState, keyboardState, gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            sceneManager.currentScene.Draw(spriteBatch, gameFont, GraphicsDevice);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
