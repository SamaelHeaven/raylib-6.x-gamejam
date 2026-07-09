using Beecon.Components;
using Beecon.Scenes;

namespace Beecon.UI;

public class UIExperienceBar : UIContainer
{
    private readonly UIRectangle _fill;
    private readonly Tween _fillTween = new(TimeSpan.FromMilliseconds(250), cycleCount: 1);
    private readonly UIText _level;
    private float _displayedPercent;
    private float _fromPercent;
    private float _targetPercent = -1f;

    public UIExperienceBar()
    {
        Width = Unit.Full;
        Add(
            new UIContainer { Width = Unit.Full, Padding = 4 }[
                new UIRectangle("#44403BB3")
                {
                    Width = Unit.Full,
                    Padding = 6,
                    Radius = 12,
                    Stroke = "#79716BB3",
                    StrokeWidth = 2,
                    Components = [new UIDropShadow()],
                }[
                    new UIContainer
                    {
                        Width = Unit.Full,
                        Direction = Direction.LeftToRight,
                        Justify = Justify.SpaceBetween,
                    }[
                        new UIRectangle("#FFA1AD")
                        {
                            Position = PositionType.Absolute,
                            Height = Unit.Full,
                            Radius = 12,
                        }.Tap(out _fill),
                        new UIContainer(),
                        new UIText(Color.White) { FontSize = 20f }.Tap(out _level)
                    ]
                ]
            ]
        );
    }

    protected override void OnUpdate()
    {
        var player = Entity.Scene.Player;
        if (player.IsNull)
            return;
        var state = player.Get<Player>();
        var target = state.ExperiencePercent;
        if (MathF.Abs(target - _targetPercent) > 0.01f)
        {
            _fromPercent = _displayedPercent;
            _targetPercent = target;
            _fillTween.Reset();
        }

        _fillTween.Update();
        _displayedPercent = _fillTween.Interpolate(_fromPercent, _targetPercent, Ease.OutCubic);
        _fill.Width = Unit.Percent(_displayedPercent);
        _level.Value = $"Lv {state.Level}";
    }
}
