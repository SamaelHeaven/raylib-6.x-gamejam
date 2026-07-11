namespace Beecon.UI;

public class UIPaused : UIContainer
{
    public UIPaused()
    {
        Add(new UIText("PAUSED") { FontSize = 64, Components = [new UIDropShadow()] });
    }
}
