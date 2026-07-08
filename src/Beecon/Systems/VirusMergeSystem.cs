using Beecon.Components;
using Beecon.Physics;
using Beecon.Prefabs;

namespace Beecon.Systems;

public sealed class VirusMergeSystem : GameSystem
{
    private ValueList<Entity> _cluster = [];
    private ValueHashSet<Entity> _consumed = [];
    private static int MergeCount => 20;
    private static float MergeRadius => 100;

    public override void Update()
    {
        _consumed.Clear();
        var world = Scene.World;
        var filter = new ShapeFilter { Category = ShapeCategory.Virus, Mask = ShapeCategory.Virus };
        var overlapCallback = (Shape shape) =>
        {
            var other = shape.Entity;
            if (!_consumed.Contains(other) && other.TryGet(out Virus v) && v.CanMerge)
                _cluster.Add(other);
        };
        foreach (var (entity, virus, body) in Entries<Virus, Body>())
        {
            if (!virus.CanMerge || _consumed.Contains(entity))
                continue;
            _cluster.Clear();
            world.Overlap(CircleShape.Make(body.Position, MergeRadius), overlapCallback, filter);
            if (_cluster.Count < MergeCount)
                continue;
            var centroid = Vector2.Zero;
            for (var i = 0; i < MergeCount; i++)
            {
                var member = _cluster[i];
                _consumed.Add(member);
                centroid += member.Get<Body>().Position;
                member.Destroy();
            }

            new VirusPrefab(big: true).Build(Scene.Entity().SetPosition(centroid / MergeCount));
        }
    }
}
