using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class Block
    {
        private static readonly int BLOCK_HEIGHT = 28;
        private static readonly int BLOCK_WIDTH = 96;
        private static readonly int LEFT_OFFSET = 64;
        private int offset;
        private TextureName textureName;
        private Texture2D Texture => Store.textures.Get(textureName);
        private int VerticalPosition => (global::Breakout.Window.HEIGHT / 2) - (offset * BLOCK_HEIGHT);
        
        public Block(TextureName textureName, int offset)
        {
            this.textureName = textureName;
            this.offset = offset;
        }

        private int HorizontalPosition(int i) {
            return LEFT_OFFSET + i * BLOCK_WIDTH;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 7; i++)
            {
                spriteBatch.Draw(Texture, new Vector2(HorizontalPosition(i), VerticalPosition), Color.White);
            }
        }
    }
}
