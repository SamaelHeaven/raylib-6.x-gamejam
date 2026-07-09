using Beecon.Components;
using Beecon.Scenes;

namespace Beecon.Systems;

public sealed class StatSystem : GameSystem
{
    public override void Update()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;
        var stats = player.Get<Player>().Stats;
        ApplyHealth(player, stats);
        ApplyBeeDamage(stats);
    }

    private static void ApplyHealth(Entity player, Stats stats)
    {
        if (!player.TryGet(out Health health))
            return;
        var targetMax = stats.MaxHealth;
        if (targetMax > health.Max)
            health.Current += targetMax - health.Max;
        health.Max = targetMax;
        if (!health.IsDead && health.Current < health.Max)
            health.Current = (
                health.Current + stats.HealthRegen * (float)Time.Delta.TotalSeconds
            ).Min(health.Max);
    }

    private void ApplyBeeDamage(Stats stats)
    {
        var beeDamage = stats.BeeDamage;
        foreach (var (_, _, damage) in Entries<Bee, Damage>())
            damage.Amount = beeDamage;
    }
}
