using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class OptionsScene : IScene
    {
        public bool easy = false;
        public bool normal = true;

        public bool hard = false;
        
        public void Update(InputState input, GameTime gameTime)
        {
            if ( easy == true && (input.WasPressed(Keys.Down) || input.WasPressed(Buttons.DPadDown)))
            {
                 easy  = false;
                 normal = true;
            }  

            else if ( normal == true && (input.WasPressed(Keys.Down) || input.WasPressed(Buttons.DPadDown)))
            {
                 normal = false;
                 hard= true;
            }
              
            if (normal== true && (input.WasPressed(Keys.Up) || input.WasPressed(Buttons.DPadUp)))
            {
               normal = false;
               easy = true;
            }

            if (hard == true && (input.WasPressed(Keys.Up) || input.WasPressed(Buttons.DPadUp)))
            {
               hard = false;
               normal = true;
            }

            
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.DarkOrchid);
            spriteBatch.Draw(
                Store.textures.Get(TextureName.OptionsButton),
                new Rectangle(25 ,50 ,440,144),
                Color.White
                );

            spriteBatch.DrawString(spriteFont, "Select Your Difficulty", new Vector2(25, 200), Color.White);
            if (easy)
            {
            spriteBatch.DrawString(spriteFont, "Easy", new Vector2(25, 250), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "Easy", new Vector2(25, 250), Color.White);
            }

            if (normal)
            {
            spriteBatch.DrawString(spriteFont, "Normal", new Vector2(25, 300), Color.Yellow);
            }

            else
            {
                spriteBatch.DrawString(spriteFont, "Normal", new Vector2(25, 300), Color.White);
            }

            if (hard)
            {
            spriteBatch.DrawString(spriteFont, "Hard", new Vector2(25, 350), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "Hard", new Vector2(25, 350), Color.White);
            }

        }
    }
 }