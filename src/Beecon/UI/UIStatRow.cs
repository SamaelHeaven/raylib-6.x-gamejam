using Beecon.Components;
using Beecon.Scenes;

namespace Beecon.UI;

public class UIStatRow : UIContainer
{
    private readonly UISprite _arrowSprite;

    private readonly Tween _arrowTween = new(
        TimeSpan.FromMilliseconds(400),
        alternateDirection: true
    );

    private readonly UIPolygon[] _pips = new UIPolygon[Gameplay.Stats.MaxLevel];
    private readonly StatType _type;

    public UIStatRow(StatType type)
    {
        _type = type;
        Width = Unit.Full;
        Add(
            new UIContainer
            {
                Width = Unit.Full,
                Direction = Direction.LeftToRight,
                AlignItems = Align.Center,
                GapX = 8,
                PaddingVertical = 2,
            }[
                new UISprite(Texture.Resource("Texture.stat-up.png")) { Size = 24 }.Tap(
                    out _arrowSprite
                ),
                new UIContainer { Direction = Direction.TopToBottom, GapY = 3 }[
                    new UIText($"[{(int)_type + 1}] {type.Label}", Color.White)
                    {
                        FontSize = 14f,
                        Components = [new UIDropShadow()],
                    },
                    new UIContainer { Direction = Direction.LeftToRight, GapX = 2 }[
                        Enumerable
                            .Range(0, _pips.Length)
                            .Select(i => new UIPolygon(6) { Size = 10 }.Tap(out _pips[i]))
                    ]
                ]
            ]
        );
    }

    protected override void OnUpdate()
    {
        _arrowSprite.Scale = 1;
        _arrowSprite.Tint = Color.DarkGray;
        var stats = Entity.Scene.Stats;
        if (stats is null)
            return;
        var level = stats.LevelOf(_type);
        for (var i = 0; i < _pips.Length; i++)
            _pips[i].Fill = i < level ? _type.Color : "#57534D";
        var canAllocate = stats.CanAllocate(_type);
        if (canAllocate)
        {
            _arrowTween.Update();
            _arrowSprite.Scale = _arrowTween.Interpolate(1, 1.5f, Ease.InOutSine);
            _arrowSprite.Tint = Color.White;
        }
        else
        {
            _arrowTween.Reset();
        }

        if (canAllocate && Keyboard.IsKeyPressed(_type.Key))
            stats.Allocate(_type);
    }

    protected override void OnClick()
    {
        Entity.Scene.Stats?.Allocate(_type);
    }

    protected override void OnMouseEnter()
    {
        var stats = Entity.Scene.Stats;
        if (stats is not null && stats.CanAllocate(_type))
            Mouse.Cursor = Cursor.Pointer;
    }

    protected override void OnMouseLeave()
    {
        Mouse.Cursor = Cursor.Default;
    }
}
