using Beecon.UI;

namespace Beecon.Components;

public sealed class Player
{
    public int MaxBees { get; set; } = Gameplay.Player.InitialMaxBees;

    public int Level { get; set; } = 1;

    public Stats Stats { get; } = new();

    public float Experience { get; set; }

    public float RequiredExperience => Gameplay.Experience.RequiredForLevel(Level);

    public float ExperiencePercent =>
        RequiredExperience <= 0f ? 0f : Experience / RequiredExperience * 100f;

    public void AddExperience(float amount)
    {
        Experience += amount;
        while (RequiredExperience > 0f && Experience >= RequiredExperience)
        {
            Experience -= RequiredExperience;
            Level++;
            Stats.GrantPoint();
        }
    }
}

public static class ScenePlayerExtensions
{
    extension(Scene scene)
    {
        public Entity Player => scene.Entities<Player>().AsValueEnumerable().FirstOrDefault();

        public UIHud Hud => scene.Components<UIHud>().AsValueEnumerable().First();
    }
}
