using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class PauseScene : IScene
    {
        private GameWindow Window;

        public PauseScene(GameWindow Window) {
            this.Window = Window;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
            string gameOverMessage = "- PAUSE -";
            graphicsDevice.Clear(Color.DarkCyan);
            spriteBatch.DrawString(
                spriteFont,
                gameOverMessage,
                new Vector2(
                    global::Breakout.Window.WIDTH / 2 - spriteFont.MeasureString(gameOverMessage).X / 2,
                    global::Breakout.Window.HEIGHT / 2 - 20
                ),
                Color.Black
                );
        }

        public void Update(GamePadState gamePadState, GamePadState previousGamePadState, KeyboardState keyboardState, KeyboardState previousKeyboardState, GameTime gameTime)
        {
            if ((!previousGamePadState.IsButtonDown(Buttons.Start) && gamePadState.IsButtonDown(Buttons.Start)) || (!previousKeyboardState.IsKeyDown(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))) {
                Store.scenes.currentScene = Store.scenes.Get(SceneName.Game);
            }
        }
    }
}