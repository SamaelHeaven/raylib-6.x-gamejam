using Beecon.Physics;

namespace Beecon.Prefabs;

public struct WallPrefab(Vector2 size) : IPrefab
{
    public Vector2 Size { get; set; } = size;

    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Static });

        entity.SetZIndex(500).Set(body).Set(new Rectangle(Color.DarkGray) { Scale = Size });

        body.CreateShape(
            new ShapeDef { Filter = new ShapeFilter { Category = ShapeCategory.Wall } },
            PolygonShape.MakeBox(Size)
        );
    }
}
