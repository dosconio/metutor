// GBK
#include "stdinc.h"
#include"multichar.h"
#include "stdio.h"

int main() {
	char str[] = { 0xE4, 0xBD, 0xA0, 0xE5, 0xA5, 0xBD };// Hello in Chinese 'ÄãºÃ'
	pureptr_t p = 0; stduint n;
	n = CscUTF(8, 16, str, byteof(str), &p);
	for0(i, n) printf("[0x%02hhx]", ((byte*)p)[i]);
	puts(""); mfree(p);
	n = CscUTF(8, 32, str, byteof(str), &p);
	for0(i, n) printf("[0x%02hhx]", ((byte*)p)[i]);
	puts(""); mfree(p);
	return 0;
}

/*RESULT 20240811:11:06
[0x60][0x4f][0x7d][0x59]
[0x60][0x4f][0x00][0x00][0x7d][0x59][0x00][0x00]
*/
