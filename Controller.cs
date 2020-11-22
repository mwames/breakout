using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Controller
    {
        public bool inGame = false;
        public bool gameOver = false;
        public float totalPoints = 0f;

        public void update()
        {
            if (inGame)
            {
                totalPoints += 0;
            }
            else
            {
                var kState = Keyboard.GetState();
                var gState = GamePad.GetState(PlayerIndex.One);
                if (kState.IsKeyDown(Keys.Enter) || gState.IsButtonDown(Buttons.Start))
                {
                    inGame = true;
                }
            }
        }
    }
}
