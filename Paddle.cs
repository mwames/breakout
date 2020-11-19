using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace breakout
{
    class Paddle
    {
        public int speed = 460;
        static public Vector2 defaultPosition = new Vector2(640, 360);
        public Vector2 position = defaultPosition;
        private int health = 3;
        
        

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

      

        public void paddleUpdate(GameTime gameTime, Controller gameController)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;    

            KeyboardState kState = Keyboard.GetState();

            if (gameController.inGame)
            {

                if (kState.IsKeyDown(Keys.Right) && position.X + 50 < 745)
                {
                    position.X += speed * dt;

                }
                if (kState.IsKeyDown(Keys.Left) && position.X - 45 > 0)
                {
                    position.X -= speed * dt;
                }

            }
         
            
        }
    }
}
