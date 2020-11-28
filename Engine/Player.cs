using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Player
    {
        public PlayerIndex Index { get; set; }
        public readonly InputState input;
        public Player(PlayerIndex index)
        {
            Index = index;
            input = new InputState(
                Keyboard.GetState(),
                Keyboard.GetState(),
                GamePad.GetState(Index),
                GamePad.GetState(Index),
                Mouse.GetState(),
                Mouse.GetState()
            );
        }

        public InputState getInput()
        {
            input.kPrevious = input.kCurrent;
            input.kCurrent = Keyboard.GetState();
            input.gPrevious = input.gCurrent;
            input.gCurrent = GamePad.GetState(Index);
            input.mPrevious = input.mCurrent;
            input.mCurrent = Mouse.GetState();
            return input;
        }
    }
}
