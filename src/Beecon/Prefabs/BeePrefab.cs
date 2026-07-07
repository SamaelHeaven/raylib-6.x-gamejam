using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct BeePrefab : IPrefab
{
    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Dynamic });

        entity
            .SetZIndex(1500)
            .Set(new Bee())
            .Set(body)
            .Set(new Circle(Color.Yellow) { Scale = 20 })
            .Set(new Health(150))
            .Set(new Damage(5, TimeSpan.FromMilliseconds(200), ShapeFilterCategory.Virus));

        var shape = CircleShape.Make(10);
        body.CreateShape(
            new ShapeDef
            {
                Filter = new ShapeFilter
                {
                    Category = ShapeFilterCategory.Bee,
                    Mask =
                        ShapeFilterCategory.DefaultMask
                        & ~ShapeFilterCategory.Player
                        & ~ShapeFilterCategory.Virus,
                },
            },
            shape
        );

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter { Category = ShapeFilterCategory.BeeSensor },
            },
            shape
        );
    }
}
