namespace Beecon.Components;

public sealed class Announcement
{
    public Announcement(TimeSpan duration, bool flash)
    {
        Timer = new Timer(duration, cycleCount: 1);
        Flash = flash;
    }

    public Timer Timer { get; }
    public bool Flash { get; }
}
