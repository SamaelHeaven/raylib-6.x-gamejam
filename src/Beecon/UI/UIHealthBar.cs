using Beecon.Components;

namespace Beecon.UI;

public class UIHealthBar : UIContainer
{
    private readonly UIRectangle _bar;

    public UIHealthBar()
    {
        Camera = Vigilance.Core.Camera.Scene;
        Add(
            new UIRectangle(Color.Brown) { Height = 6f, Width = 60f }[
                new UIRectangle(Color.Red) { Height = Unit.Full }.Tap(out _bar)
            ]
        );
    }

    protected override void OnUpdate()
    {
        var health = Entity.AncestorsAndSelf().Last().Get<Health>();
        _bar.Width = Unit.Percent(health.Percent);
    }
}
