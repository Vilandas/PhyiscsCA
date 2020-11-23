using System;
using GDLibrary.Actors;
using GDLibrary.Managers;
using GDLibrary.Parameters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.UI.Forms;

namespace GDGame.MyGame.Controllers
{
    public class UIController : ControlManager
    {
        private KeyboardManager keyboardManager;
        private TextArea infoPanel;
        private Button inputButton;
        private ModelObject ball;
        private Physics.Properties p;
        private Physics.Rk4 rk4;
        private bool run;
        private bool step;
        private bool mainHidden;
        private bool useGameSpeed;
        private GameTime gameTime;

        private Button[] buttons;
        private Button[] mainButtons;
        private Button[] modifySetters;
        private Button[] modifyButtons;

        public UIController(Game game, KeyboardManager keyboardManager, ModelObject ball) : base(game)
        {
            this.keyboardManager = keyboardManager;
            this.ball = ball;
            this.p = Physics.ExampleData.example1;
            this.run = false;
            this.step = false;
            this.mainHidden = false;
            this.rk4 = new Physics.Rk4(this.p);
            this.useGameSpeed = false;
        }

        public override void InitializeComponent()
        {
            Button btn1 = new Button()
            {
                Text = "1: Example 1",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 20),
                ZIndex = 1
            };

            Button btn2 = new Button()
            {
                Text = "2: Example 2",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 90),
                ZIndex = 1
            };

            Button btn3 = new Button()
            {
                Text = "3: Example 3",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 160),
                ZIndex = 1
            };

            Button btn4 = new Button()
            {
                Text = "4: Custom",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 230),
                ZIndex = 1
            };

            Button btnModify = new Button()
            {
                Text = "5: Modify Custom",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 300),
                ZIndex = 1
            };

            Button btnStart = new Button()
            {
                Text = "C: Start",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 370),
                ZIndex = 1
            };

            Button btnStep = new Button()
            {
                Text = "Space: Step",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 440),
                ZIndex = 1
            };

            Button btnGameSpeed = new Button()
            {
                Text = "Use Game Time\nAs Steps: OFF",
                Size = new Vector2(220, 80),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 510),
                ZIndex = 1
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

            buttons = new Button[] { btn1, btn2, btn3, btn4 };
            mainButtons = new Button[] { btn1, btn2, btn3, btn4, btnModify, btnStart, btnStep, btnGameSpeed };

            foreach (Button button in buttons)
            {
                button.Clicked += ExampleBtn_Clicked;
                button.MouseEnter += Example_MouseEnter;
                button.MouseLeave += Example_MouseLeave;
                Controls.Add(button);
            }

            Controls.Add(btnStart);
            Controls.Add(btnStep);
            Controls.Add(btnModify);
            Controls.Add(btnGameSpeed);
            btnStart.Clicked += BtnStart_Clicked;
            btnStep.Clicked += BtnStep_Clicked;
            btnModify.Clicked += BtnModify_Clicked;
            btnGameSpeed.Clicked += BtnGameSpeed_Clicked;
            Controls.Add(infoPanel);

            InitModifyMenu();
        }

        private void BtnGameSpeed_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if(useGameSpeed)
            {
                useGameSpeed = false;
                btn.Text = "Use Game Time\nAs Steps: OFF";
            }
            else
            {
                useGameSpeed = true;
                btn.Text = "Use Game Time\nAs Steps: ON";
            }
        }

        private void InitModifyMenu()
        {
            inputButton = new Button()
            {
                Text = "",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 20),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };

            Button b1 = new Button()
            {
                Text = "Set Gravity",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 90),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };

            Button b2 = new Button()
            {
                Text = "Set Time",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 160),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };

            Button b3 = new Button()
            {
                Text = "Set Steps",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 230),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };

            Button b4 = new Button()
            {
                Text = "Set Radius",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 300),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };

            Button b5 = new Button()
            {
                Text = "Set Density",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 370),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };

            Button back = new Button()
            {
                Text = "Back",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(1200, 440),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };

            #region Position
            Button b6 = new Button()
            {
                Text = "Position: ",
                Size = new Vector2(220, 20),
                BackgroundColor = Color.Black,
                Location = new Vector2(960, 20),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };

            Button b7 = new Button()
            {
                Text = "p.X",
                Size = new Vector2(73, 30),
                BackgroundColor = Color.Black,
                Location = new Vector2(960, 40),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };
            Button b8 = new Button()
            {
                Text = "p.Y",
                Size = new Vector2(73, 30),
                BackgroundColor = Color.Black,
                Location = new Vector2(1033, 40),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };
            Button b9 = new Button()
            {
                Text = "p.Z",
                Size = new Vector2(74, 30),
                BackgroundColor = Color.Black,
                Location = new Vector2(1106, 40),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };
            #endregion

            #region Velocity
            Button b10 = new Button()
            {
                Text = "Velocity: ",
                Size = new Vector2(220, 20),
                BackgroundColor = Color.Black,
                Location = new Vector2(960, 90),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };

            Button b11 = new Button()
            {
                Text = "v.X",
                Size = new Vector2(73, 30),
                BackgroundColor = Color.Black,
                Location = new Vector2(960, 110),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };
            Button b12 = new Button()
            {
                Text = "v.Y",
                Size = new Vector2(73, 30),
                BackgroundColor = Color.Black,
                Location = new Vector2(1033, 110),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };
            Button b13 = new Button()
            {
                Text = "v.Z",
                Size = new Vector2(74, 30),
                BackgroundColor = Color.Black,
                Location = new Vector2(1106, 110),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };
            #endregion

            #region Spin
            Button b14 = new Button()
            {
                Text = "Spin: ",
                Size = new Vector2(220, 20),
                BackgroundColor = Color.Black,
                Location = new Vector2(960, 160),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };

            Button b15 = new Button()
            {
                Text = "s.X",
                Size = new Vector2(73, 30),
                BackgroundColor = Color.Black,
                Location = new Vector2(960, 180),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };
            Button b16 = new Button()
            {
                Text = "s.Y",
                Size = new Vector2(73, 30),
                BackgroundColor = Color.Black,
                Location = new Vector2(1033, 180),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };
            Button b17 = new Button()
            {
                Text = "s.Z",
                Size = new Vector2(74, 30),
                BackgroundColor = Color.Black,
                Location = new Vector2(1106, 180),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };
            #endregion

            #region FlowRate
            Button b18 = new Button()
            {
                Text = "Flow Rate: ",
                Size = new Vector2(220, 20),
                BackgroundColor = Color.Black,
                Location = new Vector2(960, 230),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };

            Button b19 = new Button()
            {
                Text = "vw.X",
                Size = new Vector2(73, 30),
                BackgroundColor = Color.Black,
                Location = new Vector2(960, 250),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };
            Button b20 = new Button()
            {
                Text = "vw.Y",
                Size = new Vector2(73, 30),
                BackgroundColor = Color.Black,
                Location = new Vector2(1033, 250),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };
            Button b21 = new Button()
            {
                Text = "vw.Z",
                Size = new Vector2(74, 30),
                BackgroundColor = Color.Black,
                Location = new Vector2(1106, 250),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };
            #endregion

            Button b22 = new Button()
            {
                Text = "Set Fluid Density",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(960, 300),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };

            Button b23 = new Button()
            {
                Text = "Set Drag Coeff",
                Size = new Vector2(220, 50),
                BackgroundColor = Color.Black,
                Location = new Vector2(960, 370),
                IsVisible = false,
                Enabled = false,
                ZIndex = 0
            };

            modifySetters = new Button[]{ b1, b2, b3, b4, b5, b6, b7, b8, b9, b10,
                b11, b12, b13, b14, b15, b16, b17, b18, b19, b20, b21, b22, b23 };

            modifyButtons = new Button[]{ b1, b2, b3, b4, b5, b6, b7, b8, b9, b10,
                b11, b12, b13, b14, b15, b16, b17, b18, b19, b20, b21, b22, b23,
                inputButton, back };

            foreach (Button button in modifySetters)
            {
                button.Clicked += ModifyButton_Clicked;
                Controls.Add(button);
            }

            back.Clicked += Back_Clicked;
            Controls.Add(back);
            Controls.Add(inputButton);
        }

        private void ModifyButton_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (inputButton.Text.Length > 0)
            {
                switch (btn.Text)
                {
                    case "Set Gravity":
                        Physics.ExampleData.custom.Gravity = Convert.ToDouble(inputButton.Text);
                        break;
                    case "Set Time":
                        Physics.ExampleData.custom.Time = Convert.ToDouble(inputButton.Text);
                        break;
                    case "Set Steps":
                        Physics.ExampleData.custom.Steps = Physics.ExampleData.custom.OriginalSteps
                            = Convert.ToDouble(inputButton.Text);
                        break;
                    case "Set Radius":
                        Physics.ExampleData.custom.Radius = Convert.ToDouble(inputButton.Text);
                        break;
                    case "Set Density":
                        Physics.ExampleData.custom.Density = Convert.ToDouble(inputButton.Text);
                        break;
                    case "Set Fluid Density":
                        Physics.ExampleData.custom.FluidDensity = Convert.ToDouble(inputButton.Text);
                        break;
                    case "Set Drag Coeff":
                        Physics.ExampleData.custom.DragCoeff = Convert.ToDouble(inputButton.Text);
                        break;
                    case "p.X":
                        Physics.ExampleData.custom.Position.X = Convert.ToDouble(inputButton.Text);
                        break;
                    case "p.Y":
                        Physics.ExampleData.custom.Position.Y = Convert.ToDouble(inputButton.Text);
                        break;
                    case "p.Z":
                        Physics.ExampleData.custom.Position.Z = Convert.ToDouble(inputButton.Text);
                        break;
                    case "v.X":
                        Physics.ExampleData.custom.Velocity.X = Convert.ToDouble(inputButton.Text);
                        break;
                    case "v.Y":
                        Physics.ExampleData.custom.Velocity.Y = Convert.ToDouble(inputButton.Text);
                        break;
                    case "v.Z":
                        Physics.ExampleData.custom.Velocity.Z = Convert.ToDouble(inputButton.Text);
                        break;
                    case "s.X":
                        Physics.ExampleData.custom.Spin.X = Convert.ToDouble(inputButton.Text);
                        break;
                    case "s.Y":
                        Physics.ExampleData.custom.Spin.Y = Convert.ToDouble(inputButton.Text);
                        break;
                    case "s.Z":
                        Physics.ExampleData.custom.Spin.Z = Convert.ToDouble(inputButton.Text);
                        break;
                    case "vw.X":
                        Physics.ExampleData.custom.FlowRate.X = Convert.ToDouble(inputButton.Text);
                        break;
                    case "vw.Y":
                        Physics.ExampleData.custom.FlowRate.Y = Convert.ToDouble(inputButton.Text);
                        break;
                    case "vw.Z":
                        Physics.ExampleData.custom.FlowRate.Z = Convert.ToDouble(inputButton.Text);
                        break;
                }
                ExampleBtn_Clicked(buttons[3], null);
            }
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            ShowMainButtons();
        }

        private void BtnModify_Clicked(object sender, EventArgs e)
        {
            HideMainButtons();
        }

        private void BtnStep_Clicked(object sender, EventArgs e)
        {
            step = true;
        }

        private void BtnStart_Clicked(object sender, EventArgs e)
        {
            run = true;
            if (useGameSpeed)
            {
                p.Steps = gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
                p.Steps = p.OriginalSteps;
        }

        private void Example_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Physics.Properties p = null;
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
            infoPanel.Text = "\n Gravity: " + p.Gravity + "\n Time: " + p.Time + " \n Step Size: " + p.Steps +
                "\n Radius: " + p.Radius + " \n Density: " + p.Density + " \n Position: " + p.OriginalPosition +
                "\n Velocity: " + p.OriginalVelocity + " \n Spin: " + p.Spin + " \n Fluid Density: " + p.FluidDensity +
                "\n Drag Coefficient: " + p.DragCoeff + " \n Flow Rate: " + p.FlowRate;
        }

        private void Example_MouseLeave(object sender, EventArgs e)
        {
            infoPanel.Text = "\n Gravity: " + p.Gravity + "\n Time: " + p.Time + " \n Step Size: " + p.Steps +
                "\n Radius: " + p.Radius + " \n Density: " + p.Density + " \n Position: " + p.Position +
                "\n Velocity: " + p.Velocity + " \n Spin: " + p.Steps + " \n Fluid Density: " + p.FluidDensity +
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
            p.Steps = p.OriginalSteps;
            p.Time = p.OriginalTime;

            rk4 = new Physics.Rk4(p);
            ball.Transform3D.Translation = new Vector3((float)p.OriginalPosition.X, (float)p.OriginalPosition.Z, (float)p.OriginalPosition.Y);
            ball.Transform3D.Scale = new Vector3((float)p.Radius, (float)p.Radius, (float)p.Radius);
        }

        private void HideMainButtons()
        {
            mainHidden = true;
            foreach(Button button in mainButtons)
            {
                button.IsVisible = false;
                button.Enabled = false;
                button.ZIndex = 0;
            }
            foreach (Button button in modifyButtons)
            {
                button.IsVisible = true;
                button.Enabled = true;
                button.ZIndex = 1;
            }
            ExampleBtn_Clicked(buttons[3], null);
        }

        private void ShowMainButtons()
        {
            mainHidden = false;
            foreach (Button button in modifyButtons)
            {
                button.IsVisible = false;
                button.Enabled = false;
                button.ZIndex = 0;
            }
            foreach (Button button in mainButtons)
            {
                button.IsVisible = true;
                button.Enabled = true;
                button.ZIndex = 1;
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            if (run || step)
            {
                Run();
            }

            CheckKeysSet();

            base.Update(gameTime);
        }

        private void Run()
        {
            Physics.Vector2 pv = rk4.CalculateRk4();
            rk4.UpdatePV(pv);
            p.Position = pv.X;
            p.Velocity = pv.Y;
            p.Time += p.Steps;

            if (p.Position.Z < p.Radius)
            {
                p.Position.Z = p.Radius;
                p.Velocity.Z = 0;
                rk4.Data.ExportData();
            }

            ball.Transform3D.Translation = new Vector3((float)p.Position.X, (float)p.Position.Z, (float)p.Position.Y);
            ball.Transform3D.RotateBy(new Vector3((float)p.Spin.X, (float)p.Spin.Z, (float)p.Spin.Y) * (float)(p.Time/p.Steps));

            infoPanel.Text = "\n Gravity: " + p.Gravity + "\n Time: " + p.Time + " \n Step Size: " + p.Steps +
            "\n Radius: " + p.Radius + " \n Density: " + p.Density + " \n Position: " + p.Position +
            "\n Velocity: " + p.Velocity + " \n Spin: " + p.Spin + " \n Fluid Density: " + p.FluidDensity +
            "\n Drag Coefficient: " + p.DragCoeff + " \n Flow Rate: " + p.FlowRate +
            "\n Acceleration: " + rk4.CalculateAcceleration(p.Velocity);

            if (ball.Transform3D.Translation.Y <= ball.Transform3D.Scale.X)
            {
                run = false;
            }
            step = false;
        }

        private void CheckKeysSet()
        {
            if(!mainHidden)
            {
                if (keyboardManager.IsFirstKeyPress(Keys.C))
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
                else if (keyboardManager.IsFirstKeyPress(Keys.D5))
                    BtnModify_Clicked(null, null);
            }
            else
            {
                if (keyboardManager.IsFirstKeyPress(Keys.D1))
                    inputButton.Text += "1";
                else if (keyboardManager.IsFirstKeyPress(Keys.D2))
                    inputButton.Text += "2";
                else if (keyboardManager.IsFirstKeyPress(Keys.D3))
                    inputButton.Text += "3";
                else if (keyboardManager.IsFirstKeyPress(Keys.D4))
                    inputButton.Text += "4";
                else if (keyboardManager.IsFirstKeyPress(Keys.D5))
                    inputButton.Text += "5";
                else if (keyboardManager.IsFirstKeyPress(Keys.D6))
                    inputButton.Text += "6";
                else if (keyboardManager.IsFirstKeyPress(Keys.D7))
                    inputButton.Text += "7";
                else if (keyboardManager.IsFirstKeyPress(Keys.D8))
                    inputButton.Text += "8";
                else if (keyboardManager.IsFirstKeyPress(Keys.D9))
                    inputButton.Text += "9";
                else if (keyboardManager.IsFirstKeyPress(Keys.D0))
                    inputButton.Text += "0";
                else if (keyboardManager.IsFirstKeyPress(Keys.OemPeriod))
                    inputButton.Text += ".";
                else if (keyboardManager.IsFirstKeyPress(Keys.Back))
                {
                    if(inputButton.Text.Length > 0)
                        inputButton.Text = inputButton.Text.Remove(inputButton.Text.Length - 1);
                }
            }
        }
    }
}
