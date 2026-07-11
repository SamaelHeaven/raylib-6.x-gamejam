using Beecon.Components;
using Beecon.Physics;
using Beecon.Prefabs;
using Beecon.Scenes;

namespace Beecon.Systems;

public sealed class VirusSpawnSystem : GameSystem
{
    private readonly Timer _spawnTimer = new(Gameplay.Virus.SpawnInterval);

    public override void Update()
    {
        var swarm = Scene.Swarm;
        var elapsed = swarm?.Elapsed ?? TimeSpan.Zero;
        _spawnTimer.Duration = Gameplay.Virus.SpawnIntervalAt(elapsed);
        if (!_spawnTimer.Update())
            return;
        var isSwarm = swarm?.IsActive ?? false;
        var spawnCount = Gameplay.Virus.SpawnCountAt(elapsed, isSwarm);
        var maxCount = Gameplay.Virus.MaxCountAt(elapsed, isSwarm);
        var camera = Scene.Camera;
        var half = Display.Size / 2f / camera.Zoom;
        var extentX = half.X + Gameplay.Virus.SpawnMargin;
        var extentY = half.Y + Gameplay.Virus.SpawnMargin;
        var filter = new ShapeFilter { Category = ShapeCategory.Virus };
        var heading = PlayerHeading();
        var candidate = () => camera.Target + EdgePoint(extentX, extentY, heading);
        for (var spawned = 0; spawned < spawnCount; spawned++)
        {
            if (Scene.Count<Virus>() >= maxCount)
                return;
            if (
                Scene.TryFindSpawnPosition(
                    candidate,
                    Gameplay.Virus.SpawnClearanceRadius,
                    filter,
                    out var position
                )
            )
                new VirusPrefab().Build(Scene.Entity().SetPosition(position));
        }
    }

    private Vector2? PlayerHeading()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return null;
        var velocity = player.Get<Body>().LinearVelocity;
        return velocity.Length() > 10f ? velocity.Normalize() : null;
    }

    private static Vector2 EdgePoint(float extentX, float extentY, Vector2? heading)
    {
        var rnd = Random.Shared;
        float angle;
        if (heading is { } direction)
        {
            var sign = rnd.Next(2) == 0 ? 1f : -1f;
            var offset = MathF.Pow(rnd.NextSingle(), Gameplay.Virus.SpawnBias) * MathF.PI;
            angle = MathF.Atan2(direction.Y, direction.X) + sign * offset;
        }
        else
        {
            angle = (rnd.NextSingle() * 2f - 1f) * MathF.PI;
        }

        var ray = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
        var tx = MathF.Abs(ray.X) < 1e-5f ? float.PositiveInfinity : extentX / MathF.Abs(ray.X);
        var ty = MathF.Abs(ray.Y) < 1e-5f ? float.PositiveInfinity : extentY / MathF.Abs(ray.Y);
        return ray * MathF.Min(tx, ty);
    }
}
