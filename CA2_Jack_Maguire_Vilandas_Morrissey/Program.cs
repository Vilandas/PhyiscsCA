using System;
using System.Diagnostics;

namespace CA2_Jack_Maguire_Vilandas_Morrissey
{
    //Jack Maguire - D00219343
    //Vilandas Morrissey - D00218436

    public class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            double gravity = 9.81;
            double time = 5;
            double h = 0.1;
            double radius = 0.05;
            double density = 7800;
            Vector3 position = new Vector3(2, -3, 6);
            Vector3 velocity = new Vector3(-5, 14, 2);
            Vector3 spin = new Vector3(-10d / 3d, -5d / 3d, 10d / 3d);

            double fluidDensity = 80;
            double dragCoeff = 0.1;
            Vector3 flowRate = new Vector3(2, 3, 0);

            Demo(gravity,
                time, h, radius, density,
                position, velocity, spin,
                fluidDensity, dragCoeff, flowRate);
        }

        public void Demo(double gravity,
            double time, double h, double radius, double density,
            Vector3 position, Vector3 velocity, Vector3 spin,
            double fluidDensity, double dragCoeff, Vector3 flowRate)
        {
            double volume = (4d / 3d) * Math.PI * Math.Pow(radius, 3);
            double mass = density * volume;

            Vector3 va = Vector3.Subtract(velocity, flowRate);
            Vector3 unitVa = Vector3.Normalise(va);
            double vaLength = va.Length();

            Vector3 fGravity = ForceGravity(mass, gravity);
            Vector3 fDrag = ForceDrag(fluidDensity, radius, dragCoeff, vaLength, unitVa);
            Vector3 fMagnus = ForceMagnus(fluidDensity, radius, vaLength, spin, va, unitVa);

            Debug.WriteLine("fGravity: " + fGravity);
            Debug.WriteLine("fDrag: " + fDrag);
            Debug.WriteLine("fMagnus: " + fMagnus);

            Vector3 fNet = fGravity + fDrag + fMagnus;
            Debug.WriteLine("\nfNet: " + fNet);

            Vector3 acceleration = Vector3.Multiply(fNet, 1d/mass);
            Debug.WriteLine("acceleration: " + fNet);

            Eulers(position, velocity, acceleration, time, h);
        }

        //F̅g = -mgk̂
        public Vector3 ForceGravity(double mass, double gravity)
        {
            return Vector3.Multiply(Vector3.Up(), (-mass * gravity));
        }

        //F̅d = ||F̅d|| * F̂d = [½ * P * A * Cd * ||Va||²][-V̂a]
        public Vector3 ForceDrag(double fluidDensity, double radius, double dragCoeff, double vaLength, Vector3 unitVa)
        {
            double fd = (1d / 2d) * fluidDensity * (Math.PI * Math.Pow(radius, 2)) * dragCoeff * Math.Pow(vaLength, 2);
            return Vector3.Multiply(unitVa, -fd);
        }

        //F̅m = ||F̅m|| * F̂m
        public Vector3 ForceMagnus(double fluidDensity, double radius, double vaLength, Vector3 spin, Vector3 va, Vector3 unitVa)
        {
            //F̅m = ||F̅m|| * F̂m
            //Fm = d * r * ||R̅|| * ||V̅a||
            //d = PI/2 * P * r²

            double d = Math.PI / 2d * fluidDensity * Math.Pow(radius, 2);
            double fm = (d * radius * spin.Length() * vaLength);

            //F̂m = R̅ x V̅a / ||R̅ x V̅a||
            Vector3 rXva = Vector3.Cross(spin, va);
            Vector3 fmHat = Vector3.Normalise(rXva);

            return Vector3.Multiply(fmHat, fm);
        }

        public void Rk4(Vector3 position, Vector3 velocity, Vector3 acceleration, double time, double h)
        {

        }

    }
}
