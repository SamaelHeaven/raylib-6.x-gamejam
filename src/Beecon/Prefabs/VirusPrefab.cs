using Beecon.Components;
using Beecon.Physics;
using Beecon.Scenes;

namespace Beecon.Prefabs;

public struct VirusPrefab(
    VirusType type = VirusType.Normal,
    int mergeCount = 0,
    int strength = 0,
    bool boss = false
) : IPrefab
{
    public VirusType Type { get; set; } = type;
    public int MergeCount { get; set; } = mergeCount;
    public int Strength { get; set; } = strength;
    public bool IsBoss { get; set; } = boss;

    private static BatchedSpriteAnimationFrame[] AnimationFrames =>
        field ??= Visuals.Virus.TextureAtlas.GetBatchedSpriteAnimationFrames(0, 3).ToArray();

    private static BatchedSpriteAnimationFrame[] ShieldAnimationFrames =>
        field ??= Visuals.Shield.TextureAtlas.GetBatchedSpriteAnimationFrames(0, 2).ToArray();

    private record struct SpriteBatchSingleton(SpriteBatch SpriteBatch);

    private record struct ShieldSpriteBatchSingleton(SpriteBatch SpriteBatch);

    public void Build(Entity entity)
    {
        if (!entity.Scene.TryGetSingleton(out SpriteBatchSingleton batchSingleton))
        {
            batchSingleton = new SpriteBatchSingleton(new SpriteBatch(Visuals.Virus.Texture));
            entity
                .Scene.Entity()
                .SetZIndex(Visuals.Virus.ZIndex)
                .Set(batchSingleton)
                .Set(batchSingleton.SpriteBatch);
        }

        var body = entity.Scene.World.CreateBody(
            new BodyDef { Type = BodyType.Dynamic, LockAngularZ = true }
        );

        var radius = IsBoss ? Gameplay.Boss.Radius : RadiusOf(Type, MergeCount);

        entity
            .SetZIndex(Visuals.Virus.ZIndex)
            .Set(
                new Virus
                {
                    Type = Type,
                    MergeCount = MergeCount,
                    Strength = Strength,
                }
            )
            .Set(body)
            .Set(
                new BatchedSprite(
                    batchSingleton.SpriteBatch,
                    new SpriteInstance
                    {
                        Tint = IsBoss ? Visuals.Boss.Tint : ColorOf(Type),
                        Scale = radius * Visuals.Virus.SizeScale,
                    }
                )
            )
            .Set(new BatchedSpriteAnimation(AnimationFrames, Visuals.Virus.AnimationDelay))
            .Set(new Health(IsBoss ? Gameplay.Boss.Health : HealthOf(Strength)))
            .Set(
                new Damage(
                    IsBoss ? Gameplay.Boss.Damage : DamageOf(Strength),
                    Gameplay.Virus.DamageCooldown,
                    ShapeCategory.Player | ShapeCategory.Bee
                )
            )
            .Set(
                new ExperienceReward(
                    IsBoss ? Gameplay.Boss.ExperienceBonus : ExperienceOf(Type),
                    IsBoss ? ExperienceType.Large : ExperienceTypeOf(Type)
                )
            );

        if (IsBoss)
            entity.Set(new Boss());

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

        if (IsBoss)
            BuildBossTurrets(entity);
        else if (Type is VirusType.Turret or VirusType.Shield)
            entity.Scope(scene => scene.Entity().Set(new Turret()));

        if (IsBoss || Type is VirusType.Shield)
            BuildShield(entity, body, IsBoss);
    }

    private static void BuildBossTurrets(Entity entity)
    {
        var count = Gameplay.Boss.TurretCount;
        for (var i = 0; i < count; i++)
        {
            var position =
                new Vector2(MathF.Cos(MathF.Tau / count * i), MathF.Sin(MathF.Tau / count * i))
                * Gameplay.Boss.TurretRingRadius;
            entity.Scope(scene => scene.Entity().SetPosition(position).Set(new Turret()));
        }
    }

    private static void BuildShield(Entity entity, Body body, bool boss)
    {
        var thickness = boss ? Gameplay.Boss.BarrierThickness : Gameplay.Virus.BarrierThickness;
        var width = boss ? Gameplay.Boss.BarrierWidth : Gameplay.Virus.BarrierWidth;
        var barrierOffset = boss ? Gameplay.Boss.BarrierOffset : Gameplay.Virus.BarrierOffset;
        var density = boss ? Gameplay.Boss.BarrierDensity : Gameplay.Virus.BarrierDensity;
        var visualSize = boss ? Visuals.Boss.ShieldSize : Visuals.Shield.Size;

        var size = new Vector2(thickness, width);
        var offset = new Vector2(barrierOffset, 0f);

        body.CreateShape(
            new ShapeDef
            {
                Density = density,
                Filter = new ShapeFilter
                {
                    Category = ShapeCategory.Shield,
                    Mask = ShapeCategory.Player | ShapeCategory.Bee,
                },
            },
            PolygonShape.MakeBox(size, offset, 0f)
        );

        if (!entity.Scene.TryGetSingleton(out ShieldSpriteBatchSingleton batchSingleton))
        {
            batchSingleton = new ShieldSpriteBatchSingleton(
                new SpriteBatch(Visuals.Shield.Texture)
            );
            entity
                .Scene.Entity()
                .SetZIndex(Visuals.Shield.ZIndex)
                .Set(batchSingleton)
                .Set(batchSingleton.SpriteBatch);
        }

        var visual = Entity.Null;
        entity.Scope(scene =>
            visual = scene
                .Entity()
                .SetZIndex(Visuals.Shield.ZIndex)
                .SetPosition(offset)
                .Set(
                    new BatchedSprite(
                        batchSingleton.SpriteBatch,
                        new SpriteInstance { Scale = visualSize }
                    )
                )
                .Set(
                    new BatchedSpriteAnimation(ShieldAnimationFrames, Visuals.Shield.AnimationDelay)
                )
        );
        entity.Set(new Shield { Visual = visual, Offset = barrierOffset });
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

    private static float HealthOf(int strength)
    {
        return Gameplay.Virus.Health + strength * Gameplay.Virus.HealthPerMerge;
    }

    private static float DamageOf(int strength)
    {
        return Gameplay.Virus.Damage + strength * Gameplay.Virus.DamagePerMerge;
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
            VirusType.Shield => Visuals.Virus.ShieldTint,
            VirusType.Turret => Visuals.Virus.TurretTint,
            _ => Visuals.Virus.Tint,
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
