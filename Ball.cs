using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
    public enum Heading
    {
        Vertical,
        Horizontal
    }

    public class Ball
    {
        public Vector2 position;
        public Vector2 speed;
        public int radius;

        public Texture2D texture => Store.textures.Get(TextureName.Ball);
        public float Top => position.Y;
        public float Bottom => position.Y + radius * 2;
        public float Left => position.X;
        public float Right => position.X + radius * 2;
        public Vector2 Center => new Vector2(position.X + radius, position.Y + radius);

        public Ball(Vector2 p, Vector2 s)
        {
            position = p;
            speed = s;
            radius = 10;
        }

        public void move(double gameTime)
        {
            position.X += speed.X * (float)gameTime;
            position.Y += speed.Y * (float)gameTime;
        }

        public void reverse(Heading heading)
        {
            if (heading == Heading.Vertical)
                speed.Y *= -1;
            else 
                speed.X *= -1;
        }

        public void Draw(SpriteBatch spriteBatch) {
            // Draws the ball
            spriteBatch.Draw(
                texture,
                new Rectangle((int)position.X, (int)position.Y, radius * 2, radius * 2),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White
            );

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
    }
}
