using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class GameOverScene : IScene
    {
        public void Update(InputState input, GameTime gameTime)
        {
            Store.songs.Play(SongName.GameOver);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
                var gameOverMessage = "So sad";
                var stringBox = spriteFont.MeasureString(gameOverMessage);
                graphicsDevice.Clear(Color.DarkBlue);
                spriteBatch.DrawString(
                    spriteFont,
                    gameOverMessage,
                    new Vector2(
                        GameWindow.WIDTH / 2 - stringBox.X / 2,
                        GameWindow.HEIGHT / 2 - stringBox.X / 2 - 20
                    ),
                    Color.DarkGoldenrod
                );
        }
    }
}
