// ASCII CPP TAB4 CRLF
// @dosconio 2024

#ifndef _MPU_STM32MP13
#error Uoption lack
#endif
#define __stdlib_h
#define _STYLE_RUST
#define BOARD_DETAIL "5DAE7" // STM32MP135
#include <cpp/MCU/ST/STM32MP13>
#include <cpp/string>
#define optional
using namespace uni;

// (No AD!) Opendev(R) Board [1o2o3x]
GPIN& LED = GPIOI[3];

extern "C" void SystemClock_Config(void);

bool init() {
	optional RCC.Sysclock.getCoreFrequency();
	StrAppend(_IDN_BOARD, BOARD_DETAIL);
	optional SysTick::enClock(1000);//SystemClock_Config_NoDDR();//{} if (!RCC.setClock(SysclkSource::HSE)) while (true);
	SystemClock_Config();
	LED.setMode(GPIOMode::OUT_PushPull);
	return true;
}

auto main() -> int {
	if (!init()) loop;
	loop {
		LED.Toggle();
		SysDelay(250);// 4Hz
	}
}
