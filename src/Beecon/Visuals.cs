namespace Beecon;

public static class Visuals
{
    public static class Background
    {
        public static int ZIndex => -1;
        public static Color Color => Color.Gray;
        public static float CellSize => 64f;
        public static float Thickness => 2f;
    }

    public static class Player
    {
        public static int ZIndex => 1_000;
        public static Color Color => Color.Gold;
    }

    public static class HealthBar
    {
        public static int ZIndex => 1;
        public static Vector2 Offset => new(0f, 40f);
    }

    public static class Bee
    {
        public static int ZIndex => 1_500;
        public static Color Color => Color.Yellow;
    }

    public static class Virus
    {
        public static int ZIndex => 1_300;
        public static Color Color => Color.Green;
        public static Color TurretColor => Color.DarkGreen;
        public static Color ShieldColor => Color.DarkBlue;
        public static int BarrierZIndex => 1;
        public static Color BarrierColor => Color.SkyBlue;
    }

    public static class Bullet
    {
        public static int ZIndex => 1_200;
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
        public static Color DeactivatedColor => Color.DarkGray;
        public static Color ChargingColor => Color.SkyBlue;
        public static Color ActivatedColor => Color.Orange;
    }

    public static class Experience
    {
        public static int ZIndex => 1_100;
        public static Color Color => Color.SkyBlue;
        public static Color TurretColor => Color.Violet;
        public static Color ShieldColor => Color.Blue;
    }

    public static class Hud
    {
        public static int ZIndex => 10_000;
    }
}
