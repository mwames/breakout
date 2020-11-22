using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class GameScene : IScene
    {
        private readonly MoveFunc move = BallOps.move;
        private readonly ReverseFunc reverse = BallOps.reverse;
        private Controller gameController;
        private Paddle paddle;
        public Ball ball;
        private GameWindow Window;
        private SoundEffect ballSound;
        public GameScene(
            Controller gameController,
            Paddle paddle,
            Ball ball,
            GameWindow Window,
            SoundEffect ballSound
        )
        {
            this.gameController = gameController;
            this.paddle = paddle;
            this.ball = ball;
            this.Window = Window;
            this.ballSound = ballSound;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
            for (int i = 0; i < paddle.health; i++)
            {
                spriteBatch.Draw(Store.textures.Get(TextureName.Heart), new Vector2(i * 63, Breakout.Window.HEIGHT - 63), Color.White);
            }

            spriteBatch.Draw(
                ball.texture,
                new Rectangle((int)ball.position.X, (int)ball.position.Y, ball.radius * 2, ball.radius * 2),
                new Rectangle(0, 0, ball.texture.Width, ball.texture.Height),
                Color.White
            );
            spriteBatch.Draw(paddle.texture, new Vector2(paddle.position.X, paddle.position.Y), Color.White);
            spriteBatch.DrawString(spriteFont, "Points: " + gameController.totalPoints.ToString(), new Vector2(3, 3), Color.Black);
        }

        public void Update(GamePadState gamePadState, KeyboardState keyboardState, GameTime gameTime)
        {
            var elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;
            gameController.update();

            // conditional update based on input type
            if (gamePadState.IsConnected)
                paddle.update(gameTime, gameController, gamePadState);
            else
                paddle.update(gameTime, gameController, keyboardState);

            ball = move(ball, elapsedTime);


            if (ball.Top <= 0 && gameController.gameOver == false)
            {
                // ballSound.Play();
                ball = reverse(ball, Heading.Vertical);
            }

            if (ball.Bottom >= paddle.position.Y && ball.Left >= paddle.position.X && ball.Right <= paddle.position.X + paddle.texture.Width)
            {
                ball = reverse(ball, Heading.Vertical);
            }

            if (ball.Bottom >= Window.ClientBounds.Height)
            {
                ball = reverse(ball, Heading.Vertical);
                paddle.health--;

                if (paddle.health <= 0)
                    Store.scenes.currentScene = Store.scenes.Get(SceneName.GameOver);
            }

            if (ball.Left <= 0 && gameController.gameOver == false || ball.Right >= Window.ClientBounds.Width && gameController.gameOver == false)
            {
                // ballSound.Play();
                ball = reverse(ball, Heading.Horizontal);
            }
        }

        public override string ToString() {
            return "This is the GameScene: " + ball.position.X;
        }
    }
}
