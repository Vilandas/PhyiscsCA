using System;
using GDLibrary.Actors;
using GDLibrary.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.UI.Forms;

namespace GDGame.MyGame.Controllers
{
    public class UIController : ControlManager
    {
        private KeyboardManager keyboardManager;
        private TextArea infoPanel;
        private ModelObject ball;
        private Physics.Properties p;
        private Physics.Rk4 rk4;
        private bool run;
        private bool step;

        Button[] buttons;

        public UIController(Game game, KeyboardManager keyboardManager, ModelObject ball) : base(game)
        {
            this.keyboardManager = keyboardManager;
            this.ball = ball;
            this.p = Physics.ExampleData.example1;
            this.run = false;
            this.step = false;
        }

        public override void InitializeComponent()
        {
            Button btn1 = new Button()
            {
                Text = "1: Example 1",
                Size = new Vector2(200, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1220, 20)
            };

            Button btn2 = new Button()
            {
                Text = "2: Example 2",
                Size = new Vector2(200, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1220, 90)
            };

            Button btn3 = new Button()
            {
                Text = "3: Example 3",
                Size = new Vector2(200, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1220, 160)
            };

            Button btn4 = new Button()
            {
                Text = "4: Custom",
                Size = new Vector2(200, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1220, 230)
            };

            Button btnStart = new Button()
            {
                Text = "S: Start",
                Size = new Vector2(200, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1220, 300)
            };

            Button btnStep = new Button()
            {
                Text = "Space: Step",
                Size = new Vector2(200, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1220, 370)
            };


            infoPanel = new TextArea()
            {
                Text = "\n Gravity: \n Time: \n Step Size: " +
                "\n Radius: \n Density: \n Position: " +
                "\n Velocity: \n Spin: \n Fluid Density: " +
                "\n Drag Coefficient: \n Flow Rate:",
                FontName = "Assets/Fonts/MyFont",
                TextColor = Color.Black,
                Location = new Vector2(20, 560)
            };

            buttons = new Button[]{ btn1, btn2, btn3, btn4 };

            foreach(Button button in buttons)
            {
                button.Clicked += ExampleBtn_Clicked;
                button.MouseEnter += Example_MouseEnter;
                button.MouseLeave += Example_MouseEnter;
                Controls.Add(button);
            }

            Controls.Add(btnStart);
            Controls.Add(btnStep);
            btnStart.Clicked += BtnStart_Clicked;
            btnStep.Clicked += BtnStep_Clicked;
            Controls.Add(infoPanel);
        }

        private void BtnStep_Clicked(object sender, EventArgs e)
        {
            step = true;
        }

        private void BtnStart_Clicked(object sender, EventArgs e)
        {
            run = true; 
        }

        private void Example_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Physics.Properties p = null;
            switch(btn.Text)
            {
                case "1: Example 1":
                    p = Physics.ExampleData.example1;
                    break;
                case "2: Example 2":
                    p = Physics.ExampleData.example2;
                    break;
                case "3: Example 3":
                    p = Physics.ExampleData.example3;
                    break;
                case "4: Custom":
                    p = Physics.ExampleData.custom;
                    break;
            }
            infoPanel.Text = "\n Gravity: " + p.Gravity + "\n Time: " + p.Time + " \n Step Size: " + p.Steps +
                "\n Radius: " + p.Radius + " \n Density: " + p.Density + " \n Position: " + p.OriginalPosition +
                "\n Velocity: " + p.OriginalVelocity + " \n Spin: " + p.Spin + " \n Fluid Density: " + p.FluidDensity +
                "\n Drag Coefficient: " + p.DragCoeff + " \n Flow Rate: " + p.FlowRate;
        }

        private void Example_MouseLeave(object sender, EventArgs e)
        {
            infoPanel.Text = "\n Gravity: " + p.Gravity + "\n Time: " + p.Time + " \n Step Size: " + p.Steps +
                "\n Radius: " + p.Radius + " \n Density: " + p.Density + " \n Position: " + p.Position +
                "\n Velocity: " + p.Velocity + " \n Spin: " + p.Spin + " \n Fluid Density: " + p.FluidDensity +
                "\n Drag Coefficient: " + p.DragCoeff + " \n Flow Rate: " + p.FlowRate;

        }

        private void ExampleBtn_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Text)
            {
                case "1: Example 1":
                    p = Physics.ExampleData.example1;
                    break;
                case "2: Example 2":
                    p = Physics.ExampleData.example2;
                    break;
                case "3: Example 3":
                    p = Physics.ExampleData.example3;
                    break;
                case "4: Custom":
                    p = Physics.ExampleData.custom;
                    break;
            }

            Example_MouseEnter(btn, null);

            p.Position = p.OriginalPosition;
            p.Velocity = p.OriginalVelocity;

            rk4 = new Physics.Rk4(p);
            ball.Transform3D.Translation = new Vector3((float)p.OriginalPosition.X, (float)p.OriginalPosition.Z, (float)p.OriginalPosition.Y);
            ball.Transform3D.Scale = new Vector3((float)p.Radius, (float)p.Radius, (float)p.Radius);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(run || step)
            {
                Physics.Vector2 pv = rk4.CalculateRk4();
                rk4.UpdatePV(pv);

                p.Position = pv.X;
                p.Velocity = pv.Y;

                ball.Transform3D.Translation = new Vector3((float)p.Position.X, (float)p.Position.Z, (float)p.Position.Y);
                
                infoPanel.Text = "\n Gravity: " + p.Gravity + "\n Time: " + p.Time + " \n Step Size: " + p.Steps +
                "\n Radius: " + p.Radius + " \n Density: " + p.Density + " \n Position: " + p.Position +
                "\n Velocity: " + p.Velocity + " \n Spin: " + p.Spin + " \n Fluid Density: " + p.FluidDensity +
                "\n Drag Coefficient: " + p.DragCoeff + " \n Flow Rate: " + p.FlowRate;

                if (ball.Transform3D.Translation.Y <= ball.Transform3D.Scale.X)
                {
                    run = false;
                }
                step = false;
            }

            if (keyboardManager.IsFirstKeyPress(Keys.S))
                BtnStart_Clicked(null, null);
            else if (keyboardManager.IsFirstKeyPress(Keys.Space))
                BtnStep_Clicked(null, null);
            else if (keyboardManager.IsFirstKeyPress(Keys.D1))
                ExampleBtn_Clicked(buttons[0], null);
            else if (keyboardManager.IsFirstKeyPress(Keys.D2))
                ExampleBtn_Clicked(buttons[1], null);
            else if (keyboardManager.IsFirstKeyPress(Keys.D3))
                ExampleBtn_Clicked(buttons[2], null);
            else if (keyboardManager.IsFirstKeyPress(Keys.D4))
                ExampleBtn_Clicked(buttons[3], null);

        }
    }
}
