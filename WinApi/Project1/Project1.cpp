// Project1.cpp : Определяет точку входа для приложения.
//
#define _USE_MATH_DEFINES
#include "framework.h"
#include "Project1.h"
#include <string>
#include <ctime>
#include <math.h>

#define MAX_LOADSTRING 100

// Глобальные переменные:
HINSTANCE hInst;                                // текущий экземпляр
WCHAR szTitle[MAX_LOADSTRING];                  // Текст строки заголовка
WCHAR szWindowClass[MAX_LOADSTRING];            // имя класса главного окна

// Отправить объявления функций, включенных в этот модуль кода:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);

// Цвета
COLORREF RED_COLOR = RGB(255, 0, 0);
COLORREF GREEN_COLOR = RGB(0, 255, 0);

HWND hWnd1;
HWND hWnd2;

static COLORREF colorWin1 = RED_COLOR;
static COLORREF colorWin2 = RED_COLOR;

static float secAngle = 360.0f / 60.0f;
static float hourAngle = 360.0f / 12.0f;

static int drawType = 0;

static float hAngle;
static float mAngle;
static float sAngle;

static int hourLen;
static int minLen;
static int secLen;

float GetAngleHour(int h, int m) {

    float hDegree = (h % 12) * hourAngle + (m / 60.0) * hourAngle;

    hDegree -= 90;

    if (hDegree < 0) {
        hDegree += 360.0;
    }

    return hDegree;
}

float GetAngleMinute(int m, int s) {
    float mDegree = m * secAngle + (s / 60.0) * secAngle;

    mDegree -= 90;

    if (mDegree < 0) {
        mDegree += 360;
    }

    return mDegree;
}

float GetAngleSecond(int s) {
    float sDegree = s * secAngle;

    sDegree -= 90;

    if (sDegree < 0) {
        sDegree += 360;
    }

    return sDegree;
}

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    // TODO: Разместите код здесь.

    // Инициализация глобальных строк
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_PROJECT1, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Выполнить инициализацию приложения:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_PROJECT1));

    MSG msg;

    // Цикл основного сообщения:
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
        {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }

    return (int) msg.wParam;
}



//
//  ФУНКЦИЯ: MyRegisterClass()
//
//  ЦЕЛЬ: Регистрирует класс окна.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_PROJECT1));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_PROJECT1);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

//
//   ФУНКЦИЯ: InitInstance(HINSTANCE, int)
//
//   ЦЕЛЬ: Сохраняет маркер экземпляра и создает главное окно
//
//   КОММЕНТАРИИ:
//
//        В этой функции маркер экземпляра сохраняется в глобальной переменной, а также
//        создается и выводится главное окно программы.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   hInst = hInstance; // Сохранить маркер экземпляра в глобальной переменной

   hWnd1 = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);

   hWnd2 = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
       CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);
   

   if (!hWnd1 || !hWnd2)
   {
      return FALSE;
   }

   ShowWindow(hWnd1, nCmdShow);
   UpdateWindow(hWnd1);
   ShowWindow(hWnd2, nCmdShow);
   UpdateWindow(hWnd2);


   return TRUE;
}

//
//  ФУНКЦИЯ: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  ЦЕЛЬ: Обрабатывает сообщения в главном окне.
//
//  WM_COMMAND  - обработать меню приложения
//  WM_PAINT    - Отрисовка главного окна
//  WM_DESTROY  - отправить сообщение о выходе и вернуться
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    //static COLORREF curColor = RED_COLOR;
    static int x = 0, y = 0;
    RECT textRect = { 60, 0, 300, 30 };
    RECT renderRect;

    COLORREF curColor;

    if (hWnd == hWnd1) {
        curColor = colorWin1;
    }
    else {
        curColor = colorWin2;
    }

    switch (message)
    {
    case WM_CREATE:
        SetTimer(hWnd, 1, 100, NULL);
        break;

    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            // Разобрать выбор в меню:
            switch (wmId)
            {
            case IDM_ABOUT:
                DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
                break;
            case IDM_EXIT:
                DestroyWindow(hWnd);
                break;
            case IDM_MODE:
                drawType = drawType == 0 ? 1 : 0;
                break;

            default:
                return DefWindowProc(hWnd, message, wParam, lParam);
            }
        }
        break;

    case WM_TIMER: {
        time_t currentTime = time(NULL);
        struct tm timeInfo;
        localtime_s(&timeInfo, &currentTime);

        RECT rect;
        GetClientRect(hWnd, &rect);
        renderRect = { 0, 30, rect.right, rect.bottom };

        InvalidateRect(hWnd, &renderRect, TRUE);

        hAngle = GetAngleHour(timeInfo.tm_hour, timeInfo.tm_min);
        mAngle = GetAngleMinute(timeInfo.tm_min, timeInfo.tm_sec);
        sAngle = GetAngleSecond(timeInfo.tm_sec);

        break;
    }

    case WM_PAINT:
        {
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hWnd, &ps);
            RECT rect;
            GetClientRect(hWnd, &rect);
           
            renderRect = { 0, 30, rect.right, rect.bottom };

            HPEN pen;

            pen = CreatePen(PS_SOLID, 2, curColor);
            auto prevObj = SelectObject(hdc, pen);

            // КРЕСТИК ----------------------------------------------------->>>
            /*LineTo(hdc, rect.right, rect.bottom);
            MoveToEx(hdc, rect.left, rect.bottom, NULL);
            LineTo(hdc, rect.right, rect.top);*/

            // ЧАСЫ -------------------------------------------------------->>>
            int centerX = renderRect.right / 2;
            int centerY = (renderRect.bottom - 30) / 2 + 30;
            int radius = renderRect.right < renderRect.bottom ? renderRect.right : renderRect.bottom - 30;
            radius /= 2;

            secLen = radius * 0.9;
            minLen = radius * 0.9;
            hourLen = radius * 0.8;

            int xs = centerX + secLen * cos(sAngle * M_PI / 180.0);
            int ys = centerY + secLen * sin(sAngle * M_PI / 180.0);

            int xm = centerX + minLen * cos(mAngle * M_PI / 180.0);
            int ym = centerY + minLen * sin(mAngle * M_PI / 180.0);

            int xh = centerX + hourLen * cos(hAngle * M_PI / 180.0);
            int yh = centerY + hourLen * sin(hAngle * M_PI / 180.0);

            Ellipse(hdc, centerX - radius, centerY - radius, centerX + radius, centerY + radius);
            MoveToEx(hdc, centerX, centerY, NULL);
            LineTo(hdc, xs, ys);

            // РИСКИ
            for (int i = 0; i < 12; i++) {
                int x1 = centerX + radius * cos(i * M_PI / 6.0);
                int y1 = centerY + radius * sin(i * M_PI / 6.0);

                int x2 = centerX + (radius * 0.9) * cos(i * M_PI / 6.0);
                int y2 = centerY + (radius * 0.9) * sin(i * M_PI / 6.0);

                MoveToEx(hdc, x1, y1, NULL);
                LineTo(hdc, x2, y2);
            }

            SelectObject(hdc, prevObj);

            pen = CreatePen(PS_SOLID, 6, curColor);
            prevObj = SelectObject(hdc, pen);

            MoveToEx(hdc, centerX, centerY, NULL);
            LineTo(hdc, xm, ym);

            MoveToEx(hdc, centerX, centerY, NULL);
            LineTo(hdc, xh, yh);

            SelectObject(hdc, prevObj);

            std::wstring text = L"X coord: " + std::to_wstring(x) + L", Y coord: " + std::to_wstring(y);
            DrawTextW(hdc, text.c_str(), -1, &textRect, DT_LEFT);

            EndPaint(hWnd, &ps);
            ReleaseDC(hWnd, hdc);
        }
        break;

    case WM_LBUTTONDOWN:
        RECT rect;
        GetClientRect(hWnd, &rect);
        renderRect = { 0, 30, rect.right, rect.bottom };

        if (hWnd == hWnd1) {
            colorWin1 = colorWin1 == RED_COLOR ? GREEN_COLOR : RED_COLOR;
        }
        else {
            colorWin2 = colorWin2 == RED_COLOR ? GREEN_COLOR : RED_COLOR;
        }

        InvalidateRect(hWnd, &renderRect, TRUE);
        break;

    case WM_MOUSEMOVE:
        x = LOWORD(lParam);
        y = HIWORD(lParam);

        if (drawType == 0) {
            InvalidateRect(hWnd, &textRect, TRUE);
        }
        else {
            InvalidateRect(hWnd1, &textRect, TRUE);
            InvalidateRect(hWnd2, &textRect, TRUE);
        }

        break;

    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

// Обработчик сообщений для окна "О программе".
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    UNREFERENCED_PARAMETER(lParam);
    switch (message)
    {
    case WM_INITDIALOG:
        return (INT_PTR)TRUE;

    case WM_COMMAND:
        if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
        {
            EndDialog(hDlg, LOWORD(wParam));
            return (INT_PTR)TRUE;
        }
        break;
    }
    return (INT_PTR)FALSE;
}