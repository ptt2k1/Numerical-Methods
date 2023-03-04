from math import*
import numpy as np
import matplotlib.pyplot as plt
#Các hằng số sử dụng
ETA = 1e-12
EPSILON = 1e-6
BETA = 1e-10
META = 1e-15
GIGA = 1e-9 
# def sign(x):
#     if x==0: return 0
#     return 1 if x>0 else -1

#Nhập bộ hệ số 
def nhapdulieu(n):
    global heso
    heso = []
    for i in range(n+1):
        a = float(input(f"He so a{n-i}: "))
        heso.append(a)
    return heso 

#Điều chỉnh eta
def fixeta(dfx0,x0,sgn):
        eta = ETA
        while sgn*df(x0+sgn*eta*dfx0)>=0 and abs(eta*dfx0)<1:
            eta *= 2
        while sgn*df(x0+sgn*eta*dfx0)<=0:
            eta /= 2
        return eta

#khoảng cách ly nghiệm
def distance_of_root(f, a, b):
    #EPSILON = 1e-6
    global df
    sgn = 1 if f(a+EPSILON) >= f(a) else -1
    count = 0
    x0 = a + 0.5*EPSILON
    eta = ETA
    xr = [a]
    while x0 < b:
        dx = df(x0)
        while abs(dx) > 0.001*EPSILON:
            dx = df(x0)
            eta = fixeta(dx,x0,sgn)
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

#Tìm nghiệm
def finding_root(f,a,b):
    za = np.sign(f(a))
    zb = np.sign(f(b))
    #GIGA = 1e-9 
    #BETA = 1e-10
    if abs(f(a)) <GIGA:
        return a,0
    if abs(f(b)) <GIGA:
        return b,0
    count = 0
    while abs(b-a) > BETA:
        c = (a+b)/2
        count += 1
        zc = np.sign(f(c))
        # if abs(f(c)) < GIGA:
        #     break
        if zc == 0: 
            break
        if zc*za>0:
            a = c
        else: 
            b = c
    return c,count



def f(x):
        y = 0
        for i in range(n+1):
            y += heso[n-i]*x**i
        return y



def df(x):
        y = 0 
        for i in range(n,0,-1):
            y += i*heso[n-i]*x**(i-1)
        return y


def main():
    global df, n
    n = int(input("Bac cua da thuc: "))
    heso = nhapdulieu(n)
    
    
    """ 
    Tính khoảng chứa nghiệm
    """
    #Công thức 1
    r = 1 + max(map(abs,heso))/abs(heso[0])
    print("Khoảng chứa nghiệm:",[-r,r])
    

    print("khoảng chứa nghiệm", [-r,r])
    # Tìm các cực trị bằng GDA
    result = distance_of_root(f,-r,r)
    print("Các cực trị có thể =",result)
    p=[]
    # xét kc ly nghiệm và sử dụng chia đôi trên các khoảng cách ly nghiệm
    for i in range (len(result)-1):
        ai = result[i]
        bi = result[i+1]
        ll = int(log2((bi-ai)/BETA)+1)
        print("Ước lượng số lần chia đôi",ll)
        x,l = finding_root(f,ai,bi)
        p.append(x)
        print("Số lần chia đôi",l)

    xr1 = set(p)
    print(xr1)
if __name__ == "__main__":
    main()
# x^3-x^2
# x^3 - 525x^2 + 91250x -5250000
# {200.0000000000001, 149.9999999999999, 175.00000000000063}
