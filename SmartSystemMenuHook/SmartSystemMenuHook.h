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
#define WM_SSM_HOOK_HSHELL_WINDOWCREATED          WM_APP + 0x020C
#define WM_SSM_HOOK_HSHELL_WINDOWDESTROYED        WM_APP + 0x020D


#define DLLEXPORT extern "C" __declspec(dllexport)

DLLEXPORT bool __stdcall InitializeCbtHook(int threadID, HWND destination);
DLLEXPORT void __stdcall UninitializeCbtHook();
DLLEXPORT bool __stdcall InitializeShellHook(int threadID, HWND destination);
DLLEXPORT void __stdcall UninitializeShellHook();
DLLEXPORT bool __stdcall InitializeCallWndProcHook(int threadID, HWND destination);
DLLEXPORT void __stdcall UninitializeCallWndProcHook();
DLLEXPORT bool __stdcall InitializeGetMsgHook(int threadID, HWND destination, int dragByMouseMenuItem);
DLLEXPORT void __stdcall UninitializeGetMsgHook();

#endif