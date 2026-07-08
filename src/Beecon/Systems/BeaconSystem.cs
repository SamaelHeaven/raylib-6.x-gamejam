using Beecon.Components;
using Beecon.Physics;
using Beecon.Prefabs;

namespace Beecon.Systems;

public sealed class BeaconSystem : GameSystem
{
    public override void Initialize()
    {
        var bound = Gameplay.Map.Size / 2f - Gameplay.Beacon.Radius - Gameplay.Map.WallThickness;
        var filter = new ShapeFilter { Category = ShapeCategory.Beacon };
        var candidate = () => RandomInBounds(bound);
        for (var i = 0; i < Gameplay.Beacon.Count; i++)
            if (
                Scene.TryFindSpawnPosition(
                    candidate,
                    Gameplay.Beacon.Radius * 2f,
                    filter,
                    out var position,
                    Gameplay.Beacon.SpawnMaxAttempts
                )
            )
                new BeaconPrefab().Build(Scene.Entity().SetPosition(position));
    }

    public override void Update()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;
        var playerPosition = player.Position;
        foreach (var (entity, beacon, body) in Entries<Beacon, Body>())
        {
            if (beacon.Activated)
                continue;

            var timer = beacon.ChargeTimer;
            if (IsInside(body, playerPosition))
            {
                if (timer.Update())
                {
                    beacon.Activated = true;
                    player.Get<Player>().MaxBees += Gameplay.Beacon.MaxBeesBonus;
                }
            }
            else
            {
                Discharge(timer);
            }

            entity.Get<RegularPolygon>().Fill = beacon.Activated
                ? Visuals.Beacon.ActivatedColor
                : Color.Lerp(
                    Visuals.Beacon.DeactivatedColor,
                    Visuals.Beacon.ChargingColor,
                    beacon.Progress
                );
        }
    }

    private static void Discharge(Timer timer)
    {
        var rate = Gameplay.Beacon.ChargeDuration / Gameplay.Beacon.DischargeDuration;
        timer.Elapsed -= Time.Delta * rate;
        if (timer.Elapsed < TimeSpan.Zero)
            timer.Elapsed = TimeSpan.Zero;
    }

    private static bool IsInside(Body body, Vector2 point)
    {
        return body.Shapes.AsValueEnumerable().Any(shape => shape.TestPoint(point));
    }

    private static Vector2 RandomInBounds(Vector2 bound)
    {
        var rnd = Random.Shared;
        return new Vector2(
            (rnd.NextSingle() * 2f - 1f) * bound.X,
            (rnd.NextSingle() * 2f - 1f) * bound.Y
        );
    }
}
