using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breakout {
    public struct Player {
        public PlayerIndex Index { get; set; }

        public Player(PlayerIndex index) {
            Index = index;
        }
    }

    public delegate GamePadState GetInputFunc(Player player);
    public static class PlayerOps {
        public static GamePadState getInput(Player player) {
            return GamePad.GetState(player.Index);
        }
    }
}
