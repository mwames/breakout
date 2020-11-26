namespace Breakout
{
    public enum Mode {
        Normal,
        Debug,
        Editor
    }

    public static class ModeManager {
        public static Mode currentMode = Mode.Normal;

        public static void Toggle(Mode mode) {
            if (mode == Mode.Normal || currentMode == mode)
                currentMode = Mode.Normal;
            else
                currentMode = mode;
        }
    }
}
