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
            .SetZIndex(1000)
            .Set(new Player())
            .Set(body)
            .Set(new Circle(Color.Gold) { Scale = 50 })
            .Set(new Health(1_000))
            .Set(new Damage(50, TimeSpan.FromMilliseconds(200), ShapeFilterCategory.BulletSensor))
            .Scope(scene =>
            {
                scene.Entity().SetZIndex(1).SetPosition(new Vector2(0, 40)).Set(new HealthBar());
            });

        var shape = CircleShape.Make(25);
        body.CreateShape(
            new ShapeDef { Filter = new ShapeFilter { Category = ShapeFilterCategory.Player } },
            shape
        );

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter { Category = ShapeFilterCategory.PlayerSensor },
            },
            shape
        );
    }
}
