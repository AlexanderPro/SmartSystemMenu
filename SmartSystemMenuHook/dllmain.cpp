// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include "tinyxml2.h"
#include <string>
#include <vector>
#include <algorithm>
#include <fstream>

extern HINSTANCE g_appInstance;

using namespace tinyxml2;
using namespace std;

string get_file_name(const string &path)
{
	size_t index = path.find_last_of("/\\");
	string fileName = path.substr(index + 1);
	return fileName;
}

string get_directory_name(const string &path)
{
	size_t index = path.find_last_of("/\\");
	string directoryName = path.substr(0, index);
	return directoryName;
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

		try
		{
			WCHAR exePath[MAX_PATH], dllPath[MAX_PATH];
			if (GetModuleFileName(NULL, exePath, MAX_PATH) != 0 && GetModuleFileName(hModule, dllPath, MAX_PATH) != 0)
			{
				wstring dllTempPath(&dllPath[0]);
				string dllFileName(dllTempPath.begin(), dllTempPath.end());
				string dllDirectoryName = get_directory_name(dllFileName);
				
				wstring exeTempPath(&exePath[0]);
				string exeFileName(exeTempPath.begin(), exeTempPath.end());
				exeFileName = get_file_name(exeFileName);
				transform(exeFileName.begin(), exeFileName.end(), exeFileName.begin(), ::tolower);

				XMLDocument doc;
				string exclusionsFileName = dllDirectoryName + "\\SmartSystemMenu.xml";
				doc.LoadFile(exclusionsFileName.c_str());
				XMLElement* element = doc.FirstChildElement("smartSystemMenu");
				if (element)
				{
					element = element->FirstChildElement("processExclusions");
					if (element)
					{
						element = element->FirstChildElement("processName");
						while (element)
						{
							const char* value = element->GetText();
							bool ignoreHook = element->BoolAttribute("ignoreHook", false);
							string processName = value;
							transform(processName.begin(), processName.end(), processName.begin(), ::tolower);
							if (processName == exeFileName && ignoreHook == false)
							{
								return FALSE;
							}
							element = element->NextSiblingElement("processName");
						}
					}
				}
			}
		}
		catch (...)
		{
			TCHAR buf[255];
			wsprintf(buf, L"SmartSystemMenu exception");
			OutputDebugString(buf);
		}
		break;

	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}
