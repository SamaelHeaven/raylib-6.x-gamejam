using Beecon.UI;

namespace Beecon.Prefabs;

public struct HudPrefab : IPrefab
{
    public void Build(Entity entity)
    {
        entity.SetZIndex(Visuals.Hud.ZIndex).SetPosition(Display.Size / 2).Set(new UIHud());
    }
}
