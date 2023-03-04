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
def vietaFormula(coeff:list,x0:float) -> list: # nhận vào hệ số bậc tăng dần coeff và số thực x0, trả về list hệ số của tích 2 đa thức khi nhân vào
                                                #VD: vietaFormula([2,-3,1],3) -> [-6,11,-6,1]
    coeff = [0]+coeff
    n = len(coeff)
    for i in range(n-1):
        coeff[i]-=x0*coeff[i+1]
    return coeff # 3(n-1) mul
def plus_poly(high: list,low: list)->list: # cộng 2 đa thức, high là đa thức hệ số bậc cao hơn, low là đa thức hệ số bậc thấp hơn, trả về list hệ số
                                            #VD: plus_poly([1,2,3,4],[2,4,3]) -> [3,6,6,4]
    n = len(high)
    if n==1:
        return high
    for i in range(n-1):
        high[i]+=low[i]
    return high # len(high) cal
def mul_list(kirito: list,ratio: float) -> list: # VD: mul_list([1,2,3,4],1111) -> [1111,2222,3333,4444]
    return [i*ratio for i in kirito] # len(kirito) cal


def deri(ys:list): # trả về sai phân cấp tiếp theo của list ys
                    # VD: deri([1,5,2,3]) -> [4,-3,1]
    n = len(ys)
    return [ys[i+1]-ys[i] for i in range(n-1)]    # len(ys)*2 cal
def nodes(xs:list,c:float): #nhận vào list xs và số thực c, trả về
    p=len(xs)
    h=xs[1]-xs[0]
    j=0
    if abs(c-xs[0]) < abs(c-xs[-1]):
        i=0
        while abs(xs[i]-c)>=h:
            i+=1
        if xs[i]==c:
            exit(colored(f'VALUE KNOWN AT {c} IS: {ys[i]}','blue'))
        while xs[i+j+1]-xs[i-j]<=1 and 0<=i+j+1<=p-1 and 0<=i-j<=p-1:
            j+=1
        j-=1
        if j<=1:
            if (i-2)*(i+3)>=0:
                
                return range(i-2,i+4)
            else:
                exit(colored('Too few usable data points to perform Bessel Interpolation'.upper(),'red'))
        elif 2<=j<=3:
            
            return range(i-j,i+j+2)
        else:
            
            return range(i-3,i+5)
    else:
        i=-1
        if xs[i]==c:
            exit(colored(f'VALUE KNOWN AT {c} IS: {ys[i]}','blue'))
        while abs(xs[i]-c)>h:
            i-=1
        i-=1
        if xs[i]==c:
            exit(colored(f'VALUE KNOWN AT {c} IS: {ys[i]}','blue'))
        while xs[i+j+1]-xs[i-j]<=1 and -p<=i+j+1<=-1 and -p<=i-j<=-1:
            j+=1
        j-=1
        if j<=1:
            if (i-2)*(i+3)>0:
                
                return range(i-2,i+4)
            else:
                exit(colored('Too few usable data points to perform Bessel Interpolation'.upper(),'red'))
        elif 2<=j<=3:
            
            return range(i-j,i+j+2)
        else:
            
            return range(i-3,i+5)
def Bessel_overhaul(xs:list,ys:list,x:float):
    # NOTE: all the below listed calculations only consider the highest power, which is 2, and its coefficients

    ys = [ys[i] for i in nodes(xs,x)] ### phải cắt ys trước nếu không sẽ ảnh hưởng algorithm
    xs = [xs[i] for i in nodes(xs,x)]
    print(colored(f'Required nodes for x = {x} and corresponding values:','yellow'))
    for m,i in enumerate(xs):
        print([xs[m],ys[m]])
    h = xs[1]-xs[0]
    n = int(len(xs)/2-1)
    x0=xs[n]
    y0 = ys[n]
    t = (x-xs[n])/h-1/2
    coeff=[-0.25,1]
    t_coeffs = []
    even_coeff = [(ys[n]+ys[n+1])/2]
    ys = deri(ys[:])
    odd_coeff = [ys[n]]
    fact = 1
    for i in range(1,n+1):
        ys = deri(ys[:]) #n^2 cal total
        
        fact *= 2*i
        diff = (ys[n-i]+ys[n-i+1])/(2*fact)
        even_coeff = plus_poly(mul_list(coeff,diff),even_coeff) # n^2 cal
        ys = deri(ys[:]) #n^2 cal
        
        fact *= 2*i+1
        diff = ys[n-i]/fact
        odd_coeff = plus_poly(mul_list(coeff,diff),odd_coeff) # n^2 cal
        coeff = vietaFormula(coeff,(i+1/2)**2) #3n^2/2 cal
    
    odd_coeff = np.array(odd_coeff)
    even_coeff = np.array(even_coeff)
    t_sq = t**2
    q = 1
    t_vec = [[1]]
    for i in range(n):
        q *= t_sq
        t_vec.append([q])
    t_vec = np.array(t_vec)

    print(colored(f'Bessel interpolation deduces value at x = {x}: ','yellow'))
    print(even_coeff@t_vec + t*(odd_coeff@ t_vec))
    for j in range(n+1):
            t_coeffs.append(even_coeff[j])
            t_coeffs.append(odd_coeff[j])
    print(colored(f'The coefficients with respect to t = (x-{x0})/{h}- 1/2 (increasing power) are:','magenta'))
    print(t_coeffs)
    
    print(colored(f'Substituting t = {t} into the derived polynomial with respect to t deduces the value at {x}:','green'))
    print(sum(i*t**m for m,i in enumerate(t_coeffs))) #neglected (exists for the sake of substantiation)

    error = abs(ys[0]/fact*prod((t_sq-(i+0.5)**2 for i in range(n+1))))
    print(colored('The error expectation shall not exceed: ','yellow'))
    print(error)
def main():
    if not xs[0] <= start <= xs[-1]:
        exit(colored('EXTRAPOLATION NOT INCLUDED','red'))
    Bessel_overhaul(xs,ys,start)
main()