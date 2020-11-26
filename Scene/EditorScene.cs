using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class EditorScene : IScene
    {
        private static readonly int COLUMNS = 5;
        private static readonly int ROWS = 15;
        private List<Block> blocks;
        private string[] lines = System.IO.File.ReadAllLines(@"./Levels/palette.txt");
        public int score;
        MouseState current;
        MouseState previous;
        int[,] playfield = new int[COLUMNS, ROWS];
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
                Store.scenes.ChangeScene(SceneName.Pause);
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
            previous = current;
            current = Mouse.GetState();

            foreach(var block in blocks)
            {
                if (current.LeftButton == ButtonState.Pressed && previous.LeftButton != ButtonState.Pressed) {
                    if (block.Clicked(new Vector2(current.Position.X, current.Position.Y))) {
                        System.Console.WriteLine(block.ToString());
                    }
                }
                block.Draw(spriteBatch, spriteFont);
            }
            spriteBatch.DrawString(spriteFont, "X: " + current.Position.X, new Vector2(3, 3), Color.Black);
            spriteBatch.DrawString(spriteFont, "Y: " + current.Position.Y, new Vector2(3, 30), Color.Black);

            for(var column = 0; column < COLUMNS; column += 1) {
                for(var row = 0; row < ROWS; row += 1) {
                    var block = new Block(column, row + 3, TextureName.RedBlock);
                    block.DrawMuted(spriteBatch);
                    // playfield[column, row] = (int)TextureName.RedBlock;
                    // spriteBatch.DrawString(spriteFont, $"({column}, {row})", new Vector2(column * 110, 60 + row * 30), Color.Black);
                }
            }
        }
    }
}
