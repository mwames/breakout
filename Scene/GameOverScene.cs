using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class GameOverScene : IScene
    {
        private GameWindow Window;

        public GameOverScene(GameWindow Window) {
            this.Window = Window;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
                string gameOverMessage = "You have died!";
                graphicsDevice.Clear(Color.Yellow);
                spriteBatch.DrawString(spriteFont, gameOverMessage, new Vector2(Window.ClientBounds.Width / 3 , Window.ClientBounds.Height /2), Color.Black);
        }

        public void Update(GamePadState gamePadState, KeyboardState keyboardState, GameTime gameTime)
        {

        }
    }
}
