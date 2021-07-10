using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ScreenShowElements
{
	internal static class PInvoke
	{
		// https://www.codeproject.com/Articles/11114/Move-window-form-without-Titlebar-in-C is ostensibly subject to GPL3, but the code sample lacks creativity (it is more or less the most straightforward way to make a draggable window in WinForms, and any alternative would probably be a more roundabout way of obfuscating the same underlying logic).
		// In any case: WM_ / HT_ definitions are from Win32 library; SendMessage/ReleaseCapture definitions are from P/Invoke and non of these 4 lines are verbatim same as the CodeProject link above; the MyOnMouseDown method is functionally equivalent to Form1_MouseDown from the CodeProject document, but the only line that is verbatim identical is "ReleaseCapture();", and that contains no creative input (being literally just a function call).
		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;
		[DllImport("user32.dll")]
		internal static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);
		[DllImport("user32.dll")]
		internal static extern bool ReleaseCapture();
	}
}
