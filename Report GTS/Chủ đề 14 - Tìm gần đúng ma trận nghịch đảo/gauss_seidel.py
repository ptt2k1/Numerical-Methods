import numpy as np
from math import *


def norm(A, normType = 2):
    return np.linalg.norm(A, normType)


def check_dom(A, n):
    K = np.abs(np.diag(A))
    sumRow = np.sum(np.abs(A), axis = 1) - K
    sumCol = np.sum(np.abs(A), axis = 0) - K
    
    if (all(K[i] > sumRow[i] for i in range(n))): return 1
    if (all(K[i] > sumCol[i] for i in range(n))): return -1
    return 0


def get_norm(A, domStatus):
    if (domStatus == 1): return norm(A, inf)
    return norm(A, 1)


def get_lambda(A, domStatus):
    if (domStatus == 1): return 1
    K = np.abs(np.diag(A))
    return max(K) / min(K)


def get_q(B, n, domStatus):
    q = 0
    if (domStatus == 1):
        for i in range(n):
            q1 = q2 = 0
            for j in range(i, n): q1 += abs(B[i, j])
            for j in range(i): q2 += abs(B[i, j])
            q = max(q, q1 / (1 - q2))
    else:
        for i in range(n):
            q1 = q2 = 0
            for j in range(i+1): q1 += abs(B[j, i])
            for j in range(i+1, n): q2 += abs(B[j, i])
            q = max(q, q1 / (1 - q2))
    return q


def get_S(B1, n):
    S = 0
    for i in range(n):
        tmp = 0
        for j in range(i+1, n): tmp += abs(B1[j, i])
        S = max(S, tmp)
    return S


def next_iter(oldX, B, T, n):
    newX = np.full((n, n), float(0))
    for i in range(n):
        for j in range(i): newX[i] += B[i, j] * newX[j]
        for j in range(i, n): newX[i] += B[i, j] * oldX[j]
        newX[i] += T[i]
    return newX


def predecessor_iter(X0, B, T, n, domStatus, lda, S, q, eps):
    qk = 1
    X = X0
    X1 = next_iter(X0, B, T, n)
    norm = get_norm(X1 - X0, domStatus)
    error = eps * (1-q) * (1-S) / (lda * norm)

    nr_iter = 0
    while (qk > error):
        X = next_iter(X, B, T, n)
        qk *= q
        nr_iter += 1
    
    print(f"Phương pháp Gauss-Seidel đánh giá tiên nghiệm kết thúc sau {nr_iter} bước lặp.")
    return X


def successor_iter(X0, B, T, n, domStatus, lda, S, q, eps):
    oldX = X0
    newX = next_iter(X0, B, T, n)
    error = eps * (1-q) * (1-S) / (lda * q)

    nr_iter = 0
    while (get_norm(newX - oldX, domStatus) > error):
        oldX = newX
        newX = next_iter(newX, B, T, n)
        nr_iter += 1

    print(f"Phương pháp Gauss-Seidel đánh giá hậu nghiệm kết thúc sau {nr_iter} bước lặp.")
    return newX


def gauss_seidel_inverse(A, n, eps, mode):
    domStatus = check_dom(A, n)

    if (domStatus == 0):
        print("A không chéo trội nên không đưa ra được ma trận nghịch đảo.")
        return np.full((n, n), float("NaN"))

    E   = np.eye(n)
    T   = np.diag(1 / np.diag(A))
    B   = E - T @ A
    lda = get_lambda(A, domStatus)

    if (domStatus == 1):
        print("A chéo trội hàng.")
        S = 0
        q = get_q(B, n, domStatus)
    else:
        print("A chéo trội cột.")
        B1 = E - A @ T
        S = get_S(B1, n)
        q = get_q(B1, n, domStatus)

    if (mode == 1):
        return predecessor_iter(A, B, T, n, domStatus, lda, S, q, eps)
    else:
        return successor_iter(A, B, T, n, domStatus, lda, S, q, eps)


mode = np.loadtxt("input.txt", max_rows = 1)
eps = np.loadtxt("input.txt", skiprows = 1, max_rows = 1)
A = np.loadtxt("input.txt", skiprows = 2)
n = len(A)

print("-" * 68)
print("Ma trận A:")
print(A)
print("-" * 68)

print("-" * 68)
A1 = gauss_seidel_inverse(A, n, eps, mode)
print("Ma trận nghịch đảo của A:")
print(A1)

print("-" * 68)
print("Kiểm tra nhân ngược:")
print(A1 @ A)
