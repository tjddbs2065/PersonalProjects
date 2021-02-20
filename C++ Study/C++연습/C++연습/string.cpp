#include <iostream>
#include <string.h>

class MyString {
	char* string_content; // 실제 문자에 해당하는 내용만 보관
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
MyString::MyString(const MyString& str) { // 복사 생성자
	string_length = str.string_length;
	memory_capacity = string_length;
	string_content = new char[string_length];

	for (int i = 0; i != string_length; i++) string_content[i] = str.string_content[i];
}
MyString::~MyString() { delete[] string_content; }

int MyString::length() const { return string_length; }; // 문자열 길이 변수를 바꾸지 못하게 상수함수로 구현! // 길이는 문자열 조작시에 값을 정확하게 조정해야 한다!
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
void MyString::reserve(int size) { // size에 맞게 메모리 확장(예약)
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
		std::cout << "범위를 다시 지정해 주세요." << std::endl;
		return *this;
	}
	//새로 메모리를 할당(합친 문자열이 기존 메모리 크기보다 크다)
	if (string_length + str.string_length > memory_capacity) {
		//잦은 메모리 할당/해제를 막을 수 있다. 많은 메모리를 낭비하지 않을 수 있다.
		//기존 메모리의 2배 크기보다 합친문자열의 크기가 작으면 메모리 크기를 2배로 설정
		// => 너무 작을 경우 적당한 크기의 메모리를 할당한다.
		if (memory_capacity * 2 > string_length + str.string_length)
			memory_capacity *= 2;
		//합친 문자열의 크기가 기존 메모리 크기보다 크면 합친 문자열의 크기에 맞춰서 할당
		// => 너무 클 경우 딱 맞는 크기의 메모리 할당
		else
			memory_capacity = string_length + str.string_length;


		char* prev_string_content = string_content;//원본데이터
		string_content = new char[memory_capacity];//메모리 새로 할당

		//입력된 위치 까지 이동&복사
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

	//기존 메모리를 그대로 사용
	//효율적으로 insert를 하기 위해, 밀리는 부분을 먼저 뒤로 민다.
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