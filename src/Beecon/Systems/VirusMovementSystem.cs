using Beecon.Components;
using Beecon.Physics;
using Beecon.Scenes;

namespace Beecon.Systems;

public sealed class VirusMovementSystem : GameSystem
{
    public override void FixedUpdate()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;
        var playerPosition = player.Position;
        foreach (var (entity, virus, body) in Entries<Virus, Body>())
        {
            var offset = playerPosition - body.Position;
            var direction = offset.Normalize();
            var speed = entity.TryGet(out Boss _) ? Gameplay.Boss.MaxSpeed : SpeedOf(virus.Type);
            body.Seek(direction * speed, Gameplay.Virus.Acceleration);
            if (direction == Vector2.Zero)
                continue;
            body.Rotation = MathF.Atan2(direction.Y, direction.X) * (180f / MathF.PI);
            if (!entity.TryGet(out Shield shield) || !shield.Visual.IsValid)
                continue;
            var visual = shield.Visual;
            visual.Position = direction * shield.Offset;
        }
    }

    private static float SpeedOf(VirusType type)
    {
        return type switch
        {
            VirusType.Shield => Gameplay.Virus.ShieldMaxSpeed,
            VirusType.Turret => Gameplay.Virus.TurretMaxSpeed,
            _ => Gameplay.Virus.MaxSpeed,
        };
    }
}
