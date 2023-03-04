#Khai báo thư viện
import math
import numpy as np 
import numpy.linalg as la

#Sai số
eps = 1e-6

#Nhận ma trận
A = np.loadtxt('D:\input.txt')

#Ma trận đơn vị
I = np.eye(A.shape[1], dtype = float)

#Chuẩn hóa
def normalize(x):
    for i in range(len(x)):
        if (abs(x[i]) == la.norm(x,np.inf)):
            x = x/x[i]
    return x

#Tính A^m*x và lưu vào cột m của B
def calc(A,m,B):
    M = np.dot(A,B[m-1])
    M = normalize(M)
    B.append(M)
    return

#vi_tri_khu = 0
#def calc(A,m,B):
    M = np.dot(A,B[m-1])
    global vi_tri_khu
    if (m <= 3):
        M = normalize(M)
        if (m == 3):
            for i in range(A.shape[1]):
                if (M[i] == 1):
                    vi_tri_khu = i
    else: M = M/M[vi_tri_khu]
    B.append(M)
    return

#Kiểm tra
def check(A,x):
    B = []
    B.append(x)
    calc(A,1,B)
    calc(A,2,B)
    m = 2
    TH = 3
    while True:
        if m == 3012:
            break   
        if (la.norm(B[m]-B[m-1],np.inf) < eps):
            TH = 1
            break
        if (la.norm(B[m]-B[m-2],np.inf) < eps): 
            TH = 2
            break
        m+=1
        calc(A,m,B)
    return m,TH,B

#Xuống thang đơn, thực
def deflate1(A,v):
    for i in range(len(v)):
        if (v[i] == 1): 
            index = i
            break
    theta = I
    for i in range(theta.shape[1]): 
        theta[i,index] = theta[i,index] - v[i]
    A = np.dot(theta,A)
    return A,theta

#Xuống thang trái dấu, thực
def deflate2(A,v1,v2):    
    A,theta1 = deflate1(A,v1)
    v2 = normalize(np.dot(theta1,v2))
    A,theta2 = deflate1(A,v2)
    return A

#Xử lý TH1
def TH_1(x,i,A,B):
    v = B[-1]
    Am1 = np.dot(A,B[-1])
    for j in range(A.shape[1]):
        if (Am1[j,0] != 0):
            lamda = Am1[j,0]/B[-1][j,0]
            break
    print('lamda = ', lamda)
    if (i==0):
        print('v = ')
        print(v)
    print('-' * 100)
    A,theta = deflate1(A,v)
    i += 1
    return A,i

#Xử lý TH2
def TH_2(x,i,A,m,B):
    Am1 = np.dot(A,B[-1])
    Am2 = np.dot(A,Am1)
    Am3 = np.dot(A,Am2)
    for j in range(A.shape[1]):
        if (Am1[j,0] != 0):
            lamda1 = math.sqrt(Am3[j,0]/Am1[j,0])
            lamda2 = -math.sqrt(Am3[j,0]/Am1[j,0])
            break
    if (m%2 == 0):
        v1 = normalize(Am2 + lamda1*Am1)
        v2 = normalize(Am2 + lamda2*Am1)
    else:
        v1 = normalize(Am3 + lamda1*Am2)
        v2 = normalize(Am3 + lamda2*Am2)
    print('lamda1 = ', lamda1)
    if (i==0):
        print('v1 = ')
        print(v1)
    print('lamda2 = ', lamda2)
    if (i==0):
        print('v2 = ')
        print(v2) 
    print('-' * 100)
    A = deflate2(A,v1,v2)
    i += 2
    return A,i

#Xử lý TH3
def TH_3(x,i,A,B):
    Am = B[-1]
    Am1 = np.dot(A,Am)
    Am2 = np.dot(A,Am1)
    for j in range(A.shape[1]):
        if (Am1[j,0] != 0):
            a1 = Am2[j][0]
            b1 = Am1[j][0]
            c1 = Am[j][0]
            break
    for j in range(A.shape[1]):
        if (Am1[j,0] == b1): continue
        if (Am1[j,0] != 0):
            a2 = Am2[j][0]
            b2 = Am1[j][0]
            c2 = Am[j][0]
    a = 1
    b = (a1 * c2 - c1 * a2) / (c1 * b2 - b1 * c2)
    c = (a1 * b2 - b1 * a2) / (b1 * c2 - c1 * b2)
    delta = b ** 2 - 4 * a * c
    if (delta < 0):
        lamda1 = complex(-b / (2 * a), -math.sqrt(abs(delta)) / (2 * a))
        lamda2 = complex(-b / (2 * a), math.sqrt(abs(delta)) / (2 * a))
        v1 = normalize(Am2 - lamda2 * Am1)
        v2 = normalize(Am2 - lamda1 * Am1)
        print('lamda1 = ', lamda1)
        if (i==0):
            print('v1 = ')
            print(v1)
        print('lamda2 = ', lamda2)
        if (i==0):
            print('v2 = ')
            print(v2) 
        print('-' * 100)
    else: print('delta khong am')

#Chương trình chính
def power_iteration(A):
    print(A)
    gtr,vtr = la.eig(A)
    print('Kiem tra lai cac gtr o day:')
    print(gtr)
    print('Kiem tra lai cac vtr o day:')
    print(vtr)
    x = np.random.rand(A.shape[1],1)
    i=0
    while (i<A.shape[1]):
        m,TH,B = check(A,x)
        print(m,' lần lặp')
        print('Đây là TH ',TH)
        if (TH == 1): 
            A,i = TH_1(x,i,A,B)
        if (TH == 2): 
            A,i = TH_2(x,i,A,m,B)
        if (TH == 3): 
            TH_3(x,i,A,B)
            break

power_iteration(A)