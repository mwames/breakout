using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Breakout
{
    public class Breakout : Game
    {
        public string env;
        public int envScene;
        public int envMode;

        public Ball ball;
        public Player player = new Player(PlayerIndex.One);
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public SpriteFont gameFont;
        public Paddle paddle;
        public bool isFinished = false;

        public Breakout()
        {
            // Set up window
            Window.AllowUserResizing = true;
            Window.Position = new Point(200, 200);
            Window.Title = "Depression Ball";
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public Ball GetBall() {
            return new Ball(
                new Vector2(
                    graphics.PreferredBackBufferWidth / 2,
                    graphics.PreferredBackBufferHeight / 2
                ),
                new Vector2(200f, 200f)
            );
        }

        public Paddle GetPaddle() {
            return new Paddle(
                new Vector2(
                    graphics.PreferredBackBufferWidth / 2 - 64,
                    graphics.PreferredBackBufferHeight - 92
                ),
                460
            );
        }

        protected override void Initialize()
        {
            Store.scenes = new SceneManager();
            Store.textures = new TextureManager();
            Store.soundEffects = new SoundEffectManager();
            Store.songs = new SongManager();
            Store.modes = new ModeManager();
            Store.lives = 3;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = GameWindow.WIDTH;
            graphics.PreferredBackBufferHeight = GameWindow.HEIGHT;
            graphics.ApplyChanges();

            ball = GetBall();
            paddle = GetPaddle();

            // Set up Scenes
            Store.scenes.Add(SceneName.Menu, new MainMenuScene());
            Store.scenes.Add(SceneName.Game, new GameScene(paddle, ball));
            Store.scenes.Add(SceneName.GameOver, new GameOverScene());
            Store.scenes.Add(SceneName.Pause, new PauseScene());
            Store.scenes.Add(SceneName.Editor, new EditorScene());
            Store.scenes.Add(SceneName.Death, new DeathScene());
            Store.scenes.Add(SceneName.Options, new OptionsScene());
            Store.scenes.ChangeScene((SceneName)envScene);

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
            Store.textures.Add(TextureName.PlayButton, Content.Load<Texture2D>("PlayButton"));
            Store.textures.Add(TextureName.OptionsButton, Content.Load<Texture2D>("OptionsButton"));
            Store.soundEffects.Add(SoundEffectName.BallSound, Content.Load<SoundEffect>("ballSound"));
            Store.soundEffects.Add(SoundEffectName.DeathSound, Content.Load<SoundEffect>("deathSound"));
            Store.soundEffects.Add(SoundEffectName.BlockSound, Content.Load<SoundEffect>("blockBust"));
            Store.songs.Add(SongName.GameOver, Content.Load<Song>("gameOver")); // Gymnopédie No.1 Erik Satie. Remixed by KhanYash
            Store.songs.Add(SongName.MainGame, Content.Load<Song>("onlyUp")); // Only Up By Joel Corelitz- MusicForNorthing.Com
            Store.songs.Add(SongName.Title, Content.Load<Song>("FinallyFoundYou"));// Finally Found You By Joel Corelitz- MusicForNorthing.Com
            gameFont = Content.Load<SpriteFont>("gameFont");
        }
        
        protected override void Update(GameTime gameTime)
        {
            var input = player.getInput();
            if (input.WasPressed(Buttons.Back) || input.WasPressed(Keys.Escape))
                Exit();

            if (!isFinished)
            {
                // F1 toggles between game and editor
                if (input.WasPressed(Keys.F1))
                {
                    if (Store.scenes.sceneName == SceneName.Editor)
                        Store.scenes.ChangeScene(SceneName.Game);
                    else
                        Store.scenes.ChangeScene(SceneName.Editor);
                }

                // F5 toggle debug mode
                if (input.WasPressed(Keys.F5))
                    Store.modes.Toggle(Mode.Debug);

                //F9 toggle locator dot mode
                if (input.WasPressed(Keys.F9))
                    Store.modes.ToggleDebugOption(DebugOptions.ShowLocators);

                // F10 toggle frame advance mode
                if (input.WasPressed(Keys.F10))
                    Store.modes.ToggleDebugOption(DebugOptions.FrameAdvance);
            }

                // F11 advances frames
                if ((!Store.modes.Active(DebugOptions.FrameAdvance)) || (Store.modes.Active(DebugOptions.FrameAdvance) && input.WasPressed(Keys.F12)))
                {
                    Store.scenes.Scene.Update(input, gameTime);
                }

                if (Store.scenes.sceneName == SceneName.Editor)
                {
                    graphics.PreferredBackBufferWidth = 1024;
                    graphics.ApplyChanges();
                }
                else
                {
                    graphics.PreferredBackBufferWidth = GameWindow.WIDTH;
                    graphics.ApplyChanges();
                }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Store.scenes.Scene.Draw(spriteBatch, gameFont, GraphicsDevice);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
