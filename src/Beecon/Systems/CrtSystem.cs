namespace Beecon.Systems;

public sealed class CrtSystem : GameSystem
{
    public override void Configure()
    {
        Scene.OnDrawScreen(
            (graphics, texture, dest) =>
            {
                var shader = Visuals.Crt.Shader;
                shader.SetVec2("uResolution", dest.Size);
                shader.SetVec2(
                    "uUvScale",
                    new Vector2(
                        texture.Width / (float)texture.PhysicalWidth,
                        texture.Height / (float)texture.PhysicalHeight
                    )
                );
                shader.SetFloat("uWarp", Visuals.Crt.Warp);
                shader.SetFloat("uScan", Visuals.Crt.Scan);
                var scale = Renderer.Scale;
                var offset = Renderer.Offset;
                var blit = new Box(
                    new Vector2(-offset.X / scale.X, -offset.Y / scale.Y),
                    new Vector2(dest.Width / scale.X, dest.Height / scale.Y)
                );
                var previousShader = graphics.SetShader(shader);
                var previousCulling = graphics.SetCulling(false);
                graphics.DrawTexture(texture, blit, Color.White);
                graphics.SetCulling(previousCulling);
                graphics.SetShader(previousShader);
            }
        );
    }
}
