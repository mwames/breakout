using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class MainMenuScene : IScene
    {
        readonly int OFFSET = 50;
        readonly string MESSAGE = "Press enter to begin!";
        TextureManager textureManager;
        GameWindow Window;
        SceneManager sceneManager;

        public MainMenuScene(TextureManager textureManager, GameWindow Window, SceneManager sceneManager)
        {
            this.textureManager = textureManager;
            this.Window = Window;
            this.sceneManager = sceneManager;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Draw(
                textureManager.Get(TextureName.TitleScreen),
                new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                Color.White
            );

            spriteBatch.DrawString(
                spriteFont,
                MESSAGE,
                new Vector2(
                    Window.ClientBounds.Width / 2 - spriteFont.MeasureString(MESSAGE).X / 2,
                    Window.ClientBounds.Height - OFFSET
                ),
                Color.Black
                );
        }

        public void Update(GamePadState gamePadState, KeyboardState keyboardState, GameTime gameTime)
        {

            if (keyboardState.IsKeyDown(Keys.Enter) || gamePadState.IsButtonDown(Buttons.Start))
            {
                sceneManager.currentScene = sceneManager.Get(SceneName.Game);
            }
        }
    }
}
