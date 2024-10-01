// UTF-8 C++(ARMCC-5) TAB4 CRLF
// @dosconio
#include "cpp/MCU/ST/STM32F1"
using namespace uni;

GPIO_Pin& LEDR = GPIO['B'][ 5];
GPIO_Pin& LEDG = GPIO['B'][ 0];
GPIO_Pin& LEDB = GPIO['B'][ 1];
GPIO_Pin& KEY1 = GPIO['A'][ 0];
GPIO_Pin& KEY2 = GPIO['C'][13];

void hand_key1() { LEDB.Toggle(); }
void hand_key2() { LEDG.Toggle(); }

word time = 0;
void hand_tim6(void) { time++; }

int main() {
	if (!RCC.setClock(SysclkSource::HSE))
	{ while (true); }
	LEDR.setMode(GPIOMode::OUT_PushPull);
	LEDG.setMode(GPIOMode::OUT_PushPull);
	LEDB.setMode(GPIOMode::OUT_PushPull);
	TIM6.setMode(hand_tim6);
	TIM6.ConfigMaster();
	TIM6.setInterruptPriority(1, 0);
	LEDR = LEDG = LEDB = !false;	
	TIM6.enInterrupt();
	while (true) {
		if ( time == 500 ) // 500 * 1 ms = 0.5 s
		{
			time = 0;
			LEDB.Toggle();
		}  
	}
}
