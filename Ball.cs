using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace breakout
{
    public struct Ball
    {
        public Ball(Vector2 p, float s, Texture2D t) {
            position = p;
            speed = s;
            texture = t;
        }

        public Vector2 position;
        public float speed;
        public Texture2D texture;
    }
}
