using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace breakout
{
    // Delegate declarations.
    public delegate Ball MoveFunc(Ball ball, Direction direction, double gameTime);
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public struct Ball
    {
        public Vector2 position;
        public float speed;
        public Texture2D texture;

        public Ball(Vector2 p, float s, Texture2D t)
        {
            position = p;
            speed = s;
            texture = t;
        }
    }

    public static class BallOps
    {
        public static Ball move(Ball ball, Direction direction, double gameTime)
        {
            if (direction == Direction.Up)
                return new Ball(
                    new Vector2(
                        ball.position.X,
                        ball.position.Y -= ball.speed * (float)gameTime
                    ),
                    ball.speed,
                    ball.texture
                );

            if (direction == Direction.Down)
                return new Ball(
                    new Vector2(
                        ball.position.X,
                        ball.position.Y += ball.speed * (float)gameTime
                    ),
                    ball.speed,
                    ball.texture
                );

            if (direction == Direction.Left)
                return new Ball(
                    new Vector2(
                        ball.position.X -= ball.speed * (float)gameTime,
                        ball.position.Y
                    ),
                    ball.speed,
                    ball.texture
                );
            

            if (direction == Direction.Right)
                return new Ball(
                    new Vector2(
                        ball.position.X += ball.speed * (float)gameTime,
                        ball.position.Y
                    ),
                    ball.speed,
                    ball.texture
                );
            ;

            return new Ball();
        }
    }
}
