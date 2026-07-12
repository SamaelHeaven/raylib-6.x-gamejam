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
        public static float Radius => 18f;
        public static float SensorRadius => 18f;
        public static float Health => 1_000f;
        public static float Damage => 50f;
        public static TimeSpan DamageCooldown => TimeSpan.FromMilliseconds(200);
        public static int InitialMaxBees => 6;
        public static float Acceleration => 5f;
        public static float MaxSpeed => 225f;
        public static float RotationSmoothing => 0.15f;
    }

    public static class Bee
    {
        public static float Radius => 10f;
        public static float SensorRadius => 20f;
        public static float Health => 160f;
        public static float Damage => 6f;
        public static TimeSpan DamageCooldown => TimeSpan.FromMilliseconds(170);
        public static float Acceleration => 8f;
        public static float MaxSpeed => 325f;
        public static float ArrivalRadius => Radius;
        public static float SpreadRadius => 120f;
        public static float RotationSmoothing => 0.2f;
        public static TimeSpan SpawnInterval => TimeSpan.FromSeconds(0.2);
        public static float SpawnRadius => 60f;
        public static float SpawnClearanceRadius => Radius;
    }

    public static class Virus
    {
        public static float Radius => 10f;
        public static float Health => 40f;
        public static float Damage => 12f;
        public static float HealthPerMerge => 25f;
        public static float DamagePerMerge => 5f;
        public static float TurretRadius => 30f;
        public static float ShieldRadius => 50f;
        public static TimeSpan DamageCooldown => TimeSpan.FromMilliseconds(200);
        public static float Acceleration => 5f;
        public static float MaxSpeed => 75f;
        public static float TurretMaxSpeed => 95f;
        public static float ShieldMaxSpeed => 120f;
        public static float SpeedMassReference => 0.3f;
        public static float SpeedMassPenalty => 0.07f;
        public static float MinSpeedFactor => 0.45f;
        public static TimeSpan SpawnInterval => TimeSpan.FromSeconds(0.25);
        public static TimeSpan MinSpawnInterval => TimeSpan.FromSeconds(0.1);
        public static float SpawnIntervalDecayPerMinute => 0.02f;
        public static float SpawnMargin => 96f;
        public static float SpawnBias => 2.5f;
        public static int BaseSpawnCount => 1;
        public static float SpawnCountPerMinute => 1.45f;
        public static int BaseMaxCount => 12;
        public static float MaxCountPerMinute => 18f;
        public static int AbsoluteMaxCount => 600;
        public static float SpawnClearanceRadius => Radius;
        public static float DespawnDistance => 1_400f;
        public static float MergeGrowth => 4f;
        public static int MergesPerPromotion => 5;
        public static TimeSpan MergeInterval => TimeSpan.FromSeconds(0.4);
        public static int TurretMergesPerPromotion => 3;
        public static TimeSpan TurretMergeInterval => TimeSpan.FromSeconds(1.0);
        public static float ExperienceBonus => 5f;
        public static float TurretExperience => 25f;
        public static float ShieldExperience => 60f;
        public static float BarrierThickness => 10f;
        public static float BarrierWidth => ShieldRadius * 2.4f;
        public static float BarrierOffset => ShieldRadius + BarrierThickness * 1.5f;
        public static float BarrierDensity => 1_000f;
        public static float ShieldHealth => 1_300f;

        public static float SpeedFactor(float mass)
        {
            var factor =
                1f
                - SpeedMassPenalty
                    * MathF.Log(MathF.Max(mass, SpeedMassReference) / SpeedMassReference);
            return Math.Clamp(factor, MinSpeedFactor, 1f);
        }

        public static TimeSpan SpawnIntervalAt(TimeSpan elapsed)
        {
            var seconds =
                SpawnInterval.TotalSeconds - SpawnIntervalDecayPerMinute * elapsed.TotalMinutes;
            return TimeSpan.FromSeconds(Math.Max(MinSpawnInterval.TotalSeconds, seconds));
        }

        public static int SpawnCountAt(TimeSpan elapsed, bool swarm)
        {
            var count = BaseSpawnCount + SpawnCountPerMinute * (float)elapsed.TotalMinutes;
            if (swarm)
                count *= Swarm.SpawnMultiplierAt(elapsed);
            return Math.Max(1, (int)count);
        }

        public static int MaxCountAt(TimeSpan elapsed, bool swarm)
        {
            var max = BaseMaxCount + MaxCountPerMinute * (float)elapsed.TotalMinutes;
            if (swarm)
                max *= Swarm.SpawnMultiplierAt(elapsed);
            return Math.Min(AbsoluteMaxCount, (int)max);
        }
    }

    public static class Swarm
    {
        public static TimeSpan Interval => TimeSpan.FromMinutes(2);
        public static TimeSpan Duration => TimeSpan.FromSeconds(30);
        public static float BaseSpawnMultiplier => 2.5f;
        public static float SpawnMultiplierPerMinute => 0.2f;
        public static float MaxSpawnMultiplier => 12f;

        public static float SpawnMultiplierAt(TimeSpan elapsed)
        {
            var multiplier =
                BaseSpawnMultiplier + SpawnMultiplierPerMinute * (float)elapsed.TotalMinutes;
            return MathF.Min(MaxSpawnMultiplier, multiplier);
        }
    }

    public static class Boss
    {
        public static float Radius => 110f;
        public static float Health => 1_300f;
        public static float Damage => 120f;
        public static float MaxSpeed => 140f;
        public static float ExperienceBonus => 1_000f;
        public static int TurretCount => 6;
        public static float TurretRingRadius => Radius * 0.65f;
        public static float BarrierThickness => 16f;
        public static float BarrierWidth => Radius * 3f;
        public static float BarrierOffset => Radius + BarrierThickness * 1.5f;
        public static float BarrierDensity => 2_000f;
        public static float ShieldHealth => 5_000f;
        public static float SpawnMargin => 120f;
    }

    public static class Turret
    {
        public static TimeSpan FireInterval => TimeSpan.FromSeconds(1.0);
    }

    public static class Bullet
    {
        public static float Radius => 4f;
        public static float Health => 10f;
        public static float Damage => 52f;
        public static TimeSpan DamageCooldown => TimeSpan.FromMilliseconds(200);
        public static float Speed => 450f;
        public static float DespawnMargin => Virus.DespawnDistance;
    }

    public static class Experience
    {
        public static float Radius => 2f;
        public static float BaseRequired => 20f;
        public static float RequiredGrowth => 1.5f;
        public static float MagnetRadius => 62f;
        public static float MagnetActiveRadius => 3_000f;
        public static float MagnetMinSpeed => 75f;
        public static float MagnetMaxSpeed => 400f;
        public static float MagnetAcceleration => 600f;
        public static int MaxCount => 6_000;

        public static float RequiredForLevel(int level)
        {
            return BaseRequired * MathF.Pow(RequiredGrowth, level - 1);
        }
    }

    public static class Stats
    {
        public static int MaxLevel => 8;

        public static float PlayerSpeed(int level)
        {
            return Player.MaxSpeed + 25f * level;
        }

        public static float BeeSpeed(int level)
        {
            return Bee.MaxSpeed + 25f * level;
        }

        public static float BeeDamage(int level)
        {
            return Bee.Damage + 2f * level;
        }

        public static TimeSpan BeeReload(int level)
        {
            return TimeSpan.FromSeconds(
                Math.Max(Bee.SpawnInterval.TotalSeconds / (1f + level * 0.12f), 0.05f)
            );
        }

        public static float PlayerHealth(int level)
        {
            return Player.Health + 250f * level;
        }

        public static float HealthRegen(int level)
        {
            return 6f * level;
        }
    }

    public static class PowerUp
    {
        public static float DropChance => 0.0026f;
        public static float Radius => 12f;
        public static TimeSpan MagnetDuration => TimeSpan.FromSeconds(4.5);
        public static float NukeDamage => 500f;
    }

    public static class Beacon
    {
        public static int Count => 160;
        public static int Sides => 6;
        public static float Radius => 150f;
        public static TimeSpan ChargeDuration => TimeSpan.FromSeconds(3);
        public static TimeSpan DischargeDuration => TimeSpan.FromSeconds(2);
        public static int MinBeesBonus => 2;
        public static int MaxBeesBonus => 10;
        public static int SpawnMaxAttempts => 32;
    }

    public static class Audio
    {
        public static TimeSpan HitInterval => TimeSpan.FromMilliseconds(60);
        public static float HitPitchVariation => 0.15f;
        public static float ShootPitchVariation => 0.15f;
        public static float ShootVolume => 0.18f;
        public static float ExperiencePitchVariation => 0.15f;
        public static float ExperienceVolume => 0.3f;
    }
}
