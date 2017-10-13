using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection; // Assembly
using System.Resources;
using System.Runtime.InteropServices;
using System.Web;
using System.Windows.Forms;
using SHDocVw;

namespace MP3Jacket
{
	/// <summary>
	/// デフォルトForm1クラス
	/// </summary>
	public partial class Form1 : Form
	{
		ArrayList arrayMp3 = new ArrayList();
		Image imageJacket = null;
		Image imageNone = null;
		//	SHDocVw.InternetExplorer ie = null;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Activated( object sender, EventArgs e )
		{
			if( imageJacket != null )
			{
				return;
			}

			noImage();
		}

		private void noImage()
		{
			Assembly thisExe = Assembly.GetExecutingAssembly();
			ResourceManager resources2 = new ResourceManager( "Mp3Jacket.Resource1", thisExe );
			imageNone = (Image)resources2.GetObject( "Image1" );

			imageJacket = imageNone;
			panelJacket.Invalidate();
		}

		[DllImport( "TestWin32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl )]
		extern static void TestSub( string sMp3File, string sJpegFile );

		[DllImport( "TestWin32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl )]
		extern static void JacketFromMp3( string sMp3File, string sJpegFile );

		[DllImport( "TestWin32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl )]
		extern static void BmpFileToJpegFile( string sBmpFile, string sJpegFile );

		/// <summary>
		/// リストボックスにドラッグがかざされた
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBoxMp3_DragEnter( object sender, DragEventArgs e )
		{
			e.Effect = DragDropEffects.Copy;
		}

		/// <summary>
		/// リストボックスにフォルダがドラッグされた
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBoxMp3_DragDrop( object sender, DragEventArgs e )
		{
			//ドロップされたデータがstring型か調べる
			if( !e.Data.GetDataPresent( DataFormats.FileDrop ) )
			{
				return;
			}

			ListBox target = (ListBox)sender;

			//ドロップされたデータ(string型)を取得
			string[] itemText = (string[])e.Data.GetData( DataFormats.FileDrop );

			Debug.WriteLine( itemText[ 0 ] );

			foreach( string dropname in itemText )
			{
				if( File.Exists( dropname ) && Path.GetExtension( dropname ) == ".mp3" )
				{
					arrayMp3.Add( dropname );
//					listBoxMp3.Items.Add( Path.GetFileName( dropname ) );
				}
				if( Directory.Exists( dropname ) )
				{
					string[] files = Directory.GetFiles( itemText[ 0 ], "*.mp3" );
					foreach( string fname in files )
					{
						arrayMp3.Add( fname );
//						listBoxMp3.Items.Add( Path.GetFileName( fname ) );
					}
				}
			}
			arrayMp3.Sort();
			listBoxMp3.Items.Clear();
			foreach( string fname in arrayMp3 )
			{
				listBoxMp3.Items.Add( Path.GetFileName( fname ) );
			}
		}

		/// <summary>
		/// リストボックスの選択が変わった場合の処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBoxMp3_SelectedIndexChanged( object sender, EventArgs e )
		{
			// 何も選ばれていなければ逃げる
			if( listBoxMp3.SelectedIndex == -1 )
			{
				imageJacket = imageNone;
				return;
			}

			// フルパスを取ってくる
			string sMp3File  = arrayMp3[ listBoxMp3.SelectedIndex ] as string;

			// mp3の画像でパネルを更新する
			entryJacket( sMp3File );
		}

		/// <summary>
		/// mp3の画像で表示パネルを更新する
		/// </summary>
		/// <param name="sMp3File"></param>
		private void entryJacket( string sMp3File )
		{
			// テンポラリフォルダ＋テンポラリJpeg名
			string tempName = Path.GetTempFileName();

			string tempJpeg = Path.GetDirectoryName( tempName );
			tempJpeg = tempJpeg.Insert( tempJpeg.Length, "\\" );
			tempJpeg = tempJpeg.Insert( tempJpeg.Length, Path.GetFileNameWithoutExtension( tempName ) );
			tempJpeg = tempJpeg.Insert( tempJpeg.Length, ".jpg" );

			// mp3ファイルからjpeg画像を取り出す
			JacketFromMp3( sMp3File, tempJpeg );

			// 書き出したはずのJpegファイルが存在しない場合
			if( !File.Exists( tempJpeg ) )
			{
				imageJacket = imageNone;
				panelJacket.Invalidate();
				return;
			}

			// ファイルがあったがすげー小さい場合
			System.IO.FileInfo fi = new System.IO.FileInfo( tempJpeg );
			if( fi.Length < 100 )
			{
				imageJacket = imageNone;
				panelJacket.Invalidate();
				File.Delete( tempJpeg );
				return;
			}

			// テンポラリjpegファイルを表示する
			// FileStream オブジェクトを使用し、画像を読み込む（ロック防止のため）
			System.IO.FileStream fs = new System.IO.FileStream( tempJpeg,
					System.IO.FileMode.Open, System.IO.FileAccess.Read );
			imageJacket = Image.FromStream( fs );
			fs.Close();

			panelJacket.Invalidate();

			File.Delete( tempJpeg );
			File.Delete( tempName );
		}

		/// <summary>
		/// ジャケットパネルの再描画
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void panelJacket_Paint( object sender, PaintEventArgs e )
		{
			Graphics g = panelJacket.CreateGraphics();
			e.Graphics.Clear( Color.Black );

			if( imageJacket != null )
			{
				int dstHeight, dstWidth;
				int dstX, dstY;

				double aspect = (double)imageJacket.Width / imageJacket.Height;
				if( aspect < 1.0 )
				{
					dstHeight = panelJacket.Height;
					dstWidth = (int)( aspect * panelJacket.Width );
					dstX = ( panelJacket.Width - dstWidth ) / 2;
					dstY = 0;
				}
				else
				{
					dstWidth = panelJacket.Width;
					dstHeight = (int)( panelJacket.Height / aspect );
					dstX = 0;
					dstY = ( panelJacket.Height - dstHeight ) / 2;
				}
				Rectangle srcRect = new Rectangle( 0, 0, imageJacket.Width, imageJacket.Height );
				Rectangle dstRect = new Rectangle( dstX, dstY, dstWidth, dstHeight );

				e.Graphics.DrawImage( imageJacket, dstRect, srcRect, GraphicsUnit.Pixel );
			}
		}

		/// <summary>
		/// ジャケット表示パネルにドラッグがかざされた時の処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void panelJacket_DragEnter( object sender, DragEventArgs e )
		{
			if( listBoxMp3.SelectedIndex == -1 )
			{
				return;
			}

			Debug.WriteLine( "Dragging" );
			e.Effect = DragDropEffects.Copy;
		}

		/// <summary>
		/// ジャケット表示パネルにJpegファイルがドラッグされた時の処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void panelJacket_DragDrop( object sender, DragEventArgs e )
		{
			// 何も選択されていなかったら戻る
			if( listBoxMp3.SelectedIndex == -1 )
			{
				return;
			}

			string[] a = e.Data.GetFormats();

			if( e.Data.GetDataPresent( DataFormats.Bitmap ) )
			{
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.CommaSeparatedValue ) )
			{
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.Dib ) )
			{
				Bitmap bm = DibToImage.Convert( e.Data.GetData( DataFormats.Dib ) );
				bm = resizeBitmapTo512( bm );

				string sTempBmp = Path.GetTempFileName();
				sTempBmp = Path.GetDirectoryName( sTempBmp ) + "\\" + Path.GetFileNameWithoutExtension( sTempBmp ) + ".bmp";
				bm.Save( sTempBmp, System.Drawing.Imaging.ImageFormat.Bmp );

				string sTempJpeg = Path.GetTempFileName();
				sTempJpeg = Path.GetDirectoryName( sTempJpeg ) + "\\" + Path.GetFileNameWithoutExtension( sTempJpeg ) + ".jpg";

				BmpFileToJpegFile( sTempBmp, sTempJpeg );

				string sMp3File = arrayMp3[ listBoxMp3.SelectedIndex ] as string;

				try
				{
					TestSub( sMp3File, sTempJpeg );
					File.Delete( sTempJpeg );
					File.Delete( sTempBmp );

				}
				catch( Exception ex )
				{
					Debug.WriteLine( ex.Message );
				}

				entryJacket( sMp3File );

				return;
			}
			//////////
			//ドロップされたデータがFileDrop型か調べる
			if( e.Data.GetDataPresent( DataFormats.Html ) )
			{
				MessageBox.Show( "HTML ファイルはドロップできません", "エラー" );
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.EnhancedMetafile ) )
			{
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.MetafilePict ) )
			{
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.Locale ) )
			{
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.MetafilePict ) )
			{
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.OemText ) )
			{
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.Riff ) )
			{
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.Rtf ) )
			{
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.Serializable ) )
			{
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.Tiff ) )
			{
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.SymbolicLink ) )
			{
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.UnicodeText ) )
			{
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.WaveAudio ) )
			{
				return;
			}
			if( e.Data.GetDataPresent( DataFormats.FileDrop ) )
			{
				//ドロップされたデータがstring型か調べる
				if( !e.Data.GetDataPresent( DataFormats.FileDrop ) )
				{
					return;
				}

				//ドロップされたデータ(string型)を取得
				string[] itemText = (string[])e.Data.GetData( DataFormats.FileDrop );

				Bitmap bm = (Bitmap)Image.FromFile( itemText[ 0 ] );
				bm = resizeBitmapTo512( bm );

				string sTempBmp = Path.GetTempFileName();
				sTempBmp = Path.GetDirectoryName( sTempBmp ) + "\\" + Path.GetFileNameWithoutExtension( sTempBmp ) + ".bmp";
				bm.Save( sTempBmp, System.Drawing.Imaging.ImageFormat.Bmp );
				string sTempJpeg = Path.GetTempFileName();
				sTempJpeg = Path.GetDirectoryName( sTempJpeg ) + "\\" + Path.GetFileNameWithoutExtension( sTempJpeg ) + ".jpg";

				BmpFileToJpegFile( sTempBmp, sTempJpeg );

				string sMp3File = arrayMp3[ listBoxMp3.SelectedIndex ] as string;

				try
				{
					TestSub( sMp3File, sTempJpeg );
					File.Delete( sTempJpeg );
					File.Delete( sTempBmp );
				}
				catch( Exception ex )
				{
					Debug.WriteLine( ex.Message );
				}

				entryJacket( sMp3File );

				return;
			}
		}

		private Bitmap resizeBitmapTo512( Bitmap src )
		{
			Bitmap dst;
			double scale;

			if( 512 < src.Width || 512 < src.Height )
			{
				if( src.Width < src.Height )
				{
					scale = 512.0 / src.Height;
				}
				else
				{
					scale = 512.0 / src.Width;
				}
			}
			else
			{
				return src;
			}

			int w = (int)( scale * src.Width );
			int h = (int)( scale * src.Height );

			dst = new Bitmap( w, h );

			Graphics g = Graphics.FromImage( dst );
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage( src, 0, 0, dst.Width, dst.Height );

			return dst;
		}

		private void panelJacket_Click( object sender, EventArgs e )
		{

			// 何も選ばれていなければNoImageで終わる
			if( listBoxMp3.SelectedIndex == -1 )
			{
				imageJacket = imageNone;
				return;
			}

			// フルパスを取ってくる
			string sMp3File = arrayMp3[ listBoxMp3.SelectedIndex ] as string;

			// ファイル名から検索名を抜き出す
			string sSearchKey = getArtistName( sMp3File );

			// "&" ではそこで検索語句が切られてしまう
			sSearchKey = sSearchKey.Replace( "&", "and" );

			string encUrl = "http://www.google.co.jp/images?q=" + HttpUtility.UrlEncode( sSearchKey );

			openNewTabPage( encUrl );

		}

		private string getArtistName( string mp3Name )
		{
			string sReturn = mp3Name;

			for( int i = mp3Name.Length - 1; 0 <= i; i-- )
			{
				if( mp3Name[ i ] == '\\' )
				{
					mp3Name = mp3Name.Substring( i + 1 );
					break;
				}
			}

			int ihyphen = mp3Name.IndexOf( ".mp3" );
			if( ihyphen == -1 )
			{
				return mp3Name;
			}
			sReturn = mp3Name.Substring( 0, ihyphen );

			int ibracket = mp3Name.IndexOf( "(" );
			if( ibracket == -1 )
			{
				return sReturn;
			}
			sReturn = mp3Name.Substring( 0, ibracket );

			return sReturn;
		}


		/// <summary>
		/// ダブルクリックされたらその曲を再生
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void listBoxMp3_DoubleClick( object sender, EventArgs e )
		{
			// フルパスを取ってくる
			string sMp3File  = arrayMp3[ listBoxMp3.SelectedIndex ] as string;
			System.Diagnostics.Process.Start( sMp3File );
		}

		/// <summary>
		/// リストを全部クリアする
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AllClear_Click( object sender, EventArgs e )
		{
			arrayMp3.Clear();
			listBoxMp3.Items.Clear();
			noImage();
		}

		/// <summary>
		/// 内部の画像をリビルドする
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonRebuild_Click( object sender, EventArgs e )
		{
			while( listBoxMp3.Items.Count != 0 )
			{
				// フルパスを取ってくる
				string sMp3File = arrayMp3[ 0 ] as string;
				rebuildMp3( sMp3File );

				arrayMp3.RemoveAt( 0 );
				listBoxMp3.Items.RemoveAt( 0 );
			}
		}

		private void rebuildMp3( string sMp3File )
		{
			// テンポラリフォルダ＋テンポラリJpeg名
			string tempName = Path.GetTempFileName();

			string tempJpeg = Path.GetDirectoryName( tempName );
			tempJpeg = tempJpeg.Insert( tempJpeg.Length, "\\" );
			tempJpeg = tempJpeg.Insert( tempJpeg.Length, Path.GetFileNameWithoutExtension( tempName ) );
			tempJpeg = tempJpeg.Insert( tempJpeg.Length, ".jpg" );

			// mp3ファイルからjpeg画像を取り出す
			JacketFromMp3( sMp3File, tempJpeg );

			// 書き出したはずのJpegファイルが存在しない場合
			if( !File.Exists( tempJpeg ) )
			{
				imageJacket = imageNone;
				panelJacket.Invalidate();
				return;
			}

			try
			{
				TestSub( sMp3File, tempJpeg );
			}
			catch( Exception ex )
			{
				Debug.WriteLine( ex.Message );
			}

			File.Delete( tempJpeg );
			File.Delete( tempName );
		}

		private void bImageEdit_Click( object sender, EventArgs e )
		{
			// 何も選ばれていなければ逃げる
			if( listBoxMp3.SelectedIndex == -1 )
			{
				imageJacket = imageNone;
				return;
			}

			// フルパスを取ってくる
			string sMp3File  = arrayMp3[ listBoxMp3.SelectedIndex ] as string;

			// テンポラリフォルダ＋テンポラリJpeg名
			string tempName = Path.GetTempFileName();

			string tempJpeg = Path.GetDirectoryName( tempName );
			tempJpeg = tempJpeg.Insert( tempJpeg.Length, "\\" );
			tempJpeg = tempJpeg.Insert( tempJpeg.Length, Path.GetFileNameWithoutExtension( tempName ) );
			tempJpeg = tempJpeg.Insert( tempJpeg.Length, ".jpg" );

			// mp3ファイルからjpeg画像を取り出す
			JacketFromMp3( sMp3File, tempJpeg );

			// 書き出したはずのJpegファイルが存在しない場合
			if( !File.Exists( tempJpeg ) )
			{
				imageJacket = imageNone;
				panelJacket.Invalidate();
				return;
			}

			var form = new FormImageEdit( tempJpeg );
			form.ShowDialog( this );
			form.Dispose();

			File.Delete( tempJpeg );
		}

		private void bResize_Click( object sender, EventArgs e )
		{
		}

		private void openNewTabPage( string url )
		{
			ShellWindows m_IEFoundBrowsers = new ShellWindowsClass();

			foreach( InternetExplorer browser in m_IEFoundBrowsers )
			{
				string processName 
				 = System.IO.Path.GetFileNameWithoutExtension( browser.FullName ).ToLower();
				if( processName.Equals( "iexplore" ) )
				{
					browser.Navigate( url, null, "JacketSearch" );
					return;
				}
			}
			// ここまで来ちゃったら開いてるIEが無いのでIEを起動する
			System.Diagnostics.Process.Start( "IExplore", url );

		}
	} // end of Form1 class
} // End of namespace MP3Jacket
