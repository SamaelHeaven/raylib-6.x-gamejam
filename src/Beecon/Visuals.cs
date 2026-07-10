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
        public static float SizeScale => 3.5f;
        public static TimeSpan AnimationDelay => TimeSpan.FromMilliseconds(75);
        public static Texture Texture => field ??= Texture.Resource("Texture.virus.png");
        public static TextureAtlas TextureAtlas => field ??= new TextureAtlas(Texture, 4, 1);
    }

    public static class Boss
    {
        public static Color Tint => "#8EC5FF";

        public static Vector2 ShieldSize =>
            new(Gameplay.Boss.BarrierThickness * 8f, Gameplay.Boss.BarrierWidth * 2f);
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

    public static class Wall
    {
        public static int ZIndex => 500;
        public static Color Color => Background.LineColor;
    }

    public static class Beacon
    {
        public static int ZIndex => 100;
        public static float Size => Gameplay.Beacon.Radius * 2f;
        public static int ShadowBlur => 5;
        public static float ShadowPadding => 1 + ShadowBlur * 3;
        public static float BakedSize => Size + 2 * ShadowPadding;
    }

    public static class Matrix
    {
        public static int Resolution => 256;
        public static float CellSize => 32f;
        public static int Trail => 6;
        public static TimeSpan StepInterval => TimeSpan.FromMilliseconds(70);
        public static Color BackgroundColor => "#192E03";
        public static Color Text => "#7CCF35";
        public static Color Head => "#F7FEE7";
    }

    public static class Experience
    {
        public static int ZIndex => 1_100;
        public static float Size => 16f;
        public static Texture Texture => field ??= Texture.Resource("Texture.experience.png");
        public static TextureAtlas TextureAtlas => field ??= new TextureAtlas(Texture, 3, 1);
    }

    public static class PowerUp
    {
        public static int ZIndex => 1_150;
        public static float Size => 28f;
        public static Texture HealthTexture => field ??= Texture.Resource("Texture.health.png");
        public static Texture MagnetTexture => field ??= Texture.Resource("Texture.magnet.png");
        public static Texture NukeTexture => field ??= Texture.Resource("Texture.nuke.png");
    }

    public static class Hud
    {
        public static int ZIndex => 10_000;
    }

    public static class Announcement
    {
        public static int ZIndex => Hud.ZIndex + 1;
        public static float FontSize => 48f;
        public static TimeSpan Duration => TimeSpan.FromSeconds(1.4);
        public static float RiseSpeed => 55f;
        public static Vector2 Spread => new(140f, 90f);
        public static Color FlashColor => "#FF6467";
        public static TimeSpan FlashInterval => TimeSpan.FromMilliseconds(110);
    }
}
