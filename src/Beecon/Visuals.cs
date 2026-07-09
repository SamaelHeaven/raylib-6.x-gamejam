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

    public static class Crt
    {
        public static float Warp => 0.12f;
        public static float Scan => 0.15f;
        public static Shader Shader => field ??= Shader.Fragment.Resource("Shader.crt.frag.glsl");
    }

    public static class Player
    {
        public static int ZIndex => 1_000;
        public static float Size => 64f;
        public static TimeSpan AnimationDelay => TimeSpan.FromMilliseconds(75);
        public static Texture Texture => field ??= Texture.Resource("Texture.queen.png");
        public static TextureAtlas TextureAtlas => field ??= new TextureAtlas(Texture, 4, 1);
        public static Color DamageFlashFrom => "#A1A1A1";
        public static Color DamageFlashTo => "#FF6467";
        public static TimeSpan DamageFlashInterval => TimeSpan.FromMilliseconds(90);
        public static int DamageFlashCycles => 4;
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
        public static Color Tint => Color.White;
        public static Color TurretTint => "#B9F8CF";
        public static Color ShieldTint => "#96F7E4";
        public static float SizeScale => 3f;
        public static int BarrierZIndex => 1;
        public static Color BarrierColor => Color.SkyBlue;
        public static TimeSpan AnimationDelay => TimeSpan.FromMilliseconds(75);
        public static Texture Texture => field ??= Texture.Resource("Texture.virus.png");
        public static TextureAtlas TextureAtlas => field ??= new TextureAtlas(Texture, 4, 1);
    }

    public static class Shield
    {
        public static int ZIndex => Virus.ZIndex + 1;

        public static Vector2 Size =>
            new(Gameplay.Virus.BarrierThickness * 8f, Gameplay.Virus.BarrierWidth * 2f);

        public static TimeSpan AnimationDelay => TimeSpan.FromMilliseconds(75);
        public static Texture Texture => field ??= Texture.Resource("Texture.shield.png");
        public static TextureAtlas TextureAtlas => field ??= new TextureAtlas(Texture, 3, 1);
    }

    public static class Bullet
    {
        public static int ZIndex => 1_200;
        public static Vector2 Size => (12, 24f);
        public static TimeSpan AnimationDelay => TimeSpan.FromMilliseconds(75);
        public static Texture Texture => field ??= Texture.Resource("Texture.bullet.png");
        public static TextureAtlas TextureAtlas => field ??= new TextureAtlas(Texture, 2, 1);
        public static float RotationOffset => 90f;
        public static float BackOffset => 10f;
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
