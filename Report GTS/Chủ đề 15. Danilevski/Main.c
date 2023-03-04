#include <D:\Main.h> // khai bao tep tieu de


int main() {
	help(); // Bat dau

	/* Doc du lieu ma tran A tu File */
	int n;
	float a[_][_];
	if (input(&n, a) == False) {
		printf("\tFailed to read.\n\n\t");
		getch();
		main();
	}
	more();
	printf("\n");
	print_m(n, a, "[A]");
	/* ----------------------------- */

	/* Phuong phap Danilevski */
	int nf = 1; // so ma tran khoi Frobenius
	int f[_]; // vi tri ma tran khoi Frobenius
	float mm[_][_]; // tich cac ma tran nghich dao cua M...
	init_e(n, mm);
	find_p(n, a, mm, &nf, f); // tim ma tran P
	print_m(n, a, "[P]");
	/* ---------------------- */

	printf("\n\tPress any key to continue...\n\t");
	getch();

	/* Tim tri rieng, vector rieng */
	int nvalue;
	float eigenvalue[_]; // gia tri rieng
	float eigenvector[_][_]; // vector rieng
	if (nf > 1) {
		FOR(i, 0, nf - 1)
			find_value(n, a[f[i + 1]], f[i + 1], f[i], &nvalue, eigenvalue);
		FOR(i, 0, nvalue)
			find_vector(n, mm, eigenvalue[i], eigenvector[i]);
	}
	else {
		find_value(n, a[0], 0, n, &nvalue, eigenvalue);
		FOR(i, 0, nvalue)
			find_vector(n, mm, eigenvalue[i], eigenvector[i]);
	}
	/* --------------------------- */

	/* In lai P, A ra man hinh */
	printf("\n\tDanilevski's method\n\n");
	print_m(n, a, "[P]");
	printf("\n\t");
	input(&n, a);
	/* ----------------------- */

	/* Ghi ket qua vao File */
	output(n, eigenvalue, eigenvector);
	/* -------------------- */

	/* In ket qua ra man hinh */
	FOR(i, 0, nvalue) {
		if (check_vector(n, eigenvector[i]) == True) {
			printf("\tEigenvalue:  %12.4f\n", eigenvalue[i]);
			printf("\tEigenvector: ");
			FOR(j, 0, n)
				printf("%12.4f", eigenvector[i][j]);
			printf("\n\n");
		}
	}
	/* ---------------------- */

	/* Lap lai chuong trinh */
	printf("\t");
	getch();
	help();
	/* -------------------- */

	return 0;
}


/* Doc, ghi File */
int input(int *n, float a[_][_]) {
	printf("INPUT: %s\n", inp);
	FILE *f = fopen(inp, "r");
	fscanf(f, "%i", n);
	if (*n < 3)
		return False;
	FOR(i, 0, *n)
		FOR(j, 0, *n)
			fscanf(f, "%f", &a[i][j]);
	fclose(f);
	print_m(*n, a, "[A]");
	return True;
}

void output(int n, float value[_], float vector[_][_]) {
	printf("\tOUTPUT: %s\n", out);
	FILE *f = fopen(out, "w");
	FOR(i, 0, n) {
		fprintf(f, "%12.4f\n", value[i]);
		FOR(j, 0, n)
			fprintf(f, "%12.4f", vector[i][j]);
		fprintf(f, "\n");
	}
	fclose(f);
	printf("\n");
}
/* ------------- */


/* In ma tran m ra man hinh console */
void print_m(int n, float m[_][_], char *s) {

	printf("\n\t%s:\n\t", s);
	FOR(i, 0, n)
		FOR(j, 0, n) {
			printf("%12.4f", m[i][j]);
			if (j == n - 1)
				printf("\n\t");
		}
	printf("\n");
}


/* Khoi tao ma tran don vi e */
void init_e(int n, float e[_][_]) {

	FOR(i, 0, n)
		FOR(j, 0, n) {
			if (j == i)
				e[i][j] = 1;
			else
				e[i][j] = 0;
		}
}


/* Nhan hai ma tran p va q, duoc ma tran r = p * q
** *	p: 2 chieu; q: 2 chieu -> mmul(n, r, p, q)
** *	p: 2 chieul q: 1 chieu -> mul(n, r, p, q)
*/
void mmul(int n, float r[_][_], float p[_][_], float q[_][_]) {

	FOR(i, 0, n)
		FOR(j, 0, n) {
			r[i][j] = 0;
			FOR(k, 0, n)
				r[i][j] += p[i][k] * q[k][j];
		}
}

void mul(int n, float r[_], float p[_][_], float q[_]) {

	FOR(i, 0, n) {
		r[i] = 0;
		FOR(j, 0, n)
			r[i] += p[i][j] * q[j];
	}
}


/* Kiem tra ma tran A tai hang k
** *	Tra ve True neu vi tri k - 1 khac 0
** *	Tra ve False neu vi tri k - 1 bang 0
** *	*	Tra ve jj = j la vi tri khac 0 dau tien truoc k - 1
** *	*	Tra ve jj = k neu cac vi tri truoc k - 1 deu bang 0
*/
int check_a(int n, float a[_][_], int k, int *jj) {

	if (a[k][k - 1] != 0)
		return True;
	else {
		FOR(j, 0, k - 1)
			if (a[k][j] != 0) {
				*jj = j; // Tim C
				return False;
			}
		*jj = k; // Tim S, S^-1
		return False;
	}
}


/* Tim ma tran M hoac M^-1
** *	jj = NORMAL -> Tim M
** *	jj = INVERSE -> Tim M^-1
** *	jj khac:
** *	*	M = M^-1 = C; C la ma tran don vi hoan vi hang k, hang jj cho nhau va hoan vi cot k, cot jj cho nhau
*/
void find_m(int n, float a[_][_], int k, int jj, float m[_][_]) {

	init_e(n, m);
	if (jj == NORMAL) {
		FOR(j, 0, n)
			m[k - 1][j] = a[k][j];
		// print_m(n, m, "M");
	}
	else if (jj == INVERSE) {
		FOR(j, 0, n)
			m[k - 1][j] = -a[k][j] / a[k][k - 1];
		m[k - 1][k - 1]= 1 / a[k][k - 1];
		// print_m(n, m, "M^-1");
	}
	else {
		m[k][k] = 0;
		m[jj][jj] = 0;
		m[k][jj] = 1;
		m[jj][k] = 1;
		// print_m(n, m, "C");
	}
}


/* Tim ma tran S hoac S^-1
** *	jj = NORMAL -> Tim S
** *	jj = INVERSE -> Tim S^-1
** S, S^-1 la ma tran don vi voi cot q + 1: tu vi tri 0 den vi tri k la tuong ung voi cot q cua ma tran A
*/
void find_s(int n, float a[_][_], int k, int q, int jj, float m[_][_]) {

	init_e(n, m);
	if (jj == NORMAL) {
		FOR(i, 0, k)
			m[i][q + 1] = -a[i][q];
		// print_m(n, m, "S");
	}
	else if (jj == INVERSE) {
		FOR(i, 0, k)
			m[i][q + 1] = a[i][q];
		// print_m(n, m, "S^-1");
	}
}


/* Kiem tra cac phan tu o cot cuoi cung cua ma tran A = S * A * S^-1
** tu vi tri dau tien den vi tri k - 1, co bang 0 hay khong
*/
int check_sas(int n, float a[_][_], int k, int *ii) {

	for (int i = k - 1; i >= 0; i--)
		if (a[i][n - 1] != 0) {
			*ii = i;
			return False;
		}
	return True;
}


/* Neu cot cuoi cung cua ma tran A = S * A * S^-1 co phan tu khac 0
** thi ta tim ma tran U va U^-1*/
void find_u(int n, float a[_][_], int ii, int jj, float m[_][_]) {

	init_e(n, m);
	if (jj == NORMAL) {
		FOR(i, ii, n)
			FOR(j, ii, n) {
				if (j == i + 1 || (i == n - 1 && j == ii))
					m[i][j] = 1;
				else
					m[i][j] = 0;
			}
		// print_m(n, m, "U");
	}
	else if (jj == INVERSE) {
		FOR(i, ii, n)
			FOR(j, ii, n) {
				if (j == i - 1 || (i == ii && j == n - 1))
					m[i][j] = 1;
				else
					m[i][j] = 0;
			}
		// print_m(n, m, "U^-1");
	}
}


/* Tim ma tran P */
void find_p(int n, float a[_][_], float mm[_][_], int *nf, int f[_]) {

	float m[_][_];
	float temp[_][_];
	float e[_][_];
	init_e(n, temp);
	init_e(n, e);
	for (int k = n - 1; k > 0; k--) {
		int jj;
		if (check_a(n, a, k, &jj) == True) {
			find_m(n, a, k, NORMAL, m);
			mmul(n, temp, m, a);

			find_m(n, a, k, INVERSE, m);
			mmul(n, a, temp, m);

			mmul(n, temp, mm, m);
			mmul(n, mm, temp, e);

			// print_m(n, a, "A");
		}
		else {
			if (jj == k) {
				FOR(q, k - 1, n) { // co ve sai sai
					find_s(n, a, k, q, NORMAL, m);
					mmul(n, temp, m, a);

					find_s(n, a, k, q, INVERSE, m);
					mmul(n, a, temp, m);

					mmul(n, temp, mm, m);
					mmul(n, mm, temp, e);

					// print_m(n, a, "A");
				}
				int ii;
				if (check_sas(n, a, k, &ii) == False) {
					find_u(n, a, ii, NORMAL, m);
					mmul(n, temp, m, a);

					find_u(n, a, ii, INVERSE, m);
					mmul(n, a, temp, m);

					mmul(n, temp, mm, m);
					mmul(n, mm, temp, e);

					// print_m(n, a, "A");
					k = n; //
					*nf = 1;
				}
				else {
					f[*nf - 1] = k;
					(*nf)++;
				}
			}
			else {
				find_m(n, a, k, jj, m);
				mmul(n, temp, m, a);
				mmul(n, a, temp, m);

				mmul(n, temp, mm, m);
				mmul(n, mm, temp, e);

				// print_m(n, a, "A");
			}
		}
	}
}


void find_value(int n, float a[_], int begin, int end, int *nvalue, float *value) {
	float coef[_];
	FOR(i, 0, n) {
		coef[0] = -1;
		FOR(j, begin, end)
			coef[j - begin + 1] = a[j];
	}
	giai_PT(coef, n, value, nvalue); //
}


void find_vector(int n, float mm[_][_], float value, float vector[_]) {
	float p_vector[_];
	FOR(i, 0, n)
		p_vector[i] = pow(value, n - i - 1);
	mul(n, vector, mm, p_vector);
}


int check_vector(int n, float vector[_]) {
	FOR(i, 0, n)
		if (vector[i] != 0)
			return True;
	return False;
}