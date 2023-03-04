# Nhập ma trận B, d từ file input
# file input là ma trận cấp n x (n+1)
# trong đó n cột đầu tiên biễn diễn ma trận B, cột cuối cùng biểu diễn vecto d
f1 = open("input.txt", "r")
data = f1.readlines()
n = sum(1 for _ in data)
f1.close()
B = []
d = []
for s in data:
    row = s.strip().split(' ')
    row1 = [float(i) for i in row]
    row2 = []
    for i in range(len(row1)-1):
        row2.append(row1[i])
    B.append(row2)
    d.append(row1[len(row1)-1])
print("B = ", B)
print("d = ", d)

e = 10e-10


# chuẩn vc của ma trận
def chuanvc(a):
    a1 = []
    for r in a:
        m = (sum(abs(i) for i in r))
        a1.append(m)
    c = max(i for i in a1)
    return c


# chuẩn 1 của ma trận
def chuan1(a):
    a1 = [sum([abs(x[i]) for x in a]) for i in range(len(a[0]))]
    m = max(abs(i) for i in a1)
    return m


# hiệu hai vecto
def sub(a, c):
    m = []
    for i in range(len(a)):
        m.append((a[i] - c[i]))
    return m


print(chuanvc(B))
# Gói A: Kiểm tra loại chuẩn
# input: Ma trận B
# output: kt(B)
def kt(B):
    if chuanvc(B) < 1:
        if chuan1(B) < chuanvc(B):
            return 1
        else:
            return 0
    else:
        if chuan1(B) < 1:
            return 1
        else:
            return -1


# Gói B: tính chuẩn
# input: kt(B), B, x
# output: chuan(B), chuan(x)
def chuan(B):
    if kt(B) == 0:
        return chuanvc(B)
    if kt(B) == 1:
        return chuan1(B)


def c_vecto(x):
    if kt(B) == 0:
        return max(abs(i) for i in x)
    else:
        if kt(B) == 1:
            return sum(abs(i) for i in x)
        else:
            print("Error")


# Gói C: gói lặp đơn giải pt x = bx + d với sai số e
# input : B, d, e
# ouput: x*
def lap_don(B, d, e):
    x0 = d

    def phi(b, x, d):
        m = []
        k = []
        for r in b:
            p = sum(x[i] * r[i] for i in range(n))
            m.append(p)
        for i in range(n):
            k.append(m[i] + d[i])
        return k

    xk = phi(B, x0, d)
    dem = 0
    while c_vecto(sub(x0, xk)) > e:
        x0 = xk
        xk = phi(B, x0, d)
        dem = dem + 1
    print("Số lần lặp: ", dem)

    print("Nghiệm của hệ phương trình là: ", xk)
    return xk


# main
if kt(B) == -1:
    print("B không phải ma trận chéo trội.")
else:
    nghiem = lap_don(B, d, e)
    print("Kiểm tra tính đúng đắn của thuật toán:")

    def kt():
        m = []
        kt = []
        for r in B:
            p = sum(nghiem[i] * r[i] for i in range(n))
            m.append(p)
        for i in range(n):
            kt.append(nghiem[i] - m[i] - d[i])
        return kt
    print("x* - Bx* - d = ", kt())
