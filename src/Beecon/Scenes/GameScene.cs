using Beecon.Prefabs;

namespace Beecon.Scenes;

public sealed class GameScene : BaseScene
{
    public override void Initialize()
    {
        // Background
        Scene.Entity().SetZIndex(-1).Set(new Grid(64, Color.Gray) { Scale = 10_000, Thick = 2 });

        // Player
        new PlayerPrefab().Build(Scene.Entity());

        // Bees
        for (var i = 0; i < 1; i++)
        {
            var rnd = Random.Shared;
            new BeePrefab().Build(
                Scene
                    .Entity()
                    .SetPosition(
                        new Vector2(rnd.Next((int)Display.Width), rnd.Next((int)Display.Height))
                        - Display.Size / 2
                    )
            );
        }
    }
}
