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


def get_q(B, E, T, A, domStatus):
    if (domStatus == 1): return get_norm(B, domStatus)
    return get_norm(E - A @ T, domStatus)


def predecessor_iter(X0, B, T, domStatus, lda, q, eps):
    norm = get_norm((B @ X0 + T) - X0, domStatus)
    X = X0
    qk = 1
    error = eps * (1 - q) / (lda * norm)

    nr_iter = 0
    while (qk > error):
        X = B @ X + T
        qk *= q
        nr_iter += 1

    print(f"Phương pháp Jacobi đánh giá tiên nghiệm kết thúc sau {nr_iter} bước lặp.")
    return X


def successor_iter(X0, B, T, domStatus, lda, q, eps):
    oldX = X0
    newX = B @ X0 + T
    error = eps * (1 - q) / (lda * q)

    nr_iter = 0
    while (get_norm(newX - oldX, domStatus) > error):
        oldX = newX
        newX = B @ oldX + T
        nr_iter += 1

    print(f"Phương pháp Jacobi đánh giá hậu nghiệm kết thúc sau {nr_iter} bước lặp.")
    return newX


def jacobi_inverse(A, n, eps, mode):
    domStatus = check_dom(A, n)

    if (domStatus == 0):
        print("A không chéo trội nên không đưa ra được ma trận nghịch đảo.")
        return np.full((n, n), float("NaN"))

    E   = np.eye(n)
    T   = np.diag(1 / np.diag(A))
    B   = E - T @ A
    lda = get_lambda(A, domStatus)
    q   = get_q(B, E, T, A, domStatus)

    if (domStatus == 1):
        print("A chéo trội hàng.")
    else:
        print("A chéo trội cột.")

    if (mode == 1):
        return predecessor_iter(A, B, T, domStatus, lda, q, eps)
    else:
        return successor_iter(A, B, T, domStatus, lda, q, eps)


mode = np.loadtxt("input.txt", max_rows = 1)
eps = np.loadtxt("input.txt", skiprows = 1, max_rows = 1)
A = np.loadtxt("input.txt", skiprows = 2)
n = len(A)

print("-" * 68)
print("Ma trận A:")
print(A)
print("-" * 68)

print("-" * 68)
A1 = jacobi_inverse(A, n, eps, mode)
print("Ma trận nghịch đảo của A:")
print(A1)

print("-" * 68)
print("Kiểm tra nhân ngược:")
print(A1 @ A)
