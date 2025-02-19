#include <cpp/MCU/ST/STM32H7>
extern "C" char _IDN_BOARD[16] {"STM32H743IIT6"};

using namespace uni;

// Not Advertisement: Openedv Board Parameters used
GPIN& LEDB = GPIOB[0];
GPIN& LEDR = GPIOB[1];

int main() {
	L1C.enAble();
	NVIC.setPriorityGroup(4); // 4 pre and 0 sub
	SysTick::enClock(1000);// 1kHz
	
	LEDB.setMode(GPIOMode::OUT);
	LEDR.setMode(GPIOMode::OUT);
	while (true) {
		LEDR.Toggle();
		SysDelay_ms(500);
		LEDB.Toggle();
		SysDelay_ms(500);
	}
}
