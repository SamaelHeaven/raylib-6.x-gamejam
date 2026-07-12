using Beecon.Components;
using Beecon.Prefabs;
using Beecon.Scenes;

namespace Beecon.Systems;

public sealed class TurretSystem : GameSystem
{
    private readonly Sound _shoot = Sound
        .Resource("Audio.shoot.wav")
        .SetVolume(Gameplay.Audio.ShootVolume);

    private ValueList<Entity> _createdBullets = [];

    public TurretSystem()
    {
        Order = -1;
    }

    public override void Update()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;
        var playerPosition = player.Position;
        foreach (var (entity, turret) in Entries<Turret>())
        {
            if (!turret.FireTimer.Update())
                continue;
            var origin = entity.WorldPosition;
            var direction = (playerPosition - origin).Normalize();
            var bulletEntity = Scene.Entity();
            new BulletPrefab(direction * Gameplay.Bullet.Speed).Build(
                bulletEntity.SetPosition(origin)
            );
            _createdBullets.Add(bulletEntity);
        }
    }

    public override void PostUpdate()
    {
        foreach (var entity in _createdBullets)
        {
            if (!entity.IsValid)
                continue;
            var pitch =
                1f + (Random.Shared.NextSingle() * 2f - 1f) * Gameplay.Audio.ShootPitchVariation;
            _shoot.SetPitch(pitch).Play();
        }

        _createdBullets.Clear();
    }
}
