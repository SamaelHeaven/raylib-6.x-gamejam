namespace Beecon.Physics;

public static class ShapeFilterCategoryExtensions
{
    extension(ShapeFilterCategory)
    {
        public static ShapeFilterCategory Wall => (ShapeFilterCategory)(1L << 1);
        public static ShapeFilterCategory Player => (ShapeFilterCategory)(1L << 2);
        public static ShapeFilterCategory Bee => (ShapeFilterCategory)(1L << 3);
        public static ShapeFilterCategory BeeSensor => (ShapeFilterCategory)(1L << 4);
        public static ShapeFilterCategory Virus => (ShapeFilterCategory)(1L << 5);
        public static ShapeFilterCategory VirusSensor => (ShapeFilterCategory)(1L << 6);
    }
}
