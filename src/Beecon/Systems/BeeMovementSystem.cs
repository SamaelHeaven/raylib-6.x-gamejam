using Beecon.Components;
using Beecon.Physics;
using Beecon.Scenes;

namespace Beecon.Systems;

public sealed class BeeMovementSystem : GameSystem
{
    private float BeeSpeed
    {
        get
        {
            var player = Scene.Player;
            return player.IsNull ? Gameplay.Bee.MaxSpeed : player.Get<Player>().Stats.BeeSpeed;
        }
    }

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
        var player = Scene.Player;
        if (
            (
                !player.IsNull
                && player.Get<Player>().Stats.AvailablePoints > 0
                && Scene.Hud.StatMenu.IsMouseInside
            ) || Inputs.BeeSpreadButton.IsDown
        )
            Spread();
        else
            FollowMouse();
        RotateTowardMouse();
    }

    private void FollowMouse()
    {
        var beeSpeed = BeeSpeed;
        var mouseWorldPosition = Mouse.WorldPosition;
        foreach (var (_, _, body) in Entries<Bee, Body>())
            body.Arrive(
                mouseWorldPosition,
                beeSpeed,
                Gameplay.Bee.Acceleration,
                Gameplay.Bee.ArrivalRadius
            );
    }

    private void RotateTowardMouse()
    {
        var mouseWorldPosition = Mouse.WorldPosition;
        foreach (var (_, _, body) in Entries<Bee, Body>())
        {
            var offset = mouseWorldPosition - body.Position;
            if (offset == Vector2.Zero)
                continue;
            var target = MathF.Atan2(offset.Y, offset.X) * (180f / MathF.PI);
            body.Rotation = float.LerpAngle(body.Rotation, target, Gameplay.Bee.RotationSmoothing);
        }
    }

    private void Spread()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;
        var playerPosition = player.Position;
        var beeSpeed = player.Get<Player>().Stats.BeeSpeed;
        var count = Scene.Count<Bee>();
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
                beeSpeed,
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
