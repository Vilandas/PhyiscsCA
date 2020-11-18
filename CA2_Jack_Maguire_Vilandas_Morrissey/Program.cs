using System;
using System.Diagnostics;
using static CA2_Jack_Maguire_Vilandas_Morrissey.Enums;

namespace CA2_Jack_Maguire_Vilandas_Morrissey
{
    //Jack Maguire - D00219343
    //Vilandas Morrissey - D00218436

    public class Program
    {
        private static void Main(string[] args)
        {
            new Program();
        }

        #region Fields

        private string inKey = "\n)> ";
        private double gravity = 9.81;
        private double time = 5;
        private double steps = 0.1;
        private double radius = 0.05;
        private double density = 7800;
        private Vector3 position = new Vector3(2, -3, 6);
        private Vector3 velocity = new Vector3(-5, 14, 2);
        private Vector3 spin = new Vector3(-10d / 3d, -5d / 3d, 10d / 3d);

        private double fluidDensity = 80;
        private double dragCoeff = 0.1;
        private Vector3 flowRate = new Vector3(2, 3, 0);

        #endregion

        #region Properties

        public double Gravity { get; set; }
        public double Time { get; set; }
        public double Steps { get; set; }
        public double Radius { get; set; }
        public double Density { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Spin { get; set; }
        public double FluidDensity { get; set; }
        public double DragCoeff { get; set; }
        public Vector3 FlowRate { get; set; }

        #endregion

        public Program()
        {
            Rk4(new Vector3(-3, 2, 0), new Vector3(1, 3, 0), 3.6, 1, 3.5);
            //Start();
        }

        public void Demo(double gravity,
            double time, double h, double radius, double density,
            Vector3 position, Vector3 velocity, Vector3 spin,
            double fluidDensity, double dragCoeff, Vector3 flowRate)
        {
            double volume = (4d / 3d) * Math.PI * Math.Pow(radius, 3);
            double mass = density * volume;

            Vector3 va = Vector3.Subtract(velocity, flowRate);
            double vaLength = va.Length();

            Vector3 fGravity = ForceGravity(mass, gravity);
            Vector3 fDrag = ForceDrag(fluidDensity, radius, dragCoeff, vaLength, va);
            Vector3 fMagnus = ForceMagnus(fluidDensity, radius, vaLength, spin, va);

            Console.WriteLine("\nfGravity: " + fGravity);
            Console.WriteLine("fDrag: " + fDrag);
            Console.WriteLine("fMagnus: " + fMagnus);

            Vector3 fNet = fGravity + fDrag + fMagnus;
            Console.WriteLine("\nfNet: " + fNet);

            Vector3 acceleration = Vector3.Multiply(fNet, 1d / mass);
            Console.WriteLine("acceleration: " + fNet);



            
            
        }

        //F̅g = -mgk̂
        public Vector3 ForceGravity(double mass, double gravity)
        {
            return Vector3.Multiply(Vector3.Up(), (-mass * gravity));
        }

        //F̅d = ||F̅d|| * F̂d = [½ * P * A * Cd * ||Va||²][-V̂a]
        public Vector3 ForceDrag(double fluidDensity, double radius, double dragCoeff, double vaLength, Vector3 va)
        {
            double fd = (1d / 2d) * fluidDensity * (Math.PI * Math.Pow(radius, 2)) * dragCoeff * Math.Pow(vaLength, 2);
            Vector3 unitVa = Vector3.Normalise(va);
            return Vector3.Multiply(unitVa, -fd);
        }

        //F̅m = ||F̅m|| * F̂m
        public Vector3 ForceMagnus(double fluidDensity, double radius, double vaLength, Vector3 spin, Vector3 va)
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

        public void Rk4(Vector3 position0, Vector3 velocity0, double tFinal, double steps, double time)
        {
            double h;
            Vector2 k, k1, k2, k3, k4, PV1;
            Vector2 PV = new Vector2(position0, velocity0);
            Console.WriteLine(PV);

            h = (tFinal - time) / steps;
            //K1
            k1 = func(time, PV) * h;
            //K2
            k2 = func((time + h) / 2, (PV + k1) / 2) * h;
            //K3
            k3 = func((time + h) / 2, (PV + k2) / 2) * h;
            //K4
            k4 = func((time + h), (PV + k3)) * h;

            k = (k1+(k2 * 2d) + (k3 * 2d) + k4) * (1/6);
            Console.WriteLine( "k1" + k1);
            //Console.WriteLine(k2);
            //Console.WriteLine(k3);
            //Console.WriteLine(k4);


            PV1 = PV + k;
            //Console.WriteLine(PV1);
        }

        public Vector2 func(double time, Vector2 PV)
        {
            Vector2 PVfunc = PV * time;
            Console.WriteLine(PVfunc);
            return PVfunc;
        }

        public void Rk42(Vector3 position, Vector3 velocity, Vector3 acceleration, double steps, double time)
        {
            Vector2 y = new Vector2(position, velocity);
            Vector2 k1 = y * steps;
            Vector2 k2 = F(time + steps / 2d, y + k1 / 2d);
            Vector2 k3 = F(time + steps/2d, y + k2/2);
            Vector2 k4 = F(time + steps, y + k3);
            Vector2 k = k3 * 2d;
            //Vector2 k = (k1 + (2 * k2) + (2 * k3) + k4);
        }

        public Vector2 F(double time, Vector2 y)
        {
            return y * time;
        }



        public void Start()
        {
            bool run = true;
            while (run)
            {
                MenuOptions menuChoice = (MenuOptions)Enum.Parse(typeof(MenuOptions), GetChoice(typeof(MenuOptions)).ToString());
                switch (menuChoice)
                {
                    case MenuOptions.Exit:
                        run = false;
                        break;
                    case MenuOptions.Example_one:
                        Demo(9.81,
                            5, 0.1, 0.05, 7800,
                            new Vector3(2, -3, 6), new Vector3(-5, 14, 2), new Vector3(-10d / 3d, -5d / 3d, 10d / 3d),
                            80, 0.1, new Vector3(2, 3, 0));
                        break;
                    case MenuOptions.Example_two:
                        Demo(9.81,
                            5, 0.1, 0.05, 7800,
                            new Vector3(2, -3, 6), new Vector3(-5, 14, 2), new Vector3(-10d / 3d, -5d / 3d, 10d / 3d),
                            80, 0.1, new Vector3(2, 3, 0));
                        break;
                    case MenuOptions.Example_three:
                        Demo(9.81,
                            5, 0.1, 0.05, 7800,
                            new Vector3(2, -3, 6), new Vector3(-5, 14, 2), new Vector3(-10d / 3d, -5d / 3d, 10d / 3d),
                            80, 0.1, new Vector3(2, 3, 0));
                        break;
                    case MenuOptions.Custom:
                        Demo(gravity,
                            time, steps, radius, density,
                            position, velocity, spin,
                            fluidDensity, dragCoeff, flowRate);
                        break;
                    case MenuOptions.Modify_Custom:
                        ModifyMenu();
                        break;
                }
            }
        }

        private void ModifyMenu()
        {
            bool run = true;

            while(run)
            {
                DisplayCurrentValues();
                ModifyOptions menuChoice = (ModifyOptions)Enum.Parse(typeof(ModifyOptions), GetChoice(typeof(ModifyOptions)).ToString());
                
                if(menuChoice != ModifyOptions.Exit)
                    Console.WriteLine("\nModifying " + Enums.GetModifyName(menuChoice) + ":");

                switch (menuChoice)
                {
                    case ModifyOptions.Exit:
                        run = false;
                        break;
                    case ModifyOptions.Modify_Gravity:
                        gravity = GetDouble();
                        break;
                    case ModifyOptions.Modify_Time:
                        time = GetDouble();
                        break;
                    case ModifyOptions.Modify_Time_Steps:
                        steps = GetDouble();
                        break;
                    case ModifyOptions.Modify_Radius:
                        radius = GetDouble();
                        break;
                    case ModifyOptions.Modify_Density:
                        density = GetDouble();
                        break;
                    case ModifyOptions.Modify_DragCoefficent:
                        dragCoeff = GetDouble();
                        break;
                    case ModifyOptions.Modify_FluidDensity:
                        fluidDensity = GetDouble();
                        break;
                    case ModifyOptions.Modify_Position:
                        position = GetVector();
                        break;
                    case ModifyOptions.Modify_Velocity:
                        velocity = GetVector();
                        break;
                    case ModifyOptions.Modify_Spin:
                        spin = GetVector();
                        break;
                    case ModifyOptions.Modify_FlowRate:
                        flowRate = GetVector();
                        break;
                }
            }
        }

        private void DisplayCurrentValues()
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

        /**
        * Ask user for input and check if it is in the menu choice range.
        * @return Menu choice number (int)
        */
        private object GetChoice(Type enumType)
        {
            while (true)
            {
                DisplayMenu(enumType);
                int number = GetInt();

                Array menus = Enum.GetValues(enumType);
                if (number < 0 || number >= menus.Length)
                {
                    Console.WriteLine("\nInvalid number");
                    continue;
                }
                else return menus.GetValue(number);
            }
        }

        /**
        * Display menu options
        */
        private void DisplayMenu(Type enumType)
        {
            Console.WriteLine("\nInput number to select option: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string[] names = Enums.GetNames(enumType);
            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine("\t" + i + ")" + names[i]);
            }
            Console.ResetColor();
            Console.Write("Enter a number" + inKey);
        }

        /**
        * Ask user for input until they enter a valid Integer number
        * @return integer number
        */
        public int GetInt()
        {
            string input;
            do
            {
                input = Console.ReadLine();
            } while (!IsValidNumber(input, false));
            return Convert.ToInt32(input);
        }

        /**
        * Ask user for input until they enter a valid Double number
        * @return long number
        */
        private double GetDouble()
        {
            string input;
            do
            {
                input = Console.ReadLine();
            } while (!IsValidNumber(input, true));
            return Convert.ToDouble(input);
        }

        private Vector3 GetVector()
        {
            Console.WriteLine("Input X value" + inKey);
            double x = GetDouble();
            Console.WriteLine("Input Y value" + inKey);
            double y = GetDouble();
            Console.WriteLine("Input Z value" + inKey);
            double z = GetDouble();

            return new Vector3(x, y, z);
        }

        /**
        * Check to see if an input can be parsed to int or double.
        * @param input String of user inputted numbers/letters
        * @param isDouble boolean if true, check to see if the input can be parsed to Double instead of Integer.
        * @return True if the input is numeric and can be parsed. False otherwise.
        */
        private bool IsValidNumber(string input, bool isDouble)
        {
            if (input == null)
            {
                DisplayInvalidInputError();
                return false;
            }
            try
            {
                if(isDouble)
                {
                    Convert.ToDouble(input);
                }
                else Convert.ToInt32(input);
                return true;
            }
            catch (FormatException)
            {
                DisplayInvalidInputError();
                return false;
            }
            catch (OverflowException)
            {
                DisplayInvalidInputError();
                return false;
            }
        }

        private void DisplayInvalidInputError()
        {
            Console.WriteLine("\nInvalid input, numbers only");
            Console.Write("\nEnter a number" + inKey);
        }

    }
}