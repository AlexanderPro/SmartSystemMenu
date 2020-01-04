#ifndef	_SMART_SYSTEM_MENU_HOOK_H_
#define	_SMART_SYSTEM_MENU_HOOK_H_

#include <windows.h>

#define DLLEXPORT extern "C" __declspec(dllexport)

DLLEXPORT bool __stdcall InitializeCbtHook(int threadID, HWND destination, int dragByMouseMenuItem);
DLLEXPORT void __stdcall UninitializeCbtHook();
DLLEXPORT bool __stdcall InitializeShellHook(int threadID, HWND destination, int dragByMouseMenuItem);
DLLEXPORT void __stdcall UninitializeShellHook();
DLLEXPORT bool __stdcall InitializeKeyboardHook(int threadID, HWND destination, int dragByMouseMenuItem);
DLLEXPORT void __stdcall UninitializeKeyboardHook();
DLLEXPORT bool __stdcall InitializeMouseHook(int threadID, HWND destination, int dragByMouseMenuItem);
DLLEXPORT void __stdcall UninitializeMouseHook();
DLLEXPORT bool __stdcall InitializeKeyboardLLHook(int threadID, HWND destination, int dragByMouseMenuItem);
DLLEXPORT void __stdcall UninitializeKeyboardLLHook();
DLLEXPORT bool __stdcall InitializeMouseLLHook(int threadID, HWND destination, int dragByMouseMenuItem);
DLLEXPORT void __stdcall UninitializeMouseLLHook();
DLLEXPORT bool __stdcall InitializeCallWndProcHook(int threadID, HWND destination, int dragByMouseMenuItem);
DLLEXPORT void __stdcall UninitializeCallWndProcHook();
DLLEXPORT bool __stdcall InitializeGetMsgHook(int threadID, HWND destination, int dragByMouseMenuItem);
DLLEXPORT void __stdcall UninitializeGetMsgHook();

#endif