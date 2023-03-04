import math

# Hàm phân tích Cholesky
def Cholesky_Decomposition(matrix, n):
 
    lower = [[0 for x in range(n + 1)]
                for y in range(n + 1)];
 
    # Phân tách thành ma trận tam giác dưới
    for i in range(n):
        for j in range(i + 1):
            sum1 = 0;
 
            # Tổng đường chéo chính
            if (j == i):
                for k in range(j):
                    sum1 += pow(lower[j][k], 2);
                lower[j][j] = float(math.sqrt(matrix[j][j] - sum1));
            else:
                 
                # Tính L(i, j) qua L(j, j)
                for k in range(j):
                    sum1 += (lower[i][k] * lower[j][k]);
                if(lower[j][j] > 0):
                    lower[i][j] = float((matrix[i][j] - sum1) / lower[j][j]);
 
    # Hiển thị ma trận tam giác dưới và ma trận chuyển vị của nó
    print("Lower Triangular\t\tTranspose");
    for i in range(n):
         
        # Ma trận tam giác dưới
        for j in range(n):
            print(lower[i][j], end = "\t");
        print("", end = "\t");
         
        # Ma trận chuyển vị
        for j in range(n):
            print(lower[j][i], end = "\t");
        print("");

# Gọi hàm
n = 3;
matrix = [[4, 12, -16],
          [12, 37, -43],
          [-16, -43, 98]]
Cholesky_Decomposition(matrix, n);