#!/usr/bin/env python
# coding: utf-8

# In[4]:

#Hướng dẫn sử dụng: File bao gồm 4 chương trình : chương trình tìm hệ số của phương pháp, thuật toán AB 4 bước, thuật toán AM 4 bước, THuật toán AB-AM 4 bước dự báo hiệu chỉnh
#Để giải bài toán Cauchy chỉ cần nhập vào các biến x0,y0,xf,n và hàm def f(x,y)
import numpy as np
from math import factorial
from fractions import Fraction
n=4
A=np.zeros((n,n))
A[0,0]=1
A[n-1,n-1]=(-1)**n
for i in range(1,n):
    for j in range (0,i+1):
        A[i,j]=(-1)**(j)*factorial(i)//(factorial(j)*factorial(i-j))
print(A)


# In[5]:


import numpy as np
A_1=np.zeros(n)
A_1[0]=1
for i in range (1,n):
    A_1[i]=1-sum(A_1[:(i)]*(1/(np.arange(i+1,1,-1))))
    print(A_1[i])


# In[6]:


import numpy as np
B=np.zeros(n)
B[0]=1
for i in range (1,n):
    B[i]=0-sum(B[:(i)]*(1/(np.arange(i+1,1,-1))))
    print(B[i])


# In[13]:


import numpy as np
B=[[1,-1/2,-0.08333333333333331,-0.041666666666666685]]


# In[14]:


C=np.matmul(B,A)
print(C)


# In[9]:


#cong thuc adam-bashforth-moulton du bao hieu chinh
import numpy as np
from matplotlib import pyplot as plt

x0 = 0
y0 = 0
xf = 1
n = 11
deltax = (xf-x0)/(n-1)
x = np.linspace(x0,xf,n)
def f(x,y):
	return y**2+x**2

y = np.zeros([n])
y[0] = y0
py = np.zeros([n])
for i in range(0,4):
	py[i] = None

for i in range(1,4):
	k1 = deltax*f(x[i-1],y0)
	k2 = deltax*f(x[i-1]+deltax/2,y0+k1/2)
	k3 = deltax*f(x[i-1]+deltax/2,y0+k2/2)
	k4 = deltax*f(x[i-1]+deltax,y0+k3)
	y[i] =  y0 + (k1 + 2*k2 + 2*k3 + k4)/6
	y0 = y[i]

for i in range(4,n):
	py[i] = deltax/24*(55*f(x[i-1],y[i-1]) - 59*f(x[i-2],y[i-2]) + 37*f(x[i-3],y[i-3]) - 9*f(x[i-4],y[i-4]) )  + y[i-1] 
	y[i] = deltax/24*( 9*f(x[i],py[i]) + 19*f(x[i-1],y[i-1]) - 5*f(x[i-2],y[i-2]) + f(x[i-3],y[i-3]) ) + y[i-1]

print("x_n\t   py_n\t           y_n")
for i in range(n):
	print (format(x[i],'6f'),"\t",format(py[i],'8f'),"\t",format(y[i],'8f'),"\t",py[i]-y[i])

plt.plot(x,y,'o')
plt.xlabel("Value of x")
plt.ylabel("Value of y")
plt.title("Approximation Solution with Adams-Bashforth-Moulton Method")
plt.show()


# In[20]:


import numpy as np
from matplotlib import pyplot as plt
x0 = 0
y0 = 1
xf = 5
n = 11
deltax = (xf-x0)/(n-1)
x = np.linspace(x0,xf,n)
def f(x,y):
	return  -2*y

y = np.zeros([n])
y[0] = y0
py = np.zeros([n])
for i in range(0,4):
	py[i] = None
for i in range(1,4):
	k1 = deltax*f(x[i-1],y0)
	k2 = deltax*f(x[i-1]+deltax/2,y0+k1/2)
	k3 = deltax*f(x[i-1]+deltax/2,y0+k2/2)
	k4 = deltax*f(x[i-1]+deltax,y0+k3)
	y[i] =  y0 + (k1 + 2*k2 + 2*k3 + k4)/6
	y0 = y[i]
for i in range(4,n):
    y[i] =deltax/24*(55*f(x[i-1],y[i-1]) - 59*f(x[i-2],y[i-2]) + 37*f(x[i-3],y[i-3]) - 9*f(x[i-4],y[i-4]) )  + y[i-1] 
print("x_n\t            y_n")
for i in range(n):
	print (format(x[i],'6f'),"\t","\t",format(y[i],'6f'))

plt.plot(x,y,'o')
plt.xlabel("Value of x")
plt.ylabel("Value of y")
plt.title("Approximation Solution with Adams-Bashforth Method")
plt.show()


# In[19]:


import numpy as np
from matplotlib import pyplot as plt

x0 = 0
y0 = 1
xf = 5
n = 11
deltax = (xf-x0)/(n-1)
x = np.linspace(x0,xf,n)
def f(x,y):
    return -2*y

y = np.zeros([n])
y[0] = y0



for i in range(1,n):
    k1 = deltax*f(x[i-1],y0)
    k2 = deltax*f(x[i-1]+deltax/2,y0+k1/2)
    k3 = deltax*f(x[i-1]+deltax/2,y0+k2/2)
    k4 = deltax*f(x[i-1]+deltax,y0+k3)
    y[i] =  y0 + (k1 + 2*k2 + 2*k3 + k4)/6
    y0 = y[i]

for i in range(4,n):
    y[i] = deltax/24*( 9*f(x[i],y[i]) + 19*f(x[i-1],y[i-1]) - 5*f(x[i-2],y[i-2]) + f(x[i-3],y[i-3]) ) + y[i-1]

print("x_n\t        y_n")
for i in range(n):
    print (format(x[i],'6f'),"\t",format(y[i],'6f'))

plt.plot(x,y,'o')
plt.xlabel("Value of x")
plt.ylabel("Value of y")
plt.title("Approximation Solution with Adams-Moulton Method")
plt.show()


# In[21]:


import numpy as np
from matplotlib import pyplot as plt

x0 = 0
y0 = 1
xf = 5
n = 11
deltax = (xf-x0)/(n-1)
x = np.linspace(x0,xf,n)
def f(x,y):
	return -2*y

y = np.zeros([n])
y[0] = y0
py = np.zeros([n])
for i in range(0,4):
	py[i] = None

for i in range(1,4):
	k1 = deltax*f(x[i-1],y0)
	k2 = deltax*f(x[i-1]+deltax/2,y0+k1/2)
	k3 = deltax*f(x[i-1]+deltax/2,y0+k2/2)
	k4 = deltax*f(x[i-1]+deltax,y0+k3)
	y[i] =  y0 + (k1 + 2*k2 + 2*k3 + k4)/6
	y0 = y[i]

for i in range(4,n):
	py[i] = deltax/24*(55*f(x[i-1],y[i-1]) - 59*f(x[i-2],y[i-2]) + 37*f(x[i-3],y[i-3]) - 9*f(x[i-4],y[i-4]) )  + y[i-1] 
	y[i] = deltax/24*( 9*f(x[i],py[i]) + 19*f(x[i-1],y[i-1]) - 5*f(x[i-2],y[i-2]) + f(x[i-3],y[i-3]) ) + y[i-1]

print("x_n\t   py_n\t           y_n")
for i in range(n):
	print (format(x[i],'6f'),"\t",format(py[i],'8f'),"\t",format(y[i],'8f'))

plt.plot(x,y,'o')
plt.xlabel("Value of x")
plt.ylabel("Value of y")
plt.title("Approximation Solution with Adams-Bashforth-Moulton Method")
plt.show()


# In[ ]:




