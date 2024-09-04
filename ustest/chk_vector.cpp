// 20240425
#define _STYLE_RUST
#include "cpp/vector"
#include "cpp/string"
#include "iostream"
using namespace uni;

cmpfof(cmp_str_by_txt) {
	cmpf_inhead(String, pa, pb);
	return StrCompare(pa->reflect(), pb->reflect());
}

cmpfof(cmp_str_by_len) {
	cmpf_inhead(String, pa, pb);
	return pa->length() - pb->length();
}

auto body() {
	auto v1 = Vector<i32>::From(13, 2);
	Vec<i32> v2 = v1;
	auto v3 = Vec<String>::From("nihao", 2);
	auto v4 = Vec<pureptr_t>::From(nullptr, 1);

	v3[0] = "hello,";
	v3 << "world";
	v3.Remove(v3.Locate("nihao"));
	v3[-1] += "!";

	for0(i, v2.Length()) {
		std::cout << v2[i] << " ";
	} puts("");

	fori(i, v3) {
		forhead(i, String);
		std::cout << i << " ";
	} puts("");

	Vec<String> v5;
	// Test Sort
	v5 << "Alice's"
		<< "Adventure"
		<< "in"
		<< "Wonderland";
	/*
		v5.Compare_f = cmp_str_by_len;
		Sort(v5);
		fori(i, v5) {
			forhead(i, String);
			std::cout << i << " ";
		} puts("");
	*/
	fori(i, v5.Sorted(cmp_str_by_len)) {
		forhead(i, String);
		std::cout << i << " ";
	} puts("");

	// Test Search
	/*
		v5.Compare_f = cmp_str_by_txt;
		String tmp = "Adventure";
		std::cout << "search \'Adventure\':" << Search(v5, (pureptr_t)&tmp) << std::endl;
		tmp = "Adventura";
		std::cout << "search \'Adventura\':" << Search(v5, (pureptr_t)&tmp) << std::endl;
	*/
	std::cout << "search \'Adventure\':" << v5.Search("Adventure", cmp_str_by_txt) << std::endl;
	std::cout << "search \'Adventura\':" << v5.Search("Adventura", cmp_str_by_txt) << std::endl;

	//
	return 0;
}

auto main() -> int {
	body();
	return malc_count;
}
