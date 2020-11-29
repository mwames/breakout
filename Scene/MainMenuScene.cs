using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class MainMenuScene : IScene
    {
        readonly string MESSAGE = "Press enter to embrace your fate";
        readonly int MARGIN_TOP = 75;
        public bool options = false;
        public bool start = true;

        public void Update(InputState input, GameTime gameTime)
        {

            if (input.WasPressed(Keys.Right) || input.WasPressed(Buttons.DPadRight))
            {
                options = true;
                start = false;
            }

            if (input.WasPressed(Keys.Left) || input.WasPressed(Buttons.DPadLeft))
            {
                options = false;
                start = true;
            }

            if (start == true && (input.WasPressed(Keys.Enter) || input.WasPressed(Buttons.Start)))
            {
                options = false;
                Store.scenes.ChangeScene(SceneName.Game);
            }

            if (options == true && (input.WasPressed(Keys.Enter) || input.WasPressed(Buttons.Start)))
            {
                start = false;
                Store.scenes.ChangeScene(SceneName.Options); // todo  come up with options and build the screen
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.White);
            spriteBatch.Draw(
                Store.textures.Get(TextureName.TitleScreen),
                new Rectangle(0, MARGIN_TOP, GameWindow.WIDTH, GameWindow.HEIGHT / 2),
                Color.White
            );

            var startColor = start ? Color.Yellow : Color.White;
            var optionsColor = options ? Color.Yellow : Color.White;

            var playButton = Store.textures.Get(TextureName.PlayButton);
            spriteBatch.Draw(
                playButton,
                new Rectangle((GameWindow.WIDTH / 2) - 96 - 10, (GameWindow.HEIGHT / 2) + MARGIN_TOP * 2, 96, 54),
                startColor
            );

            spriteBatch.Draw(
                Store.textures.Get(TextureName.OptionsButton),
                new Rectangle((GameWindow.WIDTH / 2) + 10, GameWindow.HEIGHT / 2 + MARGIN_TOP * 2, 110, 54),
                optionsColor
            );

            spriteBatch.DrawString(
                spriteFont,
                MESSAGE,
                new Vector2(
                    GameWindow.WIDTH / 2 - spriteFont.MeasureString(MESSAGE).X / 2,
                    GameWindow.HEIGHT / 2 - 30 + MARGIN_TOP
                ),
                Color.Black
                );
        }
    }
}
