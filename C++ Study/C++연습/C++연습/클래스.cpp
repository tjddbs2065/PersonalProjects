#include <iostream>

class Animal {
private:
	int food;
	int weight;

public:
	void set_animal(int _food, int _weight) {
		food = _food;
		weight = _weight;
	}
	void increase_food(int inc) {
		food += inc;
		weight += (inc / 3);
	}
	void view_stat() {
		std::cout << "�� ������ food   : " << food << std::endl;
		std::cout << "�� ������ weight : " << weight << std::endl;
	}
};

class Date {
	int year_;
	int month_;
	int day_;
public:
	void SetDate(int year, int month, int day);
	void AddDay(int inc);
	void AddMonth(int inc);
	void AddYear(int inc);

	int GetCurrentMonthTotalDays(int year, int month);
	void ShowDate();

	//������(��ü ���� �� �ڵ����� ȣ��Ǵ� �Լ�-��ü �ʱ�ȭ ���� ���): ���ϰ�x, Ŭ���� �̸��� �Լ� �̸��� ���
	/*Date(int year, int month, int day) {
		std::cout << "����3 ���� ������ ȣ��" << std::endl;
		year_ = year;
		month_ = month;
		day_ = day;
	}*/

	//����Ʈ ������(���ڸ� ������ �ʴ� ������): Ŭ������ �����ڸ� �������� �ʾ��� ��� �����Ϸ��� �ڵ����� �߰�, ���� �����Ϸ��� �߰��ϸ� �ƹ� ��ɵ� ���� �ʴ´�.
	//����Ʈ �����ڸ� ����ڰ� ���� ������ ���� �ִ�. => ���ڰ��� ���� ������
	//Date() {
	//	std::cout << "�⺻ ������ ȣ��" << std::endl;
	//	year_ = 2020;
	//	month_ = 2;
	//	day_ = 7;
	//}

	//��������� ����Ʈ ������ ��� - �������� �ǵ��� �˱� ����(������ ���Ǹ� �����Ѱ���, ����Ʈ �����ڸ� ����Ϸ��� ����)
	Date() = default;

	//�����ڵ� ���������� �Լ��̱� ������ �����ε��� �����ϴ�.
};

void Date::SetDate(int year, int month, int day) {
	year_ = year;
	month_ = month;
	day_ = day;
}
int Date::GetCurrentMonthTotalDays(int year, int month) {
	static int month_day[12] = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
	if (month != 2) {
		return month_day[month - 1];
	}
	else if (year % 4 == 0 && year % 100 != 0) {
		return 29;
	}
	else {
		return 28;
	}
}

void Date::AddDay(int inc) {
	while (true) {
		int current_month_total_days = GetCurrentMonthTotalDays(year_, month_);

		if (day_ + inc <= current_month_total_days) {
			day_ += inc;
			return;
		}
		else {
			inc -= (current_month_total_days - day_ + 1);
			day_ = 1;
			AddMonth(1);
		}
	}
}

void Date::AddMonth(int inc) {
	AddYear((inc + month_ - 1) / 12);
	month_ = month_ + inc % 12;
	month_ = (month_ == 12 ? 12 : month_ % 12);
}

void Date::AddYear(int inc) { year_ += inc; }
void Date::ShowDate() {
	std::cout << "������ " << year_ << "�� " << month_ << "�� " << day_ << "�� �Դϴ�." << std::endl;
}



int main() {
	//Animal animal;
	//animal.set_animal(100, 50);
	//animal.increase_food(30);
	//animal.view_stat();

	//������ ���� �� Ŭ���� ��ü
	//Date day;
	//day.SetDate(2011, 3, 1);
	//day.ShowDate();

	//day.AddDay(30);
	//day.ShowDate();

	//day.AddDay(2000);
	//day.ShowDate();

	//day.SetDate(2012, 1, 31); // ����
	//day.AddDay(29);
	//day.ShowDate();

	//day.SetDate(2012, 8, 4);
	//day.AddDay(2500);
	//day.ShowDate();

	//������ ���� �� Ŭ���� ��ü
	//Date day = Date(2011, 3, 1);
	//Date day(2011, 3, 1);
	//day.ShowDate();

	//day.AddYear(10);
	//day.ShowDate();

	//����Ʈ ������ ���� �� Ŭ���� ��ü
	Date day = Date();
	Date day2;
	//Date day3(); // ���ϰ��� Date�� �Լ� ���ǰ� �Ǿ� ������.
	day.ShowDate();
	day2.ShowDate();
	

	return 0;
}