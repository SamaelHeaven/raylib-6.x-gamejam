namespace Beecon.Components;

public sealed class Virus
{
    public static readonly VirusType[] Progression =
    [
        VirusType.Normal,
        VirusType.Turret,
        VirusType.Shield,
    ];

    public VirusType Type { get; set; }

    public int MergeCount { get; set; }

    public bool CanMerge => Type != Progression[^1];

    public VirusType NextType()
    {
        var index = Array.IndexOf(Progression, Type);
        return index >= 0 && index + 1 < Progression.Length ? Progression[index + 1] : Type;
    }
}
