namespace Beecon.Components;

public sealed class ExperienceReward(float amount, ExperienceType type)
{
    public float Amount { get; } = amount;
    public ExperienceType Type { get; } = type;
}
