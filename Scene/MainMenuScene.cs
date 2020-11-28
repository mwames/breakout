using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class MainMenuScene : IScene
    {
        readonly int OFFSET = 50;
        readonly string MESSAGE = "Press enter to begin!";
        
        public void Update(InputState input, GameTime gameTime)
        {

            if (input.WasPressed(Keys.Enter) || input.WasPressed(Buttons.Start))
            {
                Store.scenes.ChangeScene(SceneName.Game);
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Draw(
                Store.textures.Get(TextureName.TitleScreen),
                new Rectangle(0, 0, GameWindow.WIDTH, GameWindow.HEIGHT),
                Color.White
            );

            spriteBatch.DrawString(
                spriteFont,
                MESSAGE,
                new Vector2(
                    GameWindow.WIDTH / 2 - spriteFont.MeasureString(MESSAGE).X / 2,
                    GameWindow.HEIGHT - OFFSET
                ),
                Color.Black
                );
        }
    }
}
