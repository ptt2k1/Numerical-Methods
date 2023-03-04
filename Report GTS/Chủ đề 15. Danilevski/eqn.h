#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <math.h>
#include <windows.h>
#define MAX 100
#define EPSILON 0.000001


/* Khai bao cac ham */

float tim_max(float *a, int n);
void tinh_dao_ham(float *a, int n, float he_so_dao_ham[MAX]);
float tim_kiem(float *a, int n, float x, float y);
float thay_gia_tri(float *a, int n, float x);
void giai_PT(float *a, int n, float *nghiem, int *m);


/* Cac ham su dung de giai phuong trinh bac n */

float tim_max(float *a, int n) {
	int i;
	float max = 0;
	for (i = 0; i <= n; i++)
		if (fabs(a[i]) > max)
			max = fabs(a[i]);
	return max;
}

void tinh_dao_ham(float *a, int n, float he_so_dao_ham[MAX]) {
	int i;
	int k = n;
	for (i = 0; i <= n - 1; i++) {
		he_so_dao_ham[i] = a[i] * k--;
	}
}


/* Phuong phap chia doi */

float tim_kiem(float *a, int n, float x, float y) {
	float z, t1, t2;
	do {
		z = (x + y) / 2;
		t1 = thay_gia_tri(a, n, z);
		t2 = thay_gia_tri(a, n, x);
		if (t1 * t2 > 0)
			x = z;
		else
			y = z;
	} while (y - x > EPSILON);
	z = roundf((float) z * 10000) / 10000;
	return z;
}

float thay_gia_tri(float *a, int n, float x) {
	int i;
	float kq = 0;
	for (i = 0; i <= n; i++)
		kq += a[i] * pow(x, n - i);
	return kq;
}


/* Giai phuong trinh bac n, he so a */

void giai_PT(float *a, int n, float *nghiem, int *m) {
	system("cls");
	printf("\n\tSolving equation...\n\n\tIf this statement doesn't disappear,\n\tSorry, my algorithm has something wrong!\n\t");
	if (n == 1) {
		(*m)++;
		nghiem[1] = -a[1] / a[0];
	}
	else if (n >= 2) {
		int i, j;
		float he_so_dao_ham[MAX][MAX];
		int cap_dao_ham[MAX];
		float *R;
		R = (float *) malloc(sizeof(float) * (n + 2));
		cap_dao_ham[0] = n;

		// Gan PT ban dau bang dao ham cap 0
		for (i = 0; i <= n; i++) {
			he_so_dao_ham[0][i] = a[i];
			R[0] = 1.0 + tim_max(he_so_dao_ham[0], cap_dao_ham[0]) / fabs(he_so_dao_ham[0][0]);
		}

		// Tinh he so dao ham, cap dao ham cac cap tu 1 den n - 1
		for (i = 1; i <= n - 1; i++) {
			tinh_dao_ham(he_so_dao_ham[i - 1], cap_dao_ham[i - 1], he_so_dao_ham[i]);
			cap_dao_ham[i] = cap_dao_ham[i - 1] - 1;
			R[i] = 1.0 + tim_max(he_so_dao_ham[i], cap_dao_ham[i]) / fabs(he_so_dao_ham[i][0]);
		}

		float cuc_tri[MAX][MAX];
		// Tim cuc tri cua dao ham cap n - 1
		cuc_tri[n - 1][0] = -R[n - 2];
		cuc_tri[n - 1][1] = -he_so_dao_ham[n - 1][1] / he_so_dao_ham[n - 1][0];
		cuc_tri[n - 1][2] = R[n - 2];

		//Tim cuc tri dao ham cap n - 2 den 0
		for (i = n - 2; i >= 0; i--) {
			cuc_tri[i][0] = -R[i - 1];
			cuc_tri[i][n - i + 1] = R[i - 1];
			for (j = 1; j <= n - i; j++) {
				cuc_tri[i][j] = tim_kiem(he_so_dao_ham[i], cap_dao_ham[i], cuc_tri[i + 1][j - 1], cuc_tri[i + 1][j]);
			}
		}

		float flag;
		int k = 0;
		// Kiem tra lai nghiem va loai bo nghiem sai (nghiem la cac cuc tri cua dao ham cap 0)
		for (i = 1; i <= n; i++) {
			flag = fabs(thay_gia_tri(a, n, cuc_tri[0][i]));
			if (flag < 0.1) {
				nghiem[k] = cuc_tri[0][i];
				k++;
				(*m)++;
			}
		}
		free(R);
		system("cls");
	}
}



