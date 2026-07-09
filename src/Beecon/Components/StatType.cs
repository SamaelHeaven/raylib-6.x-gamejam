namespace Beecon.Components;

public enum StatType
{
    Speed,
    BeeSpeed,
    BeeDamage,
    BeeReload,
    Health,
    HealthRegen,
}

public static class StatTypeExtensions
{
    private static readonly StatType[] All = Enum.GetValues<StatType>();

    extension(StatType type)
    {
        public static StatType[] All => All;

        public string Label =>
            type switch
            {
                StatType.Speed => "Speed",
                StatType.BeeSpeed => "Bee Speed",
                StatType.BeeDamage => "Bee Damage",
                StatType.BeeReload => "Bee Reload",
                StatType.Health => "Health",
                StatType.HealthRegen => "Health Regen",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
            };

        public Color Color =>
            type switch
            {
                StatType.Speed => "#8EC5FF",
                StatType.BeeSpeed => "#BBF451",
                StatType.BeeDamage => "#FDA5D5",
                StatType.BeeReload => "#9AE630",
                StatType.Health => "#FFA1AD",
                StatType.HealthRegen => "#F4A8FF",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
            };

        public Key Key => (Key)((int)type + (int)Key.One);
    }
}
