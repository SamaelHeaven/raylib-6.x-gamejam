using Beecon.Components;
using Beecon.Prefabs;

namespace Beecon.Systems;

public sealed class VirusMergeSystem : GameSystem
{
    private readonly EntitySparseSet<EntitySparseSet<TimeSpan>> _contacts = new();

    public override void WorldContactBegin(Shape shapeA, Shape shapeB)
    {
        var a = shapeA.Entity;
        var b = shapeB.Entity;
        if (!a.TryGet(out Virus va) || !b.TryGet(out Virus vb))
            return;
        if (!CanMerge(va, vb))
            return;
        Track(a, b);
    }

    public override void WorldContactEnd(Shape shapeA, Shape shapeB)
    {
        Untrack(shapeA.Entity, shapeB.Entity);
    }

    public override void FixedUpdate()
    {
        for (var i = _contacts.Count - 1; i >= 0; i--)
        {
            var (first, partners) = _contacts[i];
            if (!first.IsValid)
            {
                _contacts.Remove(first);
                continue;
            }

            for (var j = partners.Count - 1; j >= 0; j--)
            {
                var (second, elapsed) = partners[j];
                if (
                    !second.IsValid
                    || !first.TryGet(out Virus va)
                    || !second.TryGet(out Virus vb)
                    || !CanMerge(va, vb)
                )
                {
                    partners.Remove(second);
                    continue;
                }

                elapsed += Time.FixedDelta;
                if (elapsed >= MergeIntervalFor(va.Type))
                {
                    Merge(first, va, second, vb);
                    partners.Remove(second);
                    continue;
                }

                partners[second] = elapsed;
            }

            if (partners.Count == 0)
                _contacts.Remove(first);
        }
    }

    private void Merge(Entity a, Virus va, Entity b, Virus vb)
    {
        var position = (a.Get<Body>().Position + b.Get<Body>().Position) / 2f;
        var type = va.Type;
        var mergeCount = Math.Max(va.MergeCount, vb.MergeCount) + 1;
        if (mergeCount >= MergesPerPromotionFor(va.Type))
        {
            type = va.NextType();
            mergeCount = 0;
        }

        a.Destroy();
        b.Destroy();
        new VirusPrefab(type, mergeCount).Build(Scene.Entity().SetPosition(position));
    }

    private static bool CanMerge(Virus a, Virus b)
    {
        return a.CanMerge && b.CanMerge && a.Type == b.Type;
    }

    private static TimeSpan MergeIntervalFor(VirusType type)
    {
        return type == VirusType.Turret
            ? Gameplay.Virus.TurretMergeInterval
            : Gameplay.Virus.MergeInterval;
    }

    private static int MergesPerPromotionFor(VirusType type)
    {
        return type == VirusType.Turret
            ? Gameplay.Virus.TurretMergesPerPromotion
            : Gameplay.Virus.MergesPerPromotion;
    }

    private void Track(Entity a, Entity b)
    {
        Order(ref a, ref b);
        if (!_contacts.TryGetValue(a, out var partners))
            _contacts[a] = partners = new EntitySparseSet<TimeSpan>();
        partners[b] = TimeSpan.Zero;
    }

    private void Untrack(Entity a, Entity b)
    {
        Order(ref a, ref b);
        if (!_contacts.TryGetValue(a, out var partners))
            return;
        partners.Remove(b);
        if (partners.Count == 0)
            _contacts.Remove(a);
    }

    private static new void Order(ref Entity a, ref Entity b)
    {
        if (a.Index > b.Index)
            (a, b) = (b, a);
    }
}
