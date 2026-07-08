using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Systems;

public sealed class BeeMovementSystem : GameSystem
{
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
            body.Arrive(
                mouseWorldPosition,
                Gameplay.Bee.MaxSpeed,
                Gameplay.Bee.Acceleration,
                Gameplay.Bee.ArrivalRadius
            );
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
            var targetPosition =
                playerPosition + HexagonSpreadOffset((float)i / count, Gameplay.Bee.SpreadRadius);
            body.Arrive(
                targetPosition,
                Gameplay.Bee.MaxSpeed,
                Gameplay.Bee.Acceleration,
                Gameplay.Bee.ArrivalRadius
            );
            i++;
        }
    }

    private static Vector2 HexagonSpreadOffset(float t, float radius)
    {
        const int sides = 6;
        var scaled = t * sides;
        var edgeIndex = (int)scaled;
        var edgeT = scaled - edgeIndex;
        var angleA = MathF.Tau / sides * edgeIndex;
        var angleB = MathF.Tau / sides * ((edgeIndex + 1) % sides);
        var vertexA = new Vector2(MathF.Cos(angleA), MathF.Sin(angleA)) * radius;
        var vertexB = new Vector2(MathF.Cos(angleB), MathF.Sin(angleB)) * radius;
        return vertexA + (vertexB - vertexA) * edgeT;
    }
}
