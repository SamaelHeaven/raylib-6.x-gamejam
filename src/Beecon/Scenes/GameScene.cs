using Beecon.Prefabs;

namespace Beecon.Scenes;

public sealed class GameScene : GameSystem
{
    public override void Initialize()
    {
        var mapSize = Gameplay.Map.Size;
        var halfMapSize = mapSize / 2f;
        var thickness = Gameplay.Map.WallThickness;

        // Background
        Scene
            .Entity()
            .SetZIndex(Visuals.Background.ZIndex)
            .Set(
                new Grid(Visuals.Background.CellSize, Visuals.Background.Color)
                {
                    Scale = mapSize,
                    Thick = Visuals.Background.Thickness,
                }
            );

        // Player
        new PlayerPrefab().Build(Scene.Entity());

        // HUD
        new HudPrefab().Build(Scene.Entity());

        // Edges
        new WallPrefab(new Vector2(mapSize.X, thickness)).Build(
            Scene.Entity().SetPosition(new Vector2(0, -halfMapSize.Y))
        );
        new WallPrefab(new Vector2(mapSize.X, thickness)).Build(
            Scene.Entity().SetPosition(new Vector2(0, halfMapSize.Y))
        );
        new WallPrefab(new Vector2(thickness, mapSize.Y)).Build(
            Scene.Entity().SetPosition(new Vector2(-halfMapSize.X, 0))
        );
        new WallPrefab(new Vector2(thickness, mapSize.Y)).Build(
            Scene.Entity().SetPosition(new Vector2(halfMapSize.X, 0))
        );
    }
}
