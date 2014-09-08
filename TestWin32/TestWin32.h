// TestWin32.h : TestWin32.DLL のメイン ヘッダー ファイル
//

#pragma once

#ifndef __AFXWIN_H__
	#error "PCH に対してこのファイルをインクルードする前に 'stdafx.h' をインクルードしてください"
#endif

#include "resource.h"		// メイン シンボル


// CTestWin32App
// このクラスの実装に関しては TestWin32.cpp を参照してください。
//

class CTestWin32App : public CWinApp
{
public:
	CTestWin32App();

// オーバーライド
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


