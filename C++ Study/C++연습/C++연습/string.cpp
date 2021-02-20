#include <iostream>
#include <string.h>

class MyString {
	char* string_content; // ���� ���ڿ� �ش��ϴ� ���븸 ����
	int string_length;
	int memory_capacity;
public:
	MyString(char c);
	MyString(const char* str);
	MyString(const MyString& str);
	~MyString();

	int length() const;
	int capacity() const;
	void reserve(int size);
	char at(int idx);

	void print() const;
	void println() const;

	MyString& assign(const MyString& str);
	MyString& assign(const char* str);

	MyString& insert(int loc, const MyString& str);
	MyString& insert(int loc, const char* str);
	MyString& insert(int loc, char c);
};
MyString::MyString(char c) {
	string_content = new char[1];
	string_content[0] = c;
	memory_capacity = 1;
	string_length = 1;
}

MyString::MyString(const char* str) {
	string_length = strlen(str);
	memory_capacity = string_length; 
	string_content = new char[string_length];

	for (int i = 0; i != string_length; i++) string_content[i] = str[i];
}
MyString::MyString(const MyString& str) { // ���� ������
	string_length = str.string_length;
	memory_capacity = string_length;
	string_content = new char[string_length];

	for (int i = 0; i != string_length; i++) string_content[i] = str.string_content[i];
}
MyString::~MyString() { delete[] string_content; }

int MyString::length() const { return string_length; }; // ���ڿ� ���� ������ �ٲ��� ���ϰ� ����Լ��� ����! // ���̴� ���ڿ� ���۽ÿ� ���� ��Ȯ�ϰ� �����ؾ� �Ѵ�!
void MyString::print() const { 
	for (int i = 0; i != string_length; i++) {
		std::cout << string_content[i];
	}
}
void MyString::println() const {
	for (int i = 0; i != string_length; i++) {
		std::cout << string_content[i];
	}
	std::cout << std::endl;
}
MyString& MyString::assign(const MyString& str) {
	if (str.string_length > memory_capacity) {
		delete[] string_content;
		string_content = new char[str.string_length];
		memory_capacity = str.string_length;
	}
	for (int i = 0; i != string_length; i++) { string_content[i] = str.string_content[i]; }
	
	string_length = str.string_length;
	return *this;
}
MyString& MyString::assign(const char* str) {
	int str_length = strlen(str);
	if (str_length > memory_capacity) {
		delete[] string_content;
		string_content = new char[str_length];
		memory_capacity = str_length;
	}
	for (int i = 0; i < str_length; i++) {
		string_content[i] = str[i];
	}
	string_length = str_length;
	return *this;
}

int MyString::capacity() const { return memory_capacity; }
void MyString::reserve(int size) { // size�� �°� �޸� Ȯ��(����)
	if (size > memory_capacity) {
		char* prev_string_content = string_content;

		string_content = new char[size];
		memory_capacity = size;

		for (int i = 0; i != string_length; i++) 
			string_content[i] = prev_string_content[i];
		
		delete[] prev_string_content;
	}
	else
		std::cout << "reserve failed! (entered size is smaller than string size)" << std::endl;
}
char MyString::at(int idx) {
	if (idx >= string_length || idx < 0) {
		return NULL;
	}
	else
		return string_content[idx];
}

MyString& MyString::insert(int loc, const MyString& str) {
	if (loc < 0 || loc > string_length) {
		std::cout << "������ �ٽ� ������ �ּ���." << std::endl;
		return *this;
	}
	//���� �޸𸮸� �Ҵ�(��ģ ���ڿ��� ���� �޸� ũ�⺸�� ũ��)
	if (string_length + str.string_length > memory_capacity) {
		//���� �޸� �Ҵ�/������ ���� �� �ִ�. ���� �޸𸮸� �������� ���� �� �ִ�.
		//���� �޸��� 2�� ũ�⺸�� ��ģ���ڿ��� ũ�Ⱑ ������ �޸� ũ�⸦ 2��� ����
		// => �ʹ� ���� ��� ������ ũ���� �޸𸮸� �Ҵ��Ѵ�.
		if (memory_capacity * 2 > string_length + str.string_length)
			memory_capacity *= 2;
		//��ģ ���ڿ��� ũ�Ⱑ ���� �޸� ũ�⺸�� ũ�� ��ģ ���ڿ��� ũ�⿡ ���缭 �Ҵ�
		// => �ʹ� Ŭ ��� �� �´� ũ���� �޸� �Ҵ�
		else
			memory_capacity = string_length + str.string_length;


		char* prev_string_content = string_content;//����������
		string_content = new char[memory_capacity];//�޸� ���� �Ҵ�

		//�Էµ� ��ġ ���� �̵�&����
		int i;
		for (i = 0; i < loc; i++)
			string_content[i] = prev_string_content[i];
		for (int j = 0; j!= str.string_length ; j++)
			string_content[i+j] = str.string_content[j];

		for (; i < string_length; i++)
			string_content[str.string_length + i] = prev_string_content[i];
		
		delete[] prev_string_content;

		string_length = string_length + str.string_length;
		return *this;
	}

	//���� �޸𸮸� �״�� ���
	//ȿ�������� insert�� �ϱ� ����, �и��� �κ��� ���� �ڷ� �δ�.
	for (int i = string_length - 1; i >= loc; i--)
		string_content[i + str.string_length] = string_content[i];
	for (int i = 0; i < str.string_length; i++)
		string_content[loc + i] = str.string_content[i];

	string_length = string_length + str.string_length;
	return *this;
}
MyString& MyString::insert(int loc, const char* str) {
	MyString tmp(str);
	return insert(loc, tmp);
}
MyString& MyString::insert(int loc, char c) {
	MyString tmp(c);
	return insert(loc, tmp);
}
//pg 169

int main() {

	MyString str1("very long string");
	MyString str2("<some string inserted between>");
	str1.reserve(30);
	
	std::cout << "Capacity: " << str1.capacity() << std::endl;
	std::cout << "String Length: " << str1.length() << std::endl;
	str1.println();
	
	str1.insert(5, str2);
	str1.println();

	std::cout << "Capacity: " << str1.capacity() << std::endl;
	std::cout << "String Length: " << str1.length() << std::endl;
	str1.println();


	return 0;
}