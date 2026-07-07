namespace Beecon.Input;

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
        new() { MouseButtons = [MouseButton.Right] };
    public static InputButton FullscreenButton { get; } = new() { Keys = [Key.Tab] };
    public static InputButton ExitButton { get; } = new() { Keys = [Key.Escape] };
    public static InputButton DebugButton { get; } = new() { Keys = [Key.F1] };
    public static InputButton RestartButton { get; } = new() { Keys = [Key.F2] };

    #endregion
}
