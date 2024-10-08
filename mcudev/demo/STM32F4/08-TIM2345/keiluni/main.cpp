// UTF-8 C++(ARMCC-5) TAB4 CRLF
// @dosconio
#include "../../board.h"

void hand_timc() { 
	LEDR.Toggle();
}

int main() {
	LEDR.setMode(GPIOMode::OUT_PushPull);
	if (!RCC.setClock(SysclkSource::HSE)) while (true);
	bool output_mode = true;
	if (output_mode) {
		// PWM Output
		TIM2[1].setPulse(10000, 5000);//<=> TIM2.setPulse(1, 10000, 5000);
	}
	else {
		TIM2.setMode(hand_timc);// 500ms-period
		// TIM2.setInterruptPriority(0, 0);
		TIM2.enInterrupt();
	}

	while (true);
}
