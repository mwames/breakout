using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Breakout
{
    public class Breakout : Game
    {
        private Ball ball;
        private Player player = new Player(PlayerIndex.One);
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont gameFont;
        private Paddle paddle;
        
        
        private KeyboardState previousKeyboardState;
        private KeyboardState keyboardState;
        private GamePadState gamePadState;
        private GamePadState previousGamePadState;

        public static class MySounds
        {
            public static SoundEffect ballSound;
            public static Song gameOver;
        }

        public Breakout()
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
            graphics.PreferredBackBufferWidth = global::Breakout.Window.WIDTH;
            graphics.PreferredBackBufferHeight = global::Breakout.Window.HEIGHT;
            graphics.ApplyChanges();
            Store.textures = new TextureManager();

            ball = new Ball(
                new Vector2(
                    graphics.PreferredBackBufferWidth / 2,
                    graphics.PreferredBackBufferHeight / 2
                ),
                new Vector2(200f, 200f)
            );

            paddle = new Paddle(
                new Vector2(
                    graphics.PreferredBackBufferWidth / 2 - 64,
                    graphics.PreferredBackBufferHeight - 92
                ),
                460
            );
           
            // Set up Scenes
            Store.scenes = new SceneManager();
            Store.scenes.Add(SceneName.Menu, new MainMenuScene(Window));
            Store.scenes.Add(SceneName.Game, new GameScene(paddle, ball, Window, MySounds.ballSound));
            Store.scenes.Add(SceneName.GameOver, new GameOverScene(Window));
            Store.scenes.Add(SceneName.Pause, new PauseScene(Window));
            Store.scenes.currentScene = Store.scenes.Get(SceneName.Menu);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Store.textures.Add(TextureName.Ball, Content.Load<Texture2D>("ball"));
            Store.textures.Add(TextureName.Paddle, Content.Load<Texture2D>("paddle"));
            Store.textures.Add(TextureName.TitleScreen, Content.Load<Texture2D>("skull"));
            Store.textures.Add(TextureName.Heart, Content.Load<Texture2D>("heart"));
            Store.textures.Add(TextureName.RedBlock, Content.Load<Texture2D>("RedBlock"));
            Store.textures.Add(TextureName.GoldBlock, Content.Load<Texture2D>("GoldBlock"));
            Store.textures.Add(TextureName.BlueBlock, Content.Load<Texture2D>("BlueBlock"));
            Store.textures.Add(TextureName.GreenBlock, Content.Load<Texture2D>("GreenBlock"));
            gameFont = Content.Load<SpriteFont>("gameFont");
            MySounds.ballSound = Content.Load<SoundEffect>("ballSound");
            MySounds.gameOver = Content.Load<Song>("gameOver");
            MediaPlayer.Play(MySounds.gameOver);
            MediaPlayer.IsRepeating = true;
            
        }

        protected override void Update(GameTime gameTime)
        {
            previousKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            previousGamePadState = gamePadState;
            gamePadState = player.getInput();

            if (gamePadState.IsButtonDown(Buttons.Back) || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            if (keyboardState.IsKeyDown(Keys.F10) && !(previousKeyboardState.IsKeyDown(Keys.F10)))
                ModeManager.Toggle(Mode.Debug);

            Store.scenes.currentScene.Update(
                gamePadState,
                previousGamePadState,
                keyboardState,
                previousKeyboardState,
                gameTime
                );

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Store.scenes.currentScene.Draw(spriteBatch, gameFont, GraphicsDevice);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
