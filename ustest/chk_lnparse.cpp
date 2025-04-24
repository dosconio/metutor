#include <cpp/unisym>
#include <cpp/nodes>
#include <cpp/parse.hpp>
#include <c/consio.h>

using namespace uni;

const char* tab_tokentype[] =
{
	"NON", "ANY",
	"SYM", "SPA", "NUM", "IDN", "XXX",
	"COM", "DIR", "STR",  "EOF", "FUN",
	"EOF","complex","position","wides",
	"u8str", // ...
};

char glbbuf[] =
#if 0 && 0
"123456789012345678901234567890\n"
"%include 123\n"
"  %include 123\n"
"AB \"C\\\'\\x20C\\0A\" + 6\n"
"%include -123\n"
"#comment-line\n"
"-123.5e+5-0.5 % 0.x # this is comment\n"
"  123 \n_XYZ_.0\n\n";
#endif

"/* test *//**//*1*/" "\n"
"#include \\" "\n"
"\t<stdio.h> // at other line, but still a directive (directive but comment)" "\n"
"int i\\\nj = 1\\" "\n"
"\\" "\n"
"2;// 12， consider this token at the line 'int i = ...'" "\n"
"1=\\" "\n"
"=2; 2\\3;" "\n"
"// abc\\" "\n"
"still the comment. And is a token with \\n" "\n";


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

#if 0 && 0
		lnparse.method_directive = '%';
		lnparse.method_string_single_quote = true;
		lnparse.method_string_double_quote = true;
		// lnparse.method_treatiden_underscore = false;
		// lnparse.method_omit_spaces = false;
#endif
		lnparse.method_directive = '#';
		lnparse.method_string_double_quote = true;
		lnparse.handler_comment = LineParse_Comment_C_Cpp;

		Console.OutFormat("%s\n", glbbuf);

		Tnode* tn = (Tnode*)lnparse.Parse(dc).Root();
		while (tn = tn->next) {
			Console.OutFormat(">%s(%u %u) |%s|\n", tab_tokentype[tn->type],
				tn->row, tn->col, tn->addr);
		}




	}
	return malc_count;
}