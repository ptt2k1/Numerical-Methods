#include <iostream> // cout
#include <fstream>  // ifstream
#include <iomanip>  // setw, setprecision, showpos, noshowpos
#include <cmath>    // fabs

// Nếu số có trị tuyện đối nhỏ hơn 1e-10 thì có thể coi là 0
#define ISZERO(x) (fabs((x)) < 1e-10)

using namespace std;

int m, n;
double **a;
const char *checkfile;  // Lưu lại tên file input để kiểm tra sau khi giải xong

// Tải ma trận từ file
void loadMatrix(const char *pathtofile) {
    ifstream fi(pathtofile);
    checkfile = pathtofile;
    
    fi >> m >> n;
    a = new double *[m + 1]; // Ma trận A dùng để lưu ma trận bổ sung A|b. Cột 0 của ma trận a sẽ lưu số số 0 \
    ở đầu mỗi hàng
    for (int i = 0; i <= m; i++) a[i] = new double[n + 2]();
    for (int i = 1; i <= m; i++) for (int j = 1; j <= n + 1; j++) fi >> a[i][j];
    fi.close();
}

// In ma trận ra màn hình
void inmatran(int precision = 4) {
    for (int i = 1; i <= m; i++) {
        cout << (int)a[i][0] << " : "; // a[i][0] dùng để lưu số số 0 đầu tiên ở hàng i
        for (int j = 1; j <= n + 1; j++)
            cout << setw(precision + 5) << left << setprecision(precision) << fixed << a[i][j] << "  ";
        cout << endl;
    }
    cout << "----------------------------------------------------------------" << endl;
}

// Đảo 2 hàng i,j (i,j khác nhau)
void daohang(int i, int j) {
    if (i != j) swap(a[i], a[j]);
}

// Đếm số số 0 đầu ở mỗi hàng từ hàng i trở xuống
void countzeros(int i = 1) {
    for (int k = i; k <= m; k++) {
        int s = 1;
        a[k][0] = 0; // Số số 0 được lưu ở cột đầu tiên của mảng a
        while (ISZERO(a[k][s]) && s <= n) a[k][0] = s++; // Đếm
    }
}

// Sắp xếp lại thứ tự các hàng từ hàng i theo tăng dần số số 0 ở đầu bằng sắp xếp lựa chọn
void sapxeplaihang(int i = 1) {
    int MIN;
    countzeros(i); // Cập nhật lại số số 0
    for (int z = i; z < m; z++) {
        MIN = z;
        for (int k = z + 1; k <= m; k++) if (a[k][0] < a[MIN][0]) MIN = k;
        daohang(MIN, z);
    }
}

// Biến đổi về ma trận bậc thang
void matranbacthang() {
    int i = 1, j = 1;
    // Đi tìm phần tử khác 0 đầu tiên (đi từ trên xuống dưới, trái sang phải)
    while (ISZERO(a[i][j])) {
        i++;
        if (i == m + 1) {
            i = 1; j++;
            if (j == n + 2) {
                countzeros();
                return;
            } // Duyệt đến phần tử cuối cùng mà vẫn chưa thấy thì ma trận A là ma trận 0
        }
    }
    cout << "Vi tri dau tien khac 0: " << i << " " << j << endl;
    if (i != 1) daohang(i, 1);

    // Bắt đầu lặp từ cột thứ j vừa tìm được ở trên đến n
    for (; j <= n + 1; j++) {
        for (i = 2; i <= m; i++) {
            // Nếu a[i][j] khác 0 ta bắt đầu khử
            if (!ISZERO(a[i][j])) {
                int k; // Tìm hàng k để a[k][j] khác 0 và a[k][l] = 0 với mọi l<j
                bool flag = false;
                for (k = i - 1; k >= 1; k--) { // Tìm từ hàng i-1 trở lên 
                    if (!ISZERO(a[k][j]) && a[k][0] == j - 1) {
                        flag = true; // Đã có hàng k thỏa mãn
                        break;
                    }
                }
                if (!flag) continue; //Nếu không tìm thấy k ta tiếp tục tăng i

                // Lấy hàng i trừ đi aij/akj lần hàng k và gán cho hàng i
                double ratio = a[i][j] / a[k][j];
                for (int z = j; z <= n + 1; z++) a[i][z] = a[i][z] - ratio * a[k][z];
                // inmatran(); // Bỏ comment để in ma trận sau mỗi lần khử 1 vị trí
            }
        }
        // Có thể không cần thiết sắp xếp lại, ta đảo hàng trong lúc khử, hiện tại chưa biết cách làm như thế
        sapxeplaihang();
    }
}

// Xác định hệ có nghiệm duy nhất, vô số nghiệm hay vô nghiệm
int xacdinhloainghiem() {
    int rankA = m, rankAn = m; // rank A va rank A ngang
    for (int i = m; i >= 1; i--) {
        if (a[i][0] == n) {    // Số số 0 ở hàng i == n tức là hàng full 0
            rankA--;
            if (ISZERO(a[i][n + 1])) rankAn--; // Giá trị ở cột bổ sung
        }
        else break;
    }

    if (rankA == rankAn && rankA < n)  return rankA; // Vô số nghiệm
    if (rankA == rankAn && rankA == n) return -1;    // Nghiệm duy nhất
    return -2; // Vô nghiệm
}

// Kiểm tra nghiệm trong trường hợp hệ có nghiệm duy nhất
bool kiemtra(double *x, int offset = 0, bool param = false) {
    ifstream fi(checkfile);
    fi >> m >> n;

    for (int i = 0; i < m; i++) {
        double tmp = 0.0, aij = 0.0;
        for (int j = 0; j < n; j++) {
            fi >> aij;
            tmp += aij * x[j + 1 + offset];
        }
        fi >> aij;
        if ((param && !ISZERO(tmp)) || (!param && fabs(tmp - aij) > 1e-6)) return false;
    }
    fi.close();
    return true;
}

// Giải nghiệm trong trường hợp có nghiệm duy nhất
void giainghiemduynhat() {
    double *x = new double[n + 1]();
    for (int i = n; i >= 1; i--) {
        x[i] = a[i][n + 1];
        for (int j = n; j > i; j--) x[i] -= x[j] * a[i][j];
        x[i] /= a[i][i];
    }

    cout << "He co nghiem duy nhat" << endl;
    for (int i = 1; i <= n; i++) cout << "x_" << i << " = " << x[i] << endl;
    if (kiemtra(x)) cout << endl << "---> True" << endl;
    else            cout << endl << "---> False" << endl;
};

// Giải nghiệm trong trường hợp vô số nghiệm
void giainghiemtongquat(int rankA) {
    double **x;

    // Tạo một ma trận lưu nghiệm 
    x = new double *[n+1];
    for (int i = 0; i <= n; i++) x[i] = new double[n]();
    for (int i = 0; i < n; i++)  x[i + 1][i] = 1;
    cout << "Rank A: " << rankA << endl;

    // Biểu diễn ẩn này qua ẩn khác (x1 = 0 -2x2 - 3x3 - 3.5x4)
    int i = rankA;
    while (i >= 1) {
        int j = a[i][0], k = 0;
        double aij = a[i][j + 1];
        for (int z = 0; z <= n; z++) x[z][j] = 0;
        x[0][j] = a[i][n + 1] / aij;
        while (++k <= n) if (k != j + 1) x[k][j] = -a[i][k] / aij;
        i--;
    }

    // Thế ẩn này vào ẩn kia vì xi biểu diễn qua các xj với j > i nên sẽ mất dần các tham số
    for (int i = 0; i < n; i++)
        for (int j = i + 2; j <= n; j++) // Ví dụ thế các xj khác vào x2 thì j > 2
            if (!ISZERO(x[j][i])) {
                double tmp = x[j][i]; // hệ số của xj trong biểu diễn xi từ bước trên (xi = ... -2xj + ... thì tmp = -2)
                for (int z = 0; z <= n; z++) x[z][i] = x[z][i] + tmp * x[z][j - 1]; // Thế xj vào xi
                x[j][i] = x[j][i] - tmp; // Phải trừ đi vì xi = bi + ai*xi + ...,\
                nếu ai = 1 (xi được chọn làm tham số) thì sau vòng lặp for trên xij = ai + tmp,\
                nếu ai = 0 (xi được biểu diễn qua ẩn khác) thì sau vòng for xij = tmp,\
                cả 2 trường hợp đều cần trừ đi tmp để đúng với hệ số
            }

    // In nghiệm
    for (int i = 0; i < n; i++) {
        cout << "x_" << i + 1 << " = " << x[0][i];
        for (int j = 1; j <= n; j++) if (!ISZERO(x[j][i]))
                cout << " " << showpos << x[j][i] << noshowpos << "x_" << j;
        cout << endl;
    }

    // Kiểm tra lại nghiệm
    for (int i = 0; i <= n; i++) {
        if (!kiemtra(x[i], -1, i)) {
            cout << endl << "---> False" << endl;
            break;
        }
        else if (i==n) cout << endl << "---> True" << endl;
    }
}

// Phương pháp Gauss giải hệ phương trình
void Gauss() {
    matranbacthang();
    inmatran();
    int r = xacdinhloainghiem();
    if (r == -2) cout << "No Solution" << endl;
    else if (r == -1)    giainghiemduynhat();
    else                 giainghiemtongquat(r);
}

int main() {
    loadMatrix("d://MaTran1.txt");
    Gauss();
    return 0;
}
