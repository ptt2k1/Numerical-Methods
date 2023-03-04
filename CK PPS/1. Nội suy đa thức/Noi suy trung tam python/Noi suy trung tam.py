import matplotlib.pyplot as plt
import pandas as pd
import numpy as np

def hoocnerChia(a, value):
    b = []
    b.append(a[0])
    for i in range(1, len(a)):
        b.append(b[i - 1] * value + a[i])
    return b

def hoocnerNhanBosung(coeffPolyTich, t):
    if len(coeffPolyTich) == 0:
        coeffPolyTich.append(1)
        coeffPolyTich.append(-t)
    else:
        coeffPolyTich.append(0)
        temp = coeffPolyTich.copy()
        for i in range(1, len(coeffPolyTich)):
            coeffPolyTich[i] = coeffPolyTich[i] - t * temp[i - 1]
    return coeffPolyTich

def saiPhan(f_0, f_1, yk, x0, xk):
    if xk > x0:
        temp = f_0.copy()
        f_0.append(0)
        f_0[0] = yk
        for i in range(1, len(f_0)):
            f_0[i] = f_0[i - 1] - temp[i - 1]
        f_1.append(f_0[len(f_0) - 1])
    else:
        temp = f_1.copy()
        f_1.append(0)
        f_1[0] = yk
        for i in range(1, len(f_1)):
            f_1[i] = temp[i - 1] - f_1[i - 1]
        f_0.append(f_1[len(f_1) - 1])
    return f_0, f_1

def noiSuyTrungTamGauss(heSodaThuc, coeffTich, f_0, f_1, k, x0, xk, yk, giaiThua_k, choose):
    if k == 0:
        heSodaThuc.append(yk)
        f_0.append(yk)
        f_1.append(yk)
        giaiThua_k = 1
    else:
        heSoDaThucCu = heSodaThuc.copy()
        f_0, f_1 = saiPhan(f_0, f_1, yk, x0, xk)
        giaiThua_k *= len(heSodaThuc)

        #do qua trinh thay t vao thi k sai lech 1 don vi
        if choose == "Gauss1":
            if k > 0:
                k = k - 1
        if choose == "Gauss2":
            if k < 0:
                k = k + 1
        coeffTich = hoocnerNhanBosung(coeffTich, - k)
        c = f_1[len(f_1) - 1] / giaiThua_k
        heSodaThuc.append(0)
        for i in range(1, len(heSodaThuc)):
            heSodaThuc[i] = heSoDaThucCu[i - 1] + c * coeffTich[i]
        heSodaThuc[0] = c
    return heSodaThuc, coeffTich, f_0, f_1, giaiThua_k

def sortData(path):
    if path.endswith('.csv'):
        data = pd.read_csv(path)
    elif path.endswith('.txt'):
        print("su dung doc file .txt")
    dataX = data['x']
    dataY = data['y']
    for i in range(len(dataX)):
        indexMin = i
        for j in range(i + 1, len(dataX)):
            if dataX[j] < dataX[indexMin]:
                indexMin = j
        if indexMin != i:
            dataX[indexMin], dataX[i] = dataX[i], dataX[indexMin]
            dataY[indexMin], dataY[i] = dataY[i], dataY[indexMin]
    return dataX, dataY

path = 'input.csv'
dataX, dataY = sortData(path)
def kiemTraMoc(dataX, x0):
    h = dataX[1] - dataX[0]
    m = round((x0 - dataX[0])/h)
    if x0 > dataX[len(dataX) - 1] or x0 < dataX[0] or x0 - (dataX[0] + m * h) > 1e-10:
        print("Moc noi suy duoc chon ban dau la khong hop le")
        print("Quay lai, chon lai moc noi suy")
    else:
        print("Moc noi suy ban dau hop le. Chuong trinh tiep tuc")
