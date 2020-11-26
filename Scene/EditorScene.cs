using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class EditorScene : IScene
    {
        private List<Block> blocks;
        private SoundEffect ballSound;
        private string[] lines = System.IO.File.ReadAllLines(@"./Levels/palette.txt");
        public int score;
        private Dictionary<string, TextureName> textureNameMap = new Dictionary<string, TextureName>()
        {
            {"red", TextureName.RedBlock},
            {"gold", TextureName.GoldBlock},
            {"green", TextureName.GreenBlock},
            {"blue", TextureName.BlueBlock}
        };
        
        public EditorScene()
        {
            this.blocks = new List<Block>();
            foreach(var line in lines) {
                if (line != "") {
                    var parts = line.Split(",");
                    blocks.Add(new Block(
                        Int32.Parse(parts[0]),
                        Int32.Parse(parts[1]),
                        textureNameMap[parts[2]]
                        )
                    );
                }
            }
        }

        public void Update(GamePadState gamePadState, GamePadState previousGamePadState, KeyboardState keyboardState, KeyboardState previousKeyboardState, GameTime gameTime)
        {
            if ((!previousGamePadState.IsButtonDown(Buttons.Start) && gamePadState.IsButtonDown(Buttons.Start)) || (!previousKeyboardState.IsKeyDown(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))) {
                Store.scenes.currentScene = Store.scenes.Get(SceneName.Pause);
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
            foreach(var block in blocks)
            {
                block.Draw(spriteBatch, spriteFont);
            }
            spriteBatch.DrawString(spriteFont, "Points: " + score.ToString(), new Vector2(3, 3), Color.Black);
        }
    }
}
