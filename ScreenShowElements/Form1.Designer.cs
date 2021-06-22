
namespace ScreenShowElements
{
	partial class MainWindowForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addBlankingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CtxMenu = new System.Windows.Forms.Button();
			this.toggleShowInTaskbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.webcamZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(697, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addBlankingToolStripMenuItem,
            this.webcamZoomToolStripMenuItem,
            this.toggleShowInTaskbarToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(195, 92);
			// 
			// addBlankingToolStripMenuItem
			// 
			this.addBlankingToolStripMenuItem.Name = "addBlankingToolStripMenuItem";
			this.addBlankingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.addBlankingToolStripMenuItem.Text = "Add blanking";
			this.addBlankingToolStripMenuItem.Click += new System.EventHandler(this.addBlankingToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// CtxMenu
			// 
			this.CtxMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CtxMenu.Location = new System.Drawing.Point(772, 6);
			this.CtxMenu.Name = "CtxMenu";
			this.CtxMenu.Size = new System.Drawing.Size(18, 16);
			this.CtxMenu.TabIndex = 2;
			this.CtxMenu.Text = "...";
			this.CtxMenu.UseVisualStyleBackColor = true;
			this.CtxMenu.Click += new System.EventHandler(this.CtxMenu_Click);
			// 
			// toggleShowInTaskbarToolStripMenuItem
			// 
			this.toggleShowInTaskbarToolStripMenuItem.Name = "toggleShowInTaskbarToolStripMenuItem";
			this.toggleShowInTaskbarToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.toggleShowInTaskbarToolStripMenuItem.Text = "Toggle show in taskbar";
			this.toggleShowInTaskbarToolStripMenuItem.Click += new System.EventHandler(this.toggleShowInTaskbarToolStripMenuItem_Click);
			// 
			// webcamZoomToolStripMenuItem
			// 
			this.webcamZoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.resetToolStripMenuItem});
			this.webcamZoomToolStripMenuItem.Name = "webcamZoomToolStripMenuItem";
			this.webcamZoomToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.webcamZoomToolStripMenuItem.Text = "Webcam zoom";
			// 
			// zoomInToolStripMenuItem
			// 
			this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
			this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.zoomInToolStripMenuItem.Text = "Zoom in";
			this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
			// 
			// zoomOutToolStripMenuItem
			// 
			this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
			this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.zoomOutToolStripMenuItem.Text = "Zoom out";
			this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
			// 
			// resetToolStripMenuItem
			// 
			this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
			this.resetToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.resetToolStripMenuItem.Text = "Reset";
			this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
			// 
			// MainWindowForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.CtxMenu);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "MainWindowForm";
			this.Text = "Maximon screen capture tools";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem addBlankingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.Button CtxMenu;
		private System.Windows.Forms.ToolStripMenuItem toggleShowInTaskbarToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem webcamZoomToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
	}
}

