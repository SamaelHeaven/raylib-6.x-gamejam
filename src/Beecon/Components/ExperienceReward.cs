namespace Beecon.Components;

public sealed class ExperienceReward(float amount, Color color)
{
    public float Amount { get; } = amount;
    public Color Color { get; } = color;
}
