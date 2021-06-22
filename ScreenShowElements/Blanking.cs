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
