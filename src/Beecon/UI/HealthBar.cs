using Beecon.Components;

namespace Beecon.UI;

public class HealthBar : UIContainer
{
    private readonly UIRectangle _bar;

    public HealthBar()
    {
        Camera = Vigilance.Core.Camera.Scene;
        Add(
            new UIRectangle(Visuals.HealthBar.BackgroundColor)
            {
                Height = Visuals.HealthBar.Height,
                Width = Visuals.HealthBar.Width,
            }[new UIRectangle(Visuals.HealthBar.FillColor) { Height = Unit.Full }.Tap(out _bar)]
        );
    }

    protected override void OnUpdate()
    {
        var health = Entity.AncestorsAndSelf().Last().Get<Health>();
        _bar.Width = Unit.Percent(health.Percent);
    }
}
