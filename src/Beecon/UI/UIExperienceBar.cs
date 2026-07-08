using Beecon.Components;

namespace Beecon.UI;

public class UIExperienceBar : UIContainer
{
    private readonly UIRectangle _fill;
    private readonly UIText _level;

    public UIExperienceBar()
    {
        Position = PositionType.Absolute;
        Left = Unit.Zero;
        Right = Unit.Zero;
        Bottom = Unit.Fixed(24f);
        Direction = Direction.TopToBottom;
        AlignItems = Align.Center;
        GapY = Unit.Fixed(4f);

        Add(
            new UIText("Lv. 1", Color.White) { FontSize = 20f }.Tap(out _level),
            new UIRectangle(Color.DarkGray) { Width = 400f, Height = 16f }[
                new UIRectangle(Color.SkyBlue) { Height = Unit.Full, Width = Unit.Zero }.Tap(
                    out _fill
                )
            ]
        );
    }

    protected override void OnUpdate()
    {
        var player = Entity.Scene.Player;
        if (player.IsNull)
            return;
        var state = player.Get<Player>();
        _fill.Width = Unit.Percent(state.ExperiencePercent);
        _level.Value = $"Lv. {state.Level}";
    }
}
