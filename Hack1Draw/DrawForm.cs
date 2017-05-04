﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Leap;

namespace PerfectPidgeon.Draw
{
    public partial class DrawForm : Form
    {
        private ArtData _ArtData;
        private GameData _Data;
        private Controls _Controls;
        private Renderer _Renderer;

        private int HealthIndex = 200;
        private string WeaponText = "Basic";
        
        public event MouseEventHandler MouseMoved;
        public event MouseEventHandler MouseUpP;
        public event MouseEventHandler MouseDownP;
        public delegate void KeyPressedDelegate(Keys keyData);
        public event KeyPressedDelegate KeyPressed;
        public delegate void AxisRotate(double Angle);
        public event AxisRotate LeftRotate;

        private System.Timers.Timer UpdateFrame;
        private System.Timers.Timer UpdateLeap;
        private System.Timers.Timer Killemll;

        private Joystick _Joystick;
        private PidgeonLeapListener Listen;
        private Controller Control;
        private bool LeapCheck = false;
        private bool LeftDown = false;
        private bool RightDown = false;
        private string Title = "TST";
        public ArtData ArtData
        {
            get
            {
                return _ArtData;
            }

            set
            {
                _ArtData = value;
            }
        }
        public GameData Data
        {
            get
            {
                return _Data;
            }

            set
            {
                _Data = value;
            }
        }

        private int counter = 0;

        public DrawForm()
        {
            InitializeComponent();

            this.Data = new GameData();
            this.ArtData = new ArtData();
            this._Controls = new PerfectPidgeon.Draw.Controls();
            this._Renderer = new Renderer(GLD, Data, ArtData, this._Controls);
            
            MouseMoved = new MouseEventHandler(OnMouseMoved);
            MouseUpP = new MouseEventHandler(OnMouseUp);
            MouseDownP = new MouseEventHandler(OnMouseDown);
            KeyPressed = new KeyPressedDelegate(OnKeyPress);
            LeftRotate = new AxisRotate(OnLeftRotate);

            this._Joystick = new Joystick(0);
            this._Joystick.LeftAxisChange += new AxisEvent(JoystickAxisLeft);
            this._Joystick.RightAxisChange += new AxisEvent(JoystickAxisRight);
            this._Joystick.JoystickButtonPress += new ButtonPress(JoystickButtonPressed);

            Listen = new PidgeonLeapListener();
            Control = new Controller();
            Control.AddListener(Listen);

            UpdateLeap = new System.Timers.Timer();
            UpdateLeap.Enabled = true;
            UpdateLeap.Elapsed += new System.Timers.ElapsedEventHandler(Leap_Tick);
            UpdateLeap.Interval = 30;
            UpdateLeap.Start();
        }
        private void Leap_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!Listen.Connected) return;
            bool LeftPress = Listen.LeftCollected;
            bool RightPress = Listen.RightCollected;
            Point Loc = new Point((int)(Listen.LeftHandLocation.X * 16 + this.Width * 1.3), (int)(-Listen.LeftHandLocation.Y * 16 + this.Height * 3));
            Point LocRot = new Point((int)(Listen.RightHandLocation.X * 16 + this.Width * 1.3), (int)(-Listen.RightHandLocation.Y * 16 + this.Height * 4));
            if (!Listen.LeftHandLocation.Equals(new Point(0, 0)))
            {
                if (LeftPress)
                {
                    MouseDownP.Invoke(null, new MouseEventArgs(System.Windows.Forms.MouseButtons.Right, 1, Loc.X, Loc.Y, 0));
                    MouseUpP.Invoke(null, new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 0, Loc.X, Loc.Y, 0));
                    LeftDown = true;
                }
                else if (LeftDown)
                {
                    MouseDownP.Invoke(null, new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, Loc.X, Loc.Y, 0));
                    MouseUpP.Invoke(null, new MouseEventArgs(System.Windows.Forms.MouseButtons.Right, 0, Loc.X, Loc.Y, 0));
                }
                MouseMoved.Invoke(null, new MouseEventArgs(System.Windows.Forms.MouseButtons.Right, 1, Loc.X, Loc.Y, 0));
            }
        }     
        private void JoystickButtonPressed(JoystickButton Button, bool Pressed)
        {
            if(Button == JoystickButton.Move)
            {
                if(Pressed) MouseDownP.Invoke(null, new MouseEventArgs(MouseButtons.Right, 0, 0, 0, 0));
                else MouseUpP.Invoke(null, new MouseEventArgs(MouseButtons.Right, 0, 0, 0, 0));
            }
            if (Button == JoystickButton.Shoot)
            {
                if (Pressed) MouseDownP.Invoke(null, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                else MouseUpP.Invoke(null, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
            }
        }
        private void JoystickAxisLeft(double Angle)
        {
            
            Vertex V = new Vertex(0, 300);
            V = V.RotateZ(Angle);
            Point P = V.ToPoint();
            MouseMoved.Invoke(null, new MouseEventArgs(MouseButtons.None, 0, -P.X + GLD.Width / 2, -P.Y + GLD.Height / 2, 0));
        }
        private void JoystickAxisRight(double Angle)
        {
            LeftRotate.Invoke(Angle);
        }
        private void GLD_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDownP.Invoke(sender, e);
        }
        public static double GetAngleDegree(Point origin, Point target)
        {
            var n = 270 - (Math.Atan2(origin.Y - target.Y, origin.X - target.X)) * 180 / Math.PI;
            return n % 360;
        }
        private void GLD_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMoved.Invoke(sender, e);
        }
        private void OnMouseMoved(object sender, MouseEventArgs e)
        {
            Point p1 = new Point(GLD.Width / 2, GLD.Height / 2);
            Point p2 = new Point(e.X, e.Y);
            this._Controls.MouseAngle = GetAngleDegree(p2, p1);
            this._Controls.MouseLoc = new Point(e.X, e.Y);
        }
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
        }
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
        }
        private void OnKeyPress(Keys keyData)
        {

        }
        private void OnLeftRotate(double Angle)
        {

        }
        private void GLD_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpP.Invoke(sender, e);
        }
        private void GLDLoad(object sender, EventArgs e)
        {
            this._Renderer.Init();
        }
        private void GLDPaint(object sender, PaintEventArgs e)
        {
            if (!this._Renderer.GLLoaded) return;
            if (this.WindowState == FormWindowState.Minimized || this.Height == 0 || this.Width == 0) return;
            HealthPanel.Size = new System.Drawing.Size(HealthIndex, HealthPanel.Height);
            WeaponLabel.Text = WeaponText;
            LevelTitle.Text = Title;
            if (this._Data.SwapingBuffers)
            {
                return;
            }
            this._Renderer.Draw();
        }
        private void Time_Tick(object sender, EventArgs e)
        {
            GLDPaint(null, null);
        }
        private void GLD_Resize(object sender, EventArgs e)
        {
            
        }
        public void RefreshData(int Health, string Weapon)
        {
            HealthIndex = Health;
            WeaponText = Weapon;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space || keyData == Keys.Escape || keyData == Keys.A || keyData == Keys.D || keyData == Keys.W || keyData == Keys.Q || keyData == Keys.E || keyData == Keys.S || keyData == Keys.D1 || keyData == Keys.D2 || keyData == Keys.D3 || keyData == Keys.D4)
            {
                KeyPressed.Invoke(keyData);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void ImgSwitch_Tick()
        {
            this.Data.ImageSwitch(this.ArtData.SpriteSets);
        }
        private void Killemll_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            Killemll.Interval = 10;
            if (this.ArtData.Environment.G > 0)
            {
                this.ArtData.Environment = Color.FromArgb(this.ArtData.Environment.R, this.ArtData.Environment.G - 1, this.ArtData.Environment.B - 1);
            }
        }
        public void DeathCall()
        {
            Killemll = new System.Timers.Timer();
            Killemll.Enabled = true;
            Killemll.Elapsed += new System.Timers.ElapsedEventHandler(Killemll_Tick);
            Killemll.Interval = 10;
            Killemll.Start();
        }
        public void SetTitle(string Title)
        {
            this.Title = Title;
        }
    }
}