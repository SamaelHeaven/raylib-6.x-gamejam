using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct PowerUpPrefab(PowerUpType type) : IPrefab
{
    public PowerUpType Type { get; set; } = type;

    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Static });

        entity
            .SetZIndex(Visuals.PowerUp.ZIndex)
            .Set(new PowerUp(Type))
            .Set(body)
            .Set(new Sprite(TextureOf(Type)) { Scale = Visuals.PowerUp.Size });

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter
                {
                    Category = ShapeCategory.PowerUp,
                    Mask = ShapeCategory.Player,
                },
            },
            CircleShape.Make(Gameplay.PowerUp.Radius)
        );
    }

    private static Texture TextureOf(PowerUpType type)
    {
        return type switch
        {
            PowerUpType.Health => Visuals.PowerUp.HealthTexture,
            PowerUpType.Magnet => Visuals.PowerUp.MagnetTexture,
            _ => Visuals.PowerUp.NukeTexture,
        };
    }
}
