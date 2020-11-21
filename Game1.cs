using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Breakout {
    public class Game1 : Game {
        private MoveFunc move = BallOps.move;
        private ReverseFunc reverse = BallOps.reverse;
        private Ball ball;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D titleScreen;
        SpriteFont gameFont;
        Texture2D paddleTexture;
        Texture2D heart;
        Controller gameController =  new Controller();
        Paddle paddle = new Paddle();

        public static class MySounds {
            public static SoundEffect ballSound;
        }
        
        public Game1() {
            // Set up window
            Window.AllowUserResizing = true;
            Window.Position = new Point(200, 200);
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            ball = new Ball(
                new Vector2(
                    graphics.PreferredBackBufferWidth / 2,
                    graphics.PreferredBackBufferHeight / 2
                ),
                new Vector2(200f, 200f),
                null
            );

            base.Initialize();

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
        }

        protected override void LoadContent() {
            // TODO: use this.Content to load your game content here
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameFont = Content.Load<SpriteFont>("gameFont");
            titleScreen = Content.Load<Texture2D>("skull");
            ball.texture = Content.Load<Texture2D>("ball");
            paddleTexture = Content.Load<Texture2D>("paddle");
            heart = Content.Load<Texture2D>("heart");

            MySounds.ballSound = Content.Load<SoundEffect>("ballSound");
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();
            var elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;

            gameController.conUpdate(gameTime);
            paddle.update(gameTime, gameController);

            if (gameController.inGame)
                ball = move(ball, elapsedTime);

            if (ball.Top <= 0 && gameController.gameOver == false) {
                MySounds.ballSound.Play();
                ball = reverse(ball, Heading.Vertical);
               // points += 5;
                
            }

            if ( ball.Bottom >= Window.ClientBounds.Height) {
                ball = reverse(ball, Heading.Vertical);
                paddle.health--;

                if (paddle.health <= 0)
                    gameController.gameOver = true;
            }

            if (ball.Left <= 0 && gameController.gameOver == false || ball.Right >= Window.ClientBounds.Width && gameController.gameOver == false) {
                MySounds.ballSound.Play();
                ball = reverse(ball, Heading.Horizontal);
            }
                
            //if (kstate.IsKeyDown(Keys.Up) && ball.Top > 0)
            //ball = move(ball, Direction.Up, elapsedTime);

            // if (kstate.IsKeyDown(Keys.Down) && ball.Bottom < Window.ClientBounds.Height)
            //     ball = move(ball, Direction.Down, elapsedTime);

            // if (kstate.IsKeyDown(Keys.Left) && ball.Left > 0)
            //     ball = move(ball, Direction.Left, elapsedTime);

            // if (kstate.IsKeyDown(Keys.Right) && ball.Right < Window.ClientBounds.Width)
            //     ball = move(ball, Direction.Right, elapsedTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (!gameController.inGame) {
                spriteBatch.Draw(titleScreen,new Rectangle(0,0,Window.ClientBounds.Width,Window.ClientBounds.Height), Color.White);
                string menuMessage = "Press enter to begin!";
                Vector2 sizeOFText = gameFont.MeasureString(menuMessage);
                spriteBatch.DrawString(gameFont, menuMessage, new Vector2(Window.ClientBounds.Width / 2 - sizeOFText.X / 2 , Window.ClientBounds.Height -50), Color.Black);
            } else {
                for (int i = 0; i < paddle.health; i++) {
                    spriteBatch.Draw(heart, new Vector2(i * 63, graphics.PreferredBackBufferHeight -63), Color.White);
                }

                if (gameController.gameOver == false) {
                    spriteBatch.Draw(ball.texture, ball.position, Color.White);
                    spriteBatch.Draw(paddleTexture, new Vector2(paddle.position.X - 34, Window.ClientBounds.Height - 35), Color.White);
                }
                spriteBatch.DrawString(gameFont, "Points: " + gameController.totalPoints.ToString(), new Vector2(3, 3), Color.Black);
            }


            if (gameController.gameOver) {
                string gameOverMessage = "You have died!";
                GraphicsDevice.Clear(Color.Yellow);
                spriteBatch.DrawString(gameFont, gameOverMessage, new Vector2(Window.ClientBounds.Width / 3 , Window.ClientBounds.Height /2), Color.Black);
            }
            
            spriteBatch.End();
         
            base.Draw(gameTime);
        }
    }
}
