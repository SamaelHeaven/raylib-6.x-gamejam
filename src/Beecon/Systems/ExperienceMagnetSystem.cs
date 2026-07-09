using Beecon.Components;

namespace Beecon.Systems;

public sealed class ExperienceMagnetSystem : GameSystem
{
    public override void FixedUpdate()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;
        var playerPosition = player.Position;
        var radius = Gameplay.Experience.MagnetRadius;
        foreach (var (_, _, body) in Entries<Experience, Body>())
        {
            var offset = playerPosition - body.Position;
            var distance = offset.Length();
            if (distance <= 0.01f || distance > radius)
                continue;
            var closeness = 1f - distance / radius;
            var speed =
                Gameplay.Experience.MagnetMinSpeed
                + (Gameplay.Experience.MagnetMaxSpeed - Gameplay.Experience.MagnetMinSpeed)
                    * closeness;
            var step = MathF.Min(speed * Time.FixedDeltaSeconds, distance);
            body.Position += offset / distance * step;
        }
    }
}
