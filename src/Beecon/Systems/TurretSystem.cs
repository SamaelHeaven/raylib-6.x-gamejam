using Beecon.Components;
using Beecon.Prefabs;
using Beecon.Scenes;

namespace Beecon.Systems;

public sealed class TurretSystem : GameSystem
{
    public override void Update()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;
        var playerPosition = player.Position;
        foreach (var (entity, turret) in Entries<Turret>())
        {
            if (!turret.FireTimer.Update())
                continue;
            var origin = entity.WorldPosition;
            var direction = (playerPosition - origin).Normalize();
            new BulletPrefab(direction * Gameplay.Bullet.Speed).Build(
                Scene.Entity().SetPosition(origin)
            );
        }
    }
}
