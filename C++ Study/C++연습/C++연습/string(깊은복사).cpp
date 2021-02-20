#define _CRT_SECURE_NO_WARNINGS
#include <iostream>

class string {
	char* str;
	int len;
public:
	string(char c, int n);	// 문자 c가 n개 있는 문자열로 정의
	string(const char* s);
	string(const string& s);
	~string();

	void add_string(const string& s);
	void copy_string(const string& s);
	int strlen();
	void print();
};

string::string(char c, int n) {
	len = n;
	str = new char[n + 1];
	int i = 0;
	for (i = 0; i < n; i++) {
		str[i] = c;
	}
	str[i] = NULL;
}
string::string(const char* s) {
	len = 0;
	while (true) {
		if (*s == NULL) {
			break;
		}
		len++;
		s++;
	}
	s -= len;
	str = new char[len + 1];
	strcpy(str, s);
}
string::string(const string& s) {
	len = s.len;
	str = new char[s.len + 1];
	strcpy(str, s.str);
}
string::~string() {
	delete[] str;
}
void string::add_string(const string& s) {
	len = len + s.len;
	char* tmp = new char[len + 1];
	strcpy(tmp, str);
	strcat(tmp, s.str);

	delete[] str;
	str = new char[len + 1];
	strcpy(str, tmp);
	delete[] tmp;
}
void string::copy_string(const string& s) {
	delete[] str;
	str = new char[s.len + 1];
	len = s.len;
	strcpy(str, s.str);
}
int string::strlen() {
	return len;
}
void string::print() {
	std::cout << str << std::endl;
}


int main() {
	string s1('s', 4);
	string s2("hello world");
	string s3 = s2;

	s1.print();
	s2.print();
	s3.print();
	s3.add_string(s1);
	s3.print();
}