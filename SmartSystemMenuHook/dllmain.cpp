// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include <string>
#include <vector>
#include <algorithm>
#include <fstream>

extern HINSTANCE g_appInstance;

bool array_contains(const std::string &value, const std::vector<std::string> &array)
{
	return std::find(array.begin(), array.end(), value) != array.end();
}

std::string get_file_name(const std::string &path)
{
	std::size_t index = path.find_last_of("/\\");
	std::string fileName = path.substr(index + 1);
	return fileName;
}

std::string get_directory_name(const std::string &path)
{
	std::size_t index = path.find_last_of("/\\");
	std::string directoryName = path.substr(0, index);
	return directoryName;
}

std::vector<std::string> read_process_exclusions(const std::string &path)
{
	std::vector<std::string> result{};
	std::string str;
	std::ifstream file(path.c_str());
	if (!file.good())
	{
		return result;
	}
	while (std::getline(file, str))
	{
		std::transform(str.begin(), str.end(), str.begin(), ::tolower);
		result.push_back(str);
	}
	return result;
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
		WCHAR exePath[MAX_PATH], dllPath[MAX_PATH];
		if (GetModuleFileName(NULL, exePath, sizeof(exePath)) != 0 && GetModuleFileName(hModule, dllPath, sizeof(dllPath)) != 0)
		{
			std::wstring dllTempPath(&dllPath[0]);
			std::string dllFileName(dllTempPath.begin(), dllTempPath.end());
			std::string dllDirectoryName = get_directory_name(dllFileName);
			std::string exclusionsFileName = dllDirectoryName + "\\SmartSystemMenuProcessExclusions.txt";
			std::vector<std::string> excludedProcessNames = read_process_exclusions(exclusionsFileName);
			std::wstring exeTempPath(&exePath[0]);
			std::string exeFileName(exeTempPath.begin(), exeTempPath.end());
			exeFileName = get_file_name(exeFileName);
			std::transform(exeFileName.begin(), exeFileName.end(), exeFileName.begin(), ::tolower);
			if (array_contains(exeFileName, excludedProcessNames))
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