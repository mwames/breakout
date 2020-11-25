using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class Block
    {
        private static readonly int BLOCK_HEIGHT = 28;
        private static readonly int BLOCK_WIDTH = 96;
        private static readonly int LEFT_OFFSET = 64;
        private TextureName textureName;
        private Texture2D Texture => Store.textures.Get(textureName);
        private bool delete = false;
        private int row;
        private int col;
        
        public Block(int col, int row, TextureName textureName)
        {
            this.textureName = textureName;
            this.row = row;
            this.col = col;
        }

        private int HorizontalPosition(int i) {
            return LEFT_OFFSET + i * BLOCK_WIDTH;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var x = col * BLOCK_WIDTH;
            var y = row * BLOCK_HEIGHT;
            spriteBatch.Draw(Texture, new Vector2(x, y), Color.White);
        }

        public void onCollide() {
            delete = true;
        }
    }
}
