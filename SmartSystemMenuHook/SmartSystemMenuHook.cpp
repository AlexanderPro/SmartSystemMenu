// SmartSystemMenuHook.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "SmartSystemMenuHook.h"

#pragma data_seg(".Shared")
HWND  hwndMain = NULL;
HHOOK hookCbt = NULL;
HHOOK hookShell = NULL;
HHOOK hookCallWndProc = NULL;
HHOOK hookGetMsg = NULL;
HWND cursorWnd = NULL;
RECT cursorWndPrevRect;
int scDragByMouseMenuItem = 0;
#pragma data_seg()
#pragma comment(linker, "/section:.Shared,rws")

//
// Store the application instance of this module to pass to
// hook initialization. This is set in DLLMain().
//
HINSTANCE g_appInstance = NULL;

typedef void (CALLBACK *HookProc)(int code, WPARAM w, LPARAM l);

static LRESULT CALLBACK CbtHookCallback(int code, WPARAM wparam, LPARAM lparam);
static LRESULT CALLBACK ShellHookCallback(int code, WPARAM wparam, LPARAM lparam);
static LRESULT CALLBACK CallWndProcHookCallback(int code, WPARAM wparam, LPARAM lparam);
static LRESULT CALLBACK GetMsgHookCallback(int code, WPARAM wparam, LPARAM lparam);

HWND GetTopLevelWindow(HWND hwnd)
{
    HWND result = hwnd;
    while (GetParent(result) != 0)
    { 
        result = GetParent(result);
    }
    return result;
}

DLLEXPORT bool __stdcall InitializeCbtHook(int threadID, HWND destination)
{
    if (g_appInstance == NULL)
    {
        return false;
    }

    hwndMain = destination;
    hookCbt = SetWindowsHookEx(WH_CBT, (HOOKPROC)CbtHookCallback, g_appInstance, threadID);
    return hookCbt != NULL;
}

DLLEXPORT void __stdcall UninitializeCbtHook()
{
    if (hookCbt != NULL)
    {
        UnhookWindowsHookEx(hookCbt);
    }
    hookCbt = NULL;
}

static LRESULT CALLBACK CbtHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
    if (code >= 0)
    {
        UINT msg = 0;

        if (code == HCBT_CREATEWND)
            msg = WM_SSM_HOOK_HCBT_CREATEWND;
        else if (code == HCBT_DESTROYWND)
            msg = WM_SSM_HOOK_HCBT_DESTROYWND;
        else if (code == HCBT_MINMAX)
            msg = WM_SSM_HOOK_HCBT_MINMAX;
        else if (code == HCBT_MOVESIZE)
            msg = WM_SSM_HOOK_HCBT_MOVESIZE;
        else if (code == HCBT_ACTIVATE)
            msg = WM_SSM_HOOK_HCBT_ACTIVATE;

        if (msg != 0)
        {
            SendNotifyMessage(hwndMain, msg, wparam, lparam);
        }
    }

    return CallNextHookEx(hookCbt, code, wparam, lparam);
}

DLLEXPORT bool __stdcall InitializeShellHook(int threadID, HWND destination)
{
    if (g_appInstance == NULL)
    {
        return false;
    }

    hwndMain = destination;
    hookShell = SetWindowsHookEx(WH_SHELL, (HOOKPROC)ShellHookCallback, g_appInstance, threadID);
    return hookShell != NULL;
}

DLLEXPORT void __stdcall UninitializeShellHook()
{
    if (hookShell != NULL)
    {
        UnhookWindowsHookEx(hookShell);
    }
    hookShell = NULL;
}

static LRESULT CALLBACK ShellHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
    if (code >= 0)
    {
        UINT msg = 0;

        if (code == HSHELL_WINDOWCREATED)
            msg = WM_SSM_HOOK_HSHELL_WINDOWCREATED;
        else if (code == HSHELL_WINDOWDESTROYED)
            msg = WM_SSM_HOOK_HSHELL_WINDOWDESTROYED;

        if (msg != 0)
        {
            SendNotifyMessage(hwndMain, msg, wparam, lparam);
        }
    }

    return CallNextHookEx(hookShell, code, wparam, lparam);
}

DLLEXPORT bool __stdcall InitializeCallWndProcHook(int threadID, HWND destination)
{
    if (g_appInstance == NULL)
    {
        return false;
    }

    hwndMain = destination;
    hookCallWndProc = SetWindowsHookEx(WH_CALLWNDPROC, (HOOKPROC)CallWndProcHookCallback, g_appInstance, threadID);
    return hookCallWndProc != NULL;
}

DLLEXPORT void __stdcall UninitializeCallWndProcHook()
{
    if (hookCallWndProc != NULL)
    {
        UnhookWindowsHookEx(hookCallWndProc);
    }
    hookCallWndProc = NULL;
}

static LRESULT CALLBACK CallWndProcHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
    if (code >= 0)
    {
        CWPSTRUCT* pCwpStruct = (CWPSTRUCT*)lparam;
        switch (pCwpStruct->message)
        {
            case WM_SYSCOMMAND:
            {
                SendNotifyMessage(hwndMain, WM_SSM_HOOK_CALLWNDPROC_SYSCOMMAND, (WPARAM)pCwpStruct->hwnd, pCwpStruct->message);
                SendNotifyMessage(hwndMain, WM_SSM_HOOK_CALLWNDPROC_SYSCOMMAND_PARAMS, pCwpStruct->wParam, pCwpStruct->lParam);
            } break;

            case WM_INITMENUPOPUP:
            {
                SendNotifyMessage(hwndMain, WM_SSM_HOOK_CALLWNDPROC_INITMENU, (WPARAM)pCwpStruct->hwnd, pCwpStruct->message);
            } break;
        }
    }

    return CallNextHookEx(hookCallWndProc, code, wparam, lparam);
}

DLLEXPORT bool __stdcall InitializeGetMsgHook(int threadID, HWND destination, int dragByMouseMenuItem)
{
    if (g_appInstance == NULL)
    {
        return false;
    }

    hwndMain = destination;
    scDragByMouseMenuItem = dragByMouseMenuItem;
    hookGetMsg = SetWindowsHookEx(WH_GETMESSAGE, (HOOKPROC)GetMsgHookCallback, g_appInstance, threadID);
    return hookGetMsg != NULL;
}

DLLEXPORT void __stdcall UninitializeGetMsgHook()
{
    if (hookGetMsg != NULL)
    {
        UnhookWindowsHookEx(hookGetMsg);
    }
    hookGetMsg = NULL;
}

static LRESULT CALLBACK GetMsgHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
    if (code >= 0 && wparam == PM_REMOVE)
    {
        MSG* pMsg = (MSG*)lparam;
        switch (pMsg->message)
        {
            case WM_SYSCOMMAND:
            {
                SendNotifyMessage(hwndMain, WM_SSM_HOOK_GETMSG_SYSCOMMAND, (WPARAM)pMsg->hwnd, pMsg->message);
                SendNotifyMessage(hwndMain, WM_SSM_HOOK_GETMSG_SYSCOMMAND_PARAMS, pMsg->wParam, pMsg->lParam);
            } break;

            case WM_INITMENUPOPUP:
            {
                SendNotifyMessage(hwndMain, WM_SSM_HOOK_GETMSG_INITMENU, (WPARAM)pMsg->hwnd, pMsg->message);
            } break;

            case WM_LBUTTONDOWN:
            {
                HWND hwnd = GetTopLevelWindow(pMsg->hwnd);
                HMENU menu = GetSystemMenu(hwnd, false);
                if (menu)
                {
                    LPWSTR szCaption = new WCHAR[MAX_PATH];
                    GetMenuString(menu, scDragByMouseMenuItem, szCaption, MAX_PATH, MF_BYCOMMAND);
                    UINT flags = GetMenuState(menu, scDragByMouseMenuItem, MF_BYCOMMAND);
                    bool isChecked = flags != -1 && (flags & MF_CHECKED) != 0;
                    if (isChecked && szCaption != NULL && szCaption[0] != 0)
                    {
                        ReleaseCapture();
                        SendMessage(hwnd, WM_SYSCOMMAND, 61458, 0);
                    }
                }
            } break;
        }
    }

    return CallNextHookEx(hookGetMsg, code, wparam, lparam);
}