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
    }
}
