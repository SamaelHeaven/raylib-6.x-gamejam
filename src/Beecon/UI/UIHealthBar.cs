using Beecon.Components;

namespace Beecon.UI;

public class UIHealthBar : UIContainer
{
    private readonly UIRectangle _bar;

    public UIHealthBar()
    {
        Camera = Vigilance.Core.Camera.Scene;
        Add(
            new UIRectangle("#460809")
            {
                Height = 6f,
                Width = 60f,
                Components = [new UIDropShadow()],
                Stroke = "#79716B",
                StrokeWidth = 1,
                Padding = 1,
                Radius = 2,
            }[new UIRectangle("#E7180B") { Height = Unit.Full, Radius = 8 }.Tap(out _bar)]
        );
    }

    protected override void OnUpdate()
    {
        var health = Entity.AncestorsAndSelf().Last().Get<Health>();
        _bar.Width = Unit.Percent(health.Percent);
    }
}
