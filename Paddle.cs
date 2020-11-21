using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breakout {
    public class Paddle {
        public int speed = 460;
        public Vector2 position = new Vector2(640, 360);
        public int health = 3;

        public void update(GameTime gameTime, Controller gameController) {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState kState = Keyboard.GetState();

            if (gameController.inGame) {
                if (kState.IsKeyDown(Keys.Right) && position.X + 50 < 745) {
                    position.X += speed * dt;
                }

                if (kState.IsKeyDown(Keys.Left) && position.X - 45 > 0) {
                    position.X -= speed * dt;
                }
            }
        }
    }
}
