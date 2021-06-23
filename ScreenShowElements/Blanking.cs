using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShowElements
{
	public partial class Blanking : Form
	{
		public Blanking()
		{
			InitializeComponent();
			this.BackColor = Color.FromArgb(43, 87, 154);
			this.TopMost = true;
			this.StartPosition = FormStartPosition.Manual;
			this.Location = new Point(1369, -5);
			this.ShowInTaskbar = false;
			this.MouseDown += Blanking_MouseDown;
			//this.Move += Blanking_Move;
		}

		private class Cfg
		{
			internal string Description;
			internal int X, Y, Width, Height;
			internal int R, G, B;

			public Cfg(string description, int x, int y, int width, int height, int r, int g, int b)
			{
				Description = description;
				X = x;
				Y = y;
				Width = width;
				Height = height;
				R = r;
				G = g;
				B = b;
			}

			virtual public void ApplyTo(Blanking blnk)
			{
				blnk.BackColor = Color.FromArgb(R,G,B);
				blnk.TopMost = true;
				blnk.StartPosition = FormStartPosition.Manual;
				blnk.Location = new Point(X, Y);
				blnk.Size = new Size(Width, Height);
				blnk.ShowInTaskbar = false;
			}
		}
		private class Cfg_Calibration : Cfg
		{
			public Cfg_Calibration(string description, int x, int y, int width, int height, int r, int g, int b)
				: base(description, x, y, width, height, r, g, b) { }
			override public void ApplyTo(Blanking blnk)
			{
				base.ApplyTo(blnk);
				blnk.ShowInTaskbar = true;
				//blnk.FormBorderStyle = FormBorderStyle.Sizable;
				blnk.Move += blnk.Blanking_Move;
			}
		}

		internal void Config(int configIdx)
		{
			var Cfgs = new Cfg[]
			{
				new Cfg_Calibration("Test to find optimal position", 512,512,100,100,255,0,0),
				new Cfg("Blanks out Word account name", 1369, -5, 112, 28, 43, 87, 154),
				new Cfg("Blanks out sys tray and clock", 1532, 1020, 150, 28, 38,85,116)
			};
			Cfgs[configIdx].ApplyTo(this);
		}

		private void Blanking_Move(object sender, EventArgs e)
		{
			this.Text = "(" + this.Location.X + "," + this.Location.Y + ")";
		}

		private void Blanking_MouseDown(object sender, MouseEventArgs e)
		{
			switch (e.Button)
			{
				case MouseButtons.Left:
					FloatyCamBox.ReleaseCapture();
					FloatyCamBox.SendMessage(this.Handle, FloatyCamBox.WM_NCLBUTTONDOWN, FloatyCamBox.HT_CAPTION, IntPtr.Zero);
					break;
			}
		}
	}
}
