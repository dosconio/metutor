#include <cpp/unisym>
#include <cpp/nodes>
#include <cpp/parse.hpp>
#include <c/consio.h>
#include <../magic/magice.hpp>

using namespace uni;

const char* tab_tokentype[] =
{
	"NON", "ANY",
	"SYM", "SPA", "NUM", "IDN", "XXX",
	"COM", "DIR", "STR",  "EOF", "FUN",
	"STA", "BLK",
	"EOF","complex","position","wides",
	"u8str", // ...
};

class MyInn : public IstreamTrait {
	FILE* fp;
public:
	MyInn() : fp(fopen("E:/PROJ/metutor/ustest/parse/src_Wbparse_0_0.cpp", "r")) {}
	~MyInn() { fclose(fp); fp = nullptr; }
	virtual int inn() {
		return fgetc(fp);
	}

};

void MyWalk(Nnode& nod, stduint nest) {
	for0(i, nest) printf(i + 1 == nest ? "->" : "--");
	Console.OutFormat("%s(%u %u) |%s|\n",
		tab_tokentype[nod.type],
		nod.GetMagnoField().row,
		nod.GetMagnoField().col,
		nod.addr);
}
uni::OstreamTrait* dst;
int main() {
	{
		MyInn myInn;
		LinearParser lnparse(myInn);
		Dchain dc;

		lnparse.GHT = false;
		lnparse.method_directive = '#';
		lnparse.method_string_double_quote = true;
		lnparse.handler_comment = LineParse_Comment_C_Cpp;
		lnparse.method_omit_comment = true;

		Tnode* tn = lnparse.Parse(dc).Root()->GetTnode();

		// Process Directive
		// ...

		NodeChain operators(NodeHeapFreeSimple);
		Operators::List(operators);

		NestedParser neparse(dc, &operators);

		neparse.ParseParen(neparse.GetNetwork()->Root());
		neparse.ParseStatements_CPL();
		neparse.ParseOperator(neparse.GetNetwork()->Root());

		neparse.GetNetwork()->Traversal(MyWalk);


	}
	return malc_count;
}