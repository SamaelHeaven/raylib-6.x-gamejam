namespace Beecon.UI;

public class UIButton : UIContainer
{
    private readonly Tween _hoverTween = new(TimeSpan.FromMilliseconds(200), cycleCount: 1);
    private readonly UIRectangle _rectangle;
    private float _hover;
    private float _hoverFrom;
    private float _hoverTarget;

    public UIButton(string label)
    {
        Add(
            new UIRectangle(Color.White)
            {
                Width = Unit.Full,
                Padding = 8,
                Radius = 8,
                Direction = Direction.LeftToRight,
                Justify = Justify.Center,
                Stroke = Color.Black,
                StrokeWidth = 1,
            }.Tap(out _rectangle)[new UIText(label) { Fill = Color.Black, FontSize = 32 }]
        );
    }

    protected override void OnUpdate()
    {
        _hoverTween.Update();
        _hover = _hoverTween.Interpolate(_hoverFrom, _hoverTarget, Ease.InOutCubic);
        _rectangle.Fill = Color.Lerp(Color.White, "#D4D4D4", _hover);
    }

    protected override void OnClick()
    {
        Mouse.Cursor = Cursor.Default;
    }

    protected override void OnMouseEnter()
    {
        Mouse.Cursor = Cursor.Pointer;
        SetHover(1f);
    }

    protected override void OnMouseLeave()
    {
        Mouse.Cursor = Cursor.Default;
        SetHover(0f);
    }

    private void SetHover(float target)
    {
        _hoverFrom = _hover;
        _hoverTarget = target;
        _hoverTween.Reset();
    }
}
