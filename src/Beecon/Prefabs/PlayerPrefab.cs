using Beecon.Components;
using Beecon.Physics;
using Beecon.UI;

namespace Beecon.Prefabs;

public struct PlayerPrefab : IPrefab
{
    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Dynamic });

        entity
            .SetZIndex(Visuals.Player.ZIndex)
            .Set(new Player())
            .Set(body)
            .Set(new Circle(Visuals.Player.Color) { Scale = Gameplay.Player.Radius * 2f })
            .Set(new Health(Gameplay.Player.Health))
            .Set(
                new Damage(
                    Gameplay.Player.Damage,
                    Gameplay.Player.DamageCooldown,
                    ShapeCategory.BulletSensor
                )
            )
            .Scope(scene =>
            {
                scene
                    .Entity()
                    .SetZIndex(Visuals.HealthBar.ZIndex)
                    .SetPosition(Visuals.HealthBar.Offset)
                    .Set(new HealthBar());
            });

        var shape = CircleShape.Make(Gameplay.Player.Radius);
        body.CreateShape(
            new ShapeDef { Filter = new ShapeFilter { Category = ShapeCategory.Player } },
            shape
        );

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter { Category = ShapeCategory.PlayerSensor },
            },
            shape
        );
    }
}
