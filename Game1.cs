using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Breakout {
    public class Game1 : Game {
        private MoveFunc move = BallOps.move;
        private ReverseFunc reverse = BallOps.reverse;
        private GetInputFunc getInput = PlayerOps.getInput;
        private Ball ball;
        private Player player = new Player(PlayerIndex.One);
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D titleScreen;
        SpriteFont gameFont;
        Texture2D heart;
        Controller gameController =  new Controller();
        Paddle paddle;

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
            graphics.PreferredBackBufferWidth = Breakout.Window.WIDTH;
            graphics.PreferredBackBufferHeight = Breakout.Window.HEIGHT;
            graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            ball = new Ball(
                new Vector2(
                    graphics.PreferredBackBufferWidth / 2,
                    graphics.PreferredBackBufferHeight / 2
                ),
                new Vector2(200f, 200f),
                null
            );

            paddle = new Paddle(
                new Vector2(
                    graphics.PreferredBackBufferWidth / 2 - 64,
                    graphics.PreferredBackBufferHeight - 92
                ),
                460,
                null
            );

            base.Initialize();
        }

        protected override void LoadContent() {
            // TODO: use this.Content to load your game content here
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameFont = Content.Load<SpriteFont>("gameFont");
            titleScreen = Content.Load<Texture2D>("skull");

            ball.texture = Content.Load<Texture2D>("ball");
            paddle.texture = Content.Load<Texture2D>("paddle");
            heart = Content.Load<Texture2D>("heart");

            MySounds.ballSound = Content.Load<SoundEffect>("ballSound");
        }

        public GamePadState GetGamePadState() {
            return GamePad.GetState(PlayerIndex.One);
        }

        protected override void Update(GameTime gameTime) {
            var keyboardState = Keyboard.GetState();
            var gamePadState = getInput(player);

            if (gamePadState.IsButtonDown(Buttons.Back) || keyboardState.IsKeyDown(Keys.Escape))
                Exit();
            
            var elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;
            gameController.update();

            // conditional update based on input type
            if (gamePadState.IsConnected)
                paddle.update(gameTime, gameController, gamePadState);
            else
                paddle.update(gameTime, gameController, keyboardState);

            if (gameController.inGame) {
                ball = move(ball, elapsedTime);
            }

            if (ball.Top <= 0 && gameController.gameOver == false) {
                MySounds.ballSound.Play();
                ball = reverse(ball, Heading.Vertical);                
            }

            if (ball.Bottom >= paddle.position.Y && ball.Left >= paddle.position.X && ball.Right <= paddle.position.X + paddle.texture.Width) {
                ball = reverse(ball, Heading.Vertical);
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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (!gameController.inGame) {
                spriteBatch.Draw(titleScreen, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
                string menuMessage = "Press enter to begin!";
                Vector2 sizeOFText = gameFont.MeasureString(menuMessage);
                spriteBatch.DrawString(gameFont, menuMessage, new Vector2(Window.ClientBounds.Width / 2 - sizeOFText.X / 2 , Window.ClientBounds.Height -50), Color.Black);
            } else {
                for (int i = 0; i < paddle.health; i++) {
                    spriteBatch.Draw(heart, new Vector2(i * 63, graphics.PreferredBackBufferHeight -63), Color.White);
                }

                if (gameController.gameOver == false) {
                    spriteBatch.Draw(
                        ball.texture,
                        new Rectangle((int)ball.position.X, (int)ball.position.Y, ball.radius * 2, ball.radius * 2),
                        new Rectangle(0, 0, ball.texture.Width, ball.texture.Height),
                        Color.White
                    );
                    spriteBatch.Draw(paddle.texture, new Vector2(paddle.position.X, paddle.position.Y), Color.White);
                }
                spriteBatch.DrawString(gameFont, "Points: " + gameController.totalPoints.ToString(), new Vector2(3, 3), Color.Black);
                spriteBatch.DrawString(gameFont, "Ball Bottom: " + ball.Bottom.ToString(), new Vector2(3, 30), Color.Black);
                spriteBatch.DrawString(gameFont, "Paddle Top: " + paddle.position.Y.ToString(), new Vector2(3, 60), Color.Black);
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
