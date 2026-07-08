using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct BeaconPrefab : IPrefab
{
    public const int Sides = 6;
    public static float Radius => 150f;
    public static Color DeactivatedColor => Color.DarkGray;
    public static Color ActivatedColor => Color.SkyBlue;

    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Static });

        entity
            .SetZIndex(100)
            .Set(new Beacon())
            .Set(body)
            .Set(new RegularPolygon(Sides, DeactivatedColor) { Scale = Radius * 2f });

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter { Category = ShapeCategory.Beacon },
            },
            PolygonShape.Make(HexagonVertices(Radius), 0f)
        );
    }

    private static Vector2[] HexagonVertices(float radius)
    {
        var vertices = new Vector2[Sides];
        for (var i = 0; i < Sides; i++)
        {
            var angle = MathF.Tau / Sides * i;
            vertices[i] = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * radius;
        }

        return vertices;
    }
}
