using System;

namespace Physics
{
    public class Properties
    {
        #region Fields

        private double gravity;
        private double time;
        private double steps;
        private double radius;
        private double density;
        private Vector3 position, originalPosition;
        private Vector3 velocity, originalVelocity;
        private Vector3 spin;

        private double fluidDensity;
        private double dragCoeff;
        private Vector3 flowRate;

        private double volume;
        private double mass;

        #endregion

        #region Properties

        public double Gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }
        public double Time
        {
            get { return time; }
            set { time = value; }
        }
        public double Steps
        {
            get { return steps; }
            set { steps = value; }
        }
        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        public double Density
        {
            get { return density; }
            set { density = value; }
        }
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector3 OriginalPosition
        {
            get { return originalPosition; }
            set { originalPosition = value; }
        }
        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public Vector3 OriginalVelocity
        {
            get { return originalVelocity; }
            set { originalVelocity = value; }
        }
        public Vector3 Spin
        {
            get { return spin; }
            set { spin = value; }
        }
        public double FluidDensity
        {
            get { return fluidDensity; }
            set { fluidDensity = value; }
        }
        public double DragCoeff
        {
            get { return dragCoeff; }
            set { dragCoeff = value; }
        }
        public Vector3 FlowRate
        {
            get { return flowRate; }
            set { flowRate = value; }
        }
        public double Volume
        {
            get { return (4d / 3d) * Math.PI * Math.Pow(radius, 3); }
        }
        public double Mass
        {
            get { return density * volume; }
        }

        #endregion

        #region Constructor

        public Properties(double gravity,
            double time, double steps, double radius, double density,
            Vector3 position, Vector3 velocity, Vector3 spin,
            double fluidDensity, double dragCoeff, Vector3 flowRate)
        {
            this.gravity = gravity;
            this.time = time;
            this.steps = steps;
            this.radius = radius;
            this.density = density;

            this.position = this.originalPosition = position;
            this.velocity = this.originalVelocity = velocity;
            this.spin = spin;

            this.fluidDensity = fluidDensity;
            this.dragCoeff = dragCoeff;
            this.flowRate = flowRate;
        }

        #endregion

        public void DisplayDetails()
        {
            Console.WriteLine("\nGravity: " + gravity);
            Console.WriteLine("Time: " + time);
            Console.WriteLine("Time-Steps: " + steps);

            Console.WriteLine("Radius: " + radius);
            Console.WriteLine("Density: " + density);
            Console.WriteLine("Position: " + position);
            Console.WriteLine("Velocity: " + velocity);
            Console.WriteLine("Spin: " + spin);

            Console.WriteLine("Fluid Density: " + fluidDensity);
            Console.WriteLine("Drag Coefficent: " + dragCoeff);
            Console.WriteLine("Flow Rate: " + flowRate);
        }
    }
}
