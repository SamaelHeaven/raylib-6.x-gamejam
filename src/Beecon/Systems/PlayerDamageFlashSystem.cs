using Beecon.Components;
using Beecon.Scenes;

namespace Beecon.Systems;

public sealed class PlayerDamageFlashSystem : GameSystem
{
    private readonly Tween _flash = new(
        Visuals.Player.DamageFlashInterval,
        cycleCount: Visuals.Player.DamageFlashCycles,
        alternateDirection: true
    );

    private bool _flashing;
    private bool _initialized;
    private float _previousHealth;

    public override void Update()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;

        var health = player.Get<Health>();
        var sprite = player.Get<Sprite>();

        if (_initialized && health.Current < _previousHealth)
        {
            _flashing = true;
            _flash.Reset();
        }

        _previousHealth = health.Current;
        _initialized = true;

        if (!_flashing)
            return;

        _flash.Update();
        if (_flash.IsCompleted)
        {
            _flashing = false;
            sprite.Tint = Color.White;
            return;
        }

        sprite.Tint = _flash.Interpolate(
            Visuals.Player.DamageFlashFrom,
            Visuals.Player.DamageFlashTo,
            Ease.InOutQuint
        );
    }
}
