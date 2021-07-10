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
	public partial class FloatyCamBox : Form
	{
		public FloatyCamBox()
		{
			this.MouseDown += MyOnMouseDown;
			InitializeComponent();
		}

		internal PictureBox MyWebcam;

		internal double Zoom { get; private set; }

		internal void AttachWebcam(PictureBox pictureBox)
		{
			MyWebcam = pictureBox;
			this.Controls.Add(pictureBox);
			pictureBox.MouseDown += MyOnMouseDown;
			pictureBox.Dock = DockStyle.None;
			ReZoom(1.5);
		}

		internal void ReZoom(double NewZoom)
		{
			Zoom = NewZoom;
			MyWebcam.Width = (int)(this.Width * Zoom);
			MyWebcam.Height = (int)(this.Height * Zoom);
			MyWebcam.Left = (int)((this.Width-MyWebcam.Width) * 0.5);
			MyWebcam.Top = (int)((this.Height-MyWebcam.Height) * 0.5);
		}

		private void MyOnMouseDown(object sender, MouseEventArgs e)
		{
			switch (e.Button)
			{ 
				case MouseButtons.Left:
					PInvoke.ReleaseCapture();
					PInvoke.SendMessage(this.Handle, PInvoke.WM_NCLBUTTONDOWN, PInvoke.HT_CAPTION, IntPtr.Zero);
					break;
			}
		}

	}
}
