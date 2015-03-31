namespace Centivus.Services
{
	partial class ServiceDebugger
	{
#if DEBUGSERVICE
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceDebugger));
			this.pauseToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
			this.powerToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.resumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.etcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sessionChangeToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this._eventLogListView = new System.Windows.Forms.ListView();
			this.typeColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.dateTimeColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.messageColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.sourceColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.categoryColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.eventIDColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.rawDataColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.serviceToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.startButton = new System.Windows.Forms.ToolStripButton();
			this.shutdownToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
			this.stopToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
			this.stopButton = new System.Windows.Forms.ToolStripButton();
			this.eventLogImages = new System.Windows.Forms.ImageList(this.components);
			this.pauseToolStrip.SuspendLayout();
			this.powerToolStrip.SuspendLayout();
			this.sessionChangeToolStrip.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.serviceToolStrip.SuspendLayout();
			this.shutdownToolStrip.SuspendLayout();
			this.stopToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// pauseToolStrip
			// 
			this.pauseToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.pauseToolStrip.Enabled = false;
			this.pauseToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.toolStripButton3,
            this.toolStripButton4});
			this.pauseToolStrip.Location = new System.Drawing.Point(80, 0);
			this.pauseToolStrip.Name = "pauseToolStrip";
			this.pauseToolStrip.Size = new System.Drawing.Size(163, 25);
			this.pauseToolStrip.TabIndex = 1;
			this.pauseToolStrip.Text = "toolStrip2";
			// 
			// toolStripLabel4
			// 
			this.toolStripLabel4.Name = "toolStripLabel4";
			this.toolStripLabel4.Size = new System.Drawing.Size(105, 22);
			this.toolStripLabel4.Text = "Can Pause/Continue";
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton3.Text = "toolStripButton3";
			// 
			// toolStripButton4
			// 
			this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
			this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton4.Name = "toolStripButton4";
			this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton4.Text = "toolStripButton4";
			// 
			// powerToolStrip
			// 
			this.powerToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.powerToolStrip.Enabled = false;
			this.powerToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.toolStripDropDownButton1});
			this.powerToolStrip.Location = new System.Drawing.Point(329, 0);
			this.powerToolStrip.Name = "powerToolStrip";
			this.powerToolStrip.Size = new System.Drawing.Size(171, 25);
			this.powerToolStrip.TabIndex = 2;
			this.powerToolStrip.Text = "toolStrip3";
			// 
			// toolStripLabel3
			// 
			this.toolStripLabel3.Name = "toolStripLabel3";
			this.toolStripLabel3.Size = new System.Drawing.Size(130, 22);
			this.toolStripLabel3.Text = "Can handle Power events";
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resumeToolStripMenuItem,
            this.etcToolStripMenuItem});
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
			this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
			// 
			// resumeToolStripMenuItem
			// 
			this.resumeToolStripMenuItem.Name = "resumeToolStripMenuItem";
			this.resumeToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.resumeToolStripMenuItem.Text = "Resume";
			// 
			// etcToolStripMenuItem
			// 
			this.etcToolStripMenuItem.Name = "etcToolStripMenuItem";
			this.etcToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.etcToolStripMenuItem.Text = "etc";
			// 
			// sessionChangeToolStrip
			// 
			this.sessionChangeToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.sessionChangeToolStrip.Enabled = false;
			this.sessionChangeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.toolStripTextBox2,
            this.toolStripButton5});
			this.sessionChangeToolStrip.Location = new System.Drawing.Point(3, 25);
			this.sessionChangeToolStrip.Name = "sessionChangeToolStrip";
			this.sessionChangeToolStrip.Size = new System.Drawing.Size(376, 25);
			this.sessionChangeToolStrip.TabIndex = 3;
			this.sessionChangeToolStrip.Text = "toolStrip4";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(137, 22);
			this.toolStripLabel1.Text = "Can handle session change";
			// 
			// toolStripTextBox1
			// 
			this.toolStripTextBox1.Name = "toolStripTextBox1";
			this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
			this.toolStripTextBox1.Text = "ID";
			// 
			// toolStripTextBox2
			// 
			this.toolStripTextBox2.Name = "toolStripTextBox2";
			this.toolStripTextBox2.Size = new System.Drawing.Size(100, 25);
			// 
			// toolStripButton5
			// 
			this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
			this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton5.Name = "toolStripButton5";
			this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton5.Text = "toolStripButton5";
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this._eventLogListView);
			this.toolStripContainer1.ContentPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(570, 262);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(570, 312);
			this.toolStripContainer1.TabIndex = 4;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.serviceToolStrip);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.pauseToolStrip);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.shutdownToolStrip);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.stopToolStrip);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.powerToolStrip);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.sessionChangeToolStrip);
			// 
			// _eventLogListView
			// 
			this._eventLogListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.typeColumnHeader,
            this.dateTimeColumnHeader,
            this.messageColumnHeader,
            this.sourceColumnHeader,
            this.categoryColumnHeader,
            this.eventIDColumnHeader,
            this.rawDataColumnHeader});
			this._eventLogListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this._eventLogListView.FullRowSelect = true;
			this._eventLogListView.Location = new System.Drawing.Point(0, 0);
			this._eventLogListView.Name = "_eventLogListView";
			this._eventLogListView.ShowItemToolTips = true;
			this._eventLogListView.Size = new System.Drawing.Size(570, 262);
			this._eventLogListView.TabIndex = 0;
			this._eventLogListView.UseCompatibleStateImageBehavior = false;
			this._eventLogListView.View = System.Windows.Forms.View.Details;
			// 
			// typeColumnHeader
			// 
			this.typeColumnHeader.Text = "Type";
			this.typeColumnHeader.Width = 43;
			// 
			// dateTimeColumnHeader
			// 
			this.dateTimeColumnHeader.Text = "Date Time";
			this.dateTimeColumnHeader.Width = 94;
			// 
			// messageColumnHeader
			// 
			this.messageColumnHeader.Text = "Message";
			this.messageColumnHeader.Width = 236;
			// 
			// sourceColumnHeader
			// 
			this.sourceColumnHeader.Text = "Source";
			// 
			// categoryColumnHeader
			// 
			this.categoryColumnHeader.Text = "Category";
			// 
			// eventIDColumnHeader
			// 
			this.eventIDColumnHeader.Text = "Event ID";
			// 
			// rawDataColumnHeader
			// 
			this.rawDataColumnHeader.Text = "Raw Data Size";
			this.rawDataColumnHeader.Width = 88;
			// 
			// serviceToolStrip
			// 
			this.serviceToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.serviceToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.startButton});
			this.serviceToolStrip.Location = new System.Drawing.Point(3, 0);
			this.serviceToolStrip.Name = "serviceToolStrip";
			this.serviceToolStrip.Size = new System.Drawing.Size(77, 25);
			this.serviceToolStrip.TabIndex = 4;
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(42, 22);
			this.toolStripLabel2.Text = "Service";
			// 
			// startButton
			// 
			this.startButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.startButton.Image = ((System.Drawing.Image)(resources.GetObject("startButton.Image")));
			this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(23, 22);
			this.startButton.Text = "toolStripButton2";
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// shutdownToolStrip
			// 
			this.shutdownToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.shutdownToolStrip.Enabled = false;
			this.shutdownToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel5,
            this.toolStripButton6});
			this.shutdownToolStrip.Location = new System.Drawing.Point(380, 25);
			this.shutdownToolStrip.Name = "shutdownToolStrip";
			this.shutdownToolStrip.Size = new System.Drawing.Size(112, 25);
			this.shutdownToolStrip.TabIndex = 1;
			this.shutdownToolStrip.Text = "toolStrip2";
			// 
			// toolStripLabel5
			// 
			this.toolStripLabel5.Name = "toolStripLabel5";
			this.toolStripLabel5.Size = new System.Drawing.Size(77, 22);
			this.toolStripLabel5.Text = "Can Shutdown";
			// 
			// toolStripButton6
			// 
			this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
			this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton6.Name = "toolStripButton6";
			this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton6.Text = "toolStripButton1";
			// 
			// stopToolStrip
			// 
			this.stopToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.stopToolStrip.Enabled = false;
			this.stopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel6,
            this.stopButton});
			this.stopToolStrip.Location = new System.Drawing.Point(243, 0);
			this.stopToolStrip.Name = "stopToolStrip";
			this.stopToolStrip.Size = new System.Drawing.Size(86, 25);
			this.stopToolStrip.TabIndex = 1;
			this.stopToolStrip.Text = "toolStrip2";
			// 
			// toolStripLabel6
			// 
			this.toolStripLabel6.Name = "toolStripLabel6";
			this.toolStripLabel6.Size = new System.Drawing.Size(51, 22);
			this.toolStripLabel6.Text = "Can Stop";
			// 
			// stopButton
			// 
			this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.stopButton.Enabled = false;
			this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
			this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(23, 22);
			this.stopButton.Text = "stopButton";
			this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
			// 
			// eventLogImages
			// 
			this.eventLogImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("eventLogImages.ImageStream")));
			this.eventLogImages.TransparentColor = System.Drawing.Color.Transparent;
			this.eventLogImages.Images.SetKeyName(0, "Error");
			this.eventLogImages.Images.SetKeyName(1, "Information");
			this.eventLogImages.Images.SetKeyName(2, "keys.ico");
			this.eventLogImages.Images.SetKeyName(3, "Warning");
			// 
			// ServiceDebugger
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(570, 312);
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "ServiceDebugger";
			this.Text = "ServiceDebugger";
			this.pauseToolStrip.ResumeLayout(false);
			this.pauseToolStrip.PerformLayout();
			this.powerToolStrip.ResumeLayout(false);
			this.powerToolStrip.PerformLayout();
			this.sessionChangeToolStrip.ResumeLayout(false);
			this.sessionChangeToolStrip.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.serviceToolStrip.ResumeLayout(false);
			this.serviceToolStrip.PerformLayout();
			this.shutdownToolStrip.ResumeLayout(false);
			this.shutdownToolStrip.PerformLayout();
			this.stopToolStrip.ResumeLayout(false);
			this.stopToolStrip.PerformLayout();
			this.ResumeLayout(false);

		}

	#endregion

		private System.Windows.Forms.ToolStrip pauseToolStrip;
		private System.Windows.Forms.ToolStripLabel toolStripLabel4;
		private System.Windows.Forms.ToolStripButton toolStripButton3;
		private System.Windows.Forms.ToolStripButton toolStripButton4;
		private System.Windows.Forms.ToolStrip powerToolStrip;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem resumeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem etcToolStripMenuItem;
		private System.Windows.Forms.ToolStrip sessionChangeToolStrip;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
		private System.Windows.Forms.ToolStripButton toolStripButton5;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip serviceToolStrip;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripButton startButton;
		private System.Windows.Forms.ToolStrip shutdownToolStrip;
		private System.Windows.Forms.ToolStripLabel toolStripLabel5;
		private System.Windows.Forms.ToolStripButton toolStripButton6;
		private System.Windows.Forms.ToolStrip stopToolStrip;
		private System.Windows.Forms.ToolStripLabel toolStripLabel6;
		private System.Windows.Forms.ToolStripButton stopButton;
		private System.Windows.Forms.ListView _eventLogListView;
		private System.Windows.Forms.ColumnHeader sourceColumnHeader;
		private System.Windows.Forms.ColumnHeader messageColumnHeader;
		private System.Windows.Forms.ColumnHeader eventIDColumnHeader;
		private System.Windows.Forms.ColumnHeader categoryColumnHeader;
		private System.Windows.Forms.ColumnHeader rawDataColumnHeader;
		private System.Windows.Forms.ImageList eventLogImages;
		private System.Windows.Forms.ColumnHeader typeColumnHeader;
		private System.Windows.Forms.ColumnHeader dateTimeColumnHeader;
#endif
	}
}