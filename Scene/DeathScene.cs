using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class DeathScene : IScene
    {
        public void Update(InputState input, GameTime gameTime)
        {
            if (input.WasPressed(Keys.Enter) || input.WasPressed(Buttons.Start)) {
                Store.scenes.ChangeScene(SceneName.Game);
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
                var deathMessage = "You died";
                var deathMessageSize = spriteFont.MeasureString(deathMessage);

                var livesMessage = $"x{Store.lives}";
                var livesMessageSize = spriteFont.MeasureString(livesMessage);

                var texture = Store.textures.Get(TextureName.Paddle);
                var totalWidth = texture.Width + livesMessageSize.X;

                graphicsDevice.Clear(Color.DarkBlue);
                spriteBatch.DrawString(
                    spriteFont,
                    deathMessage,
                    new Vector2(
                        GameWindow.WIDTH / 2 - deathMessageSize.X / 2 ,
                        GameWindow.HEIGHT / 2 - deathMessageSize.Y / 2 - 60
                    ),
                    Color.DarkGoldenrod
                );

                for(var i = 0; i < Store.lives; i += 1) {
                    spriteBatch.Draw(
                        texture,
                        new Vector2(
                            GameWindow.WIDTH / 2 - totalWidth / 2,
                            GameWindow.HEIGHT / 2 - texture.Height / 2 - 20
                    ),
                        new Rectangle(0, 0, texture.Width, texture.Height),
                        Color.White
                    );
                }

                spriteBatch.DrawString(
                    spriteFont,
                    livesMessage,
                    new Vector2(
                        GameWindow.WIDTH / 2 - totalWidth / 2 + texture.Width + 10,
                        GameWindow.HEIGHT / 2 - livesMessageSize.Y / 2 - 20
                    ),
                    Color.DarkGoldenrod
                );
                
        }
    }
}
