#include<bits/stdc++.h>
#pragma GCC optimize("Ofast")
#pragma GCC tarGet_Value_y("popcnt")
#pragma GCC tarGet_Value_y("sse,sse2,sse3,ssse3,sse4,popcnt,abm,mmx,avx,tune=native")
using namespace std;

const int N = 1e4 + 12;
const long long Mod = 1e9 + 7;
const long long oo = 1e18;

mt19937 rd(chrono::steady_clock::now().time_since_epoch().count());

#define fi first
#define se second
#define pb push_back
#define ldb long double 

#define Bit(a, i) (((long long)a>>((int)i))&1ll)
#define dBit(x) __builtin_popcountll((long long)x)
#define FOR(i, a, b) for (int _ = a, __ = b, i = _; i <= __; i++)
#define FORD(i, a, b) for (int _ = a, __ = b, i = _; i >= __; i--)
#define FIX(n, x) cout << setprecision((int)n) << fixed << x << "\n";

int n;
ldb dw[N]; 					// w'[x_i]
vector<ldb> arr_wi;			// Hệ số của (x-x_1)(x-x_2)...(x-x_n)
vector<ldb> arr_Lagrange;			// Hệ số của đa thức ns Lagrange


struct Data {
	ldb x, y;
	bool operator < (const Data &newa) const{
		return x < newa.x;
	}
} a[N];

void Print(vector<ldb> &v) {
	cout << "In cac he so tu x^0 den x^" << v.size() - 1 << "\n";
	for (auto x : v) cout << x << " " ; cout << "\n\n";
}

vector<ldb> hoocnerNhan(vector<ldb> &arr, ldb hs, ldb xk) {				// arr nhân (hs * x + xk)  
	vector<ldb> ret; ret.resize(arr.size() + 1);

	if (arr.size() == 0) {
		ret.resize(2);
		ret[0] = xk, ret[1] = hs;
		return ret;
	}

	FORD(i, ret.size() - 1, 0) {
		ldb tmp = 0; 							// hệ số x^i
		if (i > 0) tmp += arr[i - 1] * hs;
		if (i < arr.size()) tmp += arr[i] * xk;
		ret[i] = tmp;
	}

	return ret;
}

vector<ldb> hoocnerChia(vector<ldb> &arr, ldb hs, ldb xk)	{			// arr chia (hs * x + xk)
	vector<ldb> ret; ret.resize(arr.size() - 1);

	ldb nho = 0;
	FORD(i, arr.size() - 1, 1) {
		ldb val = (arr[i] - nho) / hs; 			// hệ số x^(i-1)
		nho = val * xk;
		ret[i - 1] = val;
	}

	return ret;
}

void Get_Lagrange(vector<ldb> &arr_wi, int pos) {
	vector<ldb> ret = hoocnerChia(arr_wi, 1, -a[pos].x);

	ldb val = a[pos].y / dw[pos];
	FOR(i, 0, arr_Lagrange.size() - 1) arr_Lagrange[i] += ret[i] * val;
	//val là giá trị y_pos / w'(x_pos)
	// ret[i] là hệ số x^i của w_{n+1}(x) / (x - x_i) 
	//arr_Lagrange[i] là hệ số x^i của đa thức nội suy Lagrange

	return ;
}

void Init() {

    sort (a, a + n + 1); 

    cout << "Sap xep theo cac moc noi suy tang dan\n";
    FOR(i, 0, n) cout << a[i].x << " " << a[i].y << "\n"; cout << "\n";


    // Tính dw[i] = w'(x_i)
    FOR(i, 0, n) {
    	dw[i] = 1;
    	FOR(j, 0, n) if (i != j) dw[i] *= (a[i].x - a[j].x);
    }

    // arr_wi là hệ số của (x-x_1)(x-x_2)...(x-x_n)
    FOR(i, 0, n) arr_wi = hoocnerNhan(arr_wi, 1, -a[i].x);


    // arr_Lagrange chứa hệ số của đa thức nội suy Lagrange
    arr_Lagrange.resize(arr_wi.size() - 1);
    FOR(i, 0, n) {
    	Get_Lagrange(arr_wi, i);
    	// Cập nhật y_i.L_i(x) cho arr_Lagrange
    }

    Print(arr_Lagrange);
}

ldb Get_Value_y(ldb value_x) {
	ldb value_y = 0;

   	ldb powx = 1; // lũy thừa của value_x
   	FOR(i, 0, arr_Lagrange.size() - 1) {
   		value_y += arr_Lagrange[i] * powx;
   		powx *= value_x;
   	}

   	//cout << "Gia tri da thuc ns Lagrange tai x = " << value_x << " bang " <<value_y << "\n";

   	return value_y;
}

main(){
    #define file "nsLagrange"
    freopen (file ".inp", "r", stdin);
    freopen (file ".out", "w", stdout);
    ios_base::sync_with_stdio(false);
    cin.tie(0);

    cin >> n; // số lượng mốc nội suy ban đầu 

    n -= 1;
    FOR(i, 0, n) cin >> a[i].x >> a[i].y;

    cout << fixed << setprecision(10);
    
    Init();


    int nq;
    cin >> nq; // số lượng mốc nội suy cần tính 
    while(nq--) {
    	ldb value_x;
    	cin >> value_x;
    	cout << "Gia tri da thuc ns Lagrange tai x = " << value_x << " bang " ;
    	cout << Get_Value_y(value_x) << "\n";
    }

}
