using System;

namespace CA2_Jack_Maguire_Vilandas_Morrissey
{
    public class Physics
    {
        #region Fields

        private double gravity;
        private double time;
        private double steps;
        private double radius;
        private double density;
        private Vector3 position;
        private Vector3 velocity;
        private Vector3 spin;

        private double fluidDensity;
        private double dragCoeff;
        private Vector3 flowRate;

        private double volume;
        private double mass;

        #endregion

        #region Constructor

        public Physics(double gravity,
            double time, double steps, double radius, double density,
            Vector3 position, Vector3 velocity, Vector3 spin,
            double fluidDensity, double dragCoeff, Vector3 flowRate)
        {
            this.gravity = gravity;
            this.time = time;
            this.steps = steps;
            this.radius = radius;
            this.density = density;

            this.position = position;
            this.velocity = velocity;
            this.spin = spin;

            this.fluidDensity = fluidDensity;
            this.dragCoeff = dragCoeff;
            this.flowRate = flowRate;

            this.volume = (4d / 3d) * Math.PI * Math.Pow(radius, 3);
            this.mass = density * volume;
        }

        #endregion

        public void Start()
        {
            Rk4();
            Eulers(CalculateAcceleration(velocity));

        }

        public Vector3 CalculateAcceleration(Vector3 velocity)
        {
            Vector3 va = Vector3.Subtract(velocity, flowRate);

            Vector3 fGravity = ForceGravity();
            Vector3 fDrag = ForceDrag(va);
            Vector3 fMagnus = ForceMagnus(va);

            Vector3 fNet = fGravity + fDrag + fMagnus;

            Vector3 acceleration = Vector3.Multiply(fNet, 1d / mass);

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
            return Vector3.Multiply(Vector3.Up(), (-mass * gravity));
        }

        //F̅d = ||F̅d|| * F̂d = [½ * P * A * Cd * ||Va||²][-V̂a]
        public Vector3 ForceDrag(Vector3 va)
        {
            double fd = (1d / 2d) * fluidDensity * (Math.PI * Math.Pow(radius, 2)) * dragCoeff * Math.Pow(va.Length, 2);
            Vector3 unitVa = Vector3.Normalise(va);
            return Vector3.Multiply(unitVa, -fd);
        }

        //F̅m = ||F̅m|| * F̂m
        public Vector3 ForceMagnus(Vector3 va)
        {
            //F̅m = ||F̅m|| * F̂m
            //Fm = d * r * ||R̅|| * ||V̅a||
            //d = PI/2 * P * r²

            double d = Math.PI / 2d * fluidDensity * Math.Pow(radius, 2);
            double fm = (d * radius * spin.Length * va.Length);

            //F̂m = R̅ x V̅a / ||R̅ x V̅a||
            Vector3 rXva = Vector3.Cross(spin, va);
            Vector3 fmHat = Vector3.Normalise(rXva);

            return Vector3.Multiply(fmHat, fm);
        }

        public void Eulers(Vector3 acceleration)
        {
            double t1 = time + steps;
            Vector3 p1 = position + (velocity * steps);
            Vector3 v1 = velocity + (acceleration * steps);
            Vector3 a1 = CalculateAcceleration(v1);
            Console.WriteLine("\nEulers: ");
            Console.WriteLine("p1: " + p1);
            Console.WriteLine("v1: " + v1);
            Console.WriteLine("a1: " + a1);
        }

        public void Rk4()
        {
            Vector2 pv0 = new Vector2(position, velocity);
            Vector2 k1 = F(pv0) * steps;
            Vector2 k2 = F(pv0 + (k1 / 2)) * steps;
            Vector2 k3 = F(pv0 + (k2 / 2)) * steps;
            Vector2 k4 = F(pv0 + k3) * steps;
            Vector2 k = (k1 + (k2 * 2) + (k3 * 2) + k4) * (1d / 6d);
            Vector2 pv1 = pv0 + k;

            Console.WriteLine("\nRk4: ");
            Console.WriteLine("pv1: " + pv1);
        }

        public Vector2 F(Vector2 y)
        {
            Vector3 acceleration = CalculateAcceleration(y.Y);
            return new Vector2(y.Y, acceleration);
        }
    }
}
