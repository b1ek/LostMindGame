// dllmain.cpp : Defines the entry point for the DLL application.
#include "framework.h"
#include <cstdio>
#include <stdlib.h>
#include <iostream>
#include <conio.h>

HANDLE stdhndl;

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    stdhndl = GetStdHandle(-11);
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

typedef enum FG_COLORS {
    FG_BLACK = 0,
    FG_BLUE = 1,
    FG_GREEN = 2,
    FG_CYAN = 3,
    FG_RED = 4,
    FG_MAGENTA = 5,
    FG_BROWN = 6,
    FG_LIGHTGRAY = 7,
    FG_GRAY = 8,
    FG_LIGHTBLUE = 9,
    FG_LIGHTGREEN = 10,
    FG_LIGHTCYAN = 11,
    FG_LIGHTRED = 12,
    FG_LIGHTMAGENTA = 13,
    FG_YELLOW = 14,
    FG_WHITE = 15
}FG_COLORS;

typedef enum BG_COLORS {
    BG_NAVYBLUE = 16,
    BG_GREEN = 32,
    BG_TEAL = 48,
    BG_MAROON = 64,
    BG_PURPLE = 80,
    BG_OLIVE = 96,
    BG_SILVER = 112,
    BG_GRAY = 128,
    BG_BLUE = 144,
    BG_LIME = 160,
    BG_CYAN = 176,
    BG_RED = 192,
    BG_MAGENTA = 208,
    BG_YELLOW = 224,
    BG_WHITE = 240
}BG_COLORS;


extern "C" {
    __declspec(dllexport) void __stdcall print(char* value) {
        printf("%s", value);
    }
    __declspec(dllexport) void __stdcall printLn(char* value) {
        printf("%s\n", value);
    }

    __declspec(dllexport) void __stdcall setConsoleColor(WORD color) {
        SetConsoleTextAttribute(stdhndl, color);
    }

    __declspec(dllexport) void __stdcall printToXY(char *value, int x, int y) {
        CONSOLE_SCREEN_BUFFER_INFO inf;
        COORD cords = { x, y };
        GetConsoleScreenBufferInfo(stdhndl, &inf);
        SetConsoleCursorPosition(stdhndl, cords);
        print(value);
        SetConsoleCursorPosition(stdhndl, inf.dwCursorPosition);
    }
    __declspec(dllexport) void __stdcall placeButton(char* buttonText, int x, int y) {
        HWND button = CreateWindow(L"BUTTON", getLPCWSTR(buttonText), WS_TABSTOP | WS_VISIBLE | WS_CHILD | BS_DEFPUSHBUTTON,
            x, y, 75, 23, GetConsoleWindow(), NULL, (HINSTANCE)GetWindowLongPtr(GetConsoleWindow(), GWLP_HINSTANCE), NULL);
    }
    #pragma region Messages


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
    #pragma endregion

    __declspec(dllexport) void __stdcall flushConsole() {
        COORD coord = { 0, 0 };
        DWORD count;
        CONSOLE_SCREEN_BUFFER_INFO csbi;
        GetConsoleScreenBufferInfo(stdhndl, &csbi);
        FillConsoleOutputCharacter(stdhndl, ' ',
            csbi.dwSize.X * csbi.dwSize.Y,
            coord, &count);
        SetConsoleCursorPosition(stdhndl, coord);

    }
}