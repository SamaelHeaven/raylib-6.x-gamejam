using Beecon.Components;
using Beecon.Physics;
using Beecon.UI;

namespace Beecon.Prefabs;

public struct PlayerPrefab : IPrefab
{
    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(
            new BodyDef { Type = BodyType.Dynamic, LockAngularZ = true }
        );

        entity
            .SetZIndex(Visuals.Player.ZIndex)
            .Set(new Player())
            .Set(body)
            .Set(new Sprite { Scale = Visuals.Player.Size })
            .Set(
                new SpriteAnimation(
                    Visuals.Player.TextureAtlas.GetSpriteAnimationFrames(0, 3),
                    Visuals.Player.AnimationDelay
                )
            )
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
                    .Set(new UIHealthBar());
            });

        body.CreateShape(
            new ShapeDef { Filter = new ShapeFilter { Category = ShapeCategory.Player } },
            CircleShape.Make(Gameplay.Player.Radius)
        );

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter { Category = ShapeCategory.PlayerSensor },
            },
            CircleShape.Make(Gameplay.Player.SensorRadius)
        );
    }
}
