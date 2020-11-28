using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Breakout
{
    public static class Collision
    {
        public static bool DidCollide(Ball ball, IRectangle paddle)
        {
            float testX = ball.Center.X;
            float testY = ball.Center.Y;

            if (ball.Center.X < paddle.Left) testX = paddle.Left;
            else if (ball.Center.X > paddle.Right) testX = paddle.Right;
            if (ball.Center.Y < paddle.Top) testY = paddle.Top;
            else if (ball.Center.Y > paddle.Bottom) testY = paddle.Bottom;

            double distX = ball.Center.X - testX;
            double distY = ball.Center.Y - testY;
            double distance = Math.Sqrt((distX * distX) + (distY * distY));

            return distance <= ball.radius;
        }

        public static bool DidCollide(Ball ball1, Ball ball2)
        {
            return false;
        }

        public static bool DidCollide(Paddle paddle1, Paddle paddle2)
        {
            return false;
        }

        private static Dictionary<string, Side> MakeCollisionDict(Side ballSide, Side boxSide) {
            return new Dictionary<string, Side>() {
                ["ball"] = ballSide,
                ["box"] = boxSide
            };
        }
        private static Dictionary<string, Side> ResolveTop(Vector2 point, IRectangle box) {
            if (point.X >= box.Left && point.X <= box.Right) {
                return MakeCollisionDict(Side.Bottom, Side.Top);
            }
            if (point.X < box.Left) {
                var rPoint = new Vector2(box.Left, box.Top);
                var xDiff = Math.Abs(rPoint.X - point.X);
                var yDiff = Math.Abs(rPoint.Y - point.Y);
                return yDiff > xDiff
                    ? MakeCollisionDict(Side.Bottom, Side.Top)
                    : MakeCollisionDict(Side.Right, Side.Left);
            } else {
                var rPoint = new Vector2(box.Right, box.Top);
                var xDiff = Math.Abs(rPoint.X - point.X);
                var yDiff = Math.Abs(rPoint.Y - point.Y);
                 return yDiff > xDiff
                    ? MakeCollisionDict(Side.Bottom, Side.Top)
                    : MakeCollisionDict(Side.Left, Side.Right);
            }
        }
        private static Dictionary<string, Side> ResolveBottom(Vector2 point, IRectangle box) {
            if (point.X >= box.Left && point.X <= box.Right)
            {
                return MakeCollisionDict(Side.Top, Side.Bottom);
            }
            if (point.X < box.Left)
            {
                var rPoint = new Vector2(box.Left, box.Bottom);
                var xDiff = Math.Abs(rPoint.X - point.X);
                var yDiff = Math.Abs(rPoint.Y - point.Y);
                return yDiff > xDiff
                    ? MakeCollisionDict(Side.Top, Side.Bottom)
                    : MakeCollisionDict(Side.Right, Side.Left);
            }
            else
            {
                var rPoint = new Vector2(box.Right, box.Bottom);
                var xDiff = Math.Abs(rPoint.X - point.X);
                var yDiff = Math.Abs(rPoint.Y - point.Y);
                return yDiff > xDiff
                    ? MakeCollisionDict(Side.Top, Side.Bottom)
                    : MakeCollisionDict(Side.Left, Side.Right);
            }
        }
        private static Dictionary<string, Side> ResolveLeft(Vector2 point, IRectangle box) {
            if (point.Y >= box.Top && point.Y <= box.Bottom)
            {
                return MakeCollisionDict(Side.Right, Side.Left);
            }
            if (point.X < box.Top)
            {
                var rPoint = new Vector2(box.Left, box.Top);
                var xDiff = Math.Abs(rPoint.X - point.X);
                var yDiff = Math.Abs(rPoint.Y - point.Y);
                return yDiff > xDiff
                    ? MakeCollisionDict(Side.Bottom, Side.Top)
                    : MakeCollisionDict(Side.Right, Side.Left);
            }
            else
            {
                var rPoint = new Vector2(box.Left, box.Bottom);
                var xDiff = Math.Abs(rPoint.X - point.X);
                var yDiff = Math.Abs(rPoint.Y - point.Y);
                return yDiff > xDiff
                    ? MakeCollisionDict(Side.Top, Side.Bottom)
                    : MakeCollisionDict(Side.Right, Side.Left);
            }
        }
        private static Dictionary<string, Side> ResolveRight(Vector2 point, IRectangle box) {
            if (point.Y >= box.Top && point.Y <= box.Bottom)
            {
                return MakeCollisionDict(Side.Left, Side.Right);
            }
            if (point.X < box.Top)
            {
                var rPoint = new Vector2(box.Right, box.Top);
                var xDiff = Math.Abs(rPoint.X - point.X);
                var yDiff = Math.Abs(rPoint.Y - point.Y);
                return yDiff > xDiff
                    ? MakeCollisionDict(Side.Bottom, Side.Top)
                    : MakeCollisionDict(Side.Left, Side.Right);
            }
            else
            {
                var rPoint = new Vector2(box.Right, box.Bottom);
                var xDiff = Math.Abs(rPoint.X - point.X);
                var yDiff = Math.Abs(rPoint.Y - point.Y);
                return yDiff > xDiff
                    ? MakeCollisionDict(Side.Top, Side.Bottom)
                    : MakeCollisionDict(Side.Left, Side.Right);
            }
        }

        public static Dictionary<string, Side> SideHit(Ball ball, IRectangle box) {
            var point = ball.Center;
            if (point.Y <= box.Top) {
                return ResolveTop(point, box);
            }
            if (point.Y >= box.Bottom) {
                return ResolveBottom(point, box);
            }
            if (point.X <= box.Left) {
                return ResolveLeft(point, box);
            }
            if (point.X >= box.Right) {
                return ResolveRight(point, box);
            }

            return MakeCollisionDict(Side.Bottom, Side.Top);
        }
    }
}
