// TestWin32.cpp : DLL �̏��������[�`���ł��B
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
//TODO: ���� DLL �� MFC DLL �ɑ΂��ē��I�Ƀ����N�����ꍇ�A
//		MFC ���ŌĂяo����邱�� DLL ����G�N�X�|�[�g���ꂽ�ǂ̊֐���
//		�֐��̍ŏ��ɒǉ������ AFX_MANAGE_STATE �}�N����
//		�����Ȃ���΂Ȃ�܂���B
//
//		��:
//
//		extern "C" BOOL PASCAL EXPORT ExportedFunction()
//		{
//			AFX_MANAGE_STATE(AfxGetStaticModuleState());
//			// �ʏ�֐��̖{�̂͂��̈ʒu�ɂ���܂�
//		}
//
//		���̃}�N�����e�֐��Ɋ܂܂�Ă��邱�ƁAMFC ����
//		�ǂ̌Ăяo�����D�悷�邱�Ƃ͔��ɏd�v�ł��B
//		����͊֐����̍ŏ��̃X�e�[�g�����g�łȂ���΂� 
//		��Ȃ����Ƃ��Ӗ����܂��A�R���X�g���N�^�� MFC
//		DLL ���ւ̌Ăяo�����s���\��������̂ŁA�I�u
//		�W�F�N�g�ϐ��̐錾�����O�łȂ���΂Ȃ�܂���B
//
//		�ڍׂɂ��Ă� MFC �e�N�j�J�� �m�[�g 33 �����
//		58 ���Q�Ƃ��Ă��������B
//

// CTestWin32App

BEGIN_MESSAGE_MAP( CTestWin32App, CWinApp )
END_MESSAGE_MAP()


// CTestWin32App �R���X�g���N�V����

CTestWin32App::CTestWin32App()
{
	// TODO: ���̈ʒu�ɍ\�z�p�R�[�h��ǉ����Ă��������B
	// ������ InitInstance ���̏d�v�ȏ��������������ׂċL�q���Ă��������B
}


// �B��� CTestWin32App �I�u�W�F�N�g�ł��B

CTestWin32App theApp;


// CTestWin32App ������

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
	// �t�@�C���T�C�Y���擾����
	struct _stat32 statInfo1, statInfo2;

	_wstat32( fileName1, &statInfo1 );
	_wstat32( fileName2, &statInfo2 );

	ifstream fin1( fileName1, ios::in | ios::binary );
	ifstream fin2( fileName2, ios::in | ios::binary );

	// �o�͗p
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
