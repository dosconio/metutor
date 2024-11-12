//20241110 19:27 dosconio
#include <c/stdinc.h>
#include <c/bitmap.h>
#include <stdio.h>
using namespace uni;
int main(int argc, rostr args[]) {
	Bitmap bm(1000);
	for0(i, 10) printf("[%d] %d\n", i, bm.bitof(i));
	bm.setof(2);
	bm.setof(12);
	for0(i, 20) printf("[%d] %d\n", i, bm[i]);
	bm.~Bitmap();
	return malc_count;
}


