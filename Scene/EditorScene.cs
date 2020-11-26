using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class EditorScene : IScene
    {
        public static readonly int COLUMNS = 5;
        public static readonly int ROWS = 15;
        public TextureName currentColor = TextureName.RedBlock;
        public List<Block> blocks;
        public string[] lines = System.IO.File.ReadAllLines(@"./Levels/palette.txt");
        public int score;
        public MouseState current;
        public MouseState previous;
        public KeyboardState kCurrent;
        public KeyboardState kPrevious;
        public int[,] playfield = new int[COLUMNS, ROWS];
        public Dictionary<string, TextureName> textureNameMap = new Dictionary<string, TextureName>()
        {
            {"red", TextureName.RedBlock},
            {"gold", TextureName.GoldBlock},
            {"green", TextureName.GreenBlock},
            {"blue", TextureName.BlueBlock}
        };
        public Dictionary<TextureName, string> colorMap = new Dictionary<TextureName, string>()
        {
            { TextureName.RedBlock, "red" },
            { TextureName.GoldBlock, "gold" },
            { TextureName.GreenBlock, "green" },
            { TextureName.BlueBlock, "blue" }
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

        public bool WasClicked(MouseState current, MouseState previous) {
            return current.LeftButton == ButtonState.Pressed && previous.LeftButton != ButtonState.Pressed;
        }

        public void SaveLevel() {
            var levelNumber = Directory.GetFiles("./Levels").Length;
            List<String> lines = new List<String>();

            for (var column = 0; column < COLUMNS; column += 1)
            {
                for (var row = 0; row < ROWS; row += 1)
                {
                    TextureName texture = (TextureName)playfield[column, row];
                    if(colorMap.ContainsKey(texture))
                        lines.Add($"{column},{row},{colorMap[texture]}");
                }
            }

            System.IO.File.WriteAllLines($@"./Levels/level{levelNumber}.txt", lines);
        }

        public void Update(GamePadState gamePadState, GamePadState previousGamePadState, KeyboardState keyboardState, KeyboardState previousKeyboardState, GameTime gameTime)
        {
            previous = current;
            current = Mouse.GetState();
            foreach(var block in blocks)
            {
                if (WasClicked(current, previous)) {
                    if (block.Clicked(new Vector2(current.Position.X, current.Position.Y))) {
                        currentColor = block.textureName;
                    }
                }
            }

            for(var column = 0; column < COLUMNS; column += 1) {
                for(var row = 0; row < ROWS; row += 1) {
                    var block = new Block(column, row + 3, TextureName.RedBlock);
                    if(current.LeftButton == ButtonState.Pressed && block.Clicked(new Vector2(current.Position.X, current.Position.Y)))
                    {
                        playfield[column, row] = (int)currentColor;
                    }
                }
            }

            kPrevious = kCurrent;
            kCurrent = Keyboard.GetState();
            if(kCurrent.IsKeyDown(Keys.Enter) && !kPrevious.IsKeyDown(Keys.Enter)) {
                SaveLevel();
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
            // Draw the options
            foreach(var block in blocks)
            {
                block.Draw(spriteBatch, spriteFont);
            }

            // Draw the playfield
            for(var column = 0; column < COLUMNS; column += 1) {
                for(var row = 0; row < ROWS; row += 1) {
                    if (playfield[column, row] == (int)TextureName.RedBlock) {
                        var block = new Block(column, row + 3, TextureName.RedBlock);
                        block.Draw(spriteBatch, spriteFont);
                    }
                    if (playfield[column, row] == (int)TextureName.BlueBlock)
                    {
                        var block = new Block(column, row + 3, TextureName.BlueBlock);
                        block.Draw(spriteBatch, spriteFont);
                    }
                    if (playfield[column, row] == (int)TextureName.GreenBlock)
                    {
                        var block = new Block(column, row + 3, TextureName.GreenBlock);
                        block.Draw(spriteBatch, spriteFont);
                    }
                    if (playfield[column, row] == (int)TextureName.GoldBlock)
                    {
                        var block = new Block(column, row + 3, TextureName.GoldBlock);
                        block.Draw(spriteBatch, spriteFont);
                    }
                }
            }

            // Draw UI components
            spriteBatch.DrawString(spriteFont, "X: " + current.Position.X, new Vector2(3, 3), Color.Black);
            spriteBatch.DrawString(spriteFont, "Y: " + current.Position.Y, new Vector2(3, 30), Color.Black);
            spriteBatch.DrawString(spriteFont, "Selected: " + currentColor, new Vector2(7 * 96, 3), Color.Black);
        }
    }
}
