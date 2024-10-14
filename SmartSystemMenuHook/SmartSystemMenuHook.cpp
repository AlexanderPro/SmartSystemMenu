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
static LRESULT CALLBACK WndProcMinMax(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

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

            case WM_GETMINMAXINFO:
            {
                HMENU systemMenu = GetSystemMenu(pCwpStruct->hwnd, false);
                if (systemMenu)
                {
                    UINT menuItemRollUpState = GetMenuState(systemMenu, SC_ROLLUP, MF_BYCOMMAND);
                    UINT menuItemResizableState = GetMenuState(systemMenu, SC_RESIZABLE, MF_BYCOMMAND);
                    bool isMenuItemRollUpChecked = menuItemRollUpState != -1 && (menuItemRollUpState & MF_CHECKED) != 0;
                    bool isMenuItemResizableChecked = menuItemResizableState != -1 && (menuItemResizableState & MF_CHECKED) != 0;
                    if (isMenuItemRollUpChecked || isMenuItemResizableChecked)
                    {
                        LONG_PTR proc = SetWindowLongPtr(pCwpStruct->hwnd, GWLP_WNDPROC, (LONG_PTR)WndProcMinMax);
                        SetProp(pCwpStruct->hwnd, L"_ssm_old_wnd_proc_", (HANDLE)(proc));
                    }
                }
            } break;
        }
    }

    return CallNextHookEx(hookCallWndProc, code, wparam, lparam);
}

DLLEXPORT bool __stdcall InitializeGetMsgHook(int threadID, HWND destination)
{
    if (g_appInstance == NULL)
    {
        return false;
    }

    hwndMain = destination;
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
                HMENU systemMenu = GetSystemMenu(hwnd, false);
                if (systemMenu)
                {
                    UINT menuItemDragByMouseState = GetMenuState(systemMenu, SC_DRAG_BY_MOUSE, MF_BYCOMMAND);
                    bool isMenuItemDragByMouseChecked = menuItemDragByMouseState != -1 && (menuItemDragByMouseState & MF_CHECKED) != 0;
                    if (isMenuItemDragByMouseChecked)
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

static LRESULT CALLBACK WndProcMinMax(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    HANDLE hdl = GetProp(hwnd, L"_ssm_old_wnd_proc_");
    RemoveProp(hwnd, L"_ssm_old_wnd_proc_");
    SetWindowLongPtr(hwnd, GWLP_WNDPROC, (LONG_PTR)(hdl));

    switch (uMsg)
    {
        case WM_GETMINMAXINFO:
        {
            MINMAXINFO* minmax = (MINMAXINFO*)(lParam);
            minmax->ptMinTrackSize.x = 0;
            minmax->ptMinTrackSize.y = 0;
            minmax->ptMaxTrackSize.x = LONG_MAX;
            minmax->ptMaxTrackSize.y = LONG_MAX;
            /*TCHAR buf[255];
            wsprintf(buf, L"MINMAXINFO Hwnd = %p, x = %ld, y = %ld", hwnd, minmax->ptMinTrackSize.x, minmax->ptMinTrackSize.y);
            OutputDebugString(buf);*/
        } break;

        case WM_WINDOWPOSCHANGING:
        {
            WINDOWPOS* wpos = (WINDOWPOS*)(lParam);
            wpos->cx = 0;
            wpos->cy = 0;
        } break;

        case WM_WINDOWPOSCHANGED:
            break;

        default:
            return CallWindowProc((WNDPROC)(hdl), hwnd, uMsg, wParam, lParam);
    }
}