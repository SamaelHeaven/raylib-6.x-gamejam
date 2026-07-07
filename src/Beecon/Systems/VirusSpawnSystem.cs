using Beecon.Components;
using Beecon.Physics;
using Beecon.Prefabs;

namespace Beecon.Systems;

public sealed class VirusSpawnSystem : GameSystem
{
    private readonly Timer _spawnTimer = new(SpawnInterval);
    public static int MaxViruses => 50;
    public static TimeSpan SpawnInterval => TimeSpan.FromSeconds(0.5);
    public static float SpawnMargin => 96;
    public static float SpawnClearanceRadius => 16;
    public static int MaxSpawnAttempts => 16;

    public override void Update()
    {
        _spawnTimer.Duration = SpawnInterval;
        if (!_spawnTimer.Update())
            return;
        if (Scene.Table<Virus>().Count >= MaxViruses)
            return;
        if (TryFindSpawnPosition(out var position))
            new VirusPrefab().Build(Scene.Entity().SetPosition(position));
    }

    private bool TryFindSpawnPosition(out Vector2 position)
    {
        var camera = Scene.Camera;
        var half = Display.Size / 2f / camera.Zoom;
        var extentX = half.X + SpawnMargin;
        var extentY = half.Y + SpawnMargin;
        var world = Scene.World;
        var filter = new ShapeFilter
        {
            Category = ShapeFilterCategory.Virus
        };
        for (var attempt = 0; attempt < MaxSpawnAttempts; attempt++)
        {
            position = camera.Target + EdgePoint(extentX, extentY);
            var blocked = false;
            world.Overlap(
                CircleShape.Make(position, SpawnClearanceRadius),
                _ =>
                {
                    blocked = true;
                    return false;
                },
                filter
            );

            if (!blocked)
                return true;
        }

        position = default;
        return false;
    }

    private static Vector2 EdgePoint(float extentX, float extentY)
    {
        var rnd = Random.Shared;
        var x = (rnd.NextSingle() * 2f - 1f) * extentX;
        var y = (rnd.NextSingle() * 2f - 1f) * extentY;
        return rnd.Next(4) switch
        {
            0 => new Vector2(x, -extentY),
            1 => new Vector2(x, extentY),
            2 => new Vector2(-extentX, y),
            _ => new Vector2(extentX, y)
        };
    }
}
