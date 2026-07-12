using Beecon.Components;
using Beecon.Physics;
using Beecon.Scenes;

namespace Beecon.Prefabs;

public struct BeePrefab : IPrefab
{
    private record struct SpriteBatchSingleton(SpriteBatch SpriteBatch);

    private record struct AnimationSingleton(BatchedSpriteAnimation Animation);

    public void Build(Entity entity)
    {
        if (!entity.Scene.TryGetSingleton(out SpriteBatchSingleton batchSingleton))
        {
            batchSingleton = new SpriteBatchSingleton(new SpriteBatch(Visuals.Bee.Texture));
            entity
                .Scene.Entity()
                .SetZIndex(Visuals.Bee.ZIndex)
                .Set(batchSingleton)
                .Set(batchSingleton.SpriteBatch);
        }

        if (!entity.Scene.TryGetSingleton(out AnimationSingleton animationSingleton))
            animationSingleton = new AnimationSingleton(
                new BatchedSpriteAnimation(
                    Visuals.Bee.TextureAtlas.GetBatchedSpriteAnimationFrames(0, 3),
                    Visuals.Bee.AnimationDelay
                )
            );

        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Dynamic });

        entity
            .SetZIndex(Visuals.Bee.ZIndex)
            .SetRotation(-90)
            .Set(new Bee())
            .Set(body)
            .Set(animationSingleton.Animation)
            .Set(
                new BatchedSprite(
                    batchSingleton.SpriteBatch,
                    new SpriteInstance { Scale = Visuals.Bee.Size, Rotation = 90 }
                )
            )
            .Set(new Health(Gameplay.Bee.Health))
            .Set(
                new Damage(
                    Gameplay.Bee.Damage,
                    Gameplay.Bee.DamageCooldown,
                    ShapeCategory.Virus | ShapeCategory.BulletSensor
                )
            );

        body.CreateShape(
            new ShapeDef
            {
                Filter = new ShapeFilter
                {
                    Category = ShapeCategory.Bee,
                    Mask = ShapeCategory.All & ~ShapeCategory.Player & ~ShapeCategory.Virus,
                },
            },
            CircleShape.Make(Gameplay.Bee.Radius)
        );

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter { Category = ShapeCategory.BeeSensor },
            },
            CircleShape.Make(Gameplay.Bee.SensorRadius)
        );
    }
}
