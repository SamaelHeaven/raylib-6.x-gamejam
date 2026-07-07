using Beecon.Components;
using Beecon.Input;

namespace Beecon.Systems;

public sealed class PlayerMovementSystem : GameSystem
{
    private Vector2 _movement;
    private static float Acceleration => 5f;
    private static float MaxSpeed => 225f;

    public override void Update()
    {
        _movement.X = Inputs.HorizontalAxis.Value;
        _movement.Y = Inputs.VerticalAxis.Value;
        _movement = _movement.Normalize();
    }

    public override void FixedUpdate()
    {
        foreach (var (_, _, body) in Entries<Player, Body>())
        {
            var desiredVelocity = _movement * MaxSpeed;
            var currentVelocity = body.LinearVelocity;
            var velocityChange = desiredVelocity - currentVelocity;
            var force = velocityChange * Acceleration;
            body.ApplyForce(force);
        }
    }
}
