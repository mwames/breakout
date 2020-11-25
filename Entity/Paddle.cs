using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Paddle : IGameObject, IRectangle
    {
        public int speed;
        public Vector2 position;
        public int health;
        public Texture2D texture => Store.textures.Get(TextureName.Paddle);

        public float Top => position.Y;
        public float Bottom => position.Y + texture.Height;
        public float Left => position.X;
        public float Right => position.X + texture.Width;
        public Vector2 Center => new Vector2(
            Left + (Right - Left) / 2,
            Top + (Bottom - Top) / 2
        );

        public Paddle(Vector2 position, int speed)
        {
            this.position = position;
            this.speed = speed;
            this.health = 3;
        }

        private bool isInBoundLeft()
        {
            return position.X > 0;
        }

        private bool isInBoundRight()
        {
            return position.X + texture.Width < global::Breakout.Window.WIDTH;
        }

        public void Update(GameTime gameTime, KeyboardState kState)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.Left) && isInBoundLeft())
            {
                position.X -= speed * dt;
            }
            if (kState.IsKeyDown(Keys.Right) && isInBoundRight())
            {
                position.X += speed * dt;
            }
        }

        public void Update(GameTime gameTime, GamePadState gState)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

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

        public void Draw(SpriteBatch spriteBatch, SpriteFont spritefont) {
            // Draws the ball
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), Color.White);

            if (ModeManager.currentMode == Mode.Debug) {
                // Center locater dot
                var center = new Vector2((int)Center.X, (int)Center.Y);
                Dot.Draw(spriteBatch, center, texture);

                // Left locator dot
                var left = new Vector2((int)Left, (int)Center.Y);
                Dot.Draw(spriteBatch, left, texture);

                // Right locator dot
                var right = new Vector2((int)Right, (int)Center.Y);
                Dot.Draw(spriteBatch, right, texture);

                // Top locator dot
                var top = new Vector2((int)Center.X, (int)Top);
                Dot.Draw(spriteBatch, top, texture);

                // Right locator dot
                var bottom = new Vector2((int)Center.X, (int)Bottom);
                Dot.Draw(spriteBatch, bottom, texture);
            }
        }

        public void OnCollide(Side sideOfImpact) {

        }
    }
}
