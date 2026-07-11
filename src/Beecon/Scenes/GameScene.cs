using Beecon.Prefabs;
using Beecon.Systems;

namespace Beecon.Scenes;

public sealed class GameScene : GameSystem
{
    public static Scene Build()
    {
        return Scene.Build<GameScene>(() =>
            [
                new PlayerMovementSystem(),
                new CameraSystem(),
                new BackgroundSystem(),
                new BeeMovementSystem(),
                new BeeSpawnSystem(),
                new BeaconSystem(),
                new MatrixRainSystem(),
                new SwarmSystem(),
                new VirusMovementSystem(),
                new VirusSpawnSystem(),
                new VirusDespawnSystem(),
                new VirusMergeSystem(),
                new TurretSystem(),
                new BulletDespawnSystem(),
                new DamageSystem(),
                new ExperienceSystem(),
                new ExperienceMagnetSystem(),
                new ExperienceDropSystem(),
                new PowerUpSystem(),
                new StatSystem(),
                new HealthSystem(),
                new PlayerDamageFlashSystem(),
                new AnnouncementSystem(),
                new GameOverSystem(),
            ]
        );
    }

    public override void Initialize()
    {
        var mapSize = Gameplay.Map.Size;
        var halfMapSize = mapSize / 2f;
        var thickness = Gameplay.Map.WallThickness;

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
