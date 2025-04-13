#include <cpp/unisym>
#include <cpp/nodes>
#include <cpp/parse.hpp>
#include <c/consio.h>

using namespace uni;

char glbbuf[] =

"123456789012345678901234567890\n"
"%include 123\n"
"AB \"C\\\'\\x20C\\0A\" + 6\n"
"%include -123\n"
"#comment-line\n"
"-123.5e+5-0.5 0.x # this is comment\n"
"  123 \n_XYZ_.0\n\n";
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

		lnparse.method_directive = '%';
		lnparse.method_string_single_quote = true;
		lnparse.method_string_double_quote = true;
		// lnparse.method_treatiden_underscore = false;
		// lnparse.method_omit_spaces = false;

		Console.OutFormat("%s\n", glbbuf);

		Tnode* tn = (Tnode*)lnparse.Parse(dc).Root();
		while (tn = tn->next) {
			Console.OutFormat(">%d(%u %u) |%s|\n", tn->type, tn->row, tn->col, tn->addr);
		}




	}
	return malc_count;
}