// TestWin32.h : TestWin32.DLL �̃��C�� �w�b�_�[ �t�@�C��
//

#pragma once

#ifndef __AFXWIN_H__
	#error "PCH �ɑ΂��Ă��̃t�@�C�����C���N���[�h����O�� 'stdafx.h' ���C���N���[�h���Ă�������"
#endif

#include "resource.h"		// ���C�� �V���{��


// CTestWin32App
// ���̃N���X�̎����Ɋւ��Ă� TestWin32.cpp ���Q�Ƃ��Ă��������B
//

class CTestWin32App : public CWinApp
{
public:
	CTestWin32App();

// �I�[�o�[���C�h
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};

extern "C" 
{
	__declspec(dllexport) void TestSub( LPCWSTR lpMp3File, LPCWSTR lpJpegFile );
	__declspec(dllexport) void JacketFromMp3( LPCWSTR lpMp3File, LPCWSTR lpJpegFile );
	__declspec(dllexport) void BmpFileToJpegFile( LPCWSTR lpBmpFile, LPCWSTR lpJpegFile );
}
void Concat( LPCWSTR fileName1, LPCWSTR fileName2, LPCWSTR outFile );
void SaveJPG( CString fileName, BOOL color );
void LoadBMP( CString fileName );


