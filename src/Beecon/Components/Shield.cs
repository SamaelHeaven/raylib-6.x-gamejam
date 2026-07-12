namespace Beecon.Components;

public sealed class Shield
{
    public Entity Visual { get; set; }
    public float Offset { get; set; }
    public Health Health { get; set; } = new(0f);
    public Shape Barrier { get; set; }
}
