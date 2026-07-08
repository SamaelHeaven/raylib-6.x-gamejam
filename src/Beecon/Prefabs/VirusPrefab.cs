using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct VirusPrefab(VirusType type = VirusType.Normal, int mergeCount = 0) : IPrefab
{
    public VirusType Type { get; set; } = type;
    public int MergeCount { get; set; } = mergeCount;

    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Dynamic });

        var radius = RadiusOf(Type, MergeCount);
        var color = Type == VirusType.Normal ? Visuals.Virus.Color : Visuals.Virus.TurretColor;

        entity
            .SetZIndex(Visuals.Virus.ZIndex)
            .Set(new Virus { Type = Type, MergeCount = MergeCount })
            .Set(body)
            .Set(new Circle(color) { Scale = radius * 2f })
            .Set(new Health(HealthOf(Type)))
            .Set(
                new Damage(
                    DamageOf(Type),
                    Gameplay.Virus.DamageCooldown,
                    ShapeCategory.Player | ShapeCategory.Bee
                )
            )
            .Set(new ExperienceReward(ExperienceOf(Type), ExperienceColorOf(Type)));

        var shape = CircleShape.Make(radius);
        body.CreateShape(
            new ShapeDef
            {
                Filter = new ShapeFilter
                {
                    Category = ShapeCategory.Virus,
                    Mask = ShapeCategory.All & ~ShapeCategory.Player & ~ShapeCategory.Bee,
                },
            },
            shape
        );

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter { Category = ShapeCategory.VirusSensor },
            },
            shape
        );

        if (Type == VirusType.Turret)
            entity.Scope(scene => new TurretPrefab().Build(scene.Entity()));
    }

    private static float RadiusOf(VirusType type, int mergeCount)
    {
        return type switch
        {
            VirusType.Turret => Gameplay.Virus.TurretRadius,
            _ => Gameplay.Virus.Radius + mergeCount * Gameplay.Virus.MergeGrowth,
        };
    }

    private static float HealthOf(VirusType type)
    {
        return type switch
        {
            VirusType.Turret => Gameplay.Virus.TurretHealth,
            _ => Gameplay.Virus.Health,
        };
    }

    private static float DamageOf(VirusType type)
    {
        return type switch
        {
            VirusType.Turret => Gameplay.Virus.TurretDamage,
            _ => Gameplay.Virus.Damage,
        };
    }

    private static float ExperienceOf(VirusType type)
    {
        return type switch
        {
            VirusType.Turret => Gameplay.Virus.TurretExperience,
            _ => Gameplay.Virus.ExperienceBonus,
        };
    }

    private static Color ExperienceColorOf(VirusType type)
    {
        return type switch
        {
            VirusType.Turret => Visuals.Experience.TurretColor,
            _ => Visuals.Experience.Color,
        };
    }
}
