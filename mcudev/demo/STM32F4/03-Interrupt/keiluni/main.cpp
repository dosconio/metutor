// UTF-8 C++(ARMCC-5) TAB4 CRLF
// @dosconio
#include "../../board.h"

void hand_key() { LEDB.Toggle(); }

int main() {
	if (!RCC.setClock(SysclkSource::HSE))
	{ while (true); }
	LEDB.setMode(GPIOMode::OUT_PushPull);
	KEY.setMode(GPIORupt::Anyedge, hand_key);
	LEDB = !false;
	KEY.enInterrupt();
	while (true);
}
