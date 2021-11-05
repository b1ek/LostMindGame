// dllmain.cpp : Defines the entry point for the DLL application.
#include "framework.h"
#include <cstdio>
#include <stdlib.h>
#include <iostream>

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
__declspec(dllexport) void __stdcall printToXY(char *value, int x, int y) {
    CONSOLE_SCREEN_BUFFER_INFO inf;
    GetConsoleScreenBufferInfo(GetStdHandle(-11), &inf);
    COORD cords = { x, y };
    SetConsoleCursorPosition(GetStdHandle(-11), cords);
    printf(value);
    SetConsoleCursorPosition(GetStdHandle(-11), inf.dwCursorPosition);
}

__declspec(dllexport) void __stdcall getTextInConsole() {
    CONSOLE_SCREEN_BUFFER_INFO a;

}

LPCWSTR getLPCWSTR(char* value) {
    int ssz = strlen(value)+1;
    wchar_t *__wchar = new wchar_t[ssz];
    size_t sz;
    mbstowcs_s(&sz, __wchar, ssz, value, ssz-1);
    return __wchar;
}

extern "C" {
    __declspec(dllexport) void __stdcall placeButton(char* buttonText, int x, int y) {
        HWND button = CreateWindow(L"BUTTON", getLPCWSTR(buttonText), WS_TABSTOP | WS_VISIBLE | WS_CHILD | BS_DEFPUSHBUTTON,
            x, y, 75, 23, GetConsoleWindow(), NULL, (HINSTANCE)GetWindowLongPtr(GetConsoleWindow(), GWLP_HINSTANCE), NULL);
    }

    __declspec(dllexport) int __stdcall displayMessage(char* message, char* title, UINT styles) {
        return MessageBox(NULL, getLPCWSTR(message), getLPCWSTR(title), styles);
        return 1;
    }

    __declspec(dllexport) int __stdcall infoMessageBox(char* message) {
        return MessageBox(NULL, getLPCWSTR(message), L"Info", MB_OK | MB_ICONINFORMATION);
    }

    __declspec(dllexport) int __stdcall warningMessageBox(char* message) {
        return MessageBox(NULL, getLPCWSTR(message), L"Warning!", MB_OK | MB_ICONWARNING);
    }

    __declspec(dllexport) int __stdcall errorMessageBox(char* message) {
        return MessageBox(NULL, getLPCWSTR(message), L"ERROR!!", MB_OK | MB_ICONERROR);
    }

    __declspec(dllexport) void __stdcall flushConsole() {
        COORD topLeft = { 0, 0 };
    }
}