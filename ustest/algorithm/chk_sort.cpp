#include <cpp/unisym>
#include <cpp/nodes>
#include <c/consio.h>

using namespace uni;

int tocomp_ft(pureptr_t a, pureptr_t b) {
	Letvar(aa, Dnode*, a);
	Letvar(bb, Dnode*, b);
	return _IMM(aa->offs) - _IMM(bb->offs);
}

int main() {
	Dchain a;
	a.Append((pureptr_t)4);
	a.Append((pureptr_t)7);
	a.Append((pureptr_t)1);
	a.Append((pureptr_t)9);
	a.Append((pureptr_t)6);
	a.Append((pureptr_t)0);
	a.Compare_f = tocomp_ft;
	//a.Sort(SortMode::Insertion);// AKA Sort(a, SortMode::Insertion) or  a.Sort();
	//a.Sort(SortMode::Selection);
	//a.Sort(SortMode::Bubble);
	//a.Sort(SortMode::Heap);
	//a.Sort(SortMode::Merge);
	a.Sort(SortMode::Quick);
	for0(i, a.Count()) {
		Console.OutFormat("%d: %[u]\n", i, (stduint)a[i]->addr);
	}
	return 0;
}

