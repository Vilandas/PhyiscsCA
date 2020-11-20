using System;
using Microsoft.Xna.Framework;
using MonoGame.UI.Forms;

namespace GDGame.MyGame.Controllers
{
    public class UIController : ControlManager
    {
        private TextArea infoPanel;

        public UIController(Game game) : base(game)
        {
        }

        public override void InitializeComponent()
        {
            Button btn1 = new Button()
            {
                Text = "Example 1",
                Size = new Vector2(200, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1220, 20)
            };

            infoPanel = new TextArea()
            {
                Text = "\n Gravity: \n Time: \n Step Size: " +
                "\n Radius: \n Density: \n Position: " +
                "\n Velocity: \n Spin: \n Fluid Density: " +
                "\n Drag Coefficient: \n Flow Rate: \n",
                TextColor = Color.White,
                BackgroundColor = Color.Black,
                Location = new Vector2(20, 630)
            };

            Controls.Add(btn1);
            Controls.Add(infoPanel);

            btn1.Clicked += Btn1_Clicked;
            btn1.MouseEnter += Example_MouseEnter;
        }

        private void Example_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if(btn.Text.Equals("Example 1"))
            {
                Physics.Properties p = Physics.ExampleData.example1;
                infoPanel.Text = "\n Gravity: " + p.Gravity + "\n Time: " + p.Time + " \n Step Size: " + p.Steps +
                "\n Radius: " + p.Radius + " \n Density: " + p.Density + " \n Position: " + p.OriginalPosition +
                "\n Velocity: " + p.OriginalVelocity + " \n Spin: " + p.Spin + " \n Fluid Density: " + p.FluidDensity +
                "\n Drag Coefficient: " + p.DragCoeff + " \n Flow Rate: \n" + p.FlowRate;
            }
        }

        private void Btn1_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
        }
    }
}
