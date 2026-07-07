using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct VirusPrefab : IPrefab
{
    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Dynamic });

        entity
            .SetZIndex(1300)
            .Set(new Virus())
            .Set(body)
            .Set(new Circle(Color.Green) { Scale = 28 })
            .Set(new Health(50))
            .Set(new Damage(15, TimeSpan.FromMilliseconds(200), ShapeFilterCategory.Player | ShapeFilterCategory.Bee));

        var shape = CircleShape.Make(14);
        body.CreateShape(
            new ShapeDef
            {
                Filter = new ShapeFilter
                {
                    Category = ShapeFilterCategory.Virus,
                    Mask = ShapeFilterCategory.DefaultMask & ~ShapeFilterCategory.Player & ~ShapeFilterCategory.Bee
                }
            },
            shape
        );

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter { Category = ShapeFilterCategory.VirusSensor }
            },
            shape
        );
    }
}
