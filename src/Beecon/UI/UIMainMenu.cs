using Beecon.Components;
using Beecon.Scenes;

namespace Beecon.UI;

public class UIMainMenu : UIContainer
{
    private readonly UISprite _background;

    public UIMainMenu()
    {
        Culling = false;
        Add(
            new UIContainer
            {
                Size = Vigilance.Core.Display.Size,
                Direction = Direction.LeftToRight,
                Justify = Justify.SpaceAround,
                AlignItems = Align.Center,
            }[
                new UIRectangle("#44403BB3")
                {
                    Width = 400,
                    Height = Unit.Full,
                    Direction = Direction.TopToBottom,
                    Justify = Justify.SpaceAround,
                    AlignItems = Align.Center,
                    Stroke = "#79716BB3",
                    StrokeWidth = 2,
                    Components = [new UIDropShadow()],
                }[
                    new UISprite
                    {
                        Size = 300,
                        Justify = Justify.Center,
                        AlignItems = Align.Center,
                    }.Tap(out _background)[
                        new UIText("BEECON")
                        {
                            FontSize = 112,
                            Fill = "#5EA529",
                            Components = [new UIDropShadow(3)],
                        },
                        new UIText("By Samael Heaven")
                        {
                            Position = PositionType.Absolute,
                            TranslateY = 64,
                            Components = [new UIDropShadow()],
                        }
                    ],
                    new UIButton("PLAY")
                    {
                        Width = Unit.Percent(50),
                        OnClick = _ =>
                        {
                            Game.Scene = GameScene.Build();
                        },
                    },
                    new UIContainer { Gap = 16 }[
                        new UIText("CONTROLS")
                        {
                            MarginHorizontal = Unit.Auto,
                            Components = [new UIDropShadow()],
                        },
                        new UIText(
                            $"""
                            Move           : WASD / Arrow Keys
                            Move Bees      : Mouse
                            Hex Formation  : Left or Right Click
                            Select Upgrade : {StatType.All.AsValueEnumerable().Select(type =>
                                (int)type + 1
                            ).JoinToString(" ")}
                            Pause          : {Inputs.PauseButton.Keys.AsValueEnumerable().Select(key => key.Name).JoinToString(" ")}
                            """
                        )
                        {
                            Components = [new UIDropShadow()],
                        }
                    ]
                ]
            ]
        );
    }

    protected override void OnUpdate()
    {
        var matrixRain = Entity.Scene.MatrixRain;
        if (matrixRain is not null)
            _background.Texture = matrixRain.Matrix;
    }
}
