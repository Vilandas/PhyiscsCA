namespace Physics
{
    public class ExampleData
    {
        public static readonly Properties example1 =
            new Properties(9.81, 0, 0.1, 0.05, 7800,
                new Vector3(2, -3, 6), new Vector3(-5, 14, 2), new Vector3(-10d / 3d, -5d / 3d, 10d / 3d),
                80, 0.1, new Vector3(2, 3, 0));

        public static readonly Properties example2 =
            new Properties(9.81, 0, 0.1, 0.05, 7800,
                new Vector3(2, -3, 6), new Vector3(-5, 14, 2), new Vector3(-10d / 3d, -5d / 3d, 10d / 3d),
                80, 0.1, new Vector3(2, 3, 0));

        public static readonly Properties example3 =
            new Properties(9.81, 0, 0.1, 0.05, 7800,
                new Vector3(2, -3, 6), new Vector3(-5, 14, 2), new Vector3(-10d / 3d, -5d / 3d, 10d / 3d),
                80, 0.1, new Vector3(2, 3, 0));

        public static readonly Properties custom =
            new Properties(9.81, 0, 0.1, 0.05, 7800,
                new Vector3(2, -3, 6), new Vector3(-5, 14, 2), new Vector3(-10d / 3d, -5d / 3d, 10d / 3d),
                80, 0.1, new Vector3(2, 3, 0));
    }
}
