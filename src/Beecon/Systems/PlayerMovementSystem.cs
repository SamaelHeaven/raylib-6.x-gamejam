using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Systems;

public sealed class PlayerMovementSystem : GameSystem
{
    private Vector2 _movement;

    public override void Update()
    {
        _movement.X = Inputs.HorizontalAxis.Value;
        _movement.Y = Inputs.VerticalAxis.Value;
        _movement = _movement.Normalize();
    }

    public override void FixedUpdate()
    {
        foreach (var (entity, player, body) in Entries<Player, Body>())
        {
            body.Seek(_movement * player.Stats.PlayerSpeed, Gameplay.Player.Acceleration);
            if (_movement == Vector2.Zero)
                continue;
            var target = MathF.Atan2(_movement.Y, _movement.X) * (180f / MathF.PI) + 90f;
            var sprite = entity.Get<Sprite>();
            sprite.Rotation = float.LerpAngle(
                sprite.Rotation,
                target,
                Gameplay.Player.RotationSmoothing
            );
        }
    }
}
