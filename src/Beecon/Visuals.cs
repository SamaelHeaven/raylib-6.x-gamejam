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
        public static int ZIndex => 1000;
        public static Color Color => Color.Gold;
    }

    public static class HealthBar
    {
        public static int ZIndex => 1;
        public static Vector2 Offset => new(0f, 40f);
        public static Color BackgroundColor => Color.Brown;
        public static Color FillColor => Color.Red;
        public static float Width => 60f;
        public static float Height => 6f;
    }

    public static class Bee
    {
        public static int ZIndex => 1500;
        public static Color Color => Color.Yellow;
    }

    public static class Virus
    {
        public static int ZIndex => 1300;
        public static Color Color => Color.Green;
        public static Color TurretColor => Color.DarkGreen;
    }

    public static class Bullet
    {
        public static int ZIndex => 1200;
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
}
