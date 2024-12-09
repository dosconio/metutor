// ASCII {C/G++, MSVC} . .
// dosconio 20241206 10:11 finished
#include <c/driver/UART.h>
#include <c/consio.h>
#include <signal.h>
extern "C" int gets(char*);

bool exit_next = false;
void sighandler(int signum) {
	exit_next = true;
}
uni::String buf0
#ifdef _WinNT
= "\\\\.\\";
#else
= "/dev/";
#endif
char buf1[64];
int main() {
	using namespace uni;
	outs("Choose your PORT for UART:\n");
	gets(buf1); buf0 += buf1;
	signal(SIGINT, sighandler);
	UART_t uart(buf0);// COM10 or ttyUSB0
	bool uart_state = uart.setMode(9600);
	printlog(_LOG_STDOUT, "UART: %d", uart_state);
	int data;
	if (uart_state) while (!exit_next) {
		if (uart >> data)
			outc(data);
	}
}
