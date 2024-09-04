// 20240425
#define _STYLE_RUST
#include "cpp/unisym"
#include "c/nnode.h"
#include "c/ustring.h"
#include "iostream"
using namespace uni;

static void body() {
	NnodeChain c;
	// c.func_free = DnodeFreeSimple;
	c.Append("Alice's");
	c.Append("Adventure");
	c.Append("in");
	c.Append("Wonderland");

	Dnode a;
	c.Append(a);
	printf("[assert] %" PRIxSTD "==%" PRIxSTD "\n", c.Youngest()->offs, &a);
	c.Remove(c.Youngest());

	Nnode* crt = c.Root();
	while (crt) {
		printf(": %s\n", crt->addr);
		crt = crt->next;
	}

}

auto main() -> int {
	body();
	return malc_count;
}
