using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Block : IGameObject, IRectangle
    {
        public static readonly int LEFT_OFFSET = 64;
        public TextureName textureName;
        public Texture2D Texture => Store.textures.Get(textureName);
        public bool delete = false;
        public int row;
        public int col;

        public float Top => row * Texture.Height;
        public float Bottom => Top + Texture.Height;
        public float Left => col * Texture.Width;
        public float Right => Left + Texture.Width;

        public Block(int col, int row, TextureName textureName)
        {
            this.textureName = textureName;
            this.row = row;
            this.col = col;
        }

        public int HorizontalPosition(int i)
        {
            return LEFT_OFFSET + i * Texture.Width;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            var x = col * Texture.Width;
            var y = row * Texture.Height;
            spriteBatch.Draw(Texture, new Vector2(x, y), Color.White);
        }

        public void Update(GameTime gameTime, InputState input)
        {

        }

        public void OnCollide(Side sideOfImpact)
        {
            delete = true;
        }

        public bool Clicked(Vector2 position)
        {
            return Left < position.X
                && Right > position.X
                && Top < position.Y
                && Bottom > position.Y;
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
                else if (point.Y < Top) {
                    return Side.TopLeft;
                }
                else {
                    return Side.BottomLeft;
                }
            }
            else {
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

        public override string ToString()
        {
            if (textureName == TextureName.RedBlock)
                return "RED";
            if (textureName == TextureName.GreenBlock)
                return "GREEN";
            if (textureName == TextureName.BlueBlock)
                return "BLUE";
            if (textureName == TextureName.GoldBlock)
                return "GOLD";
            return "NAKEY BRIK!";
        }
    }
}
