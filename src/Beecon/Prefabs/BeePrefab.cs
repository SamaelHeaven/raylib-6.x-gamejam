using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct BeePrefab : IPrefab
{
    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Dynamic });

        body.CreateShape(
            new ShapeDef
            {
                Filter = new ShapeFilter
                {
                    Category = ShapeFilterCategory.Bee,
                    Mask = ShapeFilterCategory.Bee,
                },
            },
            CircleShape.Make(10)
        );

        entity
            .SetZIndex(1500)
            .Set(new Bee())
            .Set(body)
            .Set(new Circle(Color.Yellow) { Scale = 20 });
    }
}
