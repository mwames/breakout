using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class GameScene : IScene
    {
        private Paddle paddle;
        public Ball ball;
        private List<Block> blocks;

        private GameWindow Window;
        private SoundEffect ballSound;
        private string[] lines = System.IO.File.ReadAllLines(@"./Levels/level2.txt");
        public int score;
        private Dictionary<string, TextureName> textureNameMap;
        
        public GameScene(
            Paddle paddle,
            Ball ball,
            GameWindow Window,
            SoundEffect ballSound
            
        )
        {
            this.paddle = paddle;
            this.ball = ball;
            this.Window = Window;
            this.ballSound = ballSound;
            this.blocks = new List<Block>();
            this.textureNameMap = CreateTextureDictionary();
            foreach(var line in lines) {
                if (line != "") {
                    Console.WriteLine(line.Split(",")[0]);
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

        private Dictionary<string, TextureName> CreateTextureDictionary() {
            return new Dictionary<string, TextureName>()
            {
                {"red", TextureName.RedBlock},
                {"gold", TextureName.GoldBlock},
                {"green", TextureName.GreenBlock},
                {"blue", TextureName.BlueBlock}
            };
        }

        public void Update(GamePadState gamePadState, GamePadState previousGamePadState, KeyboardState keyboardState, KeyboardState previousKeyboardState, GameTime gameTime)
        {
            if ((!previousGamePadState.IsButtonDown(Buttons.Start) && gamePadState.IsButtonDown(Buttons.Start)) || (!previousKeyboardState.IsKeyDown(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))) {
                Store.scenes.currentScene = Store.scenes.Get(SceneName.Pause);
            }

            if (gamePadState.IsConnected)
                paddle.Update(gameTime, gamePadState);
            else
                paddle.Update(gameTime, keyboardState);

            ball.Update(gameTime, gamePadState);

            if (Collision.DidCollide(ball, paddle))
            {
                // Bottom was hit
                if (ball.Bottom >= paddle.Top && ball.Top < paddle.Top)
                {
                    ball.OnCollide(Side.Bottom);
                    ball.position.Y = paddle.Top - ball.radius * 2;
                    score ++;
                }

                // Left was hit
                if (ball.Right >= paddle.Left && ball.Left < paddle.Left)
                {
                    ball.OnCollide(Side.Left);
                    ball.position.X = paddle.Left - ball.radius * 2;
                    score ++;
                }

                // Right was hit
                if (ball.Left <= paddle.Right && ball.Right > paddle.Right)
                {
                    ball.OnCollide(Side.Right);
                    ball.position.X = paddle.Right;
                    score ++;
                }
            }

            foreach(var block in blocks) {
                if (Collision.DidCollide(ball, block)) {
                    block.OnCollide(Side.Bottom);
                    score +=5;
                }
            }

            blocks = blocks.Where(block => !block.delete).ToList();


            if (ball.Top <= 0)
            {
                ball.OnCollide(Side.Top);
                ball.position.Y = 0;
                // ballSound.Play();
            }

            if (ball.Left <= 0)
            {
                ball.OnCollide(Side.Left);
                ball.position.X = 0;
                // ballSound.Play();
            }

            if (ball.Right >= Window.ClientBounds.Width)
            {
                ball.OnCollide(Side.Right);
                ball.position.X = Window.ClientBounds.Width - ball.radius * 2;
                // ballSound.Play();
            }

            if (ball.Bottom >= Window.ClientBounds.Height)
            {
                ball.OnCollide(Side.Bottom);
                ball.position.Y = Window.ClientBounds.Height - ball.radius * 2;
                paddle.health--;

                if (paddle.health <= 0)
                    Store.scenes.currentScene = Store.scenes.Get(SceneName.GameOver);
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
            for (int i = 0; i < paddle.health; i++)
            {
                spriteBatch.Draw(Store.textures.Get(TextureName.Heart), new Vector2(i * 63, global::Breakout.Window.HEIGHT - 63), Color.White);
            }

            ball.Draw(spriteBatch, spriteFont);
            paddle.Draw(spriteBatch, spriteFont);
            foreach(var block in blocks)
            {
                block.Draw(spriteBatch, spriteFont);
            }
            spriteBatch.DrawString(spriteFont, "Points: " + score.ToString(), new Vector2(3, 3), Color.Black);
        }

        public override string ToString()
        {
            return "This is the GameScene: " + ball.position.X;
        }
    }
}
