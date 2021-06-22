using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebcamCapturer.Controls.WinForms;
using WebcamCapturer.Controls;
using WebcamCapturer.Core;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ScreenShowElements
{
	public partial class MainWindowForm : Form
	{
		WebcamCaptureForm view;
		WebcamCapturePresenter presenter;

		public MainWindowForm()
		{
			InitializeComponent();

			this.StartPosition = FormStartPosition.Manual;
			this.Size = new Size(800, 200);
			this.TopMost = true;

			int MyPaddingX = 100;
			int MyPaddingY = (int)(MyPaddingX * 0.5);
			var Scrn = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
			this.Location = new Point(MyPaddingX, Scrn.Height - 200 - MyPaddingY);

			this.SizeChanged += MainWindowForm_SizeChanged;
			this.FormClosing += Form1_FormClosing;
			this.MouseDown += MainWindowForm_MouseDown;

			this.label1.Text = "";

			StartKeyboardHook();

			try
			{
				/*var WC = new WebcamCapturer.Controls.WinForms.WebcamCaptureForm();
				WC.Location = new System.Drawing.Point(10, 50);
				WC.Size = new System.Drawing.Size(100, 100);
				WC.TopLevel = false;
				this.Controls.Add(WC);*/

				view = new WebcamCaptureForm();
				presenter = new WebcamCapturePresenter(view);
				view.Show();
			}
			catch(Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}

		private void MainWindowForm_MouseDown(object sender, MouseEventArgs e)
		{
			switch (e.Button)
			{
				case MouseButtons.Left:
					FloatyCamBox.ReleaseCapture();
					FloatyCamBox.SendMessage(this.Handle, FloatyCamBox.WM_NCLBUTTONDOWN, FloatyCamBox.HT_CAPTION, IntPtr.Zero);
					break;
			}
		}

		private void MainWindowForm_SizeChanged(object sender, EventArgs e)
		{
			ResizeKeys();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			StopKeyboardHook();

			if (view is object)
				view.Visible = true;

			if (Floaty is object)
			{
				// Give the webcam control back to the webcam view window.
				if(Floaty.MyWebcam is object && OutpContainer is object)
				{
					OutpContainer.Controls.Add(Floaty.MyWebcam);
					Floaty.MyWebcam = null;
				}

				Floaty.Close();
			}

			// Disconnect webcam
			if (BtnDisconnect is object)
				BtnDisconnect.PerformClick();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			void Iter(Control C, StringBuilder SB, string Pfx)
			{
				foreach (Control X in C.Controls)
				{
					SB.Append(Pfx).Append(X.Name).Append(" | ").AppendLine(X.Text);
					Iter(X, SB, Pfx + "- ");
				}
			}

			var xSB = new StringBuilder();
			Iter(view, xSB, "");
			MessageBox.Show(xSB.ToString());
		}

		ComboBox Combo;
		Button BtnConnect, BtnDisconnect;
		PictureBox Outp;
		FloatyCamBox Floaty;
		Control OutpContainer;

		private void Form1_Load(object sender, EventArgs e)
		{
			LoadWebcam();
			LoadKeyboard();
		}

		private void LoadWebcam()
		{
			Combo = (view.Controls["splitContainer1"].Controls[0].Controls["comboBox1"] as ComboBox);
			BtnConnect = (view.Controls["splitContainer1"].Controls[0].Controls["BtnConnect"] as Button);
			BtnDisconnect = (view.Controls["splitContainer1"].Controls[0].Controls["BtnDisconnect"] as Button);
			OutpContainer = view.Controls["splitContainer1"].Controls[1].Controls["splitContainer2"].Controls[0];
			Outp = (OutpContainer.Controls["PbCamImage"] as PictureBox);

			Combo.SelectedIndex = -1;
			Combo.SelectedIndex = 0;
			BtnConnect.PerformClick();

			int FloatyWidth = 100;
			int FloatyHeight = FloatyWidth; //(int)(((double)FloatyWidth) * 0.85);// 640x480, so height should be 3/4 of width, but that doesn't seem to look right
			int FloatyPaddingX = FloatyHeight;
			int FloatyPaddingY = (int)(FloatyPaddingX * 0.5);

			var Scrn = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;

			Floaty = new FloatyCamBox();
			Floaty.WindowState = FormWindowState.Normal;
			Floaty.StartPosition = FormStartPosition.Manual;
			Floaty.BringToFront();
			Floaty.Size = new Size(FloatyWidth, FloatyHeight);
			Floaty.Location = new Point(Scrn.Width - FloatyWidth - FloatyPaddingX, Scrn.Height - FloatyHeight - FloatyPaddingY);
			Floaty.TopMost = true;
			Floaty.ShowInTaskbar = false;
			Floaty.Show();

			Floaty.AttachWebcam(Outp);

			view.Visible = false;
		}


		/// <summary>
		/// Simple abstraction to build a row
		/// </summary>
		private class Row
		{
			List<char> Chars=new List<char>();

			/// <summary>
			/// Append a char to the row
			/// </summary>
			public Row C(char val)
			{
				Chars.Add(val);
				return this;
			}
			/// <summary>
			/// Append a key to the row
			/// </summary>
			public Row K(Keys val)
			{
				Chars.Add((char)val);
				return this;
			}
			/// <summary>
			/// Append a string to the row
			/// </summary>
			public Row S(string val)
			{
				Chars.AddRange(val);
				return this;
			}

			public override string ToString()
			{
				return new string(Chars.ToArray());
			}
		}

		Dictionary<int, string> KeyNames = new Dictionary<int, string>() { { 0, "No key pressed" }, { 1, "Mouse 1" }, { 2, "Mouse 2" }, { 3, "Cancel" }, { 4, "Mouse 3" }, { 5, "Mouse 4" }, { 6, "Mouse 5" }, { 8, "←" }, { 9, "↹" }, { 10, "Linefeed" }, { 12, "Clear" }, { 13, "Enter" }, { 16, "Shift" }, { 17, "Ctrl" }, { 18, "Alt" }, { 19, "Pause" }, { 20, "Caps lock" }, { 21, "IME Hangul mode" }, { 22, "IME Kana mode" }, { 23, "IME Junja mode" }, { 24, "IME final mode" }, { 25, "IME Hanja mode" }, { 26, "IME Kanji mode" }, { 27, "Esc" }, { 28, "IME convert" }, { 29, "IME nonconvert" }, { 30, "IME accept key" }, { 31, "IME mode change" }, { 32, "Space" }, { 33, "PgUp" }, { 34, "PgDn" }, { 35, "End" }, { 36, "Home" }, { 37, '←'.ToString() }, { 38, '↑'.ToString() }, { 39, '→'.ToString() }, { 40, '↓'.ToString() }, { 41, "Select" }, { 42, "Print" }, { 43, "Exec" }, { 44, "PrtScr" }, { 45, "Ins" }, { 46, "Del" }, { 47, "Help" }, { 48, "0" }, { 49, "1" }, { 50, "2" }, { 51, "3" }, { 52, "4" }, { 53, "5" }, { 54, "6" }, { 55, "7" }, { 56, "8" }, { 57, "9" }, { 65, "A" }, { 66, "B" }, { 67, "C" }, { 68, "D" }, { 69, "E" }, { 70, "F" }, { 71, "G" }, { 72, "H" }, { 73, "I" }, { 74, "J" }, { 75, "K" }, { 76, "L" }, { 77, "M" }, { 78, "N" }, { 79, "O" }, { 80, "P" }, { 81, "Q" }, { 82, "R" }, { 83, "S" }, { 84, "T" }, { 85, "U" }, { 86, "V" }, { 87, "W" }, { 88, "X" }, { 89, "Y" }, { 90, "Z" }, { 91, "Win" }, { 92, "Win" }, { 93, "Menu key" }, { 95, "Sleep" }, { 96, "`" }, { 97, "Num 1" }, { 98, "Num 2" }, { 99, "Num 3" }, { 100, "Num 4" }, { 101, "Num 5" }, { 102, "Num 6" }, { 103, "Num 7" }, { 104, "Num 8" }, { 105, "Num 9" }, { 106, "Num *" }, { 107, "Num +" }, { 108, "separator" }, { 109, "Num -" }, { 110, "Num ." }, { 111, "Num /" }, { 112, "F1" }, { 113, "F2" }, { 114, "F3" }, { 115, "F4" }, { 116, "F5" }, { 117, "F6" }, { 118, "F7" }, { 119, "F8" }, { 120, "F9" }, { 121, "F10" }, { 122, "F11" }, { 123, "F12" }, { 124, "F13" }, { 125, "F14" }, { 126, "F15" }, { 127, "F16" }, { 128, "F17" }, { 129, "F18" }, { 130, "F19" }, { 131, "F20" }, { 132, "F21" }, { 133, "F22" }, { 134, "F23" }, { 135, "F24" }, { 144, "Num lock" }, { 145, "Scroll lock" }, { 160, "Shift" }, { 161, "Shift" }, { 162, "Ctrl" }, { 163, "Ctrl" }, { 164, "Alt" }, { 165, "Alt" }, { 166, "Browser back" }, { 167, "Browser forward" }, { 168, "Browser refresh" }, { 169, "Browser stop" }, { 170, "Browser search" }, { 171, "Browser favorites" }, { 172, "Browser home" }, { 173, "Vol mute" }, { 174, "Vol-" }, { 175, "Vol+" }, { 176, '⏭'.ToString() }, { 177, '⏮'.ToString() }, { 178, '⏹'.ToString() }, { 179, '⏯'.ToString() }, { 180, "Mail" }, { 181, "Media" }, { 182, "App 1" }, { 183, "App 2" }, { 186, "OEM 1" }, { 187, "OEM +" }, { 188, "," }, { 189, "-" }, { 190, "." }, { 191, "/" }, { 192, "=" }, { 219, "[" }, { 220, "OEM 5" }, { 221, "]" }, { 222, "'" }, { 223, "OEM 8" }, { 226, "\\" }, { 229, "Process key" }, { 231, "Unicode character" }, { 246, "Attn" }, { 247, "CRSEL" }, { 248, "EXSEL" }, { 249, "ERASE EOF" }, { 250, "Play" }, { 251, "Zoom" }, { 252, "RSVD" }, { 253, "PA1" }, { 254, "Clear" }, { 65535, "KVAL" }, { 65536, "Shift mod" }, { 131072, "Ctrl mod" }, { 262144, "Alt mod" } }; // "LEFT"},


		class KeyBtn
		{
			double X;
			int Y;
			public double WidthMultiplier;
			public int Code;

			public Button Btn;

			public KeyBtn(Button btn, char code, double x, int y)
			{
				Btn = btn;
				X = x;
				Y = y;
				Code = code;
				WidthMultiplier = 1.0;
			}
			public void Resize(int KeyWidth, int KeyHeight)
			{
				Btn.Size = new Size((int)(KeyWidth * WidthMultiplier), KeyHeight);
				Btn.Location = new Point((int)(KeyWidth * X), KeyHeight * Y);
			}
		}

		List<KeyBtn> KeyBtns;


		static char LittleBreak = (char)0, NoKey = (char)1, ExtendLastKey = (char)2, PartExtendLastKey = (char)3;
		private List<string> _KeyRows = null;
		private List<string> GetKeyRows()
		{
			if (_KeyRows is object)
				return _KeyRows;

			_KeyRows = new List<string>();

			{
				// Row 1
				var R = new Row().K(Keys.Escape).C(LittleBreak);
				for (char C = (char)Keys.F1; C <= (char)Keys.F12; ++C)
				{
					if (((int)(C - (char)Keys.F1)) % 4 == 0)
						R.C(LittleBreak);
					R.C(C);
				}
				_KeyRows.Add(R.ToString());
			}
			// Rows 2ff.
			_KeyRows.Add(new Row().S("`1234567890").K(Keys.OemMinus).C('=').K(Keys.Oemtilde).K(Keys.Back).C(LittleBreak).K(Keys.Insert).K(Keys.Home).K(Keys.PageUp).ToString());
			_KeyRows.Add(new Row().C('\t').C(PartExtendLastKey).S("QWERTYUIOP").K(Keys.OemOpenBrackets).K(Keys.OemCloseBrackets).K(Keys.OemBackslash).C(PartExtendLastKey).C(LittleBreak).K(Keys.Delete).K(Keys.End).K(Keys.PageDown).ToString());
			_KeyRows.Add(new Row().K(Keys.CapsLock).C(ExtendLastKey).S("ASDFGHJKL;").K(Keys.OemQuotes).K(Keys.Enter).C(ExtendLastKey).C(LittleBreak).C(NoKey).C(NoKey).C(NoKey).ToString());
			_KeyRows.Add(new Row().K(Keys.LShiftKey).C(ExtendLastKey).C(PartExtendLastKey).S("ZXCVBNM").K(Keys.Oemcomma).K(Keys.OemPeriod).K(Keys.OemQuestion).K(Keys.RShiftKey).C(ExtendLastKey).C(PartExtendLastKey).C(LittleBreak).C(NoKey).K(Keys.Up).C(NoKey).ToString());
			_KeyRows.Add(new Row().K(Keys.LControlKey).C(ExtendLastKey).K(Keys.LWin).K(Keys.Menu).K(Keys.Space).K(Keys.RMenu).K(Keys.RWin).K(Keys.RControlKey).C(LittleBreak).K(Keys.Left).K(Keys.Down).K(Keys.Right).ToString());

			return _KeyRows;
		}

		Color MyDefaultBtnColor;

		private void LoadKeyboard()
		{
			var KeyRows = GetKeyRows();

			KeyBtns = new List<KeyBtn>();
			int CurrentY = 0;
			foreach(var R in KeyRows)
			{
				double CurrentX = 0;
				KeyBtn LastKey = null;

				foreach (var C in R)
				{
					if (C == LittleBreak)
						CurrentX += 0.5;
					else if (C == NoKey)
						CurrentX += 1.0;
					else if (C == ExtendLastKey)
					{
						if (LastKey is object)
							LastKey.WidthMultiplier += 1.0;
						CurrentX += 1.0;
					}
					else if(C== PartExtendLastKey)
					{
						if (LastKey is object)
							LastKey.WidthMultiplier += 0.5;
						CurrentX += 0.5;
					}
					else
					{
						string Lbl;
						double WidthMult = 1.0;
						if(C==' ')
						{
							Lbl = "";
							WidthMult = 8.0;
						}
						else if (!KeyNames.TryGetValue((int)C, out Lbl))
							Lbl = C.ToString();
						Button Btn = new Button();
						MyDefaultBtnColor = Btn.BackColor;
						Btn.TabStop = false;
						Btn.Name = "Key_" + ((int)C).ToString();
						Btn.Text = Lbl;
						this.Controls.Add(Btn);
						LastKey = new KeyBtn(Btn, C, CurrentX, CurrentY);
						LastKey.WidthMultiplier = WidthMult;
						KeyBtns.Add(LastKey);

						CurrentX += WidthMult;
					}
				}

				++CurrentY;
			}

			ResizeKeys();
		}

		private void ResizeKeys()
		{
			if (!(KeyBtns is object))
				return;

			var KeyRows = GetKeyRows();

			int KeyHeight = this.Size.Height / KeyRows.Count;
			int KeyWidth = 0;
			foreach (var R in KeyRows)
			{
				int NumBreaks = R.Count(C => C == LittleBreak || C == PartExtendLastKey);
				int aKeyWidth = (int)(this.Size.Width / (((double)(R.Length - NumBreaks)) + (NumBreaks * 0.5)));
				if (KeyWidth == 0 || aKeyWidth < KeyWidth)
					KeyWidth = aKeyWidth;
			}
			foreach (var aKB in KeyBtns)
				aKB.Resize(KeyWidth, KeyHeight);
		}

		const int WH_KEYBOARD = 2, WH_KEYBOARD_LL = 13;
		delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);
		//delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, [In] KBDLLHOOKSTRUCT lParam);
		HookProc MyHookProc;
		//LowLevelKeyboardProc MyHookProc_LL;
		IntPtr hHook;
		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr SetWindowsHookEx(int hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);
		//[DllImport("user32.dll", SetLastError = true)]
		//static extern IntPtr SetWindowsHookEx(int hookType, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
		//[DllImport("coredll.dll", EntryPoint = "GetModuleHandleW", SetLastError = true)]
		//static extern IntPtr GetModuleHandle(string moduleName);
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);
		[DllImport("user32.dll")]
		static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam,  IntPtr lParam);
		//[DllImport("user32.dll")]
		//static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, [In] KBDLLHOOKSTRUCT lParam);
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool UnhookWindowsHookEx(IntPtr hhk);
		[DllImport("kernel32.dll")]
		static extern uint GetCurrentThreadId();
		const int HC_ACTION = 0;

		private void StartKeyboardHook()
		{
			try
			{
				MyHookProc = DoMyHookProc_LL;
				using (Process process = Process.GetCurrentProcess())
				using (ProcessModule module = process.MainModule)
				{
					IntPtr hModule = GetModuleHandle(module.ModuleName);
					hHook = SetWindowsHookEx(WH_KEYBOARD_LL, MyHookProc, hModule, 0);
					//hHook = SetWindowsHookEx(WH_KEYBOARD_LL, MyHookProc, IntPtr.Zero, GetCurrentThreadId());
				}
			}
			catch(Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}
		private void StopKeyboardHook()
		{
			if (hHook != null)
			{
				UnhookWindowsHookEx(hHook);
				hHook = IntPtr.Zero;
				MyHookProc = null;
			}
		}
		private IntPtr DoMyHookProc(int code, IntPtr wParam, IntPtr lParam)
		{
			if (code==HC_ACTION && KeyBtns is object)
			{
				int wParamInt = wParam.ToInt32();

				this.label1.Text = wParamInt.ToString();

				var Btn = KeyBtns.Where(M => M.Code == wParamInt);

				if (Btn.Any())
				{
					bool TransitionState = (((lParam.ToInt32() >> 31) & 1) == 1);

					if (!TransitionState) // KeyUp
					{
						foreach(var aBtn in Btn)
							aBtn.Btn.BackColor = Color.LightBlue;
					}
					else // KeyDown
					{
						foreach (var aBtn in Btn)
							aBtn.Btn.BackColor = MyDefaultBtnColor;
					}
				}
			}

			return CallNextHookEx(hHook, code, wParam, lParam);
		}
		const int WM_KEYDOWN = 0x100, WM_KEYUP = 0x101, WM_SYSKEYDOWN = 0x104, WM_SYSKEYUP = 0x105;
		[StructLayout(LayoutKind.Sequential)]
		public class KBDLLHOOKSTRUCT
		{
			public uint vkCode;
			public uint scanCode;
			public KBDLLHOOKSTRUCTFlags flags;
			public uint time;
			public UIntPtr dwExtraInfo;
		}

		[Flags]
		public enum KBDLLHOOKSTRUCTFlags : uint
		{
			LLKHF_EXTENDED = 0x01,
			LLKHF_INJECTED = 0x10,
			LLKHF_ALTDOWN = 0x20,
			LLKHF_UP = 0x80,
		}
		
		private IntPtr DoMyHookProc_LL(int code, IntPtr wParam, IntPtr lParam)
		//private IntPtr DoMyHookProc_LL(int code, IntPtr wParam, [In] KBDLLHOOKSTRUCT lParam)
		{
			if (code == HC_ACTION && KeyBtns is object)
			{
				int wParamInt = wParam.ToInt32();

				bool TransitionState = (wParamInt == WM_KEYUP || wParamInt == WM_SYSKEYUP);

				KBDLLHOOKSTRUCT kbd = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

				//label1.Text = kbd.vkCode + "/" + kbd.scanCode;

				var Btn = KeyBtns.Where(M => M.Code == kbd.vkCode);

				if (Btn.Any())
				{
					if (!TransitionState) // KeyUp
					{
						foreach (var aBtn in Btn)
							aBtn.Btn.BackColor = Color.LightBlue;
					}
					else // KeyDown
					{
						foreach (var aBtn in Btn)
							aBtn.Btn.BackColor = Color.Transparent;
					}
				}
			}

			return CallNextHookEx(hHook, code, wParam, lParam);
		}

	}
}
