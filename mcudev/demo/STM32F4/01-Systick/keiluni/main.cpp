// UTF-8 C++(ARMCC-5) TAB4 CRLF
// @dosconio
#include "../../board.h"

int main() {
	LEDR.setMode(GPIOMode::OUT_PushPull);
	if (!RCC.setClock(SysclkSource::HSE)) while (true);
	
	while (true) {
		LEDR.Toggle();
		SysDelay(250);// ms
	}
}

