#define _CRT_SECURE_NO_WARNINGS
#include <string.h>
#include <iostream>

class Marine {
	int hp;					// 마린 체력
	int coord_x, coord_y;	// 마린 위치
	int damage;				// 공격력
	bool is_dead;
	char* name;
public:
	Marine();				// 기본 생성자
	Marine(int x, int y);	// x, y 좌표에 마린 생성
	Marine(int x, int y, const char* marine_name);	// x, y 좌표에 마린 생성 + 이름 설정
	~Marine();

	int attack();						// 데미지를 리턴
	void be_attacked(int damage_earn);	// 입는 데미지
	void move(int x, int y);			// 새로운 위치

	void show_status();		// 상태를 보여준다.
};

Marine::Marine() {
	hp = 50;
	coord_x = coord_y = 0;
	damage = 5;
	is_dead = false;
	name = NULL;
}
Marine::Marine(int x, int y) {
	hp = 50;
	coord_x = x;
	coord_y = y;
	damage = 5;
	is_dead = false;
	name = NULL;
}
Marine::Marine(int x, int y, const char* marine_name) {
	name = new char[strlen(marine_name) + 1];	//마지막 null을 위해 +1을 해준다.
	strcpy(name, marine_name);

	hp = 50;
	coord_x = x;
	coord_y = y;
	damage = 5;
	is_dead = false;
}
void Marine::move(int x, int y) {
	coord_x = x;
	coord_y = y;
}
int Marine::attack() { return damage; }
void Marine::be_attacked(int damage_earn) {
	hp -= damage_earn;
	if (hp <= 0) is_dead = true;
}
void Marine::show_status() {
	std::cout << "*** Marine : " << name  << " ***" << std::endl;
	std::cout << "Location : (" << coord_x << ", " << coord_y <<")" << std::endl;
	std::cout << "HP: " << hp << std::endl << std::endl;
}
Marine::~Marine() {
	std::cout << name << "의 소멸자 호출!" << std::endl;
	if (name != NULL) {
		delete[] name;
	}
}

class Photon_Cannon {
	int hp, shield;
	int coord_x, coord_y;
	int damage;
	char* name;
public:
	Photon_Cannon(int x, int y);
	Photon_Cannon(int x, int y, const char* cannon_name);
	Photon_Cannon(const Photon_Cannon& pc); 
	~Photon_Cannon();

	void show_status();
};

Photon_Cannon::Photon_Cannon(const Photon_Cannon& pc) { // 복사생성자
	std::cout << "복사 생성자 호출!" << std::endl;
	hp = pc.hp;
	shield = pc.shield;
	coord_x = pc.coord_x;
	coord_y = pc.coord_y;
	damage = pc.damage;

	name = new char[strlen(pc.name) + 1];
	strcpy(name, pc.name);
}
Photon_Cannon::Photon_Cannon(int x, int y) {
	std::cout << "생성자 호출!" << std::endl;
	hp = shield = 100;
	coord_x = x;
	coord_y = y;
	damage = 20;
	name = NULL;
}
Photon_Cannon::Photon_Cannon(int x, int y, const char* cannon_name) {
	hp = shield = 100;
	coord_x = x;
	coord_y = y;
	damage = 20;
	name = new char[strlen(cannon_name) + 1];
	strcpy(name, cannon_name);
}
Photon_Cannon::~Photon_Cannon() {
	if (name) delete[] name;
}
void Photon_Cannon::show_status() {
	std::cout << "Photon Cannon :: " << name << std::endl;
	std::cout << " Location : (" << coord_x << ", " << coord_y << ") " << std::endl;
	std::cout << "HP: " << hp << std::endl;
}



int main() {
	//Marine marine1 = Marine(2, 3);
	//Marine marine2(3, 5);
	//marine1.show_status();
	//marine2.show_status();
	//std::cout << "마린1이 마린2를 공격!" << std::endl;
	//marine2.be_attacked(marine1.attack());
	//marine1.show_status();
	//marine2.show_status();

	//Marine* marines[100];
	//marines[0] = new Marine(2, 3, "Marine2");
	//marines[1] = new Marine(1, 5, "Marine1");
	//marines[0]->show_status();
	//marines[1]->show_status();
	//std::cout << "마린1이 마린2를 공격!" << std::endl;
	//marines[0]->be_attacked(marines[1]->attack());
	//marines[0]->show_status();
	//marines[1]->show_status();

	//delete marines[0];
	//delete marines[1];

	//Photon_Cannon pc1(3, 3);
	//Photon_Cannon pc2(pc1);
	//Photon_Cannon pc3 = pc2; // = Photon_Caannon pc3(pc2)

	//pc1.show_status();
	//pc2.show_status();

	Photon_Cannon pc1(3, 3, "Cannon");
	Photon_Cannon pc2 = pc1; // 복사 생성자 호출

	pc1.show_status();
	pc2.show_status();
}





