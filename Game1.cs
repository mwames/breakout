using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace breakout
{
    public class Game1 : Game
    {
        private MoveFunc move = BallOps.move;
        private ReverseFunc reverse = BallOps.reverse;
        private Ball ball;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

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

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ball.texture = Content.Load<Texture2D>("ball");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();
            var elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;

            if (ball.Top <= 0 || ball.Bottom >= Window.ClientBounds.Height)
                ball = reverse(ball, Heading.Vertical);

            if (ball.Left <= 0 || ball.Right >= Window.ClientBounds.Width)
                ball = reverse(ball, Heading.Horizontal);

            ball = move(ball, elapsedTime);

            // if (kstate.IsKeyDown(Keys.Up) && ball.Top > 0)
            //     ball = move(ball, Direction.Up, elapsedTime);

            // if (kstate.IsKeyDown(Keys.Down) && ball.Bottom < Window.ClientBounds.Height)
            //     ball = move(ball, Direction.Down, elapsedTime);

            // if (kstate.IsKeyDown(Keys.Left) && ball.Left > 0)
            //     ball = move(ball, Direction.Left, elapsedTime);

            // if (kstate.IsKeyDown(Keys.Right) && ball.Right < Window.ClientBounds.Width)
            //     ball = move(ball, Direction.Right, elapsedTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(ball.texture, ball.position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
