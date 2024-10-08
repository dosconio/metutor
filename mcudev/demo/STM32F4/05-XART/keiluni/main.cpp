// UTF-8 C++(ARMCC-5) TAB4 CRLF
// @dosconio
#include "../../board.h"

void hand_uart() {
	int data;
	XART1 >> data;
	XART1 << data + 1;
}

int main() {
	LEDR.setMode(GPIOMode::OUT_PushPull);
	if (!RCC.setClock(SysclkSource::HSE)) while (true);
	XART1.setMode();
	XART1.setInterrupt(hand_uart);
	XART1.enInterrupt();
	SysDelay(1);
	XART1 << "Hello~\n";
	while (true) {
		LEDR.Toggle();
		XART1 << ">>>\n";
		SysDelay(1000);
	}
}
