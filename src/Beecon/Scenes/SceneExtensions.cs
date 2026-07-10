using System.Diagnostics.CodeAnalysis;
using Beecon.Components;
using Beecon.UI;

namespace Beecon.Scenes;

public static class SceneExtensions
{
    extension(Scene scene)
    {
        public Entity Player => scene.Entities<Player>().AsValueEnumerable().FirstOrDefault();

        public Stats? Stats =>
            scene.Components<Player>().AsValueEnumerable().FirstOrDefault()?.Stats;

        public Swarm? Swarm => scene.Components<Swarm>().AsValueEnumerable().FirstOrDefault();

        public MatrixRain? MatrixRain =>
            scene.Components<MatrixRain>().AsValueEnumerable().FirstOrDefault();

        public UIHud Hud => scene.Components<UIHud>().AsValueEnumerable().First();

        public void Announce(string text, bool flash = false)
        {
            var rnd = Random.Shared;
            var spread = Visuals.Announcement.Spread;
            var position =
                Display.Size / 2f
                + new Vector2(
                    (rnd.NextSingle() * 2f - 1f) * spread.X,
                    (rnd.NextSingle() * 2f - 1f) * spread.Y
                );
            scene
                .Entity()
                .SetZIndex(Visuals.Announcement.ZIndex)
                .SetPosition(position)
                .Set(new Announcement(Visuals.Announcement.Duration, flash))
                .Set(
                    new UIText(text, Color.White)
                    {
                        FontSize = Visuals.Announcement.FontSize,
                        Components = [new UIDropShadow()],
                    }
                );
        }

        public bool TryGetSingleton<T>([MaybeNullWhen(false)] out T singleton)
        {
            if (scene.Count<T>() == 0)
            {
                singleton = default!;
                return false;
            }

            singleton = scene.Components<T>().AsValueEnumerable().First();
            return true;
        }
    }
}
