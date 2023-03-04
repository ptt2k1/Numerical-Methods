import numpy as np
import matplotlib.pyplot as plt

y0 = 0.2 #Giá trị cần tìm nghiệm
eps = 1e-6 #Sai số tương đối


def input_data():
    x_t: list[float] = []
    y_t: list[float] = []
    with open('input.txt', 'r+') as f:
        for line in f.readlines():
            x_temp = float(line.split(' ')[0])
            y_temp = float(line.split(' ')[1])
            if x_temp in x_t:
                continue
            x_t.append(x_temp)
            y_t.append(y_temp)
    f.close()
    return x_t, y_t


def check_cachdeu(x):
    for i in range(1, len(x)-1):
        x1 = x[i] - x[i - 1]
        x2 = x[i + 1] - x[i]
        if abs(x1 - x2) >= 1e-7:
            print("x không cách đều")
            return False
    return True


def Timsonganh(y, n):
    q = 0
    Q = np.zeros([n, 2])
    for i in range(1, n - 1):
        sign = np.sign(y[i] - y[i - 1])
        if sign != np.sign(y[i + 1] - y[i]):
            Q[q, 1] = i
            Q[q + 1, 0] = i
            q = q + 1
    Q[q, 1] = n - 1
    q += 1
    return Q[: q, :2], q

def ThietlapkhoangNS(x, y, a, b, y0):
    sol_inter = x[a: b]
    y_inter = y[a: b]
    print("\n\nKhoảng song ánh chứa nghiệm:")
    print(sol_inter)
    print(y_inter)
    while(True):
        m = int(input("Nhập số mốc muốn sử dụng cho tìm nghiệm: "))
        if m <= len(sol_inter):
            print("Nhập vị trí đầu mốc nội suy tìm nghiệm: ")
            left = int(input("Cận trái: "))
            right = left + m
            if right <= len(sol_inter):
                return sol_inter[left: right], y_inter[left: right], 0
        print("Chọn phương pháp nsNewton để xây dựng dãy lặp:")
        print("1. Nội suy Newton tiến.")
        print("2. Nội suy Newton lùi.")
        choose = int(input("Lựa chọn: "))
        if choose == 1:
            if y0 - y_inter[0] > 0:
                sign = 1
            else:
                sign = -1
            for i in range(len(sol_inter) - 1):
                if (y0 - y_inter[i + 1]) * sign < 0:
                    break
            left = i
            if a + left + m - 1 < len(x):
                return x[a+left: a + left + m], y[a + left: a + left + m], 1
        else:
            if y0 - y_inter[len(sol_inter) - 1] > 0:
                sign = 1
            else:
                sign = -1
            for i in range(len(sol_inter)):
                if (y0 - y_inter[len(sol_inter) - i - 1]) * sign < 0:
                    break
            right = len(sol_inter) - i + 1
            if a + right - m + 1 > 0:
                return x[a + right - m: a + right], y[a + right - m: a + right], 2


def Xapxinghiem(x, y, y0, eps, choose):
    if choose == 0:
        n = len(x) - 1
        p_heso = nsNewtonbatky(y, x, n)
        sol = P_n(y0, n, p_heso)
        return sol
    elif check_cachdeu(x):
        if choose == 1:
            t = []
            h = x[1] - x[0]
            for i in x:
                t.append((i - x[0]) / h)
            p_heso = nsNewtontien(t, y, len(t) - 1)
            t0 = (y0 - y[0]) / (y[1] - y[0])
            sol = Lapnghiem(p_heso, t0, y[1] - y[0], y0, eps)
            return x[0] + h * sol
        elif choose == 2:
            t = []
            h = x[1] - x[0]
            k = len(y)
            for i in x:
                t.append((i - x[k - 1]) / h)
            p_heso = nsNewtontien(t, y, k - 1)
            t0 = (y0 - y[k - 1]) / (y[k - 1] - y[k - 2])
            sol = Lapnghiem(p_heso, t0, y[k - 1] - y[k - 2], y0, eps)
            return x[k-1] + h * sol


def Lapnghiem(p_heso, t0, delta_y, y0, eps):
    deg = len(p_heso) - 1
    p_heso[deg-1] = p_heso[deg - 1] - delta_y
    t = t0
    while (True):
        t = (y0 - P_n(t, deg, p_heso)) / delta_y
        print(P_n(t, deg, p_heso))
        if abs(t - t0) < eps:
            break
        t0 = t
    return t

def P_n(t, n, p_heso):
    p = float(0)
    for i in range(n + 1):
        p += p_heso[i] * t ** (n - i)
    return p

def nhandathuc(prev, x):
    n = len(prev) + 1
    xpoly = np.zeros(n)
    xpoly[0] = 1
    xpoly[n - 1] = -prev[n - 2] * x
    for i in range(1, n - 1):
        xpoly[i] = -x * prev[i - 1] + prev[i]
    return xpoly

def Bangtihieu(x, y, n):
    BTH = np.zeros([n + 1, n + 1])
    for i in range(n + 1):
        BTH[i, 0] = y[i]
    for j in range(1, n + 1):
        for i in range(n + 1 - j):
            BTH[i, j] = (BTH[i + 1, j - 1] - BTH[i, j - 1]) / (x[i + j] - x[i])
    return BTH[0, :n+1]

def Bangsaiphan(y, n):
    BSP = np.zeros([n + 1, n + 1])
    for i in range(n + 1):
        BSP[i, 0] = y[i]
    for j in range(1, n + 1):
        for i in range(n + 1 - j):
            BSP[i, j] = (BSP[i + 1, j - 1] - BSP[i, j - 1])
    return BSP[0, :n+1]


def nsNewtonbatky(x, y, n):
    index = np.zeros(n + 1)
    index[n] = y[0]
    poly = [1]
    BTH = Bangtihieu(x, y, n)
    for i in range(1, n + 1):
        poly = nhandathuc(poly, x[i - 1])
        for j in range(i + 1):
            index[n - i + j] = index[n - i + j] + BTH[i] * poly[j]
    return index

def nsNewtontien(x, y, n):
    index = np.zeros(n + 1)
    index[n] = y[0]
    poly = [1]
    BSP = Bangsaiphan(y, n)
    for i in range(n+1):
        BSP[i] = BSP[i] / giaithua(i)
    for i in range(1, n + 1):
        poly = nhandathuc(poly, x[i - 1])
        for j in range(i + 1):
            index[n - i + j] = index[n - i + j] + BSP[i] * poly[j]
    return index
def giaithua(n):
    giai_thua = 1;
    if (n == 0 or n == 1):
        return giai_thua;
    else:
        for i in range(2, n + 1):
            giai_thua = giai_thua * i;
        return giai_thua;

def graphic(x, y, y0):
    xx = [min(x), max(x)]
    yy = [y0, y0]
    plt.scatter(x, y, s=30, cmap='palete')
    plt.plot(xx, yy)
    plt.show()

if __name__ == "__main__":
    x, y = input_data()
    sol = []
    n = len(x)
    Q, k = Timsonganh(y, n)
    for i in range(k):
        left = int(Q[i, 0])
        right = int(Q[i, 1]) + 1
        if (y[left] < y0 < y[right-1]) or (y[left] > y0 > y[right-1]):
            x_t, y_t, choose = ThietlapkhoangNS(x, y, left, right, y0)
            print("Khoảng tìm nghiệm thứ  (", i+1, "): ", x_t)
            x0 = Xapxinghiem(x_t, y_t, y0, eps, choose)
            print("Nghiệm tìm được thứ (", i+1, "): ", x0)
            sol.append(x0)
    print("Tất cả các nghiệm tìm được: ", sol)
    graphic(x, y, y0)
