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
            this.health = 1;
        }

        public bool isInBoundLeft()
        {
            return position.X > 0;
        }

        public bool isInBoundRight()
        {
            return position.X + texture.Width < GameWindow.WIDTH;
        }

        public void Update(GameTime gameTime, InputState input)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var leftPressed = input.IsPressed(Buttons.LeftThumbstickLeft)
                || input.IsPressed(Buttons.X)
                || input.IsPressed(Buttons.DPadLeft)
                || input.IsPressed(Buttons.LeftShoulder)
                || input.IsPressed(Keys.Left);

            var rightPressed = input.IsPressed(Buttons.LeftThumbstickRight)
                || input.IsPressed(Buttons.B)
                || input.IsPressed(Buttons.DPadRight)
                || input.IsPressed(Buttons.RightShoulder)
                || input.IsPressed(Keys.Right);

            if (leftPressed && isInBoundLeft())
            {
                position.X -= speed * dt;
            }
            if (rightPressed && isInBoundRight())
            {
                position.X += speed * dt;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spritefont)
        {
            // Draws the ball
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), Color.White);

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

                // Right locator dot
                var bottom = new Vector2((int)Center.X, (int)Bottom);
                Dot.Draw(spriteBatch, bottom, texture);
            }
        }

        public Side CollidedOn(Vector2 point)
        {
            if (point.Y < Top)
            {
                if (point.X >= Left && point.X <= Right)
                {
                    return Side.Top;
                }
                else if (point.X < Left && point.Y < Top)
                {
                    return Side.TopLeft;
                }
                else
                {
                    return Side.TopRight;
                }
            }
            else if (point.Y > Bottom)
            {
                if (point.X >= Left && point.X <= Right)
                {
                    return Side.Bottom;
                }
                else if (point.X < Left && point.Y > Bottom)
                {
                    return Side.BottomLeft;
                }
                else
                {
                    return Side.BottomRight;
                }
            }
            else if (point.X < Left)
            {
                if (point.Y >= Top && point.Y <= Bottom)
                {
                    return Side.Left;
                }
                else if (point.Y < Top)
                {
                    return Side.TopLeft;
                }
                else
                {
                    return Side.BottomLeft;
                }
            }
            else
            {
                if (point.Y >= Top && point.Y <= Bottom)
                {
                    return Side.Right;
                }
                else if (point.Y < Top)
                {
                    return Side.TopRight;
                }
                else
                {
                    return Side.BottomRight;
                }
            }
        }

        public void OnCollide(Side sideOfImpact)
        {

        }
    }
}
