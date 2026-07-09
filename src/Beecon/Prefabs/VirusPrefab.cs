using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct VirusPrefab(VirusType type = VirusType.Normal, int mergeCount = 0) : IPrefab
{
    public VirusType Type { get; set; } = type;
    public int MergeCount { get; set; } = mergeCount;

    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(
            new BodyDef { Type = BodyType.Dynamic, LockAngularZ = true }
        );

        var radius = RadiusOf(Type, MergeCount);

        entity
            .SetZIndex(Visuals.Virus.ZIndex)
            .Set(new Virus { Type = Type, MergeCount = MergeCount })
            .Set(body)
            .Set(new Circle(ColorOf(Type)) { Scale = radius * Visuals.Virus.SizeScale })
            .Set(new Health(HealthOf(Type)))
            .Set(
                new Damage(
                    DamageOf(Type),
                    Gameplay.Virus.DamageCooldown,
                    ShapeCategory.Player | ShapeCategory.Bee
                )
            )
            .Set(new ExperienceReward(ExperienceOf(Type), ExperienceTypeOf(Type)));

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

        if (Type is VirusType.Turret or VirusType.Shield)
            entity.Scope(scene => new TurretPrefab().Build(scene.Entity()));

        if (Type is VirusType.Shield)
            BuildShield(entity, body);
    }

    private static void BuildShield(Entity entity, Body body)
    {
        var size = new Vector2(Gameplay.Virus.BarrierThickness, Gameplay.Virus.BarrierWidth);
        var offset = new Vector2(Gameplay.Virus.BarrierOffset, 0f);

        body.CreateShape(
            new ShapeDef
            {
                Density = Gameplay.Virus.BarrierDensity,
                Filter = new ShapeFilter
                {
                    Category = ShapeCategory.Shield,
                    Mask = ShapeCategory.Player | ShapeCategory.Bee,
                },
            },
            PolygonShape.MakeBox(size, offset, 0f)
        );

        var visual = Entity.Null;
        entity.Scope(scene =>
            visual = scene
                .Entity()
                .SetZIndex(Visuals.Virus.BarrierZIndex)
                .SetPosition(offset)
                .Set(new Rectangle(Visuals.Virus.BarrierColor) { Scale = size })
        );
        entity.Set(new Shield { Visual = visual });
    }

    private static float RadiusOf(VirusType type, int mergeCount)
    {
        return BaseRadiusOf(type) + mergeCount * Gameplay.Virus.MergeGrowth;
    }

    private static float BaseRadiusOf(VirusType type)
    {
        return type switch
        {
            VirusType.Shield => Gameplay.Virus.ShieldRadius,
            VirusType.Turret => Gameplay.Virus.TurretRadius,
            _ => Gameplay.Virus.Radius,
        };
    }

    private static float HealthOf(VirusType type)
    {
        return type switch
        {
            VirusType.Shield => Gameplay.Virus.ShieldHealth,
            VirusType.Turret => Gameplay.Virus.TurretHealth,
            _ => Gameplay.Virus.Health,
        };
    }

    private static float DamageOf(VirusType type)
    {
        return type switch
        {
            VirusType.Shield => Gameplay.Virus.ShieldDamage,
            VirusType.Turret => Gameplay.Virus.TurretDamage,
            _ => Gameplay.Virus.Damage,
        };
    }

    private static float ExperienceOf(VirusType type)
    {
        return type switch
        {
            VirusType.Shield => Gameplay.Virus.ShieldExperience,
            VirusType.Turret => Gameplay.Virus.TurretExperience,
            _ => Gameplay.Virus.ExperienceBonus,
        };
    }

    private static Color ColorOf(VirusType type)
    {
        return type switch
        {
            VirusType.Shield => Visuals.Virus.ShieldColor,
            VirusType.Turret => Visuals.Virus.TurretColor,
            _ => Visuals.Virus.Color,
        };
    }

    private static ExperienceType ExperienceTypeOf(VirusType type)
    {
        return type switch
        {
            VirusType.Shield => ExperienceType.Large,
            VirusType.Turret => ExperienceType.Medium,
            _ => ExperienceType.Small,
        };
    }
}
