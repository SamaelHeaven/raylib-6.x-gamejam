using Beecon.Components;
using Beecon.Scenes;

namespace Beecon.Systems;

public sealed class ExperienceMagnetSystem : GameSystem
{
    public override void FixedUpdate()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;
        var state = player.Get<Player>();
        if (state.MagnetActive)
            state.MagnetRemaining -= Time.FixedDelta;
        var active = state.MagnetActive;
        var playerPosition = player.Position;
        var radius = Gameplay.Experience.MagnetRadius;
        var activeRadius = Gameplay.Experience.MagnetActiveRadius;

        var activeSpeed = Gameplay.Experience.MagnetMinSpeed;
        if (active)
        {
            var elapsed = (float)
                (Gameplay.PowerUp.MagnetDuration - state.MagnetRemaining).TotalSeconds;
            activeSpeed = MathF.Min(
                Gameplay.Experience.MagnetMaxSpeed,
                Gameplay.Experience.MagnetMinSpeed
                    + Gameplay.Experience.MagnetAcceleration * elapsed
            );
        }

        foreach (var (_, _, body) in Entries<Experience, Body>())
        {
            var offset = playerPosition - body.Position;
            var distance = offset.Length();
            if (distance <= 0.01f)
                continue;
            float speed;
            if (active && distance <= activeRadius)
            {
                speed = activeSpeed;
            }
            else if (!active && distance <= radius)
            {
                var closeness = 1f - distance / radius;
                speed =
                    Gameplay.Experience.MagnetMinSpeed
                    + (Gameplay.Experience.MagnetMaxSpeed - Gameplay.Experience.MagnetMinSpeed)
                        * closeness;
            }
            else
            {
                continue;
            }

            var step = MathF.Min(speed * Time.FixedDeltaSeconds, distance);
            body.Position += offset / distance * step;
        }
    }
}
