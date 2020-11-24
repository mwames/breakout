using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class GameScene : IScene
    {
        private Paddle paddle;
        public Ball ball;
        private GameWindow Window;
        private SoundEffect ballSound;
        private Block red;
        public GameScene(
            Paddle paddle,
            Ball ball,
            GameWindow Window,
            SoundEffect ballSound
            
        )
        {
            this.paddle = paddle;
            this.ball = ball;
            this.Window = Window;
            this.ballSound = ballSound;
            this.red = new Block();
        }

        public void Update(GamePadState gamePadState, GamePadState previousGamePadState, KeyboardState keyboardState, KeyboardState previousKeyboardState, GameTime gameTime)
        {
            if ((!previousGamePadState.IsButtonDown(Buttons.Start) && gamePadState.IsButtonDown(Buttons.Start)) || (!previousKeyboardState.IsKeyDown(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))) {
                Store.scenes.currentScene = Store.scenes.Get(SceneName.Pause);
            }

            if (gamePadState.IsConnected)
                paddle.update(gameTime, gamePadState);
            else
                paddle.update(gameTime, keyboardState);

            ball.move(gameTime.ElapsedGameTime.TotalSeconds);

            if (Collision.DidCollide(ball, paddle))
            {
                // Top was hit
                if (ball.Bottom >= paddle.Top && ball.Top < paddle.Top)
                {
                    ball.reverse(Heading.Vertical);
                    ball.position.Y = paddle.Top - ball.radius * 2;
                }

                // Left was hit
                if (ball.Right >= paddle.Left && ball.Left < paddle.Left)
                {
                    ball.reverse(Heading.Horizontal);
                    ball.position.X = paddle.Left - ball.radius * 2;
                }

                // Right was hit
                if (ball.Left <= paddle.Right && ball.Right > paddle.Right)
                {
                    ball.reverse(Heading.Horizontal);
                    ball.position.X = paddle.Right;
                }
            }


            if (ball.Top <= 0)
            {
                ball.reverse(Heading.Vertical);
                ball.position.Y = 0;
                // ballSound.Play();
            }

            if (ball.Left <= 0)
            {
                ball.reverse(Heading.Horizontal);
                ball.position.X = 0;
                // ballSound.Play();
            }

            if (ball.Right >= Window.ClientBounds.Width)
            {
                ball.reverse(Heading.Horizontal);
                ball.position.X = Window.ClientBounds.Width - ball.radius * 2;
                // ballSound.Play();
            }

            if (ball.Bottom >= Window.ClientBounds.Height)
            {
                ball.reverse(Heading.Vertical);
                ball.position.Y = Window.ClientBounds.Height - ball.radius * 2;
                paddle.health--;

                if (paddle.health <= 0)
                    Store.scenes.currentScene = Store.scenes.Get(SceneName.GameOver);
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
            for (int i = 0; i < paddle.health; i++)
            {
                spriteBatch.Draw(Store.textures.Get(TextureName.Heart), new Vector2(i * 63, global::Breakout.Window.HEIGHT - 63), Color.White);
            }

            ball.Draw(spriteBatch, spriteFont);
            paddle.Draw(spriteBatch);
            spriteBatch.DrawString(spriteFont, "Points: ", new Vector2(3, 3), Color.Black);
            red.Draw(spriteBatch);
        }

        public override string ToString()
        {
            return "This is the GameScene: " + ball.position.X;
        }
    }
}
