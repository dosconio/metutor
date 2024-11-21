
#ifndef BOARD_DETAIL
#warning Recommand define BOARD_DETAIL and do init_specific
#endif

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
