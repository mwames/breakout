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

    public struct Ball
    {
        public Vector2 position;
        public Vector2 speed;
        public int radius;

        public Texture2D texture => Store.textures.Get(TextureName.Ball);
        public float Top => position.Y;
        public float Bottom => position.Y + radius * 2;
        public float Left => position.X;
        public float Right => position.X + radius * 2;

        public Ball(Vector2 p, Vector2 s)
        {
            position = p;
            speed = s;
            radius = 10;
        }
    }

    // Delegate declarations.
    public delegate Ball MoveFunc(Ball ball, double gameTime);
    public delegate Ball ReverseFunc(Ball ball, Heading heading);

    public static class BallOps
    {
        public static Ball move(Ball ball, double gameTime)
        {
            return new Ball(
                new Vector2(
                    ball.position.X + ball.speed.X * (float)gameTime,
                    ball.position.Y + ball.speed.Y * (float)gameTime
                ),
                ball.speed
            );
        }

        public static Ball reverse(Ball ball, Heading heading)
        {
            return heading == Heading.Vertical
                ? new Ball(ball.position, new Vector2(ball.speed.X, ball.speed.Y * -1))
                : new Ball(ball.position, new Vector2(ball.speed.X * -1, ball.speed.Y));
        }
    }
}
