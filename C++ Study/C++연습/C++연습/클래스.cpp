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
		std::cout << "이 동물의 food   : " << food << std::endl;
		std::cout << "이 동물의 weight : " << weight << std::endl;
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

	//생성자(객체 생성 시 자동으로 호출되는 함수-객체 초기화 역할 담당): 리턴값x, 클래스 이름을 함수 이름에 사용
	/*Date(int year, int month, int day) {
		std::cout << "인자3 개인 생성자 호출" << std::endl;
		year_ = year;
		month_ = month;
		day_ = day;
	}*/

	//디폴트 생성자(인자를 가지지 않는 생성자): 클래스에 생성자를 정의하지 않았을 경우 컴파일러가 자동으로 추가, 물론 컴파일러가 추가하면 아무 기능도 하지 않는다.
	//디폴트 생성자를 사용자가 직접 정의할 수도 있다. => 인자값이 없는 생성자
	//Date() {
	//	std::cout << "기본 생성자 호출" << std::endl;
	//	year_ = 2020;
	//	month_ = 2;
	//	day_ = 7;
	//}

	//명시적으로 디폴트 생성자 사용 - 개발자의 의도를 알기 위해(생성자 정의를 깜빡한건지, 디폴트 생성자를 사용하려고 인지)
	Date() = default;

	//생성자도 마찬가지로 함수이기 때문에 오버로딩이 가능하다.
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
	std::cout << "오늘은 " << year_ << "년 " << month_ << "월 " << day_ << "일 입니다." << std::endl;
}



int main() {
	//Animal animal;
	//animal.set_animal(100, 50);
	//animal.increase_food(30);
	//animal.view_stat();

	//생성자 적용 전 클래스 객체
	//Date day;
	//day.SetDate(2011, 3, 1);
	//day.ShowDate();

	//day.AddDay(30);
	//day.ShowDate();

	//day.AddDay(2000);
	//day.ShowDate();

	//day.SetDate(2012, 1, 31); // 윤년
	//day.AddDay(29);
	//day.ShowDate();

	//day.SetDate(2012, 8, 4);
	//day.AddDay(2500);
	//day.ShowDate();

	//생성자 적용 후 클래스 객체
	//Date day = Date(2011, 3, 1);
	//Date day(2011, 3, 1);
	//day.ShowDate();

	//day.AddYear(10);
	//day.ShowDate();

	//디폴트 생성자 적용 후 클래스 객체
	Date day = Date();
	Date day2;
	//Date day3(); // 리턴값이 Date인 함수 정의가 되어 버린다.
	day.ShowDate();
	day2.ShowDate();
	

	return 0;
}