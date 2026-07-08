using Beecon.Components;

namespace Beecon.Systems;

public sealed class BulletDespawnSystem : GameSystem
{
    public static float Margin => 64f;

    public override void Update()
    {
        var camera = Scene.Camera;
        var target = camera.Target;
        var half = Display.Size / 2f / camera.Zoom + Margin;
        foreach (var (entity, _, body) in Entries<Bullet, Body>())
        {
            var offset = body.Position - target;
            if (MathF.Abs(offset.X) > half.X || MathF.Abs(offset.Y) > half.Y)
                entity.Destroy();
        }
    }
}
