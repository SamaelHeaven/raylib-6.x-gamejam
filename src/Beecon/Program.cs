using Beecon;
using Beecon.Scenes;
using Beecon.Systems;

var scene = MainMenuScene.Build();

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
        drawing.RenderTexturePoolLifetime = TimeSpan.FromSeconds(100);
    })
    .Audio(audio =>
    {
        audio.DefaultSoundMaxAliases = 8;
    })
    .Font(font =>
    {
        font.Default = () => Font.Resource("Font.default.woff");
    })
    .World(world =>
    {
        world.DefaultGravity = Vector2.Zero;
    })
    .Systems(() =>
        [
            new DebugSystem(),
            new CrtSystem(),
            new DrawableSystem(),
            new SpriteBatchSystem(),
            new UISystem(),
            new AnimationSystem(),
            new MatrixRainSystem(),
            new MusicSystem(),
        ]
    )
    .Build();

Game.Launch(config, scene);
