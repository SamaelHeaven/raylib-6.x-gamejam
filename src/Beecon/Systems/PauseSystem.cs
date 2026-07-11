using Beecon.UI;

namespace Beecon.Systems;

public sealed class PauseSystem : GameSystem
{
    private ValueHashSet<Type> _alwaysEnabled;
    private bool _isPaused;

    public override void Initialize()
    {
        _alwaysEnabled = Ecs
            .Systems.Invoke()
            .AsValueEnumerable()
            .Select(system => system.GetType())
            .ToValueHashSet();
        _alwaysEnabled.Add(typeof(PauseSystem));
    }

    public override void Update()
    {
        if (!Inputs.PauseButton.IsPressed)
            return;
        _isPaused = !_isPaused;
        if (_isPaused)
            Scene
                .Entity()
                .SetZIndex(Visuals.Hud.ZIndex)
                .SetPosition(Display.Size / 2 + new Vector2(0, -100))
                .Set(new UIPaused());
        else
            foreach (var entity in Scene.Entities<UIPaused>())
                entity.Destroy();
        foreach (var system in Scene.Systems<GameSystem>())
        {
            if (_alwaysEnabled.Contains(system.GetType()))
                continue;
            system.IsDisabled = _isPaused;
        }
    }
}
