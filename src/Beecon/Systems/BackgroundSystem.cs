namespace Beecon.Systems;

public sealed class BackgroundSystem : GameSystem
{
    private Entity _entity;
    private Sprite _sprite = null!;

    public CameraProvider Camera { get; set; } = Vigilance.Core.Camera.Scene;

    public override void Initialize()
    {
        var shader = Visuals.Background.Shader;
        shader.SetColor("uBackgroundColor", Visuals.Background.BackgroundColor);
        shader.SetColor("uLineColor", Visuals.Background.LineColor);
        shader.SetFloat("uHexSize", Visuals.Background.HexSize);
        shader.SetFloat("uLineThickness", Visuals.Background.LineThickness);
        _sprite = new Sprite(Texture.White)
        {
            Camera = Vigilance.Core.Camera.Null,
            Shader = shader,
            Scale = Display.Size,
        };

        _entity = Scene
            .Entity()
            .SetZIndex(Visuals.Background.ZIndex)
            .SetPosition(Display.Size / 2f)
            .Set(_sprite);
    }

    public override void PreRender()
    {
        var camera = Camera.Get();
        var size = Display.Size;
        _sprite.Scale = size;
        _entity.Position = size / 2f;
        var shader = Visuals.Background.Shader;
        shader.SetVec2("uResolution", size);
        shader.SetVec2("uScroll", (camera?.Target ?? 0) * Visuals.Background.ParallaxFactor);
    }
}
