using Beecon.Components;

namespace Beecon.UI;

public class UIStatMenu : UIContainer
{
    private readonly UIText _points;

    public UIStatMenu()
    {
        Add(
            new UIContainer { Padding = 12 }[
                new UIRectangle("#44403BBF")
                {
                    Padding = 8,
                    Radius = 12,
                    GapY = 4,
                    Direction = Direction.TopToBottom,
                    Stroke = "#79716BBF",
                    StrokeWidth = 2,
                    Components = [new UIDropShadow()],
                }[
                    new UISprite(Texture.Resource("Texture.stat.png"))
                    {
                        Size = 32,
                        Position = PositionType.Absolute,
                        Translate = -10,
                    },
                    new UIText
                    {
                        MarginHorizontal = Unit.Auto,
                        Components = [new UIDropShadow()],
                    }.Tap(out _points)
                ][StatType.All.Select(type => new UIStatRow(type))]
            ]
        );
    }

    protected override void OnUpdate()
    {
        var player = Entity.Scene.Player;
        if (player.IsNull)
            return;
        _points.Value = $"Points: {player.Get<Player>().Stats.AvailablePoints}";
    }
}
