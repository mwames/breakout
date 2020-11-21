using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Paddle
    {
        public int speed;
        public Vector2 position;
        public int health;
        public Texture2D texture;

        public Paddle(Vector2 position, int speed, Texture2D texture) {
            this.position = position;
            this.speed = speed;
            this.texture = texture;
            this.health = 3;
        }

        private bool isInBoundLeft() {
            return position.X > 0;
        }

        private bool isInBoundRight() {
            return position.X + texture.Width < Breakout.Window.WIDTH;
        }

        public void update(GameTime gameTime, Controller gameController, KeyboardState kState)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (gameController.inGame)
            {
                if (kState.IsKeyDown(Keys.Left) && isInBoundLeft())
                {
                    position.X -= speed * dt;
                }
                if (kState.IsKeyDown(Keys.Right) && isInBoundRight())
                {
                    position.X += speed * dt;
                }
            }
        }

        public void update(GameTime gameTime, Controller gameController, GamePadState gState)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (gameController.inGame)
            {
                var leftPressed = gState.IsButtonDown(Buttons.LeftThumbstickLeft)
                    || gState.IsButtonDown(Buttons.X)
                    || gState.IsButtonDown(Buttons.DPadLeft);

                var rightPressed = gState.IsButtonDown(Buttons.LeftThumbstickRight)
                    || gState.IsButtonDown(Buttons.B)
                    || gState.IsButtonDown(Buttons.DPadRight);

                if (leftPressed && isInBoundLeft())
                {
                    position.X -= speed * dt;
                }
                if (rightPressed && isInBoundRight())
                {
                    position.X += speed * dt;
                }

            }
        }
    }
}
