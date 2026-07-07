using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct PlayerPrefab : IPrefab
{
    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Dynamic });

        entity
            .SetZIndex(1000)
            .Set(new Player())
            .Set(body)
            .Set(new Circle(Color.Red) { Scale = 50 })
            .Set(new Health(1_000));

        body.CreateShape(
            new ShapeDef { Filter = new ShapeFilter { Category = ShapeFilterCategory.Player } },
            CircleShape.Make(25)
        );
    }
}
