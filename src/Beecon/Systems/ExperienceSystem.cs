using Beecon.Components;

namespace Beecon.Systems;

public sealed class ExperienceSystem : GameSystem
{
    public override void WorldSensorBegin(Shape sensor, Shape visitor)
    {
        if (!sensor.Entity.TryGet(out Experience experience))
            return;
        if (!visitor.Entity.TryGet(out Player player))
            return;
        player.AddExperience(experience.Amount);
        sensor.Entity.Destroy();
    }
}
