#include "stdio.h"
#include "strpage.h"

void main()
{
	Strpage* sp = StrpageNew();
	for0(i, 4) sp = StrpageSet(sp, i + 4, i + 4);
	for0(i, 10) printf("%02llu: %llu\n", i, StrpageGet(sp, i));
	for0(i, 4) sp = StrpageSet(sp, i + 10002, i + 10002);
	for0(i, 8) printf("%02llu: %llu\n", i + 10000, StrpageGet(sp, i + 10000));

	StrpageFree(sp);
	return malc_count;
}