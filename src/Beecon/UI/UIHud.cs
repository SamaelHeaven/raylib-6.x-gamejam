namespace Beecon.UI;

public class UIHud : UIContainer
{
    public readonly UIStatMenu StatMenu;

    public UIHud()
    {
        Add(
            new UIContainer
            {
                Size = Vigilance.Core.Display.Size,
                Direction = Direction.TopToBottom,
                Justify = Justify.SpaceBetween,
                AlignItems = Align.Start,
            }[new UIExperienceBar(), new UIStatMenu().Tap(out StatMenu)]
        );
    }
}
