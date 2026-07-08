namespace Beecon.Systems;

public sealed class DebugSystem : GameSystem
{
    private PhysicsSystem PhysicsSystem => field ??= Scene.System<PhysicsSystem>();

    public bool IsDebugEnabled
    {
        get;
        set
        {
            field = value;
            PhysicsSystem.IsDebugDrawEnabled = field;
        }
    } = false;

    public override void Update()
    {
#if DEBUG
        if (!Inputs.DebugModifierButton.IsDown)
            return;
        if (Inputs.DebugToggleButton.IsPressed)
            IsDebugEnabled = !IsDebugEnabled;
        if (Inputs.DebugRestartButton.IsPressed)
            Game.Scene = new Scene(Scene.SystemsFunc);
#endif
    }

    public override void PostRender()
    {
        if (IsDebugEnabled)
            Renderer.Graphics.FillText($"FPS: {Time.AverageFps}", 0, 0);
    }
}
