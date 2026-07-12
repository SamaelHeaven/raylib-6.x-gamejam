using Beecon.Components;
using Beecon.Prefabs;

namespace Beecon.Systems;

public sealed class ExperienceDropSystem : GameSystem
{
    private static readonly PowerUpType[] PowerUpTypes = Enum.GetValues<PowerUpType>();

    public override void Update()
    {
        foreach (var (_, reward, health, body) in Entries<ExperienceReward, Health, Body>())
        {
            if (!health.IsDead)
                continue;
            if (Random.Shared.NextSingle() < Gameplay.PowerUp.DropChance)
            {
                new PowerUpPrefab(RandomPowerUp()).Build(Scene.Entity().SetPosition(body.Position));
            }
            else
            {
                new ExperiencePrefab(reward.Amount, reward.Type).Build(
                    Scene.Entity().SetPosition(body.Position)
                );
                DespawnOldestIfExceeded();
            }
        }
    }

    private void DespawnOldestIfExceeded()
    {
        if (Scene.Count<Experience>() <= Gameplay.Experience.MaxCount)
            return;
        foreach (var (entity, _) in Entries<Experience>())
        {
            entity.Destroy();
            break;
        }
    }

    private static PowerUpType RandomPowerUp()
    {
        return PowerUpTypes[Random.Shared.Next(PowerUpTypes.Length)];
    }
}
