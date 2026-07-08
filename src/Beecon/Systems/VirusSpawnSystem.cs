using Beecon.Components;
using Beecon.Physics;
using Beecon.Prefabs;

namespace Beecon.Systems;

public sealed class VirusSpawnSystem : GameSystem
{
    private readonly Timer _spawnTimer = new(SpawnInterval);
    private static int MaxViruses => 50;
    private static int SpawnCount => 5;
    private static TimeSpan SpawnInterval => TimeSpan.FromSeconds(0.5);
    private static float SpawnMargin => 96;
    private static float SpawnClearanceRadius => 16;

    public override void Update()
    {
        if (!_spawnTimer.Update())
            return;
        var camera = Scene.Camera;
        var half = Display.Size / 2f / camera.Zoom;
        var extentX = half.X + SpawnMargin;
        var extentY = half.Y + SpawnMargin;
        var filter = new ShapeFilter { Category = ShapeCategory.Virus };
        for (var spawned = 0; spawned < SpawnCount; spawned++)
        {
            if (Scene.Table<Virus>().Count >= MaxViruses)
                return;
            if (
                Scene.TryFindSpawnPosition(
                    () => camera.Target + EdgePoint(extentX, extentY),
                    SpawnClearanceRadius,
                    filter,
                    out var position
                )
            )
                new VirusPrefab().Build(Scene.Entity().SetPosition(position));
        }
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
            _ => new Vector2(extentX, y),
        };
    }
}
