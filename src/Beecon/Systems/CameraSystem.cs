using Beecon.Components;

namespace Beecon.Systems;

public sealed class CameraSystem : GameSystem
{
    public override void PreRender()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;
        Scene.Camera.Target = player.RenderPosition;
        Scene.Camera.Offset = Display.Size / 2;
    }
}
