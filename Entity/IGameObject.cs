using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public interface IGameObject
    {
        void Update(GameTime gameTime, InputState inputState);
        void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont);
    }
}
