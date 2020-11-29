using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class MainMenuScene : IScene
    {
        readonly int OFFSET = 50;
        readonly string MESSAGE = "Press enter to embrace your fate";
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
            spriteBatch.Draw(
                Store.textures.Get(TextureName.TitleScreen),
                new Rectangle(0, 0, GameWindow.WIDTH, GameWindow.HEIGHT),
                Color.White
            );

            if (start)
            {
            spriteBatch.Draw(
                Store.textures.Get(TextureName.PlayButton),
                new Rectangle(50,GameWindow.HEIGHT - 100,96,54),
                Color.Yellow
            );
            }

            else {
                spriteBatch.Draw(
                Store.textures.Get(TextureName.PlayButton),
                new Rectangle(50,GameWindow.HEIGHT - 100,96,54),
                Color.White
                );
            }

            if (options)
            {
             spriteBatch.Draw(
                Store.textures.Get(TextureName.OptionsButton),
                new Rectangle(GameWindow.WIDTH - 150,GameWindow.HEIGHT - 100,110,54),
                Color.Yellow
                );
            
            }

            else {
                spriteBatch.Draw(
                Store.textures.Get(TextureName.OptionsButton),
                new Rectangle(GameWindow.WIDTH - 150,GameWindow.HEIGHT - 100,110,54),
                Color.White
                );
            }

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