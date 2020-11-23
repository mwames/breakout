using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Player
    {
        public PlayerIndex Index { get; set; }

        public Player(PlayerIndex index)
        {
            Index = index;
        }

        public GamePadState getInput()
        {
            return GamePad.GetState(Index);
        }
    }
}
