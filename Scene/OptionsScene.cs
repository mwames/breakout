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
        public bool back = false;
        
        public void Update(InputState input, GameTime gameTime)
        {
            if ( easy == true && (input.WasPressed(Keys.Right) || input.WasPressed(Buttons.DPadRight)))
            {
                 easy  = false;
                 normal = true;
            }  

            else if ( normal == true && (input.WasPressed(Keys.Right) || input.WasPressed(Buttons.DPadRight)))
            {
                 normal = false;
                 hard= true;
            }
              
            if (normal== true && (input.WasPressed(Keys.Left) || input.WasPressed(Buttons.DPadLeft)))
            {
               normal = false;
               easy = true;
            }

            if (hard == true && (input.WasPressed(Keys.Left) || input.WasPressed(Buttons.DPadLeft)))
            {
               hard = false;
               normal = true;
            }

            if ((input.WasPressed(Keys.Down) || input.WasPressed(Buttons.DPadDown)))
            {
                 back = true;
            }  
            if ((input.WasPressed(Keys.Up) || input.WasPressed(Buttons.DPadUp)))
            {
                 back = false;
            }  

            if (back == true && (input.WasPressed(Keys.Enter) || input.WasPressed(Buttons.Start)))
            {
                Store.scenes.ChangeScene(SceneName.Menu);
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
            spriteBatch.DrawString(spriteFont, "Normal", new Vector2(125, 250), Color.Yellow);
            }

            else
            {
                spriteBatch.DrawString(spriteFont, "Normal", new Vector2(125, 250), Color.White);
            }

            if (hard)
            {
            spriteBatch.DrawString(spriteFont, "Hard", new Vector2(275, 250), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "Hard", new Vector2(275, 250), Color.White);
            }
            if (back)
            {
            spriteBatch.DrawString(spriteFont, "Back", new Vector2(25, 300), Color.Yellow);
            }
            else 
            {
                spriteBatch.DrawString(spriteFont, "Back", new Vector2(25, 300), Color.White);
            }

        }
    }
 }