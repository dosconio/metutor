// 20240425
#define _STYLE_RUST
#include "cpp/unisym"
#include "c/mnode.h"
#include "c/dnode.h"
#include "c/ustring.h"
#include "iostream"
using namespace uni;

int my_compare(pureptr_t a, pureptr_t b) {
	return StrCompare((const char*)a, (const char*)b);
}

void my_free(pureptr_t c) {
	Letvar(cc, Dnode*, c);
	mfree(cc->addr);
	mfree(cc->type);
}

// 1. p->p
// 2. str->int*
// Alice(1) Licea(2) Iceal(3) Ceali(4) Ealic(5)

static void body() {
	Mchain mc1;
	mc1.Map((pureptr_t)0, (pureptr_t)1);
	mc1.Map((pureptr_t)1, (pureptr_t)2);
	mc1.Map((pureptr_t)1, (pureptr_t)3);
	std::cout << "mc1 has "<< mc1.refChain().Count() << " nodes." << std::endl;
	for0(i, mc1.refChain().Count())
		std::cout << "[" << i << "] is "<< mc1[(pureptr_t)i]->lens << std::endl;
	mc1.unMap((pureptr_t)0);
	std::cout << "mc1 has " << mc1.refChain().Count() << " nodes." << std::endl;
	for0(i, mc1.refChain().Count())
	std::cout << "[1] is " << mc1[(pureptr_t)1]->lens << std::endl;

	Mchain mc2;
	mc2.func_comp = my_compare;
	mc2.refChain().func_free = my_free;
	mc2.Map((pureptr_t)StrHeap("Alice"), (pureptr_t)new int(1));
	mc2.Map((pureptr_t)StrHeap("Licea"), (pureptr_t)new int(2));
	mc2.Map((pureptr_t)StrHeap("Iceal"), (pureptr_t)new int(3));
	mc2.Map((pureptr_t)StrHeap("Ceali"), (pureptr_t)new int(4));
	mc2.Map((pureptr_t)StrHeap("Ealic"), (pureptr_t)new int(4));
	mc2.Map((pureptr_t)StrHeap("Ealic"), (pureptr_t)new int(5));
	std::cout << "mc2 has " << mc2.refChain().Count() << " nodes." << std::endl;
	fori(i, mc2.refChain()) {
		forhead(i, Dnode);
		std::cout << "\t" << i.addr << " with " << *(int*)i.type << std::endl;
	}
}

auto main() -> int {
	body();
	return malc_count;
}
