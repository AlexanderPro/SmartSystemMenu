// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"

extern HINSTANCE g_appInstance;

BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
	switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
			if (g_appInstance == NULL)
			{
				g_appInstance = hModule;
			}
			break;

		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}