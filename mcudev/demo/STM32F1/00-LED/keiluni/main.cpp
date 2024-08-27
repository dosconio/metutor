// UTF-8 C++(ARMCC-5) TAB4 CRLF
// @dosconio
#include "cpp/MCU/ST/STM32F1"
using namespace uni;

GPIO_Pin& LEDR = GPIO['B'][5];
GPIO_Pin& LEDG = GPIO['B'][0];
GPIO_Pin& LEDB = GPIO['B'][1];

void loop() {
	static byte idx = 0;
	LEDB = idx & 0x01;
	LEDG = idx & 0x02;
	LEDR = idx & 0x04;
	Ranginc(idx, 2*2*2);
}

int main() {
	LEDR.setMode(GPIOMode::OUT_PushPull);
	LEDG.setMode(GPIOMode::OUT_PushPull);
	LEDB.setMode(GPIOMode::OUT_PushPull);
	while (true) {
		loop();
		for0 (i, 1000000);
	}
}
