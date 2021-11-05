// dllmain.cpp : Defines the entry point for the DLL application.
#include "framework.h"
#include <cstdio>

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

extern "C" {
    __declspec(dllexport) void __stdcall printToXY(char *value, int x, int y) {
        CONSOLE_SCREEN_BUFFER_INFO inf;
        GetConsoleScreenBufferInfo(GetStdHandle(-11), &inf);
        COORD cords;
        cords.X = x; cords.Y = y;
        SetConsoleCursorPosition(GetStdHandle(-11), cords);
        printf(value);
        SetConsoleCursorPosition(GetStdHandle(-11), inf.dwCursorPosition);
    }
}