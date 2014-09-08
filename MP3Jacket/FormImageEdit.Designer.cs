namespace MP3Jacket
{
	partial class FormImageEdit
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.previewBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
			this.SuspendLayout();
			// 
			// previewBox
			// 
			this.previewBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.previewBox.Location = new System.Drawing.Point(0, 0);
			this.previewBox.Name = "previewBox";
			this.previewBox.Size = new System.Drawing.Size(320, 320);
			this.previewBox.TabIndex = 0;
			this.previewBox.TabStop = false;
			this.previewBox.Paint += new System.Windows.Forms.PaintEventHandler(this.previewBox_Paint);
			// 
			// FormImageEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(320, 353);
			this.Controls.Add(this.previewBox);
			this.Name = "FormImageEdit";
			this.Text = "Image Editor";
			this.Shown += new System.EventHandler(this.FormImageEdit_Shown);
			((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox previewBox;
	}
}