namespace Beecon.Physics;

public static class ShapeCategory
{
    private static int Offset
    {
        get
        {
            var result = ++field;
            Debug.Assert(result < 64);
            return result;
        }
    }

    private static ShapeFilterCategory Category => (ShapeFilterCategory)(1UL << Offset);
    public static ShapeFilterCategory Default => ShapeFilterCategory.DefaultCategory;
    public static ShapeFilterCategory All => ShapeFilterCategory.DefaultMask;
    public static ShapeFilterCategory Wall { get; } = Category;
    public static ShapeFilterCategory Player { get; } = Category;
    public static ShapeFilterCategory Bee { get; } = Category;
    public static ShapeFilterCategory BeeSensor { get; } = Category;
    public static ShapeFilterCategory Virus { get; } = Category;
    public static ShapeFilterCategory VirusSensor { get; } = Category;
    public static ShapeFilterCategory PlayerSensor { get; } = Category;
    public static ShapeFilterCategory BulletSensor { get; } = Category;
    public static ShapeFilterCategory Beacon { get; } = Category;
    public static ShapeFilterCategory Experience { get; } = Category;
}
