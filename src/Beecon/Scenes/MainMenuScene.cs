using Beecon.Prefabs;
using Beecon.Systems;
using Beecon.UI;

namespace Beecon.Scenes;

public sealed class MainMenuScene : GameSystem
{
    private const int BeeCount = 6;

    public Camera BackgroundCamera = new();

    public static Scene Build()
    {
        return Scene.Build<MainMenuScene>(() =>
            [
                new MatrixRainSystem(),
                new BackgroundSystem(),
                new BeeMovementSystem(),
                new PhysicsSystem { Order = 1 },
            ]
        );
    }

    public override void Initialize()
    {
        Scene.System<BackgroundSystem>().Camera = BackgroundCamera;

        Scene.Entity().SetPosition(Display.Size / 2).Set(new UIMainMenu());

        var center = Display.Size / 2 + new Vector2(0, -168);
        for (var i = 0; i < BeeCount; i++)
        {
            var angle = MathF.Tau / BeeCount * i;
            var offset =
                new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * Gameplay.Bee.SpreadRadius;
            new BeePrefab().Build(Scene.Entity().SetPosition(center + offset));
        }
    }

    public override void Update()
    {
        BackgroundCamera.Target -= 100 * Time.DeltaSeconds;
    }
}
