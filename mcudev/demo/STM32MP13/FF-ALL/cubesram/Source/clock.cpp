#include <cpp/MCU/ST/STM32MP13>
#include <c/driver/RCC/RCC-registers.hpp>

using namespace uni;
extern bool useDDR;

_ESYM_C void erro();

static bool init_clock_no_ddr() {
	using namespace RCCReg;
	RCC.canMode();
	PWR.setDBP(true);
	// AKA __HAL_RCC_LSEDRIVE_CONFIG(RCC_LSEDRIVE_MEDIUMHIGH);
	RCC[BDCR].maset(_IMM(_BDCR::LSEDRV), 2, 0b10);
	// OSC PART:  Order: HSE HSI CSI LSI LSE PLL
	/*
	RCC_OscInitStructure.OscillatorType = (
		RCC_OSCILLATORTYPE_HSI | RCC_OSCILLATORTYPE_HSE |
		RCC_OSCILLATORTYPE_CSI | RCC_OSCILLATORTYPE_LSI | RCC_OSCILLATORTYPE_LSE);
	RCC_OscInitStructure.HSIState = RCC_HSI_ON;
	RCC_OscInitStructure.HSEState = RCC_HSE_ON;
	RCC_OscInitStructure.LSEState = RCC_LSE_ON;
	RCC_OscInitStructure.LSIState = RCC_LSI_ON;
	RCC_OscInitStructure.CSIState = RCC_CSI_ON;
	RCC_OscInitStructure.HSICalibrationValue = 0x00; //Default reset value
	RCC_OscInitStructure.CSICalibrationValue = 0x10; //Default reset value
	RCC_OscInitStructure.HSIDivValue = RCC_HSI_DIV1; //Default value
	*/
	uni::RCC.HSE.setMode(false);// no-bypass
	uni::RCC.HSI.setMode(0, 0);// DIV1 and Cali(0x0)
	uni::RCC.CSI.setMode(0x10);
	uni::RCC.LSI.setMode();
	uni::RCC.LSE.setMode(false);// no-bypass
	/* PLL1
	RCC_OscInitStructure.PLL.PLLState = RCC_PLL_ON;
	RCC_OscInitStructure.PLL.PLLSource = RCC_PLL12SOURCE_HSE;
	RCC_OscInitStructure.PLL.PLLM = 3;
	RCC_OscInitStructure.PLL.PLLN = 81;
	RCC_OscInitStructure.PLL.PLLP = 1;
	RCC_OscInitStructure.PLL.PLLQ = 2;
	RCC_OscInitStructure.PLL.PLLR = 2;
	RCC_OscInitStructure.PLL.PLLFRACV = 0x800;
	RCC_OscInitStructure.PLL.PLLMODE = RCC_PLL_FRACTIONAL;
	*/
	if (!RCC.PLL1.setModePLL12(PLLMode::Fractional, PLL1Source::HSE,
		.m=3, .n=81, .p=1, .q=2, .r=2, .frac=0x800)) erro();
	/* PLL2
	RCC_OscInitStructure.PLL2.PLLState = RCC_PLL_ON;
	RCC_OscInitStructure.PLL2.PLLSource = RCC_PLL12SOURCE_HSE;
	RCC_OscInitStructure.PLL2.PLLM = 3;
	RCC_OscInitStructure.PLL2.PLLN = 66;
	RCC_OscInitStructure.PLL2.PLLP = 2;
	RCC_OscInitStructure.PLL2.PLLQ = 2;
	RCC_OscInitStructure.PLL2.PLLR = 1;
	RCC_OscInitStructure.PLL2.PLLFRACV = 0x1400;
	RCC_OscInitStructure.PLL2.PLLMODE = RCC_PLL_FRACTIONAL;
	*/
	if (!RCC.PLL2.setModePLL12(PLLMode::Fractional, PLL2Source::HSE,
		.m=3, .n=66, .p=2, .q=2, .r=1, .frac=0x1400)) erro();
	/* PLL3
	RCC_OscInitStructure.PLL3.PLLState = RCC_PLL_ON;
	RCC_OscInitStructure.PLL3.PLLSource = RCC_PLL3SOURCE_HSE;
	RCC_OscInitStructure.PLL3.PLLM = 2;
	RCC_OscInitStructure.PLL3.PLLN = 34;
	RCC_OscInitStructure.PLL3.PLLP = 2;
	RCC_OscInitStructure.PLL3.PLLQ = 17;
	RCC_OscInitStructure.PLL3.PLLR = 2;
	RCC_OscInitStructure.PLL3.PLLRGE = RCC_PLL3IFRANGE_1;
	RCC_OscInitStructure.PLL3.PLLFRACV = 0x1a04;
	RCC_OscInitStructure.PLL3.PLLMODE = RCC_PLL_FRACTIONAL;
	*/
	bool if_range3 = true;
	if (!RCC.PLL3.setModePLL34(PLLMode::Fractional, PLL3Source::HSE, if_range3,
		.m=2, .n=34, .p=2, .q=17, .r=2, .frac=0x1a04)) erro();
	/* PLL4
	RCC_OscInitStructure.PLL4.PLLState = RCC_PLL_ON;
	RCC_OscInitStructure.PLL4.PLLSource = RCC_PLL4SOURCE_HSE;
	RCC_OscInitStructure.PLL4.PLLM = 2;
	RCC_OscInitStructure.PLL4.PLLN = 50;
	RCC_OscInitStructure.PLL4.PLLP = 12;
	RCC_OscInitStructure.PLL4.PLLQ = 60;
	RCC_OscInitStructure.PLL4.PLLR = 6;
	RCC_OscInitStructure.PLL4.PLLRGE = RCC_PLL4IFRANGE_1;
	RCC_OscInitStructure.PLL4.PLLFRACV = 0;
	RCC_OscInitStructure.PLL4.PLLMODE = RCC_PLL_INTEGER;
	*/
	bool if_range4 = true;
	if (!RCC.PLL4.setModePLL34(PLLMode::Fractional, PLL4Source::HSE, if_range4,
		.m=2, .n=38, .p=12, .q=14, .r=6, .frac=4096)) erro();// 33M for LTDC
		//.m=2, .n=35, .p=12, .q=14, .r=6, .frac=0)) erro();   // 30M for LTDC

	bool state = true;
	// BUS PART:  Order: MPU ACLK HCLK PCLK4 PCLK5 PCLK1 PCLK2 PCLK3 PCLK6
	/* RCC_ClkInitStructure.ClockType = (
		RCC_CLOCKTYPE_MPU   | RCC_CLOCKTYPE_ACLK  |
		RCC_CLOCKTYPE_HCLK  | RCC_CLOCKTYPE_PCLK4 |
		RCC_CLOCKTYPE_PCLK5 | RCC_CLOCKTYPE_PCLK1 |
		RCC_CLOCKTYPE_PCLK6 |
		RCC_CLOCKTYPE_PCLK2 | RCC_CLOCKTYPE_PCLK3); */
	/* MPU Clock
	 * 	RCC_ClkInitStructure.MPUInit.MPU_Clock = RCC_MPUSOURCE_PLL1;
	 *  RCC_ClkInitStructure.MPUInit.MPU_Div = RCC_MPU_DIV2;
	 * */
	asserv(state) = RCC.Sysclock.setMode(SysclkSource::PLL1, 1);// PLL1 DIV2
	else erro();
	/* AXISS
	 * 	RCC_ClkInitStructure.AXISSInit.AXI_Clock = RCC_AXISSOURCE_PLL2;
	 *  RCC_ClkInitStructure.AXISSInit.AXI_Div = RCC_AXI_DIV1;
	 * */
	asserv(state) = RCC.AXIS.setMode(AxisSource::PLL2, 1);// PLL2 DIV1
	else erro();
	/* MLAHB
	 * 	RCC_ClkInitStructure.MLAHBInit.MLAHB_Clock = RCC_MLAHBSSOURCE_PLL3;
	 *  RCC_ClkInitStructure.MLAHBInit.MLAHB_Div = RCC_MLAHB_DIV1;
	 * */
	asserv(state) = RCC.MLAHB.setMode(MLAHBSource::PLL3, 0);// PLL3 DIV1
	else erro();
	// APBs
	RCC.APB4.setMode(1);// PCLK4 RCC_APB4_DIV2
	RCC.APB5.setMode(2);// PCLK5 RCC_APB5_DIV4
	RCC.APB1.setMode(1);// PCLK1 RCC_APB1_DIV2
	RCC.APB2.setMode(1);// PCLK2 RCC_APB2_DIV2
	RCC.APB3.setMode(1);// PCLK3 RCC_APB3_DIV2
	RCC.APB6.setMode(1);// PCLK6 RCC_APB6_DIV2
	//
	// Note : The activation of the I/O Compensation Cell is recommended with communication  interfaces
	// (GPIO, SPI, FMC, XSPI ...)  when  operating at  high frequencies(please refer to product datasheet)
	// The I/O Compensation Cell activation  procedure requires :
	// - The activation of the CSI clock
	// - The activation of the SYSCFG clock
	// - Enabling the I/O Compensation Cell : setting bit[0] of register SYSCFG_CCCSR
	// To do this please enable the block
	if (false) {
		//{} __HAL_RCC_CSI_ENABLE() ;
		//{} __HAL_RCC_SYSCFG_CLK_ENABLE() ;
		//{} HAL_EnableCompensationCell();
	}
	return true;
}

bool init_clock(void) {
	RCC.Sysclock.getCoreFrequency();
	SysTick::enClock(1000);//SystemClock_Config_NoDDR();//{} if (!RCC.setClock(SysclkSource::HSE)) while (true);
	if (useDDR) _TODO return false;
	return init_clock_no_ddr();
	return true;
}

extern "C" { void erro(void) { loop; } }
extern "C" { void Error_Handler(void) { erro(); } }
extern "C" { void assert_failed(void) { erro(); } }
