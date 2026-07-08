using Beecon.Components;
using Beecon.Input;
using Beecon.Physics;

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

    public override void Configure()
    {
        Scene.OnRemove<Player>(
            (_, _) =>
            {
                foreach (var entity in Scene.Entities<Bee>())
                    entity.Destroy();
            }
        );
    }

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
            body.Arrive(mouseWorldPosition, MaxSpeed, Acceleration, ArrivalRadius);
    }

    private void Spread()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;
        var playerPosition = player.Position;
        var count = Scene.Table<Bee>().Count;
        var i = 0;
        foreach (
            var (_, _, body) in Entries<Bee, Body>()
                .AsValueEnumerable()
                .OrderBy(entry => entry.Item1.Index)
        )
        {
            var angle = 2f * MathF.PI / count * i;
            var targetPosition =
                playerPosition + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * SpreadRadius;
            body.Arrive(targetPosition, SpreadMaxSpeed, SpreadAcceleration, SpreadArrivalRadius);
            i++;
        }
    }
}
