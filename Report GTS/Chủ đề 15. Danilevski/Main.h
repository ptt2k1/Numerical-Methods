#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include <windows.h>
#include <D:\eqn.h> // giai phuong trinh da thuc bac n
#define True 1
#define False 0
#define inp "D:\\input.txt" // duong dan file doc
#define out "D:\\output.txt" // duong dan file ghi
#define FOR(i, m, n) for (int i = m; i < n; i++)
#define _ 100
#define NORMAL -2
#define INVERSE -1

/* Khai bao cac ham duoc su dung chu yeu */
int input(int *n, float a[_][_]);
void output(int n, float value[_], float vector[_][_]);
void print_m(int n, float m[_][_], char *s);
void init_e(int n, float e[_][_]);
void mmul(int n, float r[_][_], float p[_][_], float q[_][_]);
void mul(int n, float r[_], float p[_][_], float q[_]);
int check_a(int n, float a[_][_], int k, int *jj);
void find_m(int n, float a[_][_], int k, int jj, float m[_][_]);
void find_s(int n, float a[_][_], int k, int q, int jj, float m[_][_]);
int check_sas(int n, float a[_][_], int k, int *ii);
void find_u(int n, float a[_][_], int ii, int jj, float m[_][_]);
void find_p(int n, float a[_][_], float mm[_][_], int *nf, int f[_]);
void find_value(int n, float a[_], int begin, int end, int *nvalue, float *value);
void find_vector(int n, float mm[_][_], float value, float vector[_]);
int check_vector(int n, float vector[_]);


/* Mot so ham khong can thiet lam :) */
void help(); // bat dau chuong trinh voi ham nay
void about(); // thong tin ve nhom chung minh :3
void pause(); // tam dung
void more(); // in ten phuong phap Danilevski :v

void help() {
	system("cls");
	printf("\n");
	printf("\tDanilevski's method (Dec. 6, 2019)\n");
	printf("\tPress RETURN (ENTER)  to continue.\n");
	printf("\tType \"about\" for more information.\n\n\t>>> ");
	char s[6];
	fflush(stdin);
	gets(s);
	if (strcmp(s, "about") == 0)
		about();
	else if (strlen(s) > 1) {
		system("cls");
		system(s);
		system(s);
		exit(0);
	}
	else {
		system("cls");
		printf("\n\tDanilevski's method\n\n\t");
	}
}

void about() {
	system("cls");
	printf("\n");
	printf("\t--------------------------------------\n");
	printf("\tCode & edited by Do Minh Tuan 20185419\n");
	printf("\tAssisted by   Nguyen Xuan Anh 20182357\n");
	printf("\tCompleted in Hanoi, December 6th, 2019\n");
	printf("\t-------------- TEAM 21 ---------------\n");
	printf("\n");
	pause();
	help();
}

void pause() {
	printf("\n\tPress any key to continue...\n\t");
	getch();
	system("cls");
}

void more() {
	pause();
	printf("\n\tDanilevski's method\n\n");
}