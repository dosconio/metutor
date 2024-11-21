// UTF-8
// @dosconio 20240716
// CW32F030C8
#include <cpp/MCU/XY/CW32F030>

using namespace uni;
GPIO_Pin& LED = GPIOC[13];
GPIO_Pin& KEY = GPIOB[2];

//{TODO} LED and SysDelay(); GPIN&;
void panic() { while (true); }
bool init() {
	// RCC_HSI_Enable(RCC_HSIOSC_DIV6);
	// InitTick(8000000);
	LED.setMode(GPIOMode::OUT_PushPull);
	KEY.setMode(GPIOMode::IN);
	return true;
}
int main() {
	if (!init()) panic();
	while (true) {
		LED = !KEY;// lit while key-down
	}
}
