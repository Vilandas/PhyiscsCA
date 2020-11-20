using System;

namespace Physics
{
    public class Vector3 : ICloneable
    {
        #region Fields

        private double x;
        private double y;
        private double z;
        private double length;

        #endregion

        #region Properties

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        public double Length
        {
            get
            {
                if (length != 0)
                    return length;
                return Math.Sqrt((x * x) + (y * y) + (z * z));
            }
        }

        #endregion

        #region Static Constructors

        public static Vector3 Zero()
        {
            return new Vector3(0, 0, 0);
        }

        public static Vector3 One()
        {
            return new Vector3(1, 1, 1);
        }

        public static Vector3 Up()
        {
            return new Vector3(0, 0, 1);
        }

        #endregion

        #region Static Methods

        public static Vector3 Add(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.X + b.X,
                a.Y + b.Y,
                a.Z + b.Z);
        }
        public static Vector3 Subtract(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.X - b.X,
                a.Y - b.Y,
                a.Z - b.Z);
        }
        public static Vector3 Multiply(Vector3 a, double n)
        {
            return new Vector3(
                a.X * n,
                a.Y * n,
                a.Z * n);
        }
        public static Vector3 Multiply(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.X * b.X,
                a.Y * b.Y,
                a.Z * b.Z);
        }
        public static Vector3 Divide(Vector3 a, double n)
        {
            return new Vector3(
                a.X / n,
                a.Y / n,
                a.Z / n);
        }
        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            Vector3 product = Vector3.Zero();
            product.X = (a.Y * b.Z) - (b.Y * a.Z);
            product.Y = -((a.X * b.Z) - (b.X * a.Z));
            product.Z = (a.X * b.Y) - (b.X * a.Y);
            return product;
        }
        public static Vector3 Normalise(Vector3 orignal)
        {
            Vector3 normal = orignal.Clone() as Vector3;
            normal.Divide(normal.Length);
            return normal;
        }

        #endregion

        #region Static Operators

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.X + b.X,
                a.Y + b.Y,
                a.Z + b.Z);
        }
        public static Vector3 operator +(Vector3 a, double b)
        {
            return new Vector3(
                a.X + b,
                a.Y + b,
                a.Z + b);
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.X - b.X,
                a.Y - b.Y,
                a.Z - b.Z);
        }
        public static Vector3 operator -(Vector3 a, double b)
        {
            return new Vector3(
                a.X - b,
                a.Y - b,
                a.Z - b);
        }
        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.X * b.X,
                a.Y * b.Y,
                a.Z * b.Z);
        }
        public static Vector3 operator *(Vector3 a, double b)
        {
            return new Vector3(
                a.X * b,
                a.Y * b,
                a.Z * b);
        }
        public static Vector3 operator /(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.X / b.X,
                a.Y / b.Y,
                a.Z / b.Z);
        }
        public static Vector3 operator /(Vector3 a, double b)
        {
            return new Vector3(
                a.X / b,
                a.Y / b,
                a.Z / b);
        }

        #endregion

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            length = 0;
        }

        public void Normalise()
        {
            Divide(Length);
        }

        public void Multiply(double n)
        {
            x *= n;
            y *= n;
            z *= n;
        }

        public void Multiply(Vector3 other)
        {
            x *= other.X;
            y *= other.Y;
            z *= other.Z;
        }

        public void Add(Vector3 other)
        {
            x += other.X;
            y += other.Y;
            z += other.Z;
        }

        public void Divide(double n)
        {
            x /= n;
            y /= n;
            z /= n;
        }

        public override string ToString()
        {
            return "(x: " + x + ", y: " + y + ", z: " + z + ")";
        }

        public object Clone()
        {
            return new Vector3(X, Y, Z);
        }
    }
}
