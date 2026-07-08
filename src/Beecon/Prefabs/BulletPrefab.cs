using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct BulletPrefab(Vector2 velocity) : IPrefab
{
    public Vector2 Velocity { get; set; } = velocity;

    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(
            new BodyDef { Type = BodyType.Dynamic, IsBullet = true }
        );

        entity
            .SetZIndex(1200)
            .Set(new Bullet())
            .Set(body)
            .Set(new Circle(Color.Orange) { Scale = 12 })
            .Set(new Health(10))
            .Set(
                new Damage(
                    100,
                    TimeSpan.FromMilliseconds(200),
                    ShapeFilterCategory.Player | ShapeFilterCategory.Bee
                )
            );

        var shape = CircleShape.Make(6);

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter { Category = ShapeFilterCategory.BulletSensor },
            },
            shape
        );

        body.LinearVelocity = Velocity;
    }
}
