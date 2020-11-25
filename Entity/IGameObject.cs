using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public interface IGameObject
    {
        void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont);
        void Update(GameTime gameTime, GamePadState gState);
        void OnCollide(Side sideOfImpact);
    }
}
