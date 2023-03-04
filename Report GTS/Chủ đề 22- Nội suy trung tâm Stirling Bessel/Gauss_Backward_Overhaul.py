import numpy as np
from termcolor import colored
from math import prod
np.set_printoptions(precision=16)
m = open('interpolation.txt','r').readlines()
xs =[]
ys = []
for i in m[:-2]:
    k=i.strip().split('\t')
    xs.append(float(k[0]))
    ys.append(float(k[1]))
k = m[-1].strip().split(' ')
start = float(k[0])
def vietaFormula(coeff:list,x0:float) -> list:
    coeff = [0]+coeff
    n = len(coeff)
    for i in range(n-1):
        coeff[i]-=x0*coeff[i+1]
    return coeff # 3(n-1) mul
def plus_poly(high: list,low: list) -> list:
    n = len(low)
    for i in range(n):
        high[i]+=low[i]
    return high #len(high) cal
def mul_list(kirito: list,ratio: float) -> list:
    return [i*ratio for i in kirito] # len(kirito) cal


def deri(ys:list):
    n = len(ys)
    return [ys[i+1]-ys[i] for i in range(n-1)]    # len(ys)*2 cal
def nodes(xs:list,c:float)-> list:
    h = xs[1] - xs[0]
    p = len(xs)
    j = 0
    k = 0
    count = 1
    if abs(xs[0]-c)<abs(c-xs[-1]):
        i = 0
        while abs(c-xs[i])>h/2:
            i+=1
        while i+j<=p-1 and 0<=i-k:
            if count%2==1:
                k+=1
            else:
                j+=1
            count+=1
        j-=1
        k-=1
        if j+k+1 <=4:
            exit(colored('TOO FEW POINTS TO PERFORM GAUSS BACKWARD INTERPOLATION','red'))
        elif 4 < j+k+1< 7:
            return range(i-k,i+j+1)
        else:
            return range(i-3,i+4)
    else:
        i = -1
        while abs(c-xs[i])>h/2:
            i-=1
        while i-k>=-p and i+j<=-1:
            if count%2==1:
                k+=1
            else:
                j+=1
            count+=1
        j-=1
        k-=1
        if j+k+1 <=4:
            exit(colored('TOO FEW POINTS TO PERFORM GAUSS BACKWARD INTERPOLATION','red'))
        elif 4 < j+k+1< 7:
            return range(i-k,i+j+1)
        else:
            return range(i-3,i+4)
def Gauss_Backward_Overhaul(xs: list,ys: list,x: float):
    ys = [ys[i] for i in nodes(xs,x)]
    xs = [xs[i] for i in nodes(xs,x)]
    print(colored(f'Required nodes for x = {x} and corresponding values:','yellow'))
    for m,i in enumerate(xs):
        print([xs[m],ys[m]])
    h = xs[1]-xs[0]
    p = len(xs)
    coeff = [1]
    if p%2==0:
        cen = int(p/2 - 1)
    else:
        cen = int((p - 1)/2)
    x0 = xs[cen]
    y0 = ys[cen]
    t = (x-x0)/h
    t_coeffs = [] # t_coeffs chỉ chứa từ bậc 1
    fact = 1
    for i in range(1,p):
        ys = deri(ys[:]) 
        fact *= i
        if i%2!=0:
            multiplier = ys[cen - int((i + 1)/2)]/fact
            t_coeffs = plus_poly(mul_list(coeff,multiplier),t_coeffs)
            coeff = vietaFormula(coeff,-int((i + 1)/2))
        else:
            multiplier = ys[cen - int(i/2)]/fact
            t_coeffs = plus_poly(mul_list(coeff,multiplier),t_coeffs)
            coeff = vietaFormula(coeff,int(i/2))
    q = 1
    t_vec = []
    for i in range(1,p):
        q *= t
        t_vec.append([q])
    t_vec = np.array(t_vec)
    print(colored(f'Gauss Backward interpolation deduces value at x = {x}: ','yellow'))
    print(y0 + t_coeffs @ t_vec)
    t_coeffs = [y0] + t_coeffs
    print(colored(f'The coefficients with respect to t = (x-{x0})/{h} (increasing power) are:','magenta'))
    print(t_coeffs)
    print(colored(f'Substituting t = {t} into the derived polynomial with respect to t deduces the value at {x}:','green'))
    print(sum(i*t**m for m,i in enumerate(t_coeffs)))
def main():
    if not xs[0] <= start <= xs[-1]:
        exit(colored('EXTRAPOLATION NOT INCLUDED','red'))
    Gauss_Backward_Overhaul(xs,ys,start)
main()