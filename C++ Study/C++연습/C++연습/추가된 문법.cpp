#include <iostream>
//using namespace std; // iostream 헤더의 std 네임 스페이스 사용
//using std::cout;

namespace start {
	void activate() {
		std::cout << "from function(start namespace)" << std::endl;
	}
}

void print(int x) { std::cout << "int : " << x << std::endl; }
//void print(char x) { std::cout << "char : " << x << std::endl; }
void print(double x) { std::cout << "double : " << x << std::endl; }

int main(void) {
	//네임스페이스 관련
	std::cout << "hello world!" << std::endl;
	start::activate(); // start 네임 스페이스의 함수 호출//using을 사용하지 않을 경우 앞에 네임 스페이스를 붙인다.

	//참조 관련
	//int a = 3;
	//int& ref_a = a; // 변수 a의 또 다른 이름//타입 뒤에 &을 붙인다.
	//std::cout << a << std::endl;
	//std::cout << ref_a << std::endl;

	//메모리 관리
	//int* pNum = new int; // int 크기 메모리 할당
	//*pNum = 123;
	//std::cout << *pNum << std::endl;
	//delete pNum; // pNum에 할당된 메모리 해제 // new를 통해 할당한 메모리만 해제할 수 있다. // new로 할당하지 않은 메모리를 delete 할 경우 heap영역이 아닌 메모리를 해제하려 한다는 오류가 뜬다.

	//배열 메모리 관리
	/*int arr_size;
	std::cout << "생성할 배열의 크기를 입력하세요: ";
	std::cin >> arr_size;
	std::cout << std::endl;
	int* mem_arr = new int[arr_size];
	for (int i = 0; i < arr_size; i++) {
		std::cout << i+1 << " 번째 값을 입력하세요: ";
		std::cin >> mem_arr[i];
	}
	for (int i = 0; i < arr_size; i++) {
		std::cout << mem_arr[i] << std::endl;
	}
	delete[] mem_arr;*/


	//함수 오버로딩(같은 이름의 함수를 인자로 구분해 사용
	//1. 타입이 정확히 일치하는 함수를 찾는다.
	//2. 정확히 일치하는 타입이 없으면 형변환을 통해 일치하는 함수를 찾는다.(char, short -> int, float -> double, enum -> int)
	//3. 변환해도 일치하지 않는다면, 좀 더 포괄적인 형변환을 통해 일치하는 함수를 찾는다.(float -> int, enum -> double, pointer -> void pointer)
	//4. 유저 정의된 타입 변환으로 일치하는 것을 찾는다.
	int a = 1;
	char b = 'c';
	double c = 3.2f;

	print(a);
	print(b);
	print(c);

	return 0;
}

