using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Breakout
{
    public class Block
    {
        public Texture2D texture => Store.textures.Get(TextureName.RedBlock);

    public Block (){}
        
    public void Draw(SpriteBatch spriteBatch) {
            // Draws the ball
            spriteBatch.Draw(texture, new Vector2(400,400), Color.White);
        }
    }
}