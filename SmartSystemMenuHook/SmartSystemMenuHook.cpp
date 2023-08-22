// SmartSystemMenuHook.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "SmartSystemMenuHook.h"

#pragma data_seg(".Shared")
HWND  hwndMain = NULL;
HHOOK hookCbt = NULL;
HHOOK hookShell = NULL;
HHOOK hookKeyboard = NULL;
HHOOK hookMouse = NULL;
HHOOK hookKeyboardLL = NULL;
HHOOK hookMouseLL = NULL;
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
static LRESULT CALLBACK KeyboardHookCallback(int code, WPARAM wparam, LPARAM lparam);
static LRESULT CALLBACK MouseHookCallback(int code, WPARAM wparam, LPARAM lparam);
static LRESULT CALLBACK KeyboardLLHookCallback(int code, WPARAM wparam, LPARAM lparam);
static LRESULT CALLBACK MouseLLHookCallback(int code, WPARAM wparam, LPARAM lparam);
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

DLLEXPORT bool __stdcall InitializeCbtHook(int threadID, HWND destination, int dragByMouseMenuItem)
{
    if (g_appInstance == NULL)
    {
        return false;
    }

    hwndMain = destination;
	scDragByMouseMenuItem = dragByMouseMenuItem;
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

DLLEXPORT bool __stdcall InitializeShellHook(int threadID, HWND destination, int dragByMouseMenuItem)
{
    if (g_appInstance == NULL)
    {
        return false;
    }

    hwndMain = destination;
	scDragByMouseMenuItem = dragByMouseMenuItem;
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

DLLEXPORT bool __stdcall InitializeKeyboardHook(int threadID, HWND destination, int dragByMouseMenuItem)
{
    if (g_appInstance == NULL)
    {
        return false;
    }

    hwndMain = destination;
	scDragByMouseMenuItem = dragByMouseMenuItem;
    hookKeyboard = SetWindowsHookEx(WH_KEYBOARD, (HOOKPROC)KeyboardHookCallback, g_appInstance, threadID);
    return hookKeyboard != NULL;
}

DLLEXPORT void __stdcall UninitializeKeyboardHook()
{
    if (hookKeyboard != NULL)
    {
        UnhookWindowsHookEx(hookKeyboard);
    }
    hookKeyboard = NULL;
}

static LRESULT CALLBACK KeyboardHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
    if (code == HC_ACTION)
    {
        if ((DWORD)lparam & 0x40000000)
        {
            SendNotifyMessage(hwndMain, WM_SSM_HOOK_KEYBOARD, wparam, lparam);
        }
    }
    return CallNextHookEx(hookKeyboard, code, wparam, lparam);
}

DLLEXPORT bool __stdcall InitializeMouseHook(int threadID, HWND destination, int dragByMouseMenuItem)
{
    if (g_appInstance == NULL)
    {
        return false;
    }

    hwndMain = destination;
	scDragByMouseMenuItem = dragByMouseMenuItem;
    hookMouse = SetWindowsHookEx(WH_MOUSE, (HOOKPROC)MouseHookCallback, g_appInstance, threadID);
    return hookMouse != NULL;
}

DLLEXPORT void __stdcall UninitializeMouseHook()
{
    if (hookMouse != NULL)
    {
        UnhookWindowsHookEx(hookMouse);
    }
    hookMouse = NULL;
}

static LRESULT CALLBACK MouseHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
    if (code >= 0)
    {
		if ((wparam == WM_LBUTTONUP || wparam == WM_NCLBUTTONUP) && cursorWnd != NULL)
		{
			RECT rect;
			GetWindowRect(cursorWnd, &rect);
			if (rect.left != cursorWndPrevRect.left || rect.right != cursorWndPrevRect.right || rect.top != cursorWndPrevRect.top || rect.bottom != cursorWndPrevRect.bottom)
			{
				MOUSEHOOKSTRUCT* mhs = (MOUSEHOOKSTRUCT*)lparam;
				POINT pt = mhs->pt;
				LONG width = cursorWndPrevRect.right - cursorWndPrevRect.left;
				LONG height = cursorWndPrevRect.bottom - cursorWndPrevRect.top;
				MoveWindow(cursorWnd, pt.x - width / 2, pt.y - height / 2, width, height, TRUE);
			}
			cursorWnd = NULL;
		}

		if (wparam == WM_LBUTTONDOWN)
		{
			MOUSEHOOKSTRUCT* mhs = (MOUSEHOOKSTRUCT*)lparam;
			HWND hwnd = GetTopLevelWindow(mhs->hwnd);
			HMENU menu = GetSystemMenu(hwnd, false);
			if (menu)
			{
				LPWSTR szCaption = new WCHAR[MAX_PATH];
				GetMenuString(menu, scDragByMouseMenuItem, szCaption, MAX_PATH, MF_BYCOMMAND);
				UINT flags = GetMenuState(menu, scDragByMouseMenuItem, MF_BYCOMMAND);
				bool isChecked = flags != -1 && (flags & MF_CHECKED) != 0;
				if (isChecked && szCaption != NULL && szCaption[0] != 0)
				{
					cursorWnd = hwnd;
					RECT rect;
					GetWindowRect(cursorWnd, &rect);
					cursorWndPrevRect = rect;
				}
			}
		}

		if (wparam == WM_MOUSEMOVE && cursorWnd != NULL)
		{
			MOUSEHOOKSTRUCT* mhs = (MOUSEHOOKSTRUCT*)lparam;
			POINT pt = mhs->pt;
			MoveWindow(cursorWnd, pt.x - 20, pt.y - 20, 41, 41, TRUE);
		}
    }

    return CallNextHookEx(hookMouse, code, wparam, lparam);
}

DLLEXPORT bool __stdcall InitializeKeyboardLLHook(int threadID, HWND destination, int dragByMouseMenuItem)
{
    if (g_appInstance == NULL)
    {
        return false;
    }

    hwndMain = destination;
	scDragByMouseMenuItem = dragByMouseMenuItem;
    hookKeyboardLL = SetWindowsHookEx(WH_KEYBOARD_LL, (HOOKPROC)KeyboardLLHookCallback, g_appInstance, threadID);
    return hookKeyboardLL != NULL;
}

DLLEXPORT void __stdcall UninitializeKeyboardLLHook()
{
    if (hookKeyboardLL != NULL)
    {
        UnhookWindowsHookEx(hookKeyboardLL);
    }
    hookKeyboardLL = NULL;
}

static LRESULT CALLBACK KeyboardLLHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
    if (code == HC_ACTION)
    {
        KBDLLHOOKSTRUCT* pKb = (KBDLLHOOKSTRUCT*)lparam;
        SendNotifyMessage(hwndMain, WM_SSM_HOOK_KEYBOARDLL, wparam, pKb->vkCode);
    }

    return CallNextHookEx(hookKeyboardLL, code, wparam, lparam);
}

DLLEXPORT bool __stdcall InitializeMouseLLHook(int threadID, HWND destination, int dragByMouseMenuItem)
{
    if (g_appInstance == NULL)
    {
        return false;
    }

    hwndMain = destination;
	scDragByMouseMenuItem = dragByMouseMenuItem;
    hookMouseLL = SetWindowsHookEx(WH_MOUSE_LL, (HOOKPROC)MouseLLHookCallback, g_appInstance, threadID);
    return hookMouseLL != NULL;
}

DLLEXPORT void __stdcall UninitializeMouseLLHook()
{
    if (hookMouseLL != NULL)
    {
        UnhookWindowsHookEx(hookMouseLL);
    }
    hookMouseLL = NULL;
}

static LRESULT CALLBACK MouseLLHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
    if (code >= 0)
    {
        SendNotifyMessage(hwndMain, WM_SSM_HOOK_MOUSELL, wparam, lparam);
    }

    return CallNextHookEx(hookMouseLL, code, wparam, lparam);
}

DLLEXPORT bool __stdcall InitializeCallWndProcHook(int threadID, HWND destination, int dragByMouseMenuItem)
{
    if (g_appInstance == NULL)
    {
        return false;
    }

    hwndMain = destination;
	scDragByMouseMenuItem = dragByMouseMenuItem;
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
        }
    }

    return CallNextHookEx(hookGetMsg, code, wparam, lparam);
}