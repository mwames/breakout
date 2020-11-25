using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Block : IGameObject, IRectangle
    {
        private static readonly int LEFT_OFFSET = 64;
        private TextureName textureName;
        private Texture2D Texture => Store.textures.Get(textureName);
        public bool delete = false;
        private int row;
        private int col;

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

        private int HorizontalPosition(int i) {
            return LEFT_OFFSET + i * Texture.Width;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            var x = col * Texture.Width;
            var y = row * Texture.Height;
            spriteBatch.Draw(Texture, new Vector2(x, y), Color.White);
        }

        public void Update(GameTime gameTime, GamePadState gState)
        {

        }
        
        public void OnCollide(Side sideOfImpact) {
            delete = true;
        }
    }
}
