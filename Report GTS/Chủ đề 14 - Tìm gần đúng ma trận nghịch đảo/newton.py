import numpy as np


def norm(A, normType = 2):
    return np.linalg.norm(A, normType)


def newton_iter(A, n, eps, X0):
    E = np.eye(n)
    X = X0
    q2k = q = norm(E - A @ X0)
    error = eps * (1 - q) / norm(X0) 

    if(q == 1):
        print("Ma trận A không khả nghịch.")
        return np.full((n, n), float("NaN"))

    nr_iter = 0
    while(q2k >= error):
        X = X @ (2 * E - A @ X)
        q2k = q2k ** 2
        nr_iter += 1

    print(f"Phương pháp Newton kết thúc sau {nr_iter} bước lặp.")
    return X


def newton_inverse(A, n, eps):
    X0 = A.T / norm(A)**2
    return newton_iter(A, n, eps, X0)


eps = np.loadtxt("input.txt", max_rows = 1)
A = np.loadtxt("input.txt", skiprows = 1)
n = len(A)

print("-" * 68)
print("Ma trận A:")
print(A)
print("-" * 68)

print("-" * 68)
A1 = newton_inverse(A, n, eps)
print("Ma trận nghịch đảo của A:")
print(A1)

print("-" * 68)
print("Kiểm tra nhân ngược:")
print(A1 @ A)
