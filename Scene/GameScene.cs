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
        public Paddle paddle;
        public Ball ball;
        public List<Block> blocks;
        public int score;
        public int currentLevel = 1;
        public Dictionary<string, TextureName> textureNameMap = new Dictionary<string, TextureName>()
        {
            {"red", TextureName.RedBlock},
            {"gold", TextureName.GoldBlock},
            {"green", TextureName.GreenBlock},
            {"blue", TextureName.BlueBlock}
        };

        public GameScene(Paddle paddle, Ball ball)
        {
            this.paddle = paddle;
            this.ball = ball;
            this.blocks = new List<Block>();
            LoadLevel();
        }

        public void Update(InputState input, GameTime gameTime)
        {
            if (input.WasPressed(Buttons.Start) || input.WasPressed(Keys.Space))
            {
                Store.scenes.ChangeScene(SceneName.Pause);
            }

            paddle.Update(gameTime, input);
            ball.Update(gameTime, input);

            if (Collision.DidCollide(ball, paddle))
            {
                var sides = Collision.SideHit(ball, paddle);
                System.Console.WriteLine(sides.ToString());
                ball.OnCollide(sides["ball"], paddle);
                score++;
                Store.soundEffects.Get(SoundEffectName.BallSound).Play();

                if (blocks.Count == 0)
                {
                    currentLevel += 1;
                    LoadLevel();
                }
            }

            foreach (var block in blocks)
            {
                if (Collision.DidCollide(ball, block))
                {
                    var sides = Collision.SideHit(ball, block);
                    System.Console.WriteLine(sides.ToString());
                    block.OnCollide(sides["box"]);
                    score += 5;
                    Store.soundEffects.Get(SoundEffectName.BallSound).Play();
                    ball.OnCollide(sides["ball"], block);
                }
            }

            blocks = blocks.Where(block => !block.delete).ToList();


            if (ball.Top <= 0)
            {
                ball.heading = ball.FlipX(ball.heading);
                ball.position.Y = 0;
                Store.soundEffects.Get(SoundEffectName.BallSound).Play();
            }

            if (ball.Left <= 0)
            {
                ball.heading = ball.FlipY(ball.heading);
                ball.position.X = 0;
                Store.soundEffects.Get(SoundEffectName.BallSound).Play();
            }

            if (ball.Right >= GameWindow.WIDTH)
            {
                ball.heading = ball.FlipY(ball.heading);
                ball.position.X = GameWindow.WIDTH - ball.radius * 2;
                Store.soundEffects.Get(SoundEffectName.BallSound).Play();
            }

            if (ball.Bottom >= GameWindow.HEIGHT)
            {
                ball.heading = ball.FlipX(ball.heading);
                ball.position.Y = GameWindow.HEIGHT - ball.radius * 2;
                paddle.health--;

                if (paddle.health <= 0)
                    Store.scenes.ChangeScene(SceneName.GameOver);
            }
        }

        public void LoadLevel()
        {
            int totalLevels = System.IO.Directory.GetFiles($@"./Levels/").Count();
            ball.velocity = 250;
            if (currentLevel < totalLevels)
            {
                var lines = System.IO.File.ReadAllLines($@"./Levels/level{currentLevel}.txt");
                foreach (var line in lines)
                {
                    if (line != "")
                    {
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
            else
            {
                // TODO: We need to make sure blocks don't stack
                Random random = new Random();
                var count = random.Next(12, 25);

                for (var i = 0; i < count; i += 1)
                {
                    var col = random.Next(0, 5);
                    var row = random.Next(2, 10);
                    TextureName color = (TextureName)random.Next(6, 10);

                    blocks.Add(new Block(col, row, color));
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
            for (int i = 0; i < paddle.health; i++)
            {
                spriteBatch.Draw(Store.textures.Get(TextureName.Heart), new Vector2(i * 63, GameWindow.HEIGHT - 64), Color.White);
            }

            ball.Draw(spriteBatch, spriteFont);
            paddle.Draw(spriteBatch, spriteFont);
            foreach (var block in blocks)
            {
                block.Draw(spriteBatch, spriteFont);
            }
            spriteBatch.DrawString(spriteFont, "Points: " + score.ToString(), new Vector2(GameWindow.WIDTH - 190, GameWindow.HEIGHT - 50), Color.Black);
            if (score >= 9999)
            {
                spriteBatch.DrawString(spriteFont, "You are the truest Breakout", new Vector2(50, GameWindow.HEIGHT / 2), Color.Black);
            }
        }

        public override string ToString()
        {
            return "This is the GameScene: " + ball.position.X;
        }
    }
}
