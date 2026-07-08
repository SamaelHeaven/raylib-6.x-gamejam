using Beecon.Input;
using Beecon.Scenes;
using Beecon.Systems;

var scene = Scene.Build<GameScene>();

var config = Config
    .Builder()
    .Display(display =>
    {
        display.Title = "Beecon";
        display.Size = (720, 720);
        display.RenderingMode = RenderingMode.Buffer();
    })
    .Input(input =>
    {
        input.FullscreenButton = Inputs.FullscreenButton;
        input.ExitButton = Inputs.ExitButton;
    })
    .Drawing(drawing =>
    {
        drawing.DefaultCulling = true;
    })
    .World(world =>
    {
        world.DefaultGravity = Vector2.Zero;
    })
    .Systems(() =>
        [
            new DrawableSystem(),
            new UISystem(),
            new PhysicsSystem { Order = 1 },
            new PlayerMovementSystem(),
            new CameraSystem(),
            new BeeMovementSystem(),
            new BeeSpawnSystem(),
            new BeaconSystem(),
            new VirusMovementSystem(),
            new VirusSpawnSystem(),
            new VirusDespawnSystem(),
            new VirusMergeSystem(),
            new TurretSystem(),
            new BulletDespawnSystem(),
            new DamageSystem(),
            new HealthSystem(),
        ]
    )
    .Build();

Game.Launch(config, scene);
