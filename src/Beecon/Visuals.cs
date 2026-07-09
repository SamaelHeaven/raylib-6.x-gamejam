namespace Beecon;

public static class Visuals
{
    public static class Background
    {
        public static int ZIndex => -1;
        public static Color BackgroundColor => Color.DarkBrown;
        public static Color LineColor => "#432004";
        public static float HexSize => 96f;
        public static float LineThickness => 4f;
        public static float ParallaxFactor => 0.5f;

        public static Shader Shader =>
            field ??= Shader.Fragment.Resource("Shader.hexagon.frag.glsl");
    }

    public static class Player
    {
        public static int ZIndex => 1_000;
        public static float Size => 64f;
        public static TimeSpan AnimationDelay => TimeSpan.FromMilliseconds(75);
        public static TextureAtlas TextureAtlas => Bee.TextureAtlas;
    }

    public static class HealthBar
    {
        public static int ZIndex => 1;
        public static Vector2 Offset => new(0f, 40f);
    }

    public static class Bee
    {
        public static int ZIndex => 1_500;
        public static float Size => 32f;
        public static TimeSpan AnimationDelay => TimeSpan.FromMilliseconds(100);
        public static Texture Texture => field ??= Texture.Resource("Texture.bee.png");
        public static TextureAtlas TextureAtlas => field ??= new TextureAtlas(Texture, 1, 4);
    }

    public static class Virus
    {
        public static int ZIndex => 1_300;
        public static Color Color => Color.Green;
        public static Color TurretColor => Color.DarkGreen;
        public static Color ShieldColor => Color.DarkBlue;
        public static float SizeScale => 2f;
        public static int BarrierZIndex => 1;
        public static Color BarrierColor => Color.SkyBlue;
    }

    public static class Bullet
    {
        public static int ZIndex => 1_200;
        public static float Size => 12f;
        public static Color Color => Color.Purple;
    }

    public static class Turret
    {
        public static int ZIndex => 1;
        public static Color Color => Color.DarkGray;
        public static float Scale => 60f;
    }

    public static class Wall
    {
        public static int ZIndex => 500;
        public static Color Color => Color.DarkGray;
    }

    public static class Beacon
    {
        public static int ZIndex => 100;
        public static float Size => Gameplay.Beacon.Radius * 2f;
        public static Color DeactivatedColor => Color.DarkGray;
        public static Color ChargingColor => Color.SkyBlue;
        public static Color ActivatedColor => Color.Orange;
    }

    public static class Experience
    {
        public static int ZIndex => 1_100;
        public static float Size => 16f;
        public static Color Color => Color.SkyBlue;
        public static Color TurretColor => Color.Violet;
        public static Color ShieldColor => Color.Blue;
        public static Texture Texture => field ??= Texture.Resource("Texture.experience.png");
        public static TextureAtlas TextureAtlas => field ??= new TextureAtlas(Texture, 3, 1);
    }

    public static class Hud
    {
        public static int ZIndex => 10_000;
    }
}
