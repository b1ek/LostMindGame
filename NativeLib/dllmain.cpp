// dllmain.cpp : Defines the entry point for the DLL application.
#include "framework.h"
#include <cstdio>
#include <stdlib.h>

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

    __declspec(dllexport) void __stdcall getTextInConsole() {
        CONSOLE_SCREEN_BUFFER_INFO a;

    }

    LPCWSTR __getLongChar(char* value) {
        int ssz = strlen(value)+1;
        wchar_t *__wchar = new wchar_t[ssz];
        size_t sz;
        mbstowcs_s(&sz, __wchar, ssz, value, ssz-1);
        return __wchar;
    }

    __declspec(dllexport) void __stdcall placeButton(char* buttonText, int x, int y) {
        HWND button = CreateWindow(L"BUTTON", __getLongChar(buttonText), WS_TABSTOP | WS_VISIBLE | WS_CHILD | BS_DEFPUSHBUTTON,
            x, y, 75, 23, GetConsoleWindow(), NULL, (HINSTANCE)GetWindowLongPtr(GetConsoleWindow(), GWLP_HINSTANCE), NULL);
    }

    __declspec(dllexport) void __stdcall displayMessage(char* message) {
        MessageBox(NULL, __getLongChar(message), L"Message!", MB_OK);
    }
}