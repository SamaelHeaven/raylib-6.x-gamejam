using Beecon.Components;
using Beecon.Physics;
using Beecon.Scenes;

namespace Beecon.Prefabs;

public struct BulletPrefab(Vector2 velocity) : IPrefab
{
    public Vector2 Velocity { get; set; } = velocity;

    private static BatchedSpriteAnimationFrame[] AnimationFrames =>
        field ??= Visuals.Bullet.TextureAtlas.GetBatchedSpriteAnimationFrames(0, 1).ToArray();

    private record struct SpriteBatchSingleton(SpriteBatch SpriteBatch);

    public void Build(Entity entity)
    {
        if (!entity.Scene.TryGetSingleton(out SpriteBatchSingleton batchSingleton))
        {
            batchSingleton = new SpriteBatchSingleton(new SpriteBatch(Visuals.Bullet.Texture));
            entity
                .Scene.Entity()
                .SetZIndex(Visuals.Bullet.ZIndex)
                .Set(batchSingleton)
                .Set(batchSingleton.SpriteBatch);
        }

        var body = entity.Scene.World.CreateBody(
            new BodyDef { Type = BodyType.Dynamic, IsBullet = true }
        );

        entity
            .SetZIndex(Visuals.Bullet.ZIndex)
            .Set(new Bullet())
            .Set(body)
            .Set(
                new BatchedSprite(
                    batchSingleton.SpriteBatch,
                    new SpriteInstance
                    {
                        Scale = Visuals.Bullet.Size,
                        Position = Velocity.Normalize() * Visuals.Bullet.BackOffset,
                        Rotation =
                            MathF.Atan2(Velocity.Y, Velocity.X) * (180f / MathF.PI)
                            + Visuals.Bullet.RotationOffset,
                    }
                )
            )
            .Set(new BatchedSpriteAnimation(AnimationFrames, Visuals.Bullet.AnimationDelay))
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
