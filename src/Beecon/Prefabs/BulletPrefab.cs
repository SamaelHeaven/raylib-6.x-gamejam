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
            .SetZIndex(Visuals.Bullet.ZIndex)
            .Set(new Bullet())
            .Set(body)
            .Set(new Circle(Visuals.Bullet.Color) { Scale = Gameplay.Bullet.Radius * 2f })
            .Set(new Health(Gameplay.Bullet.Health))
            .Set(
                new Damage(
                    Gameplay.Bullet.Damage,
                    Gameplay.Bullet.DamageCooldown,
                    ShapeCategory.Player | ShapeCategory.Bee
                )
            );

        var shape = CircleShape.Make(Gameplay.Bullet.Radius);

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter { Category = ShapeCategory.BulletSensor },
            },
            shape
        );

        body.LinearVelocity = Velocity;
    }
}
