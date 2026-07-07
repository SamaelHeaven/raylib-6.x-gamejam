using Beecon.Components;
using Beecon.Physics;
using Beecon.Prefabs;

namespace Beecon.Systems;

public sealed class BeeSpawnSystem : GameSystem
{
    private readonly Timer _spawnTimer = new(SpawnInterval);
    public static TimeSpan SpawnInterval => TimeSpan.FromSeconds(0.2);
    public static float SpawnRadius => 60;
    public static float SpawnClearanceRadius => 12;

    public override void Update()
    {
        if (!_spawnTimer.Update())
            return;
        var player = Scene.Player;
        if (player.IsNull)
            return;
        if (Scene.Table<Bee>().Count >= player.Get<Player>().MaxBees)
            return;
        var playerPosition = player.Position;
        var filter = new ShapeFilter { Category = ShapeFilterCategory.Bee };
        if (
            Scene.TryFindSpawnPosition(
                () => playerPosition + RandomInCircle(SpawnRadius),
                SpawnClearanceRadius,
                filter,
                out var position
            )
        )
            new BeePrefab().Build(Scene.Entity().SetPosition(position));
    }

    private static Vector2 RandomInCircle(float radius)
    {
        var rnd = Random.Shared;
        var angle = rnd.NextSingle() * MathF.Tau;
        var distance = MathF.Sqrt(rnd.NextSingle()) * radius;
        return new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * distance;
    }
}
