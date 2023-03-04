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
def nodes(xs:list,c:float):
    p = len(xs)
    h = xs[1]-xs[0]
    j = 0
    if abs(c-xs[0]) < abs(c-xs[-1]):
        i=0
        while abs(xs[i]-c) > h/2:
            i+=1
        if xs[i]==c:
            exit(colored(f'VALUE KNOWN AT {c} IS: {ys[i]}','yellow'))
        while xs[i+j]-xs[i-j]<=1 and 0<=i+j<=p-1 and 0<=i-j<=p-1:
            j+=1
        j-=1
        if j<=1:
            if (i-2)*(i+2)>=0:
                return range(i-2,i+3)
            else:
                exit(colored('Too few usable data points to perform Stirling Interpolation'.upper(),'red'))
        elif 2<=j<=4:
            return range(i-j,i+j+1)
        else:
            return range(i-4,i+5)
    else:
        i=-1
        while abs(xs[i]-c) > h/2:
            i-=1
        if xs[i]==c:
            exit(colored(f'VALUE KNOWN AT {c} IS: {ys[i]}','yellow'))
        while xs[i+j]-xs[i-j]<=1 and -p<=i+j<=-1 and -p<=i-j<=-1:
            j+=1
        j-=1
        if j<=1:
            if (i-2)*(i+2)>0:
                return range(i-2,i+3) # min 5 nodes
            else:
                exit(colored('Too few usable data points to perform Stirling Interpolation'.upper(),'red'))
        elif 2<=j<=4:
            return range(i-j,i+j+1)
        else:
            return range(i-4,i+5) # max 9 nodes
def Stirling_overhaul(xs:list,ys:list,x:float):
    ys = [ys[i] for i in nodes(xs,x)] ### phải cắt ys trước nếu không sẽ ảnh hưởng algorithm
    xs = [xs[i] for i in nodes(xs,x)]
    print(colored(f'Required nodes for x = {x} and corresponding values:','yellow'))
    for m,i in enumerate(xs):
        print([xs[m],ys[m]]) # in ra các điểm đã lấy
    h = xs[1]-xs[0] # khoảng cách giữa 2 mốc nội suy liên tiếp
    n = int((len(xs)-1)/2) # là giá trị n trong công thức
    x0 = xs[n] # why not? :v It's comprehensible
    y0 = ys[n]
    t = (x-x0)/h # là giá trị t trong lý thuyết
    coeff = [1] # sẽ được giải thích sau
    t_coeffs = [y0] # kết thúc sẽ chứa toàn bộ hệ số theo ẩn t
    even_coeff = [] # kết thúc sẽ chứa toàn bộ hs bậc chẵn
    odd_coeff = [] # kết thúc sẽ chữa toàn bộ hs bậc lẻ
    fact = 1
    for i in range(n):
        ys = deri(ys[:])
        
        fact *= 2*i+1
        a_i = (ys[n-i]+ys[n-i-1])/(2*fact) # chính là a_i trong phân tích gói
        odd_coeff = plus_poly(mul_list(coeff,a_i),odd_coeff)
        ys = deri(ys[:])
        
        fact *= 2*i+2
        b_i = ys[n-i-1]/fact # chính là b_i trong phân tích gói
        even_coeff = plus_poly(mul_list(coeff,b_i),even_coeff)
        coeff = vietaFormula(coeff,(i+1)**2)
        
    odd_coeff = np.array(odd_coeff)
    even_coeff = np.array(even_coeff)
    t_sq = t**2
    q = 1
    t_vec = []
    for i in range(1,n+1):
        q *= t_sq
        t_vec.append([q])
    t_vec = np.array(t_vec)
    print(colored(f'Stirling interpolation deduces value at x = {x}: ','yellow'))
    print(y0 + even_coeff @ t_vec + (odd_coeff @ t_vec) / t)
    for j in range(n):
        t_coeffs.append(odd_coeff[j])
        t_coeffs.append(even_coeff[j])
    print(colored(f'The coefficients with respect to t = (x-{x0})/{h} (increasing power) are:','magenta'))
    print(t_coeffs)
    
    print(colored(f'Substituting t = {t} into the derived polynomial with respect to t deduces the value at {x}:','green'))
    print(sum(i*t**m for m,i in enumerate(t_coeffs))) #neglected (exists for the sake of substantiation)

    error = abs(ys[0]/fact*t*prod((t_sq-i**2 for i in range(1,n+1))))
    print(colored('The error expectation shall not exceed: ','yellow'))
    print(error)
def main():
    if not xs[0] <= start <= xs[-1]:
        exit(colored('EXTRAPOLATION NOT INCLUDED','red'))
    Stirling_overhaul(xs,ys,start)
main()