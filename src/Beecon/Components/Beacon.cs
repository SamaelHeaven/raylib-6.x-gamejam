namespace Beecon.Components;

public sealed class Beacon
{
    public static TimeSpan ChargeDuration => TimeSpan.FromSeconds(3);

    public Timer ChargeTimer { get; } = new(ChargeDuration, cycleCount: 1);
    public bool Activated { get; set; }
    public float Progress => ChargeTimer.Progress;
}
