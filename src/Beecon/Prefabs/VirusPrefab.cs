using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct VirusPrefab(bool big = false) : IPrefab
{
    public bool Big { get; set; } = big;

    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Dynamic });

        var radius = Big ? 100f : 14f;
        var health = Big ? 250f : 50f;
        var damage = Big ? 40f : 15f;
        var color = Big ? Color.DarkGreen : Color.Green;

        entity
            .SetZIndex(1300)
            .Set(new Virus { CanMerge = !Big })
            .Set(body)
            .Set(new Circle(color) { Scale = radius * 2f })
            .Set(new Health(health))
            .Set(
                new Damage(
                    damage,
                    TimeSpan.FromMilliseconds(200),
                    ShapeCategory.Player | ShapeCategory.Bee
                )
            );

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

        if (Big)
            entity.Scope(scene => new TurretPrefab().Build(scene.Entity()));
    }
}
