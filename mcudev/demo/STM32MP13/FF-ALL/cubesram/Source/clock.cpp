#include <cpp/MCU/ST/STM32MP13>
#include <c/driver/RCC/RCC-registers.hpp>

using namespace uni;
extern bool useDDR;

_ESYM_C void erro();

bool init_ddr() {
	RCC.CSI.enAble(true);
	RCC.enSyscfg(true);
	MCE.enClock();
	TZC.enClock();
	TZC[TZCReg::GATE_KEEPER] = 0;
	TZC[TZCReg::REG_ID_ACCESSO] = 0xFFFFFFFF;// Allow DDR Region0 R/W  non secure for all IDs
	TZC[TZCReg::REG_ATTRIBUTESO] = 0xC0000001;
	TZC[TZCReg::GATE_KEEPER] |= 1;// Enable the access in secure Mode. filter 0 request close
	// Enable ETZPC & BACKUP SRAM for security
	ETZPC.enClock();
	BKPSRAM.enClock();
	BSEC[BSECReg::BSEC_DENABLE] = 0x47f;
	if (!DDR.setMode(DDRType::DDR3, DDR_t::DDRSize_256M)) return false;
	// Check DDR Write/Read
	{
		Letvar(tmpp, uint32*, 0xC0000000);
		tmpp[0] = 0x55AAAA55;
		return *tmpp == 0x55AAAA55;
	}
}

bool init_clock() {
	using namespace RCCReg;
	RCC.Sysclock.getCoreFrequency();
	SysTick::enClock(1000);
	RCC.canMode();
	PWR.setDBP(true);
	// AKA __HAL_RCC_LSEDRIVE_CONFIG(RCC_LSEDRIVE_MEDIUMHIGH);
	RCC[BDCR].maset(_IMM(_BDCR::LSEDRV), 2, 0b10);
	// OSC PART:  Order: HSE HSI CSI LSI LSE PLL
	uni::RCC.HSE.setMode(false);// no-bypass
	uni::RCC.HSI.setMode(0, 0);// DIV1 and Cali(0x0)
	uni::RCC.CSI.setMode(0x10);
	uni::RCC.LSI.setMode();
	uni::RCC.LSE.setMode(false);// no-bypass

	if (!RCC.PLL1.setModePLL12(PLLMode::Fractional, PLL1Source::HSE,
		.m=3, .n=81, .p=1, .q=2, .r=2, .frac=0x800)) erro();

	if (!RCC.PLL2.setModePLL12(PLLMode::Fractional, PLL2Source::HSE,
		//.m=3, .n=66, .p=2, .q=2, .r=1, .frac=0x1400)) erro();
		//.m=3, .n=50, .p=2, .q=2, .r=1, .frac=0)) erro();// for DDR 400M
		//.m=3, .n=62, .p=2, .q=2, .r=1, .frac=4096)) erro();// for DDR 500M
		.m=3, .n=66, .p=2, .q=2, .r=1, .frac=5120)) erro();// for 533M

	bool if_range3 = true;
	if (!RCC.PLL3.setModePLL34(PLLMode::Fractional, PLL3Source::HSE, if_range3,
		.m=2, .n=34, .p=2, .q=17, .r=2, .frac=0x1a04)) erro();

	bool if_range4 = true;
	if (!RCC.PLL4.setModePLL34(PLLMode::Fractional, PLL4Source::HSE, if_range4,
		.m=2, .n=38, .p=12, .q=14, .r=6, .frac=4096)) erro();// 33M for LTDC (.Q)
		//.m=2, .n=35, .p=12, .q=14, .r=6, .frac=0)) erro();   // 30M for LTDC

	bool state = true;

	// BUS PART:  Order: MPU ACLK HCLK PCLK4 PCLK5 PCLK1 PCLK2 PCLK3 PCLK6
	asserv(state) = RCC.Sysclock.setMode(SysclkSource::PLL1, 1);// PLL1 DIV2
	else erro();
	asserv(state) = RCC.AXIS.setMode(AxisSource::PLL2, 1);// PLL2 DIV1
	else erro();
	asserv(state) = RCC.MLAHB.setMode(MLAHBSource::PLL3, 0);// PLL3 DIV1
	else erro();
	// APBs
	RCC.APB4.setMode(1);// PCLK4 RCC_APB4_DIV2
	RCC.APB5.setMode(2);// PCLK5 RCC_APB5_DIV4
	RCC.APB1.setMode(1);// PCLK1 RCC_APB1_DIV2
	RCC.APB2.setMode(1);// PCLK2 RCC_APB2_DIV2
	RCC.APB3.setMode(1);// PCLK3 RCC_APB3_DIV2
	RCC.APB6.setMode(1);// PCLK6 RCC_APB6_DIV2
	// {unchk}
	bool compensate = false;
	// Note : The activation of the I/O Compensation Cell is recommended with communication  interfaces
	// (GPIO, SPI, FMC, XSPI ...)  when  operating at [HIGH frequencies] (refer to product datasheet)
	// The I/O Compensation Cell activation  procedure requires :
	// - The activation of the CSI clock
	// - The activation of the SYSCFG clock
	// - Enabling the I/O Compensation Cell : setting bit[0] of register SYSCFG_CCCSR
	// To do this please enable the block
	if (compensate) {
		//{} __HAL_RCC_CSI_ENABLE() ;
		//{} __HAL_RCC_SYSCFG_CLK_ENABLE() ;
		//{} HAL_EnableCompensationCell();
	}
	return true;
}

extern "C" { void erro(void) { loop; } }
extern "C" { void Error_Handler(void) { erro(); } }
extern "C" { void assert_failed(void) { erro(); } }
extern "C" {
#include <c/stdinc.h>
#include <c/proctrl/ARM.h>

// Internal References
void Vectors       (void) __attribute__ ((naked, section("RESET")));
void Reset_Handler (void) __attribute__ ((naked, target("arm")));

// ExcRupt Handler
void Undef_Handler (void) __attribute__ ((weak, alias("Default_Handler")));
void SVC_Handler   (void) __attribute__ ((weak, alias("Default_Handler")));
void PAbt_Handler  (void) __attribute__ ((weak, alias("Default_Handler")));
void DAbt_Handler  (void) __attribute__ ((weak, alias("Default_Handler")));
void Rsvd_Handler  (void) __attribute__ ((weak, alias("Default_Handler")));
void IRQ_Handler   (void) __attribute__ ((weak, alias("Default_Handler")));
void FIQ_Handler   (void) __attribute__ ((weak, alias("Default_Handler")));

void Vectors(void) {
	__asm__ volatile(
	".align 7                                         \n"
	"LDR    PC, =Reset_Handler                        \n"
	"LDR    PC, =Undef_Handler                        \n"
	"LDR    PC, =SVC_Handler                          \n"
	"LDR    PC, =PAbt_Handler                         \n"
	"LDR    PC, =DAbt_Handler                         \n"
	"LDR    PC, =Rsvd_Handler                         \n"
	"LDR    PC, =IRQ_Handler                          \n"
	"LDR    PC, =FIQ_Handler                          \n"
	);
}

void Default_Handler(void) {
	loop;
}

// ABOVE: Thumb Mode
// BELOW: ARM 32b Mode
#if 1
void Reset_Handler(void) {
  __asm__ volatile(
  ".code 32                                         \n"
  /* Mask interrupts */
  "CPSID   if                                      \n"

  /* Put any cores other than 0 to sleep */
  "MRC     p15, 0, R0, c0, c0, 5                   \n"  /* Read MPIDR */
  "ANDS    R0, R0, #3                              \n"
  "goToSleep:                                      \n"
  "ITT  NE                                         \n"  /* Needed when in Thumb mode for following WFINE instruction */
  "WFINE                                           \n"
  "BNE     goToSleep                               \n"

  /* Reset SCTLR Settings */
  "MRC     p15, 0, R0, c1, c0, 0                   \n"  /* Read CP15 System Control register */
  "BIC     R0, R0, #(0x1 << 12)                    \n"  /* Clear I bit 12 to disable I Cache */
  "BIC     R0, R0, #(0x1 <<  2)                    \n"  /* Clear C bit  2 to disable D Cache */
  "BIC     R0, R0, #0x1                            \n"  /* Clear M bit  0 to disable MMU */
  "BIC     R0, R0, #(0x1 << 11)                    \n"  /* Clear Z bit 11 to disable branch prediction */
  "BIC     R0, R0, #(0x1 << 13)                    \n"  /* Clear V bit 13 to disable hivecs */
  "BIC     R0, R0, #(0x1 << 29)                    \n"  /* Clear AFE bit 29 to enable the full range of access permissions */
  "ORR     R0, R0, #(0x1 << 30)                    \n"  /* Set TE bit to take exceptions in Thumb mode */
  "MCR     p15, 0, R0, c1, c0, 0                   \n"  /* Write value back to CP15 System Control register */
  "ISB                                             \n"

  /* Configure ACTLR */
  "MRC     p15, 0, r0, c1, c0, 1                   \n"  /* Read CP15 Auxiliary Control Register */
  "ORR     r0, r0, #(1 <<  1)                      \n"  /* Enable L2 prefetch hint (UNK/WI since r4p1) */
  "MCR     p15, 0, r0, c1, c0, 1                   \n"  /* Write CP15 Auxiliary Control Register */

  /* Set Vector Base Address Register (VBAR) to point to this application's vector table */
  "LDR    R0, =Vectors                             \n"
  "MCR    p15, 0, R0, c12, c0, 0                   \n"
  "ISB                                             \n"

  /* Setup Stack for each exceptional mode */
  "CPS    %[fiq_mode]                              \n"
  "LDR    SP, =FIQ_STACK                           \n"
  "CPS    %[irq_mode]                              \n"
  "LDR    SP, =IRQ_STACK                           \n"
  "CPS    %[svc_mode]                              \n"
  "LDR    SP, =SVC_STACK                           \n"
  "CPS    %[abt_mode]                              \n"
  "LDR    SP, =ABT_STACK                           \n"
  "CPS    %[und_mode]                              \n"
  "LDR    SP, =UND_STACK                           \n"
  "CPS    %[sys_mode]                              \n"
  "LDR    SP, =SYS_STACK                           \n"

  /* Call SystemInit */
  "BL     SystemInit                               \n"

  /* Unmask interrupts */
  "CPSIE  if                                       \n"

  /* Initialize libc */
  "BL __libc_init_array \n"
  /* Call __main */
  "BL     main                                     \n"
  ::[usr_mode] "M" (_PCUMODE_USR),
    [fiq_mode] "M" (_PCUMODE_FIQ),
    [irq_mode] "M" (_PCUMODE_IRQ),
    [svc_mode] "M" (_PCUMODE_SVC),
    [abt_mode] "M" (_PCUMODE_ABT),
    [und_mode] "M" (_PCUMODE_UND),
    [sys_mode] "M" (_PCUMODE_SYS));
}
#endif

}
