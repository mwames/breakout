using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

    public class Ball : IGameObject
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

        public void Update(GameTime gameTime, InputState input)
        {
            speed.X = CalcX(heading);
            speed.Y = CalcY(heading);
            position.X += speed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y += speed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public int FlipY(int theta)
        {
            return (180 - theta) % 360;
        }

        public int FlipX(int theta)
        {
            return (-1 * theta) % 360;
        }

        public float CalcX(int theta)
        {
            return (float)(velocity * Math.Cos((Math.PI * theta) / 180));
        }

        public float CalcY(int theta)
        {
            return (float)(velocity * Math.Sin((Math.PI * theta) / 180));
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            spriteBatch.Draw(
                texture,
                new Rectangle((int)position.X, (int)position.Y, radius * 2, radius * 2),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White
            );

            if (Store.modes.Active(DebugOptions.ShowLocators))
            {
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

                // Bottom locator dot
                var bottom = new Vector2((int)Center.X, (int)Bottom);
                Dot.Draw(spriteBatch, bottom, texture);
            }
        }

        public void OnCollide(Side sideOfImpact, IRectangle box)
        {
            Random random = new Random();
            var variance = 0; // random.Next(-9, 10);
            velocity += random.Next(5, 11);

            if (sideOfImpact == Side.Top && speed.Y < 0)
            {
                position.Y = box.Bottom;
                this.heading = FlipX(this.heading + variance);
            }
            else if (sideOfImpact == Side.Right && speed.X > 0)
            {
                position.X = box.Left - radius * 2;
                this.heading = FlipY(this.heading + variance);
            }
            else if (sideOfImpact == Side.Bottom && speed.Y > 0)
            {
                position.Y = box.Top - radius * 2;
                this.heading = FlipX(this.heading + variance);
            }
            else if (sideOfImpact == Side.Left && speed.X < 0)
            {
                position.X = box.Right;
                this.heading = FlipY(this.heading + variance);
            }
        }
    }
}
