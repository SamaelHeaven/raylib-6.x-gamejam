namespace Beecon.UI;

public class UIHud : UIContainer
{
    public UIHud()
    {
        Size = Vigilance.Core.Display.Size;
        Add(new UIExperienceBar());
    }
}
