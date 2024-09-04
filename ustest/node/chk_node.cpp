// 20240425
#define _STYLE_RUST
#include "cpp/unisym"
#include "c/node.h"
#include "c/ustring.h"
#include "iostream"
using namespace uni;

int mysort(pureptr_t a, pureptr_t b) {
	Letvar(na, Node*, a);
	Letvar(nb, Node*, b);
	return StrLength(na->addr) - StrLength(nb->addr);
}

static void body() {
	Chain c;
	c.func_free = NodeHeapFreeSimple;
	c << "Alice's" << "Adventure" << "in" << "Wonderland";
	std::cout << c.Count() << " items" << std::endl;
	std::cout << "The last one is " << c[-1]->addr << std::endl;
	fori(i, c) {
		forhead(i, Node);
		std::cout << "\t" << i.addr << std::endl;
	}

	puts("\nSorting by length:");
	c.Sorted(mysort);// also enable auto sort
	c << "Ciallo";
	c.Remove(c[0]);// remove "in"
	fori(i, c) {
		forhead(i, Node);
		std::cout << "\t" << i.addr << std::endl;
	}

	std::cout << (c[0]->ReheapString("Ciall~"))->addr << std::endl;
}

auto main() -> int {
	body();
	return malc_count;
}
