import math                                
import numpy as np

# Đọc input từ file, input là ma trận bổ sung có dạng (A|B)
def loadFromFile(pathtofile): 
    mat = []
    col = []
    with open(pathtofile) as f:
        lines = f.readlines()
    for i,line in enumerate(lines):
        mat.append([float(a) for a in line.split()])
        col.append([mat[i].pop()])
    return mat,col

# In ma trận
def printMat(matrix, detail = None, precision = 15):  
    """ In ma trận với độ chính xác `precision` số sau dấu phẩy """
    if matrix:
        if detail:
            print('\n' + detail)
        for row in matrix:
            print('[' +', '.join([("% .*f" % (8,x)) for x in row])+ ']' if type(row) == list else "% .*f" % (8,row))

# Kiểm tra ma trận đối xứng
def isSymmetric(mat):
    n = len(mat)
    for i in range(n):
        for j in range(n):
            if mat[i][j] != mat[j][i]:     
                return False
    return True

def transposition_mult(mat):
    """ Nhân ma trận vuông chuyển vị A_T với ma trận ban đầu A """
    n = len(mat)                                        
    ans = [[0] * n for _ in range(n)]                              
    for i in range(n):
        for j in range(n):
            ans[i][j] = sum([mat[k][i] * mat[k][j] for k in range(n)]) 
    return ans

def mult_tmatrix(M, N):
    """ Nhân ma trận kích thước n * n với ma trận kích thước n * 1 """
    n = len(M)
    ans = [[0] for _ in range(n)]      
    for i in range(n):
        ans[i][0] = sum([M[j][i] * N[j][0] for j in range(n)])   
    return ans

# Gói phân tích Cholesky
def Cholesky(matA1):
    n = len(matA1)
    S = [[0] * n for i in range(n)]                           
    for i in range(n):                                      
        for j in range(i + 1):                              
            Sum = sum(S[k][i] * S[k][j] for k in range(j))  
            if Sum == matA1[i][i]:                          
                break                                          
            if i == j:
                S[j][i] = math.sqrt(matA1[i][i] - Sum)
            else:
                S[j][i] = (matA1[j][i] - Sum) / S[j][j]
        if S[i][i] == 0:
            break                   
    return S             

def solve1(matS, colB):
    """ Giải phương trình ma trận tam giác dưới """
    n = len(matS)
    Y = [0] * n               
    for i in range(n):
        Y[i] = (colB[i][0] - sum(matS[k][i] * Y[k] for k in range(i))) / matS[i][i]
    return Y

def solve2(matS, colY):
    """ Giải phương trình ma trận tam giác trên """
    n = len(matS)
    X = [0] * n                 
    for i in range(1, n + 1):
        X[-i] = (colY[-i] - sum(matS[-i][-k] * X[-k] for k in range(1, i))) / matS[-i][-i]
    return X

def main():
    # matA, colB = inputMatrix()
    matA, colB = loadFromFile('Cholesky.txt')   # Nhập ma trận từ file 'Cholesky.txt' lưu cùng thư mục với file này
    matAA = matA.copy()
    if not isSymmetric(matA):                   # Nếu ma trận A không đối xứng thì nhân thêm A_T vào để có ma trận đối xứng
        colB = mult_tmatrix(matA,colB)          # Tính A_T * B
        matA = transposition_mult(matA)         # Tính A_T * A
        
    printMat(matA, "A1 =", 6)                   # In ra ma trận A1 và B1 để dễ theo dõi
    printMat(colB, "B1 =", 6)

    s = Cholesky(matA)                          # Phân tích Cholesky
    if s is not None:                           # Nếu phân tích được thì s sẽ không là None
        printMat(s, "Ma trận S tìm được: ")
        y   = solve1(s, colB)                   # Giải S_T * Y = B tìm Y
        x   = solve2(s, y)                      # Giải S * X = Y tìm X
        printMat(x, "Nghiệm: ")

        """ Kiểm tra lại kết quả """
        x = [[i] for i in x]
        print("Kiểm tra nhân AX")
        print(np.asmatrix(matAA) * np.asmatrix(x))

    else:                                       # Ngược lại nếu không phân tích được thì ta sẽ thông báo và kết thúc chương trình
        print("Không giải được bằng phương pháp Cholesky")

main()