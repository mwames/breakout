using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout {
    public struct Gamepad {
        public Vector2 position;
        public Vector2 speed;
        public Texture2D texture;
        
        public float Top => position.Y;
        public float Bottom => position.Y + texture.Height;
        public float Left => position.X;
        public float Right => position.X + texture.Width;

        public Gamepad(Vector2 p, Vector2 s, Texture2D t) {
            position = p;
            speed = s;
            texture = t;
           
        }
    }

    // Delegate declarations.
    public delegate Ball MoveFunc(Ball ball, double gameTime);
    public delegate Ball ReverseFunc(Ball ball, Heading heading);
    
    public static class BallOps {
        public static Ball move(Ball ball, double gameTime) {
            return new Ball(
                new Vector2(
                    ball.position.X + ball.speed.X * (float)gameTime,
                    ball.position.Y + ball.speed.Y * (float)gameTime
                ),
                ball.speed,
                ball.texture
            );
        }

        public static Ball reverse(Ball ball, Heading heading) {
            return heading == Heading.Vertical
                ? new Ball(ball.position, new Vector2(ball.speed.X, ball.speed.Y * -1), ball.texture)
                : new Ball(ball.position, new Vector2(ball.speed.X * -1, ball.speed.Y), ball.texture);
        }
    }
}
