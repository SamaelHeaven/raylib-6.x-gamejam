namespace Beecon.Physics;

public static class SpawnExtensions
{
    extension(Scene scene)
    {
        public bool TryFindSpawnPosition(
            Func<Vector2> candidate,
            float clearanceRadius,
            ShapeFilter filter,
            out Vector2 position,
            int maxAttempts = 16
        )
        {
            var world = scene.World;
            for (var attempt = 0; attempt < maxAttempts; attempt++)
            {
                position = candidate.Invoke();
                var blocked = false;
                world.Overlap(
                    CircleShape.Make(position, clearanceRadius),
                    _ =>
                    {
                        blocked = true;
                        return false;
                    },
                    filter
                );
                if (!blocked)
                    return true;
            }

            position = default;
            return false;
        }
    }
}
