#include <cpp/unisym>
#include <cpp/nodes>
#include <cpp/parse.hpp>
#include <c/consio.h>

using namespace uni;

char glbbuf[] = "AB C +\n\n  123 \nXYZ.0";
char* glbbpt = glbbuf;

class MyInn : public IstreamTrait {
public:
	virtual int inn() {
		return *glbbpt ? *glbbpt++ : -1;
	}

};

int main() {
	{
		MyInn myInn;
		LinearParser lnparse(myInn);
		Dchain dc;

		lnparse.Parse(dc);

		Console.OutFormat("%s\n", glbbuf);

		Tnode* tn = (Tnode*)dc.Root();
		while (tn = tn->next) {
			Console.OutFormat(">%d(%u %u) \'%s\'\n", tn->type, tn->row, tn->col, tn->addr);
		}




	}
	return malc_count;
}