using Beecon.Components;
using Beecon.Input;

namespace Beecon.Systems;

public sealed class BeeMovementSystem : GameSystem
{
    private static float Acceleration => 8f;
    private static float MaxSpeed => 325f;
    private static float ArrivalRadius => 10f;

    private static float SpreadAcceleration => Acceleration;
    private static float SpreadMaxSpeed => MaxSpeed;
    private static float SpreadArrivalRadius => ArrivalRadius;
    private static float SpreadRadius => 120f;

    public override void FixedUpdate()
    {
        if (Inputs.BeeSpreadButton.IsDown)
            Spread();
        else
            FollowMouse();
    }

    private void FollowMouse()
    {
        var mouseWorldPosition = Mouse.WorldPosition;
        foreach (var (_, _, body) in Entries<Bee, Body>())
        {
            var offset = mouseWorldPosition - body.Position;
            var distance = offset.Length();
            var speedScale = MathF.Min(distance / ArrivalRadius, 1f);
            var desiredVelocity =
                distance > 0.01f ? offset / distance * MaxSpeed * speedScale : Vector2.Zero;
            var velocityChange = desiredVelocity - body.LinearVelocity;
            body.ApplyForce(velocityChange * Acceleration);
        }
    }

    private void Spread()
    {
        var playerPosition = Scene.PlayerEntity.Get<Body>().Position;
        var count = Scene.Table<Bee>().Count;
        var i = 0;
        foreach (var (_, _, body) in Entries<Bee, Body>().OrderBy(entry => entry.Item1.Index))
        {
            var angle = 2f * MathF.PI / count * i;
            var targetPosition =
                playerPosition + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * SpreadRadius;
            var offset = targetPosition - body.Position;
            var distance = offset.Length();
            var speedScale = MathF.Min(distance / SpreadArrivalRadius, 1f);
            var desiredVelocity =
                distance > 0.01f ? offset / distance * SpreadMaxSpeed * speedScale : Vector2.Zero;
            var velocityChange = desiredVelocity - body.LinearVelocity;
            body.ApplyForce(velocityChange * SpreadAcceleration);
            i++;
        }
    }
}
