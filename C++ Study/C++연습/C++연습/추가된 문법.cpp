#include <iostream>
//using namespace std; // iostream ����� std ���� �����̽� ���
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
	//���ӽ����̽� ����
	std::cout << "hello world!" << std::endl;
	start::activate(); // start ���� �����̽��� �Լ� ȣ��//using�� ������� ���� ��� �տ� ���� �����̽��� ���δ�.

	//���� ����
	//int a = 3;
	//int& ref_a = a; // ���� a�� �� �ٸ� �̸�//Ÿ�� �ڿ� &�� ���δ�.
	//std::cout << a << std::endl;
	//std::cout << ref_a << std::endl;

	//�޸� ����
	//int* pNum = new int; // int ũ�� �޸� �Ҵ�
	//*pNum = 123;
	//std::cout << *pNum << std::endl;
	//delete pNum; // pNum�� �Ҵ�� �޸� ���� // new�� ���� �Ҵ��� �޸𸮸� ������ �� �ִ�. // new�� �Ҵ����� ���� �޸𸮸� delete �� ��� heap������ �ƴ� �޸𸮸� �����Ϸ� �Ѵٴ� ������ ���.

	//�迭 �޸� ����
	/*int arr_size;
	std::cout << "������ �迭�� ũ�⸦ �Է��ϼ���: ";
	std::cin >> arr_size;
	std::cout << std::endl;
	int* mem_arr = new int[arr_size];
	for (int i = 0; i < arr_size; i++) {
		std::cout << i+1 << " ��° ���� �Է��ϼ���: ";
		std::cin >> mem_arr[i];
	}
	for (int i = 0; i < arr_size; i++) {
		std::cout << mem_arr[i] << std::endl;
	}
	delete[] mem_arr;*/


	//�Լ� �����ε�(���� �̸��� �Լ��� ���ڷ� ������ ���
	//1. Ÿ���� ��Ȯ�� ��ġ�ϴ� �Լ��� ã�´�.
	//2. ��Ȯ�� ��ġ�ϴ� Ÿ���� ������ ����ȯ�� ���� ��ġ�ϴ� �Լ��� ã�´�.(char, short -> int, float -> double, enum -> int)
	//3. ��ȯ�ص� ��ġ���� �ʴ´ٸ�, �� �� �������� ����ȯ�� ���� ��ġ�ϴ� �Լ��� ã�´�.(float -> int, enum -> double, pointer -> void pointer)
	//4. ���� ���ǵ� Ÿ�� ��ȯ���� ��ġ�ϴ� ���� ã�´�.
	int a = 1;
	char b = 'c';
	double c = 3.2f;

	print(a);
	print(b);
	print(c);

	return 0;
}

