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
        public bool saved = false;
        public int[,] playfield = new int[COLUMNS, ROWS];
        public InputState inputState;
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

        public void Update(InputState input, GameTime gameTime)
        {
            inputState = input;
            foreach(var block in blocks)
            {
                if (input.Clicked) {
                    if (block.Clicked(input.ClickedAt)) {
                        currentColor = block.textureName;
                        saved = false;
                    }
                }
            }

            for(var column = 0; column < COLUMNS; column += 1) {
                for(var row = 0; row < ROWS; row += 1) {
                    var block = new Block(column, row, TextureName.RedBlock);
                    if (input.Clicked && block.Clicked(input.ClickedAt))
                    {
                        playfield[column, row] = (int)currentColor;
                    }
                }
            }

            if(input.WasPressed(Keys.Enter)) {
                SaveLevel();
                saved = true;
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
                        var block = new Block(column, row, TextureName.RedBlock);
                        block.Draw(spriteBatch, spriteFont);
                    }
                    else if (playfield[column, row] == (int)TextureName.BlueBlock)
                    {
                        var block = new Block(column, row, TextureName.BlueBlock);
                        block.Draw(spriteBatch, spriteFont);
                    }
                    else if (playfield[column, row] == (int)TextureName.GreenBlock)
                    {
                        var block = new Block(column, row, TextureName.GreenBlock);
                        block.Draw(spriteBatch, spriteFont);
                    }
                    else if (playfield[column, row] == (int)TextureName.GoldBlock)
                    {
                        var block = new Block(column, row, TextureName.GoldBlock);
                        block.Draw(spriteBatch, spriteFont);
                    }
                    else {
                        var block = new Block(column, row, TextureName.GoldBlock);
                        block.DrawMuted(spriteBatch, spriteFont);
                    }
                }
            }

            // Draw UI components
            spriteBatch.Draw(Store.textures.Get(currentColor), new Vector2(inputState.mCurrent.X, inputState.mCurrent.Y), Color.White);
            spriteBatch.DrawString(spriteFont, "Selected: " + currentColor, new Vector2(7 * 96, 3), Color.Black);
            spriteBatch.DrawString(spriteFont, "Press enter to save level" , new Vector2(7 * 96, 120), Color.Black);
            if (saved)
            {
                spriteBatch.DrawString(spriteFont, "Level saved" , new Vector2(GameWindow.WIDTH, GameWindow.HEIGHT/2), Color.Black);
            }
        }
    }
}
