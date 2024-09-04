// 20240425
#define _STYLE_RUST
#include "cpp/unisym"
#include "c/node.h"
#include "c/dnode.h"
#include "c/ustring.h"
#include "iostream"
using namespace uni;

int mysort(pureptr_t a, pureptr_t b) {
	Letvar(na, Dnode*, a);
	Letvar(nb, Dnode*, b);
	return StrLength(na->addr) - StrLength(nb->addr);
}

static void body() {
	DnodeChain c;
	c.func_free = DnodeFreeSimple;
	c << "Alice's" << "Adventure" << "in" << "Wonderland";
	std::cout << c.Count() << " items" << std::endl;
	fori(i, c) {
		forhead(i, Dnode);
		std::cout << i.addr << std::endl;
	}

	puts("\nSorting by length:");
	c.Sorted(mysort);// also enable auto sort
	c << "Ciallo";
	fori(i, c) {
		forhead(i, Dnode);
		std::cout << i.addr << std::endl;
	}

	//{TODO} test Dnode-special functions
}

auto main() -> int {
	body();
	return malc_count;
}
