using Beecon.Components;

namespace Beecon.Systems;

public sealed class AnnouncementSystem : GameSystem
{
    public override void Update()
    {
        foreach (var (entity, announcement, text) in Entries<Announcement, UIText>())
        {
            var completed = announcement.Timer.Update();

            var position = entity.Position;
            position.Y -= Visuals.Announcement.RiseSpeed * Time.DeltaSeconds;
            entity.Position = position;

            var color =
                announcement.Flash && FlashOff(announcement.Timer.Elapsed)
                    ? Visuals.Announcement.FlashColor
                    : Color.White;
            color.A = (byte)((1f - Ease.InCubic(announcement.Timer.Progress)) * 255f);
            text.Fill = color;

            if (completed)
                entity.Destroy();
        }
    }

    private static bool FlashOff(TimeSpan elapsed)
    {
        var interval = Visuals.Announcement.FlashInterval.TotalSeconds;
        return (int)(elapsed.TotalSeconds / interval) % 2 == 1;
    }
}
