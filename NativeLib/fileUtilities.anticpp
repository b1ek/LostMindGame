#include "framework.h"
#include <cstdio>
#include <stdlib.h>
#include <iostream>
#include <conio.h>
#include <fstream>
#include <sstream>
#include "fileUtil.h"

extern "C" {
    using namespace std;
    __declspec(dllexport) char* __stdcall readAllFileText(char* path) {
        ifstream t(path);
        stringstream buffer;
        buffer << t.rdbuf();
        char* ret = (char*) buffer.str().c_str();
        t.close();
        return ret;
    }
}