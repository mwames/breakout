using System;

namespace Breakout
{
    public static class Collision
    {
        public static bool DidCollide(Ball ball, IRectangle paddle)
        {
            var cx = ball.Center.X;
            var cy = ball.Center.Y;
            var rl = paddle.Left;
            var rr = paddle.Right;
            var rt = paddle.Top;
            var rb = paddle.Bottom;

            // temporary variables to set edges for testing
            float testX = cx;
            float testY = cy;

            // which edge is closest?
            if (cx < rl) testX = rl;      // test left edge
            else if (cx > rr) testX = rr;   // right edge
            if (cy < rt) testY = rt;      // top edge
            else if (cy > rb) testY = rb;   // bottom edge

            // get distance from closest edges
            double distX = cx - testX;
            double distY = cy - testY;
            double distance = Math.Sqrt((distX * distX) + (distY * distY));

            // if the distance is less than the radius, collision!
            if (distance <= ball.radius)
            {
                return true;
            }
            return false;
        }

        public static bool DidCollide(Ball ball1, Ball ball2)
        {
            return false;
        }

        public static bool DidCollide(Paddle paddle1, Paddle paddle2)
        {
            return false;
        }
    }
}
