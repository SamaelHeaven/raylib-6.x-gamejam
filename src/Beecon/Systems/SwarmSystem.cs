using Beecon.Components;
using Beecon.Prefabs;

namespace Beecon.Systems;

public sealed class SwarmSystem : GameSystem
{
    private Swarm _swarm = null!;
    private bool _wasActive;

    public override void Initialize()
    {
        _swarm = new Swarm();
        Scene.Entity().Set(_swarm);
    }

    public override void Update()
    {
        _swarm.Elapsed += Time.Delta;
        var elapsed = _swarm.Elapsed.TotalSeconds;
        var interval = Gameplay.Swarm.Interval.TotalSeconds;
        var duration = Gameplay.Swarm.Duration.TotalSeconds;
        var isActive = elapsed >= interval && elapsed % interval < duration;
        if (_wasActive && !isActive)
            SpawnBoss();
        _wasActive = isActive;
        _swarm.IsActive = isActive;
    }

    private void SpawnBoss()
    {
        var camera = Scene.Camera;
        var half = Display.Size / 2f / camera.Zoom;
        var extentX = half.X + Gameplay.Boss.SpawnMargin + Gameplay.Boss.Radius;
        var extentY = half.Y + Gameplay.Boss.SpawnMargin + Gameplay.Boss.Radius;
        var angle = Random.Shared.NextSingle() * MathF.Tau;
        var ray = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
        var tx = MathF.Abs(ray.X) < 1e-5f ? float.PositiveInfinity : extentX / MathF.Abs(ray.X);
        var ty = MathF.Abs(ray.Y) < 1e-5f ? float.PositiveInfinity : extentY / MathF.Abs(ray.Y);
        var position = camera.Target + ray * MathF.Min(tx, ty);
        new VirusPrefab(VirusType.Shield, boss: true).Build(Scene.Entity().SetPosition(position));
    }
}
