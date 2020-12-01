using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class GameOverScene : IScene
    {
        public bool again = true;
        public bool exit = false;
        public void Update(InputState input, GameTime gameTime)
        {
            Store.songs.Play(SongName.GameOver);

            if (input.WasPressed(Keys.Right) || input.WasPressed(Buttons.DPadRight))
            {
                exit = true;
                again = false;
            }

            if (input.WasPressed(Keys.Left) || input.WasPressed(Buttons.DPadLeft))
            {
                exit = false;
                again = true;
            }

            if (again == true && (input.WasPressed(Keys.Enter) || input.WasPressed(Buttons.Start)))
            {
                exit = false;
                Store.scenes.ChangeScene(SceneName.Menu); // To do Load new Game to restart points and lives.
            }

            if (exit == true && (input.WasPressed(Keys.Enter) || input.WasPressed(Buttons.Start)))
            {
                again = false;
                // to do call the exit commmand
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
                var gameOverMessage = "So Sad";
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

            spriteBatch.DrawString(spriteFont, "Choose your fate", new Vector2(GameWindow.WIDTH/2-150, GameWindow.HEIGHT-200), Color.DarkGoldenrod);
            if (again)
            {
            spriteBatch.DrawString(spriteFont, "Try Again", new Vector2(GameWindow.WIDTH/2 - 150, GameWindow.HEIGHT-150), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "Try Again", new Vector2(GameWindow.WIDTH/2 - 150, GameWindow.HEIGHT-150), Color.DarkGoldenrod);
            }

            if (exit)
            {
            spriteBatch.DrawString(spriteFont, "Exit", new Vector2(GameWindow.WIDTH/2 + 25 , GameWindow.HEIGHT-150), Color.Yellow);
            }

            else
            {
                spriteBatch.DrawString(spriteFont, "Exit", new Vector2(GameWindow.WIDTH/2+ 25 , GameWindow.HEIGHT-150), Color.DarkGoldenrod);
            }

        }
    }
}
