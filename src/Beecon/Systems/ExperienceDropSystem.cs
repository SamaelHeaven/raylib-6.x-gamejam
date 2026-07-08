using Beecon.Components;
using Beecon.Prefabs;

namespace Beecon.Systems;

public sealed class ExperienceDropSystem : GameSystem
{
    public override void Update()
    {
        foreach (var (_, reward, health, body) in Entries<ExperienceReward, Health, Body>())
        {
            if (!health.IsDead)
                continue;
            new ExperiencePrefab(reward.Amount, reward.Color).Build(
                Scene.Entity().SetPosition(body.Position)
            );
        }
    }
}
