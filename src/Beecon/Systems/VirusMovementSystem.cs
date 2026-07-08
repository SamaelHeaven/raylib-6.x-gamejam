using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Systems;

public sealed class VirusMovementSystem : GameSystem
{
    public override void FixedUpdate()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;
        var playerPosition = player.Position;
        foreach (var (entity, _, body) in Entries<Virus, Body>())
        {
            var offset = playerPosition - body.Position;
            var direction = offset.Normalize();
            body.Seek(direction * Gameplay.Virus.MaxSpeed, Gameplay.Virus.Acceleration);
            if (direction == Vector2.Zero)
                continue;
            body.Rotation = MathF.Atan2(direction.Y, direction.X) * (180f / MathF.PI);
            if (!entity.TryGet(out Shield shield) || !shield.Visual.IsValid)
                continue;
            var visual = shield.Visual;
            visual.Position = direction * Gameplay.Virus.BarrierOffset;
        }
    }
}
