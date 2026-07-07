namespace Beecon.Components;

public sealed class Damage
{
    public Damage(float amount, TimeSpan cooldown, ShapeFilterCategory targetMask)
    {
        Amount = amount;
        Cooldown = cooldown;
        TargetMask = targetMask;
    }

    public float Amount { get; set; }
    public TimeSpan Cooldown { get; set; }
    public ShapeFilterCategory TargetMask { get; set; }
}
