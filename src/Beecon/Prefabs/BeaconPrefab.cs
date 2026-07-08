using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct BeaconPrefab : IPrefab
{
    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Static });

        entity
            .SetZIndex(Visuals.Beacon.ZIndex)
            .Set(new Beacon())
            .Set(body)
            .Set(
                new RegularPolygon(Gameplay.Beacon.Sides, Visuals.Beacon.DeactivatedColor)
                {
                    Scale = Gameplay.Beacon.Radius * 2f,
                }
            );

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter { Category = ShapeCategory.Beacon },
            },
            PolygonShape.Make(HexagonVertices(Gameplay.Beacon.Radius), 0f)
        );
    }

    private static Vector2[] HexagonVertices(float radius)
    {
        var vertices = new Vector2[Gameplay.Beacon.Sides];
        for (var i = 0; i < Gameplay.Beacon.Sides; i++)
        {
            var angle = MathF.Tau / Gameplay.Beacon.Sides * i;
            vertices[i] = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * radius;
        }

        return vertices;
    }
}
