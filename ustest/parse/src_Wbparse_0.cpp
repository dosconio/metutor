#include <iostream>
using namespace std;

int globalArry[2][3][3] = {
 1, {2}, 3,
};

int main() {
	for (int test_out = 0; test_out < 3; test_out++) {
		for (int test_inn = 0; test_inn < 3; test_inn++) {
			cout << globalArry[test_out][test_out][test_inn] << " ";
			//cout << globalArry[test_out][test_inn][test_inn] << " ";
		}
		0;
	}
	return 0;
}
;

void f(){}
