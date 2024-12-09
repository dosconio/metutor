// ASCII CPP TAB4 CRLF
// @dosconio 2024

#define BOARD_DETAIL "5DAE7" // STM32MP135-DAE7
#include <cpp/MCU/ST/STM32MP13>
#include <cpp/string>
#include "metutor.h"

#define setAF(x) setMode(GPIOMode::OUT_AF_PushPull, spd)._set_alternate(x)

//{}  bool useDDR = false;//{}
bool exist_ddr = true;

void hand() { LED.Toggle(); }

bool init() {
	auto spd = GPIOSpeed::Veryhigh;
	if (!init_specific() || !init_clock()) return false;
	// LED
	LED.setMode(GPIOMode::OUT_PushPull);
	// LCD
	LCD_BL.setMode(GPIOMode::OUT_PushPull).setPull(true);
	LCD_DE.setAF(11);
	LCD_CLK.setAF(13);
	LCD_HSYNC.setAF(13);
	LCD_VSYNC.setAF(11);
	for0a(i, LCDR) LCDR[i]->setAF(LCDR_AF[i]);
	for0a(i, LCDG) LCDG[i]->setAF(LCDG_AF[i]);
	for0a(i, LCDB) LCDB[i]->setAF(LCDB_AF[i]);
	LCD_BL = true;
	// default polarity D'DP and LTDC_PCPOLARITY_IPC
	auto& hpara = LTDC.refHorizontal(); {
		hpara.active_len = 800;
		hpara.back_porch = 88;
		hpara.front_porch = 40;
		hpara.sync_len = 48;
	}
	auto& vpara = LTDC.refVertical(); {
		vpara.active_len = 480;
		vpara.back_porch = 32;
		vpara.front_porch = 13;
		vpara.sync_len = 3;
	}
	if (LTDC.getFrequency() != 33e6) return false;// 33MHz
	LTDC.setMode(Color::Black);
	// EXTI
	GPIOA[3].setMode(GPIORupt::Anyedge);// USART2_RX
	GPIOG[10].setMode(GPIOMode::OUT_PushPull);// FDCAN1_TX
	GPIOA[3].setInterrupt(hand);
	return true;
}

extern "C" uint8_t test(void);
extern "C" uint8_t test2(void);
#define _times0  (*(uint32*)(0xC0000000 + 4*800*480 + 4))
#define _times  (*(uint32*)(0xC0000000 + 4*800*480))
fn main() -> int {
	if (!init()) loop;
	GPIOA[3].enInterrupt();
	test();
	test2();
	Rectangle rect(Point(0,0), Size2(800, 480));
	loop {
		static unsigned k = 10;
		GPIOG[10].Toggle();
		*(uint32*)&rect.color = k += 10;
		LCD.Draw(rect);
		uint32 times0, times;
		GPIOG[10].Toggle();
		SysDelay(200);
	}
}

// Global Data
VideoControlBlock LCD = LTDC[1].getControlBlock();

void LTDC_LAYER_t::DrawFont(const Point& disp, const DisplayFont& font) const {}

