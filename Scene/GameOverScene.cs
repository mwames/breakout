using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class GameOverScene : IScene
    {
        public void Update(GamePadState gamePadState, GamePadState previousGamePadState, KeyboardState keyboardState, KeyboardState previousKeyboardState, GameTime gameTime)
        {
            Store.songs.Play(SongName.GameOver);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
                string gameOverMessage = "You have died!";
                graphicsDevice.Clear(Color.Yellow);
                spriteBatch.DrawString(spriteFont, gameOverMessage, new Vector2(GameWindow.WIDTH / 3 , GameWindow.HEIGHT /2), Color.Black);
        }
    }
}
