using System;
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
        public int velocity = 200;
        public int heading = 315;
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
            speed.X = CalcX(heading);
            speed.Y = CalcY(heading);
            position.X += speed.X * (float)gameTime;
            position.Y += speed.Y * (float)gameTime;
        }

        public int FlipY(int theta) {
            return (180 - theta) % 360;
        }

        public int FlipX(int theta) {
            return (-1 * theta) % 360;
        }

        public float CalcX(int theta) {
            return (float)(velocity * Math.Cos((Math.PI * theta) / 180));
        }

        public float CalcY(int theta) {
            return (float)(velocity * Math.Sin((Math.PI * theta) / 180));
        }

        public void reverse(Heading heading)
        {
            Random random = new Random();
            var variance = random.Next(-9, 10);
            velocity += 10;
            if (heading == Heading.Vertical)
                this.heading = FlipX(this.heading + variance);
            else 
                this.heading = FlipY(this.heading + variance);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont) {
            spriteBatch.DrawString(spriteFont, $"X Speed: {speed.X}", new Vector2(3, 30), Color.Black);
            spriteBatch.DrawString(spriteFont, $"Y Speed: {speed.Y}", new Vector2(3, 60), Color.Black);

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
