using Beecon.Components;
using Beecon.Physics;
using Beecon.Scenes;

namespace Beecon.Prefabs;

public struct ExperiencePrefab(float amount, ExperienceType type) : IPrefab
{
    public float Amount { get; set; } = amount;
    public ExperienceType Type { get; set; } = type;

    private record struct SpriteBatchSingleton(SpriteBatch SpriteBatch);

    public void Build(Entity entity)
    {
        if (!entity.Scene.TryGetSingleton(out SpriteBatchSingleton batchSingleton))
        {
            batchSingleton = new SpriteBatchSingleton(new SpriteBatch(Visuals.Experience.Texture));
            entity
                .Scene.Entity()
                .SetZIndex(Visuals.Experience.ZIndex)
                .Set(batchSingleton)
                .Set(batchSingleton.SpriteBatch);
        }

        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Static });

        entity
            .SetZIndex(Visuals.Experience.ZIndex)
            .Set(new Experience(Amount))
            .Set(body)
            .Set(
                new BatchedSprite(
                    batchSingleton.SpriteBatch,
                    new SpriteInstance
                    {
                        Source = Visuals.Experience.TextureAtlas.GetRegion((int)Type),
                        Scale = Visuals.Experience.Size,
                        Rotation = Random.Shared.Next(0, 360),
                    }
                )
            );

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
