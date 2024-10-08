// UTF-8 C++(ARMCC-5) TAB4 CRLF
// @dosconio
#include "../../board.h"

int main() {
	if (!RCC.setClock(SysclkSource::HSE)) while (true);
	LEDR.setMode(GPIOMode::OUT_PushPull);
	LEDG.setMode(GPIOMode::OUT_PushPull);
	LEDB.setMode(GPIOMode::OUT_PushPull);
	KEY.setMode(GPIOMode::IN_Floating);
	while (true) {
		LEDB = KEY;
	}
}

