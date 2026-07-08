namespace Beecon.Components;

public sealed class Turret
{
    public static TimeSpan FireInterval => TimeSpan.FromSeconds(0.8);

    public Timer FireTimer { get; } = new(FireInterval);
}
