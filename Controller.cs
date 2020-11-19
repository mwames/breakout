using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace breakout
{
    public class Controller
    {
        public bool inGame = false;
        public bool gameOver = false;
        public float totalPoints = 0f;

        public void conUpdate(GameTime gameTime)
        {
            if (inGame)
                totalPoints += 0;
            else
            {
                KeyboardState kState = Keyboard.GetState();
                if (kState.IsKeyDown(Keys.Enter))
                {
                    inGame = true;

                }

            }
        }
     }
 }
