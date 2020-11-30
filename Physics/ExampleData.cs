namespace Physics
{
    public class ExampleData
    {
        //public static readonly Properties example1 =
        //    new Properties("Example 1", 9.81, 0, 0.1, 0.055, 7860,
        //        new Vector3(0, 0, 200), new Vector3(0, 0, 0), new Vector3(0, 0, 0),
        //        1.225, 0, new Vector3(2, 3, 0));

        public static readonly Properties example1 =
            new Properties("Example 1", 9.81, 0, 0.1, 0.05, 7800,
                new Vector3(2, -3, 6), new Vector3(-5, 14, 2), new Vector3(-10d/3d, -5d/3d, 10d/3d),
                80, 0.1, new Vector3(2, 3, 0));

        public static readonly Properties example2 =
            new Properties("Example 2", 9.81, 0, 0.1, 0.055, 7860,
                new Vector3(0, 0, 3), new Vector3(120, 60, 20), new Vector3(-4d, -10d, -2d),
                1.225, 0.47, new Vector3(2, 3, 0));
        
        public static readonly Properties example3 =
            new Properties("Example 3", 9.81, 0, 0.02, 0.12, 700,
                new Vector3(8, 4, 2), new Vector3(12, 2, 6), new Vector3(0, -1, -1),
                1.225, 0.47, new Vector3(0, 0, 0));

        public static readonly Properties custom =
            new Properties("Example 2", 9.81, 0, 0.1, 5.5, 7860,
                new Vector3(0, 0, 100), new Vector3(120, 120, 20), new Vector3(-4d, -10d, -2d),
                1.225, 0.47, new Vector3(2, 3, 0));
    }
}
