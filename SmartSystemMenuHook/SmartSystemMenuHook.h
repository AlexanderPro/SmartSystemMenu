#ifndef	_SMART_SYSTEM_MENU_HOOK_H_
#define	_SMART_SYSTEM_MENU_HOOK_H_

#include <windows.h>

#define WM_SSM_HOOK_HCBT_CREATEWND                WM_APP + 0x0201
#define WM_SSM_HOOK_HCBT_DESTROYWND               WM_APP + 0x0202
#define WM_SSM_HOOK_HCBT_MINMAX                   WM_APP + 0x0203
#define WM_SSM_HOOK_HCBT_MOVESIZE                 WM_APP + 0x0204
#define WM_SSM_HOOK_HCBT_ACTIVATE                 WM_APP + 0x0205
#define WM_SSM_HOOK_CALLWNDPROC_SYSCOMMAND        WM_APP + 0x0206
#define WM_SSM_HOOK_CALLWNDPROC_SYSCOMMAND_PARAMS WM_APP + 0x0207
#define WM_SSM_HOOK_CALLWNDPROC_INITMENU          WM_APP + 0x0208
#define WM_SSM_HOOK_GETMSG_SYSCOMMAND             WM_APP + 0x0209
#define WM_SSM_HOOK_GETMSG_SYSCOMMAND_PARAMS      WM_APP + 0x020A
#define WM_SSM_HOOK_GETMSG_INITMENU               WM_APP + 0x020B
#define WM_SSM_HOOK_KEYBOARD                      WM_APP + 0x020C
#define WM_SSM_HOOK_KEYBOARD_REPLACED             WM_APP + 0x020D
#define WM_SSM_HOOK_KEYBOARDLL                    WM_APP + 0x020E
#define WM_SSM_HOOK_KEYBOARDLL_REPLACED           WM_APP + 0x020F
#define WM_SSM_HOOK_MOUSE                         WM_APP + 0x0210
#define WM_SSM_HOOK_MOUSE_REPLACED                WM_APP + 0x0211
#define WM_SSM_HOOK_MOUSELL                       WM_APP + 0x0212
#define WM_SSM_HOOK_MOUSELL_REPLACED              WM_APP + 0x0213
#define WM_SSM_HOOK_HSHELL_WINDOWCREATED          WM_APP + 0x0214
#define WM_SSM_HOOK_HSHELL_WINDOWDESTROYED        WM_APP + 0x0215


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