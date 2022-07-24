#include<DxLib.h>
#include<math.h>

#define INITIAL 3
#define DIVIDE 8.0f

struct Point {
	float x;
	float y;
};

float GetRadian(int degree) {
	return DX_PI_F * degree / 180;
}

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	SetOutApplicationLogValidFlag(false);
	ChangeWindowMode(true);
	SetMainWindowText("Pentagon");

	if (DxLib_Init() == -1) {
		return -1;
	}

	SetBackgroundColor(0, 0, 0);
	SetDrawScreen(DX_SCREEN_BACK);

	float r = INITIAL;
	Point A, B;
	float sin18 = sinf(GetRadian(18));
	float cos18 = cosf(GetRadian(18));
	float sin81 = sinf(GetRadian(81));
	float cos81 = cosf(GetRadian(81));

	int color = GetColor(255, 255, 0);

	while (!CheckHitKey(KEY_INPUT_ESCAPE) && ProcessMessage() == 0 && ClearDrawScreen() == 0) {
		r += GetMouseWheelRotVol() / DIVIDE;
		if (r <= 0) {
			//r = 1;
		}

		float p = 460 / (sin18 + r * sin81);
		A.y = p * sin18;
		A.x = p * cos18;
		B.y = p * r * sin81 + A.y;
		B.x = p * r * cos81 + A.x;

		/*DrawTriangleAA(320, 10, A.x + 320, A.y + 10, B.x + 320, B.y + 10, color, true);
		DrawTriangleAA(320, 10, -A.x + 320, A.y + 10, -B.x + 320, B.y + 10, color, true);
		DrawTriangleAA(320, 10, B.x + 320, B.y + 10, -B.x + 320, B.y + 10, color, true);*/

		DrawTriangle(320, 10, A.x + 320, A.y + 10, B.x + 320, B.y + 10, color, true);
		DrawTriangle(320, 10, -A.x + 320, A.y + 10, -B.x + 320, B.y + 10, color, true);
		DrawTriangle(320, 10, B.x + 320, B.y + 10, -B.x + 320, B.y + 10, color, true);

		DrawFormatString(10, 10, GetColor(255, 255, 255), "r = %f", r);
		DrawFormatString(10, 50, GetColor(255, 255, 255), "A = (%f, %f)\nB = (%f, %f)", A.x, A.y, B.x, B.y);

		ScreenFlip();
	}

	DxLib_End();

	return 0;
}