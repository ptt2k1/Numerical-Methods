#THIS FILE IS USED TO CHECK ACCURACY OF INTERPOLATION METHODS USING SPECIFIC FUNCTIONS

from math import *
from sympy import *
from os import system
from termcolor import colored
system('cls')
k = open('Interpolation.txt','w', encoding='utf-8') # write into interpolation.txt, deleting previous content
x = symbols('x')
f = lambdify(x, 'x**(5/3)+1/x' ,'math') # adjust your function here
xs = [i/8 for i in range(10,30)]
xs_str = [str(i) for i in xs]
fx = [f(i) for i in xs]
fx_str = [str(f(i)) for i in xs]
n = len(xs)
t = 2.88 # insert the number t you wish to find the exact f(t)
k.writelines([str(xs[i])+'\t'+str(f(xs[i]))+'\n' for i in range(n)]+['Cần tính giá trị tại:\n']+[str(t)])
k.close

print(colored(f'The exact value of f({t}) is:','green'))
print(f(t))