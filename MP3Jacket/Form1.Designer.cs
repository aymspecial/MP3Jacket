﻿namespace MP3Jacket
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.listBoxMp3 = new System.Windows.Forms.ListBox();
			this.panelJacket = new System.Windows.Forms.Panel();
			this.AllClear = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonRebuild = new System.Windows.Forms.Button();
			this.bImageEdit = new System.Windows.Forms.Button();
			this.bResize = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.終了XToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBoxMp3
			// 
			this.listBoxMp3.AllowDrop = true;
			this.listBoxMp3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxMp3.FormattingEnabled = true;
			this.listBoxMp3.HorizontalScrollbar = true;
			this.listBoxMp3.ItemHeight = 12;
			this.listBoxMp3.Location = new System.Drawing.Point(210, 28);
			this.listBoxMp3.Name = "listBoxMp3";
			this.listBoxMp3.Size = new System.Drawing.Size(214, 328);
			this.listBoxMp3.TabIndex = 8;
			this.listBoxMp3.SelectedIndexChanged += new System.EventHandler(this.listBoxMp3_SelectedIndexChanged);
			this.listBoxMp3.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBoxMp3_DragDrop);
			this.listBoxMp3.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBoxMp3_DragEnter);
			this.listBoxMp3.DoubleClick += new System.EventHandler(this.listBoxMp3_DoubleClick);
			// 
			// panelJacket
			// 
			this.panelJacket.AllowDrop = true;
			this.panelJacket.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelJacket.Location = new System.Drawing.Point(6, 28);
			this.panelJacket.Name = "panelJacket";
			this.panelJacket.Size = new System.Drawing.Size(200, 200);
			this.panelJacket.TabIndex = 9;
			this.panelJacket.Click += new System.EventHandler(this.panelJacket_Click);
			this.panelJacket.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelJacket_DragDrop);
			this.panelJacket.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelJacket_DragEnter);
			this.panelJacket.Paint += new System.Windows.Forms.PaintEventHandler(this.panelJacket_Paint);
			// 
			// AllClear
			// 
			this.AllClear.Location = new System.Drawing.Point(6, 241);
			this.AllClear.Name = "AllClear";
			this.AllClear.Size = new System.Drawing.Size(75, 23);
			this.AllClear.TabIndex = 10;
			this.AllClear.Text = "ListClear";
			this.AllClear.UseVisualStyleBackColor = true;
			this.AllClear.Click += new System.EventHandler(this.AllClear_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 308);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(189, 48);
			this.label1.TabIndex = 11;
			this.label1.Text = "IEから直接ドラッグする場合には\r\nインターネットオプション->セキュリティタブ\r\n->保護モードを有効にする\r\nのチェックボックスをOFFにする\r\n";
			// 
			// buttonRebuild
			// 
			this.buttonRebuild.Location = new System.Drawing.Point(6, 270);
			this.buttonRebuild.Name = "buttonRebuild";
			this.buttonRebuild.Size = new System.Drawing.Size(75, 23);
			this.buttonRebuild.TabIndex = 12;
			this.buttonRebuild.Text = "All Rebuild";
			this.buttonRebuild.UseVisualStyleBackColor = true;
			this.buttonRebuild.Click += new System.EventHandler(this.buttonRebuild_Click);
			// 
			// bImageEdit
			// 
			this.bImageEdit.Location = new System.Drawing.Point(131, 241);
			this.bImageEdit.Name = "bImageEdit";
			this.bImageEdit.Size = new System.Drawing.Size(75, 23);
			this.bImageEdit.TabIndex = 10;
			this.bImageEdit.Text = "ImageEdit";
			this.bImageEdit.UseVisualStyleBackColor = true;
			this.bImageEdit.Click += new System.EventHandler(this.bImageEdit_Click);
			// 
			// bResize
			// 
			this.bResize.Location = new System.Drawing.Point(131, 270);
			this.bResize.Name = "bResize";
			this.bResize.Size = new System.Drawing.Size(75, 23);
			this.bResize.TabIndex = 10;
			this.bResize.Text = "Resize";
			this.bResize.UseVisualStyleBackColor = true;
			this.bResize.Click += new System.EventHandler(this.bResize_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(428, 24);
			this.menuStrip1.TabIndex = 13;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了XToolStripMenuItem1});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
			this.fileToolStripMenuItem.Text = "ファイル(F)";
			this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
			// 
			// 終了XToolStripMenuItem1
			// 
			this.終了XToolStripMenuItem1.Name = "終了XToolStripMenuItem1";
			this.終了XToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.終了XToolStripMenuItem1.Text = "終了(X)";
			// 
			// Form1
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(428, 366);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.buttonRebuild);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.bResize);
			this.Controls.Add(this.bImageEdit);
			this.Controls.Add(this.AllClear);
			this.Controls.Add(this.panelJacket);
			this.Controls.Add(this.listBoxMp3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "MP3Jacket";
			this.Activated += new System.EventHandler(this.Form1_Activated);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.ListBox listBoxMp3;
		private System.Windows.Forms.Panel panelJacket;
		private System.Windows.Forms.Button AllClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRebuild;
		private System.Windows.Forms.Button bImageEdit;
		private System.Windows.Forms.Button bResize;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem1;
	}
}

