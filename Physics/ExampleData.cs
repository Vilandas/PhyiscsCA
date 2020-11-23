namespace Physics
{
    public class ExampleData
    {
        public static readonly Properties example1 =
            new Properties("Example 1", 9.81, 0, 0.1, 2, 7800,
                new Vector3(0, 0, 200), new Vector3(0, 0, 0), new Vector3(0, 0, 0),
                101, 0, new Vector3(2, 3, 0));

        public static readonly Properties example2 =
            new Properties("Example 2", 9.81, 0, 0.1, 5.5, 7860,
                new Vector3(0, 0, 100), new Vector3(120, 120, 20), new Vector3(-4d, -10d, -2d),
                101, 0.47, new Vector3(2, 3, 0));

        public static readonly Properties example3 =
            new Properties("Example 3", 9.81, 0, 0.1, 5, 7800,
                new Vector3(0, 0, 200), new Vector3(10, 10, 10), new Vector3(-10d / 3d, -5d / 3d, 10d / 3d),
                80, 0.1, new Vector3(2, 3, 0));

        public static readonly Properties custom =
            new Properties("Custom", 9.81, 0, 0.1, 0.05, 7800,
                new Vector3(2, -3, 6), new Vector3(-5, 14, 2), new Vector3(-10d / 3d, -5d / 3d, 10d / 3d),
                80, 0.1, new Vector3(2, 3, 0));
    }
}
