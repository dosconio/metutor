/**
  ******************************************************************************
  * @file    startup_stm32mp135dxx_ca7.c
  * @author  MCD Application Team
  * @brief   CMSIS Cortex-A7 Device Peripheral Access Layer A7 Startup source file
  *          for GCC toolchain
  *   This file :
  *      - Sets up initial PC => Reset Handler
  *      - Puts all A7 cores except first one on "Wait for interrupt"
  *      - Configures cores
  *      - Sets up Exception vectors
  *      - Sets up stacks for all exception modes
  *      - Call SystemInit to initialize IRQs, caches, MMU, ...
  *
  */

#include "stm32mp13xx.h"
#include "irq_ctrl.h"
#include <c/stdinc.h>

#define USR_MODE 0x10            /* User mode */
#define FIQ_MODE 0x11            /* Fast Interrupt Request mode */
#define IRQ_MODE 0x12            /* Interrupt Request mode */
#define SVC_MODE 0x13            /* Supervisor mode */
#define ABT_MODE 0x17            /* Abort mode */
#define UND_MODE 0x1B            /* Undefined Instruction mode */
#define SYS_MODE 0x1F            /* System mode */

/*----------------------------------------------------------------------------
  Internal References
 *----------------------------------------------------------------------------*/
void Vectors       (void) __attribute__ ((naked, section("RESET")));
void Reset_Handler (void) __attribute__ ((naked, target("arm")));

/*----------------------------------------------------------------------------
  Exception / Interrupt Handler
 *----------------------------------------------------------------------------*/
void Undef_Handler (void) __attribute__ ((weak, alias("Default_Handler")));
void SVC_Handler   (void) __attribute__ ((weak, alias("Default_Handler")));
void PAbt_Handler  (void) __attribute__ ((weak, alias("Default_Handler")));
void DAbt_Handler  (void) __attribute__ ((weak, alias("Default_Handler")));
void Rsvd_Handler  (void) __attribute__ ((weak, alias("Default_Handler")));
void IRQ_Handler   (void) __attribute__ ((weak, alias("Default_Handler")));
void FIQ_Handler   (void) __attribute__ ((weak, alias("Default_Handler")));

extern Handler_t IRQ_Vector_Table[MAX_IRQ_n];

/*----------------------------------------------------------------------------
  Exception / Interrupt Vector Table
 *----------------------------------------------------------------------------*/
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

/*----------------------------------------------------------------------------
  Default Handler for Exceptions / Interrupts
 *----------------------------------------------------------------------------*/
void Default_Handler(void) {
  while(1);
}

/*----------------------------------------------------------------------------
  Reset Handler called on controller reset
 *----------------------------------------------------------------------------*/
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
  ::[usr_mode] "M" (USR_MODE),
    [fiq_mode] "M" (FIQ_MODE),
    [irq_mode] "M" (IRQ_MODE),
    [svc_mode] "M" (SVC_MODE),
    [abt_mode] "M" (ABT_MODE),
    [und_mode] "M" (UND_MODE),
    [sys_mode] "M" (SYS_MODE));
}

/*----------------------------------------------------------------------------
  Reset_Handler is declared as a 32 bit function  - target("arm") instead of Thumb.
  Any function added after Reset_Handler() will conserve this mode declaration.
  To keep Thumb mode, new function should be added before Reset_Handler().
 *----------------------------------------------------------------------------*/
