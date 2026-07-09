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

        public UIHud Hud => scene.Components<UIHud>().AsValueEnumerable().First();

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
