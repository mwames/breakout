using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout {
    public static class Dot {
        public static void Draw(SpriteBatch spriteBatch, Vector2 vector, Texture2D texture) {
            spriteBatch.Draw(
                Store.textures.Get(TextureName.TitleScreen),
                new Rectangle((int)vector.X - 1, (int)vector.Y - 1, 2, 2),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.LimeGreen
            );
        }
    }
}
