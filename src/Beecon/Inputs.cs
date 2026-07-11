namespace Beecon;

public static class Inputs
{
    #region Axes

    public static InputAxis HorizontalAxis { get; } =
        new() { NegativeKeys = [Key.A, Key.Left], PositiveKeys = [Key.D, Key.Right] };

    public static InputAxis VerticalAxis { get; } =
        new() { NegativeKeys = [Key.W, Key.Up], PositiveKeys = [Key.S, Key.Down] };

    #endregion

    #region Buttons

    public static InputButton BeeSpreadButton { get; } =
        new() { MouseButtons = [MouseButton.Left, MouseButton.Right] };

    public static InputButton FullscreenButton { get; } = new() { Keys = [Key.Tab] };
    public static InputButton ExitButton { get; } = new() { Keys = [Key.Escape] };
    public static InputButton PauseButton { get; } = new() { Keys = [Key.P] };
    public static InputButton DebugModifierButton { get; } = new() { Keys = [Key.LeftShift] };
    public static InputButton DebugToggleButton { get; } = new() { Keys = [Key.One] };
    public static InputButton DebugRestartButton { get; } = new() { Keys = [Key.Two] };

    #endregion
}
