using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct ExperiencePrefab(float amount, Color color) : IPrefab
{
    public float Amount { get; set; } = amount;
    public Color Color { get; set; } = color;

    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Static });

        entity
            .SetZIndex(Visuals.Experience.ZIndex)
            .Set(new Experience(Amount))
            .Set(body)
            .Set(new Circle(Color) { Scale = Gameplay.Experience.Radius * 2f });

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter
                {
                    Category = ShapeCategory.Experience,
                    Mask = ShapeCategory.Player,
                },
            },
            CircleShape.Make(Gameplay.Experience.Radius)
        );
    }
}
