#include <cpp/unisym>
#include <cpp/nodes>
#include <c/consio.h>

using namespace uni;

int main() {
	// Check Heap String
	// strA
	// strB


	String strA = "this is strA";
	String strB;
	if (!strB.getCharCount()) ploginfo("strB is an empty string.");
	Console.OutFormat("strA is \'%s\', length %[u]\n", strA.reference(), strA.getCharCount());
	strB = strA.reference();
	Console.OutFormat("strB is \'%s\', length %[u]\n", strB.reference(), strB.getCharCount());
	strB[4] = 0; strB.Refresh();
	Console.OutFormat("strB is \'%s\', length %[u]. Last Char is %c.\n", strB.reference(), strB.getCharCount(), strB[-1]);
	if (strB.getCharCount()) ploginfo("strB is not an empty string.");

	//Console.OutFormat("%d", Console.CountFormat("1 %d 2", 9));

	String strC(String::Charset::Memory);
	Console.OutFormat("strC> CNT%d LIM%d\n", strC.getCharCount(), strC.getLimit());
	strC << 'A';
	strC << 'B';
	strC << '_';
	strC << '\0';
	strC << 'E';
	if (strC.inn() != 'E') return -1;
	if (strC.inn() != 0) return -1;
	Console.OutFormat("strC> CNT%d LIM%d\n", strC.getCharCount(), strC.getLimit());
	if (strC.inn() != '_') return -1;
	if (strC.inn() != 'B') return -1;
	strC << 'C';
	strC << 'D';
	strC.inn_direction = true;
	if (strC.inn() != 'A') return -1;
	if (strC.inn() != 'C') return -1;
	if (strC.inn() != 'D') return -1;
	if (strC.inn() != -1) return -1;
	//{TODO} make XstreamTrait in String or Strstr

	return 0;
}

/*
[INFOR]strB is an empty string.
strA is 'this is strA', length 12
strB is 'this is strA', length 12
strB is 'this', length 4. Last Char is s.
[INFOR]strB is not an empty string.
strC> CNT0 LIM1
strC> CNT5 LIM10
*/
