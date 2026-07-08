using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct BeePrefab : IPrefab
{
    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Dynamic });

        entity
            .SetZIndex(Visuals.Bee.ZIndex)
            .Set(new Bee())
            .Set(body)
            .Set(new Circle(Visuals.Bee.Color) { Scale = Gameplay.Bee.Radius * 2f })
            .Set(new Health(Gameplay.Bee.Health))
            .Set(
                new Damage(
                    Gameplay.Bee.Damage,
                    Gameplay.Bee.DamageCooldown,
                    ShapeCategory.Virus | ShapeCategory.BulletSensor
                )
            );

        var shape = CircleShape.Make(Gameplay.Bee.Radius);
        body.CreateShape(
            new ShapeDef
            {
                Filter = new ShapeFilter
                {
                    Category = ShapeCategory.Bee,
                    Mask = ShapeCategory.All & ~ShapeCategory.Player & ~ShapeCategory.Virus,
                },
            },
            shape
        );

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter { Category = ShapeCategory.BeeSensor },
            },
            shape
        );
    }
}
