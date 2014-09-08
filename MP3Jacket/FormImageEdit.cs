using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection; // Assembly
using System.Resources;

namespace MP3Jacket
{
public partial class FormImageEdit : Form
{
	string jpgFile;
	Image imageJacket = null;

	public FormImageEdit()
	{
		InitializeComponent();
	}


	public FormImageEdit( string _jpgFile )
	{
		InitializeComponent();

		jpgFile = _jpgFile;
	}

	private void FormImageEdit_Shown( object sender, EventArgs e )
	{
		// テンポラリjpegファイルを表示する
		// FileStream オブジェクトを使用し、画像を読み込む（ロック防止のため）
		System.IO.FileStream fs = new System.IO.FileStream( jpgFile,
				System.IO.FileMode.Open, System.IO.FileAccess.Read );
		imageJacket = Image.FromStream( fs );
		fs.Close();
	}

	private void previewBox_Paint( object sender, PaintEventArgs e )
	{
		Graphics g = previewBox.CreateGraphics();
		e.Graphics.Clear( Color.Black );

		if( imageJacket == null )
		{
			return;
		}
		int dstHeight, dstWidth;
		int dstX, dstY;

		double aspect = (double)imageJacket.Width / imageJacket.Height;
		if( aspect < 1.0 )
		{
			dstHeight = previewBox.Height;
			dstWidth = (int)( aspect * previewBox.Width );
			dstX = ( previewBox.Width - dstWidth ) / 2;
			dstY = 0;
		}
		else
		{
			dstWidth = previewBox.Width;
			dstHeight = (int)( previewBox.Height / aspect );
			dstX = 0;
			dstY = ( previewBox.Height - dstHeight ) / 2;
		}
		Rectangle srcRect = new Rectangle( 0, 0, imageJacket.Width, imageJacket.Height );
		Rectangle dstRect = new Rectangle( dstX, dstY, dstWidth, dstHeight );

		e.Graphics.DrawImage( imageJacket, dstRect, srcRect, GraphicsUnit.Pixel );
	}
}
}
