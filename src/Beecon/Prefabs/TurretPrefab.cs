using Beecon.Components;

namespace Beecon.Prefabs;

public struct TurretPrefab : IPrefab
{
    public void Build(Entity entity)
    {
        entity.SetZIndex(Visuals.Turret.ZIndex).Set(new Turret());
    }
}
