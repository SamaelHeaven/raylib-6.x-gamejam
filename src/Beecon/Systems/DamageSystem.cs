using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Systems;

public sealed class DamageSystem : GameSystem
{
    private readonly Sound _playerHit = Sound.Resource("Audio.player-hit.wav");
    private readonly EntitySparseSet<EntitySparseSet<DamageContact>> _targetsByDamager = new();
    private readonly Sound _virusHit = Sound.Resource("Audio.virus-hit.wav");
    private TimeSpan _playerHitLast;
    private TimeSpan _virusHitLast;

    public override void WorldSensorBegin(Shape sensor, Shape visitor)
    {
        var target = visitor.Entity;
        if (!sensor.Entity.TryGet(out Damage damage))
            return;
        if ((visitor.Filter.Category & damage.TargetMask) == 0)
            return;
        var isShield = (visitor.Filter.Category & ShapeCategory.Shield) != 0;
        if (!ApplyDamage(target, damage.Amount, isShield))
            return;
        if (!_targetsByDamager.TryGetValue(sensor.Entity, out var targets))
            _targetsByDamager[sensor.Entity] = targets = new EntitySparseSet<DamageContact>();
        targets[target] = new DamageContact(damage.Amount, damage.Cooldown, isShield);
    }

    public override void WorldSensorEnd(Shape sensor, Shape visitor)
    {
        if (!_targetsByDamager.TryGetValue(sensor.Entity, out var targets))
            return;
        targets.Remove(visitor.Entity);
        if (targets.Count == 0)
            _targetsByDamager.Remove(sensor.Entity);
    }

    public override void FixedUpdate()
    {
        for (var i = _targetsByDamager.Count - 1; i >= 0; i--)
        {
            var (damager, targets) = _targetsByDamager[i];
            if (!damager.IsValid)
            {
                _targetsByDamager.Remove(damager);
                continue;
            }

            for (var j = targets.Count - 1; j >= 0; j--)
            {
                var (target, contact) = targets[j];
                if (!target.IsValid)
                {
                    targets.Remove(target);
                    continue;
                }

                contact.Elapsed += Time.FixedDelta;
                if (contact.Elapsed >= contact.Cooldown)
                {
                    contact.Elapsed = TimeSpan.Zero;
                    if (!ApplyDamage(target, contact.Amount, contact.IsShield))
                    {
                        targets.Remove(target);
                        continue;
                    }
                }

                targets[target] = contact;
            }

            if (targets.Count == 0)
                _targetsByDamager.Remove(damager);
        }
    }

    private bool ApplyDamage(Entity target, float amount, bool isShield)
    {
        if (isShield)
        {
            if (!target.TryGet(out Shield shield))
                return false;
            shield.Health.Damage(amount);
            Hit(target);
            if (shield.Health.IsDead)
                BreakShield(target, shield);
            return true;
        }

        if (!target.TryGet(out Health health))
            return false;
        health.Damage(amount);
        Hit(target);
        return true;
    }

    private static void BreakShield(Entity virus, Shield shield)
    {
        virus.Remove<Shield>();
        shield.Barrier.Destroy();
        if (shield.Visual.IsValid)
            shield.Visual.Destroy();
    }

    private void Hit(Entity target)
    {
        if (target.TryGet(out Player _))
            TryPlay(_playerHit, ref _playerHitLast);
        else if (target.TryGet(out Virus _))
            TryPlay(_virusHit, ref _virusHitLast);
    }

    private static void TryPlay(Sound sound, ref TimeSpan last)
    {
        var now = Time.Elapsed;
        if (now - last < Gameplay.Audio.HitInterval)
            return;
        last = now;
        var pitch = 1f + (Random.Shared.NextSingle() * 2f - 1f) * Gameplay.Audio.HitPitchVariation;
        sound.SetPitch(pitch).Play();
    }

    private record struct DamageContact(
        float Amount,
        TimeSpan Cooldown,
        bool IsShield = false,
        TimeSpan Elapsed = default
    );
}
