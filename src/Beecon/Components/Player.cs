namespace Beecon.Components;

public sealed class Player { }

public static class ScenePlayerExtensions
{
    extension(Scene scene)
    {
        public Entity Player => scene.Entities<Player>().AsValueEnumerable().FirstOrDefault();
    }
}
