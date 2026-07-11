using Beecon.Scenes;
using Beecon.UI;

namespace Beecon.Systems;

public sealed class GameOverSystem : GameSystem
{
    private TimeSpan _elapsed;
    private bool _gameOver;
    private bool _promptShown;
    private bool _started;

    public override void Update()
    {
        if (_gameOver)
        {
            _elapsed += Time.Delta;
            if (!_promptShown && _elapsed >= Visuals.GameOver.PromptDelay)
            {
                _promptShown = true;
                ShowButtons();
            }

            return;
        }

        if (Scene.Player.IsNull)
        {
            if (_started)
            {
                _gameOver = true;
                ShowText(
                    "GAME OVER",
                    Visuals.GameOver.TitleFontSize,
                    Visuals.GameOver.TitleOffset,
                    Visuals.GameOver.TitleColor
                );
            }

            return;
        }

        _started = true;
    }

    private void ShowButtons()
    {
        Scene
            .Entity()
            .SetZIndex(Visuals.GameOver.ZIndex)
            .SetPosition(Display.Size / 2f + Visuals.GameOver.PromptOffset)
            .Set(
                new UIContainer { Direction = Direction.LeftToRight, GapX = 24 }[
                    new UIButton("RESTART")
                    {
                        Width = 240,
                        OnClick = _ => Game.Scene = GameScene.Build(),
                    },
                    new UIButton("MENU")
                    {
                        Width = 240,
                        OnClick = _ => Game.Scene = MainMenuScene.Build(),
                    }
                ]
            );
    }

    private void ShowText(string text, float fontSize, Vector2 offset, Color color)
    {
        Scene
            .Entity()
            .SetZIndex(Visuals.GameOver.ZIndex)
            .SetPosition(Display.Size / 2f + offset)
            .Set(
                new UIText(text, color) { FontSize = fontSize, Components = [new UIDropShadow()] }
            );
    }
}
