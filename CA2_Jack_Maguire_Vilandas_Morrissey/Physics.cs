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

        private Vector3 va;

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
            Rk42(CalculateAcceleration());
            Rk4();
            Eulers(CalculateAcceleration());

        }

        public Vector3 CalculateAcceleration()
        {
            va = Vector3.Subtract(velocity, flowRate);

            Vector3 fGravity = ForceGravity();
            Vector3 fDrag = ForceDrag();
            Vector3 fMagnus = ForceMagnus();

            Vector3 fNet = fGravity + fDrag + fMagnus;

            Vector3 acceleration = Vector3.Multiply(fNet, 1d / mass);

            //Console.WriteLine("\nfGravity: " + fGravity);
            //Console.WriteLine("fDrag: " + fDrag);
            //Console.WriteLine("fMagnus: " + fMagnus);

            //Console.WriteLine("\nfNet: " + fNet);
            //Console.WriteLine("acceleration: " + fNet);
            return acceleration;
        }

        //F̅g = -mgk̂
        public Vector3 ForceGravity()
        {
            return Vector3.Multiply(Vector3.Up(), (-mass * gravity));
        }

        //F̅d = ||F̅d|| * F̂d = [½ * P * A * Cd * ||Va||²][-V̂a]
        public Vector3 ForceDrag()
        {
            double fd = (1d / 2d) * fluidDensity * (Math.PI * Math.Pow(radius, 2)) * dragCoeff * Math.Pow(va.Length, 2);
            Vector3 unitVa = Vector3.Normalise(va);
            return Vector3.Multiply(unitVa, -fd);
        }

        //F̅m = ||F̅m|| * F̂m
        public Vector3 ForceMagnus()
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
            Vector3 a1 = CalculateAcceleration();
            Console.WriteLine("\nEulers: ");
            Console.WriteLine("p1: " + p1);
            Console.WriteLine("v1: " + v1);
            Console.WriteLine("a1: " + a1);
        }

        public void Rk4()
        {
            double h;
            Vector2 k, k1, k2, k3, k4, PV1;
            Vector2 PV = new Vector2(position, velocity);

            //h = (tFinal - time) / steps;
            h = steps;
            //K1
            k1 = func(time, PV) * h;
            //K2
            k2 = func((time + h) / 2, (PV + k1) / 2) * h;
            //K3
            k3 = func((time + h) / 2, (PV + k2) / 2) * h;
            //K4
            k4 = func((time + h), (PV + k3)) * h;

            k = (k1 + (k2 * 2d) + (k3 * 2d) + k4) * (1d / 6d);
            //Console.WriteLine("k1" + k1);
            //Console.WriteLine(k2);
            //Console.WriteLine(k3);
            //Console.WriteLine(k4);


            PV1 = PV + k;
            Console.WriteLine("\n" + PV1);
        }

        public Vector2 func(double time, Vector2 PV)
        {
            Vector2 PVfunc = PV * time;
            //Console.WriteLine(PVfunc);
            return PVfunc;
        }

        public void Rk42(Vector3 acceleration)
        {
            Vector2 pv0 = new Vector2(position, velocity);
            Vector2 k1 = new Vector2(velocity, acceleration) * steps;
            Vector2 k2 = F(steps / 2d, pv0 + k1 / 2) * steps;
            Vector2 k3 = F(steps / 2d, pv0 + k2 / 2) * steps;
            Vector2 k4 = F(steps, pv0 + k3) * steps;
            Vector2 k = (k1 + (k2 * 2) + (k3 * 2) + k4);
            Vector2 pv1 = pv0 + k;

            Console.WriteLine("\nRk4: ");
            Console.WriteLine("pv0: " + pv0);
            Console.WriteLine("pv1: " + pv1);
        }

        public Vector2 F(double time, Vector2 y)
        {
            return new Vector2(y.Y, CalculateAcceleration() * time);
        }
    }
}
