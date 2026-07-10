using Beecon.Components;
using Beecon.Scenes;

namespace Beecon.Systems;

public sealed class PowerUpSystem : GameSystem
{
    public override void WorldSensorBegin(Shape sensor, Shape visitor)
    {
        if (!sensor.Entity.TryGet(out PowerUp powerUp))
            return;
        if (!visitor.Entity.TryGet(out Player player))
            return;
        Apply(powerUp.Type, visitor.Entity, player);
        sensor.Entity.Destroy();
    }

    private void Apply(PowerUpType type, Entity playerEntity, Player player)
    {
        switch (type)
        {
            case PowerUpType.Health:
                if (playerEntity.TryGet(out Health health))
                    health.Restore();
                break;
            case PowerUpType.Magnet:
                player.ActivateMagnet();
                break;
            case PowerUpType.Nuke:
                foreach (var (_, _, virusHealth) in Entries<Virus, Health>())
                    virusHealth.Damage(Gameplay.PowerUp.NukeDamage);
                Scene.Announce("BOOM!");
                break;
        }
    }
}
