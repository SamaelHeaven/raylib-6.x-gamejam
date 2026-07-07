namespace Beecon.Components;

public sealed class Player
{
    public int MaxBees { get; set; } = 5;
}

public static class ScenePlayerExtensions
{
    extension(Scene scene)
    {
        public Entity Player => scene.Entities<Player>().AsValueEnumerable().FirstOrDefault();
    }
}
