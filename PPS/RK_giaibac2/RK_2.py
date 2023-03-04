import numpy as np
import math
import scipy as sp
from scipy.integrate import odeint
import matplotlib.pyplot as plt

#giai ptvp y" + xy' + y = 0
def f(x,y,z):
    return z - 2*y
def g(x,y,z):
    return -2*z + y
def RK4(x0, y0, z0, x, stepnum):
    h = (x-x0)/stepnum
    xk =[x0]
    yk =[y0]
    zk =[z0]
    print("xk", "".ljust(2),"|","yk", "".ljust(22), "|","zk".ljust(25))
    print("-----------------------------------------------------")
    print(x0,"".ljust(3),"|", y0,"".ljust(23),"|",z0,"".ljust(25))
    for i in range(stepnum):
        k1 = h*f(x0, y0 ,z0)
        l1 = h*g(x0, y0, z0)
        k2 = h*f(x0 + h/2, y0 + k1/2,z0 +l1/2)
        l2 = h*g(x0 + h/2, y0 + k1/2, z0 + l1/2)
        k3 = h*f(x0 + h/2, y0 + k2/2, z0 + l2/2)
        l3 = h*g(x0 + h/2, y0 + k2/2, z0 + l2/2)
        k4 = h*f(x0 + h, y0 + k3, z0 + l3)
        l4 = h*g(x0 + h, y0 + k3, z0 + l3 )
        y0 = y0 + 1/6*(k1 + 2*k2 + 2*k3 + k4)
        z0 = z0 + 1/6*(l1 + 2*l2 + 2*l3 + l4)
        x0 = x0 + h
        print(str(np.round(x0,3)).ljust(5) , "|", str(y0).ljust(25),"|",str(z0).ljust(7))
        xk.append(x0)
        yk.append(y0)
        zk.append(z0)
    return np.asarray(xk), np.asarray(yk), np.asarray(zk)

s1, s2, s3 = RK4(3.0,-0.5,1.0,6.0,0.1)
#--------------------------------
#hàm giải sẵn ptvp cấp 2 trong thư viện scipy của python
def h(y,x):
    y0 = y[0]
    y1 = y[1]
    y2 = -x*y1 - y0
    return y1, y2
#khoi tao y, y' tai x = 0
init = 0, 1
x = np.linspace(0,0.5,6)
sol = odeint(h, init, x)
kq = sol[:,0]
print('\nNghiệm tìm theo hàm trong thư viện của Pyhton')
print(sol)
ss = []
print('\nSai số:')
for i in range(0, len(s1)):
    print(math.fabs(kq[i] - s2[i]))
plt.plot(s1, s2, 'go--', label = 'RK4')
plt.plot(s1, kq, 'ro--', label = 'Nghiệm đúng')
plt.title('Biểu diễn nghiệm')
plt.legend()
plt.show()