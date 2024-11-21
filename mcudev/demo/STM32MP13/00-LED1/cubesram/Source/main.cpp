// ASCII CPP TAB4 CRLF
// @dosconio 2024

#define BOARD_DETAIL "5DAE7" // STM32MP135-DAE7
#include <cpp/MCU/ST/STM32MP13>
#include <cpp/string>
#include "metutor.h"

bool useDDR = false;

// (No ADVERTISEMENT!) Openedv(R) Board [1o2o3x]
GPIN& LED = GPIOI[3];

bool init() {
	if (!init_specific() || !init_clock()) return false;
	LED.setMode(GPIOMode::OUT_PushPull);
	return true;
}

auto main() -> int {
	if (!init()) loop;
	loop {
		LED.Toggle();
		SysDelay(500);// ms
	}
}
