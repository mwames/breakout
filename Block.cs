using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class Block
    {
    public Block (){}
    }
    public class RedBlock : Block
    {
        private Texture2D texture => Store.textures.Get(TextureName.RedBlock);
        public void Draw(SpriteBatch spriteBatch) {
             for (int i = 0; i < 7; i++)
            {
                spriteBatch.Draw(Store.textures.Get(TextureName.RedBlock), 
                new Vector2(64+ i * 96, global::Breakout.Window.HEIGHT/2), 
                Color.White);
            }
        }
    }
    public class GoldBlock : Block
    {
        public Texture2D texture => Store.textures.Get(TextureName.GoldBlock);
        public void Draw(SpriteBatch spriteBatch) {
             for (int i = 0; i < 7; i++)
            {
                spriteBatch.Draw(Store.textures.Get(TextureName.GoldBlock), 
                new Vector2(64+ i * 96, global::Breakout.Window.HEIGHT/2 -84), 
                Color.White);
            }
        }
    }
    public class GreenBlock : Block
    {
        public Texture2D texture => Store.textures.Get(TextureName.GreenBlock);
        public void Draw(SpriteBatch spriteBatch) {
             for (int i = 0; i < 7; i++)
            {
                spriteBatch.Draw(Store.textures.Get(TextureName.GreenBlock), 
                new Vector2(64+ i * 96, global::Breakout.Window.HEIGHT/2 - 56), 
                Color.White);
            }
        }
    }
    public class BlueBlock : Block
    {
        public Texture2D texture => Store.textures.Get(TextureName.BlueBlock);
        public void Draw(SpriteBatch spriteBatch) {
             for (int i = 0; i < 7; i++)
            {
                spriteBatch.Draw(Store.textures.Get(TextureName.BlueBlock), 
                new Vector2(64+ i * 96, global::Breakout.Window.HEIGHT/2 -28), 
                Color.White);
            }
        }
    }
}