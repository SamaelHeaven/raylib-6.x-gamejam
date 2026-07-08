using Beecon.Components;

namespace Beecon.Prefabs;

public struct TurretPrefab : IPrefab
{
    public void Build(Entity entity)
    {
        entity.SetZIndex(1).Set(new Turret()).Set(new Circle(Color.DarkGray) { Scale = 60 });
    }
}
