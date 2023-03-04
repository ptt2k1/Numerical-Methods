from sympy import *
from math import log

ETA = 1e-15
EPSILON = 1e-6
def f_(f, x,eps=EPSILON):
    return (f(x+eps)-f(x-eps))/(2*eps)

def gradient_descent(f, a, b):
    def fixeta(dfx0,x0):
        eta = ETA
        while sgn*f_(f,x0+sgn*eta*dfx0)>=0 and eta<1:
            eta *= 2
        while sgn*f_(f,x0+sgn*eta*dfx0)<=0:
            eta /= 2
        return eta
    sgn, count = 1 if f(a+EPSILON) > f(a) else -1, 0
    xr = [a]
    x0 = a + 0.5*EPSILON
    eta = ETA
    while x0 < b:
        dx = f_(f, x0)
        while abs(dx) > 0.001*EPSILON:
            dx = f_(f, x0)
            eta = fixeta(dx,x0)
            x0 += sgn*eta*dx
            count += 1
            if x0 > b:
                break
        sgn = -sgn
        if x0 > a and x0 < b:
            xr.append(x0)
        x0 += 0.5*EPSILON
    xr.append(b)
    return xr

def findMax(f, a, b):
    if a>b:
        a,b = b,a
    lastPoints = gradient_descent(f, a, b)
    result = max(map(f, lastPoints))
    return result
def findMin(f, a, b):
    if a>b:
        a,b = b,a
    lastPoints = gradient_descent(f, a, b)
    result = min(map(f, lastPoints))
    return result

x = symbols("x")
q = 0
e = int(input("Làm tròn đến chữ số thứ  : "))
# Nhập hàm f(x) và kiểm tra điều kiện hội tụ
while True:
    a = float(input(" Nhập cận dưới là: "))
    b = float(input(" Nhập cận trên là : "))
    if a < b:
        break
while True:
    hamso = input("Enter function y = f(x)\ny = ")
    f = lambdify(x, sympify(hamso), "math")
    ap = lambdify(x, sympify(hamso).diff(x), "math")
    g = lambdify(x, sympify(hamso).diff(x, 2), "math")
    p = lambda x:abs(ap(x))
    
    q = findMax(p,a,b)
    print( f"gia tri cua q la {q}")
    if q >= 1:
        print("Giá trị của q khong thoả mãn điều kiện co")
        print("Nhap  lai ham  f(x) ")
    if 0 <= q <= 1:
        break
qo = round(q, e)
print(f"Giá trị của q được làm tròn là  {qo}")
while True:
    x = float(input("Nhap gia tri ban dau la :"))
    if a <= x <= b:
        break
print("---------------------------------------")
t = x
k = 0
count = 0
w = 1
print(f"Giá trị khởi tạo ban đầu là : {x} ")
Nghiem_dung = x
# Tìm nghiệm đúng theo sai số hậu nghiệm
print("***Chương trình tìm nghiệm theo công thức hậu nghiệm***")
x = t
k = 0
count = 0
w = 1
Nghiem_dung1 = x
print(f"Giá trị khởi tạo ban đầu là : {x} ")
while(abs(x-k) >= ((1-q)*(10**(-e)))/q):
    y = f(x)
    print(f"Giá trị thứ  {w} là :{y}")
    k = x
    x = y
    count += 1
    w += 1
Nghiem_dung1 = y
X1 = round(Nghiem_dung1, e)
print(f"Nghiem theo công thức hậu nghiệm chưa được làm tròn  là {Nghiem_dung1} ")
print(f"NNghiệm của phương trình theo công thức hậu nghiệm làm tròn là Xo= {X1}")
print(f"Số lần lặp theo công thức hậu nghiệm là {count}")
print("-------------------------------------")
# Tìm nghiệm đúng theo công thức tiên nghiệm
x = t
qe = f(t)
k = 0
count = 1
Nghiem_dung2 = x
if(Nghiem_dung2-f(Nghiem_dung2) == 0):
    Nghiem_dung2 = x
n = log(abs(((1-q)*(10**(-e)))/(x-qe))) / log(q)
n = int(n)+1
for i in range(1, n+1, 1):
    x = f(x)
Nghiem_dung2 = x
X3 = round(y, e)
print(f"Nghiệm theo công thức tiên nghiệm chưa được làm tròn là {Nghiem_dung2}")
print(f"Nghiem dung theo công thức tiên nghiệm là {X3}")
print(f"Số lần lặp cần thiết của công thức tiên nghiệm là {n}")
