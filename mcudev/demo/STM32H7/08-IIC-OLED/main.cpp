#define _DEBUG
#include <cpp/MCU/ST/STM32H7>
#include "../../../device/OLED.h"

extern "C" char _IDN_BOARD[16] {"STM32H743IIT6"};

OLED_PAGE_t OLED_Page;

using namespace uni;

// Not Advertisement: Openedv Board Parameters used
GPIN& LEDB = GPIOB[ 0];
GPIN& LEDR = GPIOB[ 1];
GPIN& KEYU = GPIOA[ 0];//   Up
GPIN& KEYL = GPIOC[13];// Left
GPIN& KEYD = GPIOH[ 2];// Down
GPIN& KEYR = GPIOH[ 3];//Right

char _buf[16 + 1]; String buf(_buf, byteof(_buf) - 1);

extern "C" {
char* StrHeap(const char* valit_str){ (void)valit_str; return nullptr; }
char* StrHeapAppendChars(char* dest, char chr, size_t n){(void)dest; (void)n; (void)chr; return nullptr; }
char* salc(size_t size){return 0;}
void outtxt(const char* str, stduint len) {XART1.out(str, len);}
}

void hand_xart1() {
	// while (!XART1.isReady());
	stduint timeout = 0x1FFFF;
	if (buf[0] == '\r') XART1.ClearBuffer();
	if (buf[XART1.getBufferPointer() - 1] == '\n') {
		//  assume \r\n
		buf[XART1.getBufferPointer() - 1] = 0;
		XART1.OutFormat("Hello, %s\n", buf.reference());
		XART1.ClearBuffer();
	}
	if (XART1.isReady()) XART1.innByInterrupt();
}

void iic_delay() { for0(i,5); }

void driv_soft() {
	// test only in -O0 mode
	using namespace uni;
	GPIN& SCL = GPIOH[ 4];
	GPIN& SDA = GPIOH[ 5];
	IIC_SOFT oled_wire(SDA, SCL);
	oled_wire.func_delay = iic_delay;
	OLED_t   oled(0x78, oled_wire);
	oled.setPage(&OLED_Page);
	SysDelay_ms(100);
	oled.setOutput();
	SysDelay_ms(100);
	oled.Fill(0);
	auto vci = oled.getControlInterface();
	VideoConsole Vcon(vci, Size2(128, 64));
	Vcon.forecolor = Color::White;
	Vcon.OutFormat("Halo%s%s!", "Happy", "Word");
	oled.Refresh();
	while (true) {
		LEDB.Toggle();
		SysDelay_ms(1000);
	}
}
void driv_hard() {

	
	
}

int main() {
	L1C.enAble();
	NVIC.setPriorityGroup(2);
	if (!RCC.setClock(SysclkSource::HSE)) erro();
	
	LEDB.setMode(GPIOMode::OUT) = !false;
	LEDR.setMode(GPIOMode::OUT) = !false;
	KEYU.setMode(GPIOMode::IN_Pull).setPull(false);
	KEYL.setMode(GPIOMode::IN_Pull).setPull( true);
	KEYD.setMode(GPIOMode::IN_Pull).setPull( true);
	KEYR.setMode(GPIOMode::IN_Pull).setPull( true);
	
	XART1.setMode(115200);
	// Interrupt Buffer Mode
	XART1.rx_buffer = buf.getSlice();
	XART1.RuptTrait::enInterrupt(hand_xart1);
	XART1.innByInterrupt();
	
	
	XART1.OutFormat("Hello, %s %s on %s\n", "Example", "IIC Drive OLED Screen", _IDN_BOARD);
	
	if (1) 
		driv_soft();
	else;
}

void erro(char* str) {
	LEDR.setMode(GPIOMode::OUT);
	while (true) {
		LEDR.Toggle();
		for(volatile unsigned i{0}; i < 4000000; i++){}
	}
}
