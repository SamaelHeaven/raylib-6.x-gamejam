using Beecon.Components;

namespace Beecon.Prefabs;

public struct TurretPrefab : IPrefab
{
    public void Build(Entity entity)
    {
        entity
            .SetZIndex(Visuals.Turret.ZIndex)
            .Set(new Turret())
            .Set(new Circle(Visuals.Turret.Color) { Scale = Visuals.Turret.Scale });
    }
}
