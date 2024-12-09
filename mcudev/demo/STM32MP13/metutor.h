
#ifndef BOARD_DETAIL
#warning Recommand define BOARD_DETAIL and do init_specific
#endif

#include <cpp/Device/_Video.hpp>

using namespace uni;

extern bool init_clock(void);

#define init_specific() StrAppend(_IDN_BOARD, BOARD_DETAIL)

// #include "stm32mp13xx_hal.h"

#undef RCC
#undef PLL1
#undef PLL2
#undef PLL3
#undef PLL4

#undef GPIO


// (No ADVERTISEMENT!) Openedv(R) Board [1o2o3x]
// LED
GPIN& LED = GPIOI[3];
// KEY
//    : ! The Pin also CONNECT the JTAG TCK. So using this is bad while debugging !
//    :   If you'd like to make EXTI Interrupt, use another GPIN connected with the RuptPIN
GPIN& KEY = GPIOF[14];
// LCD 888
GPIN* LCDR[8] {
	&GPIOE[11], &GPIOD[ 4], &GPIOG[ 7], &GPIOB[12],
	&GPIOD[14], &GPIOE[ 7], &GPIOE[13], &GPIOE[ 9]
};
byte LCDR_AF[8] {9,11,14,13, 14,14,14,14 };
GPIN* LCDG[8] {
	&GPIOF[ 5], &GPIOF[ 1], &GPIOH[13], &GPIOF[ 3],
	&GPIOE[12], &GPIOG[ 0], &GPIOA[12], &GPIOA[15]
};
byte LCDG_AF[8] {9,14,14,14, 11,14,14,11 };
GPIN* LCDB[8] {
	&GPIOD[ 5], &GPIOG[ 8], &GPIOD[10], &GPIOF[ 2],
	&GPIOG[15], &GPIOD[15], &GPIOD[ 1], &GPIOE[15]
};
byte LCDB_AF[8] {13,7,14,14, 11,14,11,14 };
GPIN& LCD_BL = GPIOD[13];
GPIN& LCD_DE = GPIOH[9];// AF11
GPIN& LCD_CLK= GPIOD[9];// AF13
GPIN& LCD_HSYNC = GPIOH[10];// AF13
GPIN& LCD_VSYNC = GPIOG[ 4];// AF11

class my_vci_t : public VideoControlInterface {
	virtual void SetCursor(const Point& disp) {}
	virtual Point GetCursor() { return Point(0,0); }
	virtual void DrawPoint (const Point& disp, Color color);
	virtual void DrawRectangle (const Rectangle& rect) {
		//ILI9341_OpenWindow (rect.x, rect.y, rect.width, rect.height);
		//ILI9341_FillColor(rect.width * rect.height, rect.color.ToRGB565());
	}
	virtual void DrawFont (const Point& disp, const DisplayFont& font) {
		//ILI9341_DispString_EN(disp.x, disp.y, (char*)font.addr);
	}
	virtual Color GetColor(Point p){return Color::From32(0);}
};
extern my_vci_t my_vci;
extern VideoControlBlock LCD;

