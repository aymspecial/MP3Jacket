// TestWin32.cpp : DLL の初期化ルーチンです。
//

#include "stdafx.h"
#include <sys/types.h> 
#include <sys/stat.h> 


#include "id3/id3lib_streams.h"
#include "id3/tag.h"

#include "JpegFile.h"
#include "Bmpfile.h"

#include "TestWin32.h"

#pragma warning(disable : 4996)

#ifdef _DEBUG
#define new DEBUG_NEW
#endif



//
//TODO: この DLL が MFC DLL に対して動的にリンクされる場合、
//		MFC 内で呼び出されるこの DLL からエクスポートされたどの関数も
//		関数の最初に追加される AFX_MANAGE_STATE マクロを
//		持たなければなりません。
//
//		例:
//
//		extern "C" BOOL PASCAL EXPORT ExportedFunction()
//		{
//			AFX_MANAGE_STATE(AfxGetStaticModuleState());
//			// 通常関数の本体はこの位置にあります
//		}
//
//		このマクロが各関数に含まれていること、MFC 内の
//		どの呼び出しより優先することは非常に重要です。
//		これは関数内の最初のステートメントでなければな 
//		らないことを意味します、コンストラクタが MFC
//		DLL 内への呼び出しを行う可能性があるので、オブ
//		ジェクト変数の宣言よりも前でなければなりません。
//
//		詳細については MFC テクニカル ノート 33 および
//		58 を参照してください。
//

// CTestWin32App

BEGIN_MESSAGE_MAP( CTestWin32App, CWinApp )
END_MESSAGE_MAP()


// CTestWin32App コンストラクション

CTestWin32App::CTestWin32App()
{
	// TODO: この位置に構築用コードを追加してください。
	// ここに InitInstance 中の重要な初期化処理をすべて記述してください。
}


// 唯一の CTestWin32App オブジェクトです。

CTestWin32App theApp;


// CTestWin32App 初期化

BOOL CTestWin32App::InitInstance()
{
	CWinApp::InitInstance();

	return TRUE;
}

void JacketFromMp3( LPCWSTR lpMp3File, LPCWSTR lpJpegFile )
{
	int nLen;
	char strMp3File[ 512 ];
	nLen = ::WideCharToMultiByte( CP_THREAD_ACP, 0, lpMp3File, -1, NULL, 0, NULL, NULL );
	WideCharToMultiByte( CP_ACP, 0, lpMp3File, -1, strMp3File, nLen, NULL, NULL );

	char strJpegFile[ 512 ];
	nLen = ::WideCharToMultiByte( CP_THREAD_ACP, 0, lpJpegFile, -1, NULL, 0, NULL, NULL );
	WideCharToMultiByte( CP_ACP, 0, lpJpegFile, -1, strJpegFile, nLen, NULL, NULL );

	ID3_Tag tag;
	try
	{
		tag.Link( strMp3File );

		ID3_Frame *lpTagFrame = tag.Find( ID3FID_PICTURE );
		if( lpTagFrame != NULL )
		{
			lpTagFrame->GetField( ID3FN_DATA )->ToFile( strJpegFile );
		}
	}
	catch( std::exception & ex )
	{
		std::cerr << "Exception occurred: " << ex.what() << std::endl;
	}
}

void TestSub( LPCWSTR lpMp3File, LPCWSTR lpJpegFile )
{
	int nLen;
	char strMp3File[ 512 ];
	nLen = ::WideCharToMultiByte( CP_THREAD_ACP, 0, lpMp3File, -1, NULL, 0, NULL, NULL );
	WideCharToMultiByte( CP_ACP, 0, lpMp3File, -1, strMp3File, nLen, NULL, NULL );

	char strJpegFile[ 512 ];
	nLen = ::WideCharToMultiByte( CP_THREAD_ACP, 0, lpJpegFile, -1, NULL, 0, NULL, NULL );
	WideCharToMultiByte( CP_ACP, 0, lpJpegFile, -1, strJpegFile, nLen, NULL, NULL );

	ID3_Tag tag;

	tag.Link( strMp3File );

	ID3_Frame *lpTagFrame = tag.Find( ID3FID_PICTURE );
	if( lpTagFrame != NULL )
	{
		tag.RemoveFrame( lpTagFrame );
	}
	ID3_Frame frame;
	frame.SetID( ID3FID_PICTURE );
	frame.GetField( ID3FN_MIMETYPE )->Set( "image/jpeg" );
	frame.GetField( ID3FN_DATA )->FromFile( strJpegFile );
	tag.AddFrame( frame );

	tag.SetPadding( false );
	tag.SetUnsync( false );
	tag.Update( ID3TT_ID3V2 );
}

void BmpFileToJpegFile( LPCWSTR lpBmpFile, LPCWSTR lpJpegFile )
{
	LoadBMP( CString( lpBmpFile ) );
	SaveJPG( CString( lpJpegFile ), TRUE );
}

BYTE *m_buf;
UINT m_width;
UINT m_height;
UINT m_widthDW;

void SaveJPG( CString fileName, BOOL color )
{
	// note, because i'm lazy, most image data in this app
	// is handled as 24-bit images. this makes the DIB
	// conversion easier. 1,4,8, 15/16 and 32 bit DIBs are
	// significantly more difficult to handle.

	if( m_buf == NULL )
	{
		AfxMessageBox( _T( "No Image!" ) );
		return;
	}

	// we vertical flip for display. undo that.
//	JpegFile::VertFlipBuf(m_buf, m_width * 3, m_height);

	// we swap red and blue for display, undo that.
//	JpegFile::BGRFromRGB(m_buf, m_width, m_height);


	// save RGB packed buffer to JPG
	BOOL bOk = JpegFile::RGBToJpegFile( fileName, m_buf, m_width, m_height, color, 75 );	// quality value 1-100.
	if( !bOk )
	{
		AfxMessageBox( _T( "Write Error" ) );
	}
}

void LoadBMP( CString fileName )
{
	if( m_buf != NULL )
	{
		delete[] m_buf;
		m_buf = NULL;
	}

	BMPFile theBmpFile;

	m_buf = theBmpFile.LoadBMP( fileName, &m_width, &m_height );

	if( ( m_buf == NULL ) || ( theBmpFile.m_errorText != "OK" ) )
	{
		AfxMessageBox( theBmpFile.m_errorText );
		m_buf = NULL;
		return;
	}
}

void Concat( LPCWSTR fileName1, LPCWSTR fileName2, LPCWSTR outFile )
{
	// ファイルサイズを取得する
	struct _stat32 statInfo1, statInfo2;

	_wstat32( fileName1, &statInfo1 );
	_wstat32( fileName2, &statInfo2 );

	ifstream fin1( fileName1, ios::in | ios::binary );
	ifstream fin2( fileName2, ios::in | ios::binary );

	// 出力用
	ofstream fout( outFile, ios::out | ios::binary | ios::trunc );
	int outSize = statInfo1.st_size + statInfo2.st_size;
	char* buf = new char[ outSize ];

	fin1.read( buf, statInfo1.st_size );
	fin2.read( buf + statInfo1.st_size, statInfo2.st_size );

	fout.write( buf, outSize );

	fout.close();

	fin1.close();
	fin2.close();

	delete[] buf;
}
