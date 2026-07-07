using Beecon.Prefabs;

namespace Beecon.Scenes;

public sealed class GameScene : BaseScene
{
    public static Vector2 MapSize => 20_000;
    public static float WallThickness => 1000;

    public override void Initialize()
    {
        var halfMapSize = MapSize / 2f;

        // Background
        Scene.Entity().SetZIndex(-1).Set(new Grid(64, Color.Gray) { Scale = MapSize, Thick = 2 });

        // Player
        new PlayerPrefab().Build(Scene.Entity());

        // Edges
        new WallPrefab(new Vector2(MapSize.X, WallThickness)).Build(
            Scene.Entity().SetPosition(new Vector2(0, -halfMapSize.Y))
        );
        new WallPrefab(new Vector2(MapSize.X, WallThickness)).Build(
            Scene.Entity().SetPosition(new Vector2(0, halfMapSize.Y))
        );
        new WallPrefab(new Vector2(WallThickness, MapSize.Y)).Build(
            Scene.Entity().SetPosition(new Vector2(-halfMapSize.X, 0))
        );
        new WallPrefab(new Vector2(WallThickness, MapSize.Y)).Build(
            Scene.Entity().SetPosition(new Vector2(halfMapSize.X, 0))
        );
    }
}
