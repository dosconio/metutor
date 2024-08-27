// UTF-8 C++(ARMCC-5) TAB4 CRLF
// @dosconio
#include "cpp/MCU/ST/STM32F1"
using namespace uni;

GPIO_Pin& LEDR = GPIO['B'][ 5];
GPIO_Pin& LEDG = GPIO['B'][ 0];
GPIO_Pin& LEDB = GPIO['B'][ 1];
GPIO_Pin& KEY1 = GPIO['A'][ 0];
GPIO_Pin& KEY2 = GPIO['C'][13];

int main() {
	if (!RCC.setClock(SysclkSource::HSE))
	{ while (true); }
	LEDR.setMode(GPIOMode::OUT_PushPull);
	LEDG.setMode(GPIOMode::OUT_PushPull);
	LEDB.setMode(GPIOMode::OUT_PushPull);
	KEY1.setMode(GPIOMode::IN_Floating);
	KEY2.setMode(GPIOMode::IN_Floating);
	LEDR = LEDG = LEDB = !false;
	while (true) {
		LEDG = !KEY1;
		LEDB = !KEY2;
	}
}
