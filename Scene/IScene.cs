using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public interface IScene
    {
        void Update(GamePadState gamePadState, KeyboardState keyboardState, GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice);
    }
}
