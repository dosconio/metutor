#include "stdio.h"
#include "strpage.h"
#include "strbuff.h"

void main()
{
	Strbuff* sb;
	char buf[16] = {0};
	sb = StrbuffNew(sizeof(int));
	StrbuffSend(sb, "nihao", 5);
	StrbuffPop(sb, buf, 2);
	printf("%s\n", buf);
	StrbuffPop(sb, buf, 3);
	printf("%s\n", buf);

	StrbuffFree(sb);
	return malc_count;
}