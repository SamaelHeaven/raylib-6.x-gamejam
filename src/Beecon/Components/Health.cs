namespace Beecon.Components;

public sealed class Health
{
    public Health(float max)
    {
        Current = max;
        Max = max;
    }

    public float Current { get; set; }
    public float Max { get; set; }

    public bool IsDead => Current <= 0;

    public void Damage(float amount)
    {
        Current -= amount;
    }
}
