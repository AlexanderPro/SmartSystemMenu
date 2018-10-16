// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include <string>
#include <vector>
#include <algorithm>

extern HINSTANCE g_appInstance;

bool array_contains(const std::string &value, const std::vector<std::string> &array)
{
	return std::find(array.begin(), array.end(), value) != array.end();
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
	switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
			if (g_appInstance == NULL)
			{
				g_appInstance = hModule;
			}
			WCHAR path[MAX_PATH];
			if (GetModuleFileName(NULL, path, sizeof(path)) != 0)
			{
				std::vector<std::string> excludedProcessNames{ "discord.exe", "slack.exe" };
				std::wstring tempPath(&path[0]);
				std::string processPath(tempPath.begin(), tempPath.end());
				std::size_t index = processPath.find_last_of("/\\");
				std::string processName = processPath.substr(index + 1);
				std::transform(processName.begin(), processName.end(), processName.begin(), ::tolower);
				if (array_contains(processName, excludedProcessNames))
				{
					return FALSE;
				}
			}
			break;

		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}