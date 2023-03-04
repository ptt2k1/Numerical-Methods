import numpy as np
import matplotlib.pyplot as plt

def Nhapdulieu():
    x_t: list[float] = []
    y_t: list[float] = []
    with open('input2.txt', 'r+') as f:
        for line in f.readlines():
            x_temp = float(line.split(' ')[0])
            y_temp = float(line.split(' ')[1])
            if x_temp in x_t:
                continue
            x_t.append(x_temp)
            y_t.append(y_temp)
    f.close()
    return x_t, y_t

def Nhapbacdathuc(degx):
    with open('bac_da_thuc.txt', 'r+') as f:
        for line in f.readlines():
            bac_temp = int(line[0])
            if bac_temp >= 0:
                degx.append(bac_temp)
    f.close()
    return degx

def check_input(x, y):
    sign_y = np.zeros(len(y))
    x_t = []
    y_t = []
    for i in range(len(y)):
        sign_y[i] = np.sign(y[i])
    a = np.mean(sign_y)
    print(a)
    for i in range(len(y)):
        if sign_y[i] == np.sign(a):
            x_t.append(x[i])
            y_t.append(y[i])
        else:
            print("Loại bỏ điểm: (", x[i], ",", y[i],")")
    if abs(a) < 1e-2:
        print("Nhiều điểm dữ liệu kỳ dị")
    return x_t, y_t, np.sign(a)

def Chonmohinh():
    print("Chọn phương pháp ước lượng:")
    print(" "*10 + "1. Đa thức {x^k}.")
    print(" "*10 + "2. Lượng giác.")
    print(" "*10 + "3. Hàm mũ a*e^({x^k})")
    print(" "*10 + "4. Hàm logarit a*ln({x^k})")
    choose = int(input("Lựa chọn: "))
    return choose

def Dathuc(x, y, degx):
    h = len(degx)
    n = len(x)
    phi = np.zeros([h, n])
    for i in range(h):
        for j in range(n):
            phi[i, j] = x[j]**degx[i]
    L = phi @ np.transpose(phi)
    P = phi @ np.transpose(y)
    return np.transpose(np.linalg.inv(L) @ P)

def Luonggiac(x, y, deg, k):
    n = len(x)
    phi = np.zeros([len(deg), n])
    t = 0
    if deg[0] == 0:
        for i in range(n):
            phi[0, i] = 1
        t = 1
    for i in range(t, len(deg)):
        if deg[i] % 2 == 1:
            for j in range(n):
                phi[i, j] = np.sin((int(deg[i]/2)+1)*x[j])
        elif deg[i] != 0:
            for j in range(n):
                phi[i, j] = np.cos((deg[i]/2)*x[j])
    L = phi @ np.transpose(phi)
    P = phi @ np.transpose(y)
    return np.transpose(np.linalg.inv(L) @ P)


def Ham_exp(x, y, degx, sign):
    y_t = []
    for i in range(len(y)):
        y_t.append(np.log(sign*y[i]))
    a = Dathuc(x, y_t, degx)
    a[0] = sign*np.exp(a[0])
    return a

def Ham_logarit(x, y, degx):
    y_t = []
    for i in range(len(y)):
        y_t.append(np.exp(y[i]))
    a = Dathuc(x, y_t, degx)
    return a

def Sigma(x, y, a, deg, choose):
    sigma = 0
    for i in range(len(y)):
        sigma += (y[i] - Phi(x[i], a, deg, choose))**2
    sigma = np.sqrt((1/len(y)) * sigma)
    return sigma


def bubble_sort(arr):
    def swap(i, j):
        arr[i], arr[j] = arr[j], arr[i]
    n = len(arr)
    swapped = True
    x = -1
    while swapped:
        swapped = False
        x = x + 1
        for i in range(1, n - x):
            if arr[i - 1] > arr[i]:
                swap(i - 1, i)
                swapped = True
    return arr

def Phi(x, a, deg, choose):
    f = 0
    if choose == 1:
        for i in range(len(deg)):
            f += a[i]*x**deg[i]
        return f
    elif choose == 2:
        for i in range(1, len(deg)):
            if deg[i] % 2 == 1:
                f += a[i] * np.sin((int(deg[i] / 2) + 1) * x)
            else:
                f += a[i] * np.cos((deg[i] / 2) * x)
        return a[0] + f
    elif choose == 3:
        for i in range(1, len(deg)):
            f += a[i] * x ** deg[i]
        return a[0]*np.exp(f)
    elif choose == 4:
        for i in range(len(deg)):
            f += a[i] * x ** deg[i]
        return np.log(f)

def graphic(x, y, a, deg, choose):
    xx = np.linspace(min(x), max(x), 1000)
    yy = []
    for i in xx:
        yy.append(Phi(i, a, deg, choose))
    plt.scatter(x, y, s=30, cmap='palete')
    plt.plot(xx, yy, 'r')
    plt.show()

if __name__ == "__main__":
    x, y = Nhapdulieu()
    choose = Chonmohinh()
    if choose == 1:
        degx = []
        degx = Nhapbacdathuc(degx)
        degx = bubble_sort(degx)
        print("Ma trận bậc: ", degx)
        a = Dathuc(x, y, degx)
        print("Ma trận hệ số: ", a)
        sai_so = Sigma(x, y, a, degx, choose)
        print("Sai số trung bình phương: ", sai_so)
        graphic(x, y, a, degx, choose)
    elif choose == 2:
        print("Nhập bậc của đa thức lượng giác: ")
        k = int(input("k = "))
        deg = []
        deg = Nhapbacdathuc(deg)
        bubble_sort(deg)
        print("Ma trận bậc: ", deg)
        a = Luonggiac(x, y, deg, k)
        print("Ma trận hệ số: ", a)
        sai_so = Sigma(x, y, a, deg, choose)
        print("Sai số trung bình phương: ", sai_so)
        graphic(x, y, a, deg, choose)
    elif choose == 3:
        degx = []
        degx = Nhapbacdathuc(degx)
        degx = bubble_sort(degx)
        print("Ma trận bậc: ", degx)
        x, y, sign = check_input(x, y)
        a = Ham_exp(x, y, degx, sign)
        print("Ma trận hệ số: ", a)
        sai_so = Sigma(x, y, a, degx, choose)
        print("Sai số trung bình phương: ", sai_so)
        graphic(x, y, a, degx, choose)
    elif choose == 4:
        degx = []
        degx = Nhapbacdathuc(degx)
        degx = bubble_sort(degx)
        print("Ma trận bậc: ", degx)
        a = Ham_logarit(x, y, degx)
        print("Ma trận hệ số: ", a)
        sai_so = Sigma(x, y, a, degx, choose)
        print("Sai số trung bình phương: ", sai_so)
        graphic(x, y, a, degx, choose)