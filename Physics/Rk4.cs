using System;

namespace Physics
{
    public class Rk4
    {
        #region Fields

        Properties prop;

        #endregion

        #region Constructor

        public Rk4(Properties prop)
        {
            this.prop = prop;
        }

        #endregion

        public void Start()
        {
            CalculateRk4();
            Eulers(CalculateAcceleration(prop.Velocity));

        }

        public Vector3 CalculateAcceleration(Vector3 velocity)
        {
            Vector3 va = Vector3.Subtract(velocity, prop.FlowRate);

            Vector3 fGravity = ForceGravity();
            Vector3 fDrag = ForceDrag(va);
            Vector3 fMagnus = ForceMagnus(va);

            Vector3 fNet = fGravity + fDrag + fMagnus;

            Vector3 acceleration = Vector3.Multiply(fNet, 1d / prop.Mass);

            //Console.WriteLine("\nfGravity: " + fGravity);
            //Console.WriteLine("fDrag: " + fDrag);
            //Console.WriteLine("fMagnus: " + fMagnus);

            //Console.WriteLine("\nfNet: " + fNet);
            //Console.WriteLine("acceleration: " + fNet);
            return acceleration;
        }

        public Vector3 CalculateAcceleration2(Vector3 velocity)
        {
            Vector3 acceleration = new Vector3(0, -10, 0) + (velocity * 4 * velocity.Length); 
            return acceleration;
        }

        //F̅g = -mgk̂
        public Vector3 ForceGravity()
        {
            return Vector3.Multiply(Vector3.Up(), (-prop.Mass * prop.Gravity));
        }

        //F̅d = ||F̅d|| * F̂d = [½ * P * A * Cd * ||Va||²][-V̂a]
        public Vector3 ForceDrag(Vector3 va)
        {
            double fd = (1d / 2d) * prop.FluidDensity * (Math.PI * Math.Pow(prop.Radius, 2)) * prop.DragCoeff * Math.Pow(va.Length, 2);
            Vector3 unitVa = Vector3.Normalise(va);
            return Vector3.Multiply(unitVa, -fd);
        }

        //F̅m = ||F̅m|| * F̂m
        public Vector3 ForceMagnus(Vector3 va)
        {
            //F̅m = ||F̅m|| * F̂m
            //||Fm|| = d * r * ||R̅|| * ||V̅a||
            //d = PI/2 * P * r²

            double d = Math.PI / 2d * prop.FluidDensity * Math.Pow(prop.Radius, 2);
            double fm = (d * prop.Radius * prop.Spin.Length * va.Length);

            //F̂m = R̅ x V̅a / ||R̅ x V̅a||
            Vector3 rXva = Vector3.Cross(prop.Spin, va);
            Vector3 fmHat = Vector3.Normalise(rXva);

            return Vector3.Multiply(fmHat, fm);
        }

        public void Eulers(Vector3 acceleration)
        {
            double t1 = prop.Time + prop.Steps;
            Vector3 p1 = prop.Position + (prop.Velocity * prop.Steps);
            Vector3 v1 = prop.Velocity + (acceleration * prop.Steps);
            Vector3 a1 = CalculateAcceleration(v1);
            Console.WriteLine("\nEulers: ");
            Console.WriteLine("p1: " + p1);
            Console.WriteLine("v1: " + v1);
            Console.WriteLine("a1: " + a1);
        }

        public Vector2 CalculateRk4()
        {
            Vector2 pv0 = new Vector2(prop.Position, prop.Velocity);
            Vector2 k1 = F(pv0) * prop.Steps;
            Vector2 k2 = F(pv0 + (k1 / 2)) * prop.Steps;
            Vector2 k3 = F(pv0 + (k2 / 2)) * prop.Steps;
            Vector2 k4 = F(pv0 + k3) * prop.Steps;
            Vector2 k = (k1 + (k2 * 2) + (k3 * 2) + k4) * (1d / 6d);
            Vector2 pv1 = pv0 + k;

            //Console.WriteLine("\nRk4: ");
            //Console.WriteLine("pv1: " + pv1);

            return pv1;
        }

        public Vector2 F(Vector2 pv)
        {
            Vector3 acceleration = CalculateAcceleration(pv.Y);
            return new Vector2(pv.Y, acceleration);
        }

        public void UpdatePV(Vector2 pv)
        {
            prop.Position = pv.X;
            prop.Velocity = pv.Y;
        }
    }
}
