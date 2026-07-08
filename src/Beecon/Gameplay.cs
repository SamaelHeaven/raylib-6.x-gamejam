namespace Beecon;

public static class Gameplay
{
    public static class Map
    {
        public static Vector2 Size => 20_000f;
        public static float WallThickness => 1_000f;
    }

    public static class Player
    {
        public static float Radius => 25f;
        public static float Health => 1_000f;
        public static float Damage => 50f;
        public static TimeSpan DamageCooldown => TimeSpan.FromMilliseconds(200);
        public static int InitialMaxBees => 4;
        public static float Acceleration => 5f;
        public static float MaxSpeed => 225f;
    }

    public static class Bee
    {
        public static float Radius => 10f;
        public static float Health => 150f;
        public static float Damage => 5f;
        public static TimeSpan DamageCooldown => TimeSpan.FromMilliseconds(200);
        public static float Acceleration => 8f;
        public static float MaxSpeed => 325f;
        public static float ArrivalRadius => Radius;
        public static float SpreadRadius => 120f;
        public static TimeSpan SpawnInterval => TimeSpan.FromSeconds(0.2);
        public static float SpawnRadius => 60f;
        public static float SpawnClearanceRadius => Radius;
    }

    public static class Virus
    {
        public static float Radius => 14f;
        public static float Health => 50f;
        public static float Damage => 15f;
        public static float TurretRadius => 50f;
        public static float TurretHealth => 250f;
        public static float TurretDamage => 40f;
        public static TimeSpan DamageCooldown => TimeSpan.FromMilliseconds(200);
        public static float Acceleration => 5f;
        public static float MaxSpeed => 75f;
        public static int MaxCount => 50;
        public static int SpawnCount => 5;
        public static TimeSpan SpawnInterval => TimeSpan.FromSeconds(0.5);
        public static float SpawnMargin => 96f;
        public static float SpawnClearanceRadius => Radius;
        public static float DespawnDistance => 1_200f;
        public static float MergeGrowth => 4f;
        public static int MergesPerPromotion => 5;
        public static TimeSpan MergeInterval => TimeSpan.FromSeconds(1);
    }

    public static class Turret
    {
        public static TimeSpan FireInterval => TimeSpan.FromSeconds(0.8);
    }

    public static class Bullet
    {
        public static float Radius => 6f;
        public static float Health => 10f;
        public static float Damage => 100f;
        public static TimeSpan DamageCooldown => TimeSpan.FromMilliseconds(200);
        public static float Speed => 450f;
        public static float DespawnMargin => 64f;
    }

    public static class Beacon
    {
        public static int Count => 120;
        public static int Sides => 6;
        public static float Radius => 150f;
        public static TimeSpan ChargeDuration => TimeSpan.FromSeconds(3);
        public static TimeSpan DischargeDuration => TimeSpan.FromSeconds(2);
        public static int MaxBeesBonus => 4;
        public static int SpawnMaxAttempts => 32;
    }
}
