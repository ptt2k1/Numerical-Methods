using System;
using System.Collections;

namespace Numberics
{
    class MainClass
    {
        public static int Giaithua(int n)
        {
            if (n <= 0) return 1;
            else return n * Giaithua(n - 1);
        }
        public static void Swap(ref double x, ref double y)
        {
            double swap = x;
            x = y;
            y = swap;
        }
        public static double[] NDT(double[] A)
        {
            double[] a = new double[A.Length + 1];
            a[0] = 1;
            a[1] = -A[0];
            for (int i = 1; i < A.Length; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    a[j] -= A[i] * a[j - 1];
                }
            }
            return a;
        }
        public static double[] Hept(double[,] A, double[] B)
        {
            int i, j, k;
            int N = B.Length;
            //double[] ind = new double[N];
            /*
            j = 0;
            for (i = 0; i < N; i++)
            {
                if (A[i, j] != 0)
                {
                    ind[i] = j;
                    j = 0;
                }
                else
                {
                    i--;
                    j++;
                }
            }
            */
            for (i = 0; i < (N - 1); i++)
            {
                for (j = i + 1; j < N; j++)
                {
                    double temp2 = A[j, i] / A[i, i];
                    B[j] = B[j] - temp2 * B[i];
                    for (k = 0; k < N; k++)
                    {
                        A[j, k] = A[j, k] - temp2 * A[i, k];
                    }
                }
            }
            double[] X = new double[N];
            double[] temp3 = new double[N];
            for (i = 0; i < N; i++)
            {
                temp3[i] = B[i];
            }
            for (i = N - 2; i >= 0; i--)
            {
                for (j = N - 1; j > i; j--)
                {
                    temp3[i] = temp3[i] - (temp3[j] / A[j, j]) * A[i, j];
                }
            }
            for (i = 0; i < N; i++)
            {
                X[i] = temp3[i] / A[i, i];
            }
            return X;
        }
        public static double f(double x, double y)
        {
            return x-2*y;
        }
        public static double p(double x)
        {
            return 1;
        }
        public static double q(double x)
        {
            return 0;
        }
        public static double r(double x)
        {
            return 1 + x;
        }
        public static double f(double x)
        {
            return 2;
        }
        public static void Horner()
        {
            int key, N;
            double c;
            Console.WriteLine("1.Pn(x) => an(x-c)^n+...+a1(x-c)+a0");
            Console.WriteLine("2.an(x-c)^n+...+a1(x-c)+a0=>Pn(x)");
            Console.WriteLine("3.Pn(x)/(x-c)");
            Console.WriteLine("4.(x-x0)(x-x1)...(x-x(n-1))=>Pn(x)");
            Console.WriteLine("5.Tìm đạo hàm cấp k tại c");
            key = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Nhập n");
            N = Convert.ToInt16(Console.ReadLine());
            double[] a = new double[N + 1];
            switch (key)
            {
                case 1:
                    for (int i = N; i >= 0; i--)
                    {
                        Console.WriteLine("Nhập hệ số x^ " + i);
                        a[N - i] = Convert.ToDouble(Console.ReadLine());
                    }
                    Console.WriteLine("Nhập hệ số c");
                    c = Convert.ToInt16(Console.ReadLine());
                    for (int i = N; i >= 1; i--)
                    {
                        for (int j = 1; j <= i; j++)
                        {
                            a[j] += c * a[j - 1];
                        }
                    }
                    Console.WriteLine("Pn(c) = " + a[N]);
                    Console.Write("Pn(x) = ");
                    for (int i = 0; i < N + 1; i++)
                    {
                        Console.Write(a[i] + "(x-" + c + ")^" + (N - i));
                        if (i != N) Console.Write("+");
                    }
                    break;
                case 2:
                    for (int i = N; i >= 0; i--)
                    {
                        Console.WriteLine("Nhập hệ số (x-c)^ " + i);
                        a[N - i] = Convert.ToDouble(Console.ReadLine());
                    }
                    Console.WriteLine("Nhập hệ số c");
                    c = Convert.ToDouble(Console.ReadLine());
                    for (int i = N; i >= 1; i--)
                    {
                        for (int j = 1; j <= i; j++)
                        {
                            a[j] -= c * a[j - 1];
                        }
                    }
                    Console.Write("an(x-c)^n+...+a1(x-c)+a0 = ");
                    for (int i = 0; i < N + 1; i++)
                    {
                        Console.Write(a[i] + "x^" + (N - i));
                        if (i != N) Console.Write("+");
                    }
                    break;
                case 3:
                    for (int i = N; i >= 0; i--)
                    {
                        Console.WriteLine("Nhập hệ số x^ " + i);
                        a[N - i] = Convert.ToDouble(Console.ReadLine());
                    }
                    Console.WriteLine("Nhập hệ số c");
                    c = Convert.ToDouble(Console.ReadLine());
                    for (int i = 1; i <= N; i++)
                    {
                        a[i] += c * a[i - 1];
                    }
                    Console.Write("Pn(x)/(x-c) = ");
                    for (int i = 0; i < N; i++)
                    {
                        Console.Write(a[i] + "x^" + (N - 1 - i));
                        if (i != N) Console.Write("+");
                    }
                    Console.WriteLine("\nPn(c) = " + a[N]);
                    break;
                case 4:
                    a[0] = 1;
                    Console.WriteLine("Nhập hệ số x1");
                    a[1] = -Convert.ToDouble(Console.ReadLine());
                    for (int i = 2; i <= N; i++)
                    {
                        Console.WriteLine("Nhập hệ số x" + i);
                        c = Convert.ToDouble(Console.ReadLine());
                        for (int j = i; j > 0; j--)
                        {
                            a[j] -= c * a[j - 1];
                        }
                    }
                    Console.Write("(x-x0)(x-x1)...(x-x(n-1)) = ");
                    for (int i = 0; i < N + 1; i++)
                    {
                        Console.Write(a[i] + "x^" + (N - i));
                        if (i != N) Console.Write("+");
                    }

                    /*
                    double[] A = { 2 };
                    for (int i = 0; i <= N; i++)
                    {
                        Console.Write(NDT(A)[i] + "x^" + (N - i));
                        if (i != N) Console.Write("+");
                    }
                    */
                    break;
                case 5:
                    for (int i = N; i >= 0; i--)
                    {
                        Console.WriteLine("Nhập hệ số x^ " + i);
                        a[N - i] = Convert.ToDouble(Console.ReadLine());
                    }
                    Console.WriteLine("Nhập cấp đạo hàm");
                    int k = Convert.ToInt16(Console.ReadLine());
                    Console.WriteLine("Nhập hệ số c");
                    c = Convert.ToDouble(Console.ReadLine());
                    if (k <= N)
                    {
                        for (int i = 0; i <= k; i++)
                        {
                            for (int j = 1; j <= N - i; j++)
                            {
                                a[j] += c * a[j - 1];
                            }
                        }
                        Console.WriteLine("Đạo hàm cấp k tại c = " + a[N - k] * Giaithua(k));
                    }
                    else
                    {
                        Console.WriteLine("Đạo hàm cấp k tại c = 0");
                    }
                    break;
            }
        }
        public static void Lagrange1()
        {
            Console.WriteLine("Nhập số điểm nội suy");
            int N = Convert.ToInt16(Console.ReadLine());
            double[] x = new double[N];
            double[] y = new double[N];
            double[] L = new double[N];
            double[] P = new double[N];
            double[] tich = new double[N - 1];
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập x" + i);
                x[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = L[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i != j) L[i] = L[i] / (x[i] - x[j]);
                }
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i > j) tich[j] = x[j];
                    else if (i < j) tich[j - 1] = x[j];
                }
                for (int j = 0; j < N; j++)
                {
                    P[j] += L[i] * NDT(tich)[j];
                }
            }
            Console.WriteLine("Hàm xấp xỉ: \n");
            for (int i = 0; i < N; i++)
            {
                Console.Write(P[i] + "x^" + (N - 1 - i));
                if (i != N) Console.Write("+");
            }
            Console.WriteLine("\nnhập x:");
            double X = Convert.ToDouble(Console.ReadLine());
            double Y = 0;
            for (int i = 0; i < N; i++)
            {
                Y += P[i] * Math.Pow(X, N - 1 - i);
            }
            Console.WriteLine(Y);
        }
        public static void Lagrange2()
        {
            Console.WriteLine("Nhập số điểm nội suy");
            int N = Convert.ToInt16(Console.ReadLine());
            double[] x = new double[N];
            double[] y = new double[N];
            double[] w = new double[N];
            double[] D = new double[N];
            double[] tich = new double[N - 1];
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập x" + i);
                x[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = w[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N - 1; j++)
                {
                    if ((i == j) & (j == 0))
                    {
                        tich[j] = x[j + 1];
                        w[i] /= (x[i] - x[j + 1]);
                    }
                    else if (j < i)
                    {
                        tich[j] = x[j];
                        w[i] /= (x[i] - x[j]);
                    }
                    else if (j > i)
                    {
                        tich[j] = x[j + 1];
                        w[i] /= (x[i] - x[j + 1]);
                    }
                    if (j == N - 2) Console.WriteLine(w[i]);
                }
            }

        }
        public static void Newton()
        {
            Console.WriteLine("Nhập số điểm nội suy");
            int N = Convert.ToInt16(Console.ReadLine());
            double[] x = new double[N];
            double[] y = new double[N];
            double[,] tyhieu = new double[N, N - 1];
            /*
            double[] x =
            {
                27,24,21,18,15,12,9,6,3,0
            };
            double[] y =
            {
                815,3631,7292,11451,15824,21126,25710,29272,31312,32536
            };
            */
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập x" + i);
                x[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N - 1; i++)
            {
                tyhieu[i, 0] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
            }
            for (int i = 1; i < N - 1; i++)
            {
                for (int j = 0; j < N - 1 - i; j++)
                {
                    tyhieu[j, i] = (tyhieu[j + 1, i - 1] - tyhieu[j, i - 1]) / (x[i + 1] - x[0]);
                }
            }
            Console.WriteLine("Bảng tỷ sai phân:\n");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N - 1; j++)
                {
                    Console.Write(tyhieu[i, j] + "   ");
                    if (j == N - 2) Console.Write("\n");
                }
            }
            double[] P = new double[N];
            for (int i = N - 1; i > 0; i--)
            {
                double[] L = new double[i];
                for (int j = 0; j < i; j++)
                {
                    L[j] = x[j];
                }
                for (int j = N - 1; j >= N - i - 1; j--)
                {
                    P[j] += tyhieu[0, i - 1] * NDT(L)[i - N + 1 + j];
                }
            }
            P[N - 1] += y[0];
            Console.WriteLine("Hàm xấp xỉ: \n");
            for (int i = 0; i < N; i++)
            {
                Console.Write(P[i] + "x^" + (N - 1 - i));
                if (i != N) Console.Write("+");
            }
            Console.WriteLine();
            Console.WriteLine("\nnhập x:");
            double X = Convert.ToDouble(Console.ReadLine());
            double Y = 0;
            for (int i = 0; i < N; i++)
            {
                Y += P[i] * Math.Pow(X, N - 1 - i);
            }
            Console.WriteLine(Y);
        }
        public static void NewtonCD()
        {
            Console.WriteLine("Nhập số điểm nội suy");
            int N = Convert.ToInt16(Console.ReadLine());
            double[] x = new double[N];
            double[] y = new double[N];
            double[,] tyhieu = new double[N, N - 1];
            /*
            double[] x =
            {
                27,24,21,18,15,12,9,6,3,0
            };
            double[] y =
            {
                815,3631,7292,11451,15824,21126,25710,29272,31312,32536
            };
            */
            Console.WriteLine("Nhập x0:");
            x[0] = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập khoảng cách :");
            double h = Convert.ToDouble(Console.ReadLine());
            for (int i = 1; i < N; i++)
            {
                x[i] = x[i - 1] + h;
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N - 1; i++)
            {
                tyhieu[i, 0] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
            }
            for (int i = 1; i < N - 1; i++)
            {
                for (int j = 0; j < N - 1 - i; j++)
                {
                    tyhieu[j, i] = (tyhieu[j + 1, i - 1] - tyhieu[j, i - 1]) / (x[i + 1] - x[0]);
                }
            }
            Console.WriteLine("Bảng tỷ sai phân:\n");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N - 1; j++)
                {
                    Console.Write(tyhieu[i, j] + "   ");
                    if (j == N - 2) Console.Write("\n");
                }
            }
            double[] P = new double[N];
            for (int i = N - 1; i > 0; i--)
            {
                double[] L = new double[i];
                for (int j = 0; j < i; j++)
                {
                    L[j] = x[j];
                }
                for (int j = N - 1; j >= N - i - 1; j--)
                {
                    P[j] += tyhieu[0, i - 1] * NDT(L)[i - N + 1 + j];
                }
            }
            P[N - 1] += y[0];
            Console.WriteLine("Hàm xấp xỉ: \n");
            for (int i = 0; i < N; i++)
            {
                Console.Write(P[i] + "x^" + (N - 1 - i));
                if (i != N) Console.Write("+");
            }
            Console.WriteLine();
            Console.WriteLine("\nnhập x:");
            double X = Convert.ToDouble(Console.ReadLine());
            double Y = 0;
            for (int i = 0; i < N; i++)
            {
                Y += P[i] * Math.Pow(X, N - 1 - i);
            }
            Console.WriteLine(Y);
        }
        public static void Gauss1()
        {
        cp:
            Console.WriteLine("Nhập số điểm nội suy N (N lẻ)");
            int N = Convert.ToInt16(Console.ReadLine());
            if (N % 2 == 0) goto cp;
            double[] x = new double[N];
            double[] y = new double[N];
            double[,] delta = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập x" + i);
                x[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N - 1; i++)
            {
                delta[i, 0] = y[i + 1] - y[i];
            }
            for (int i = 1; i < N; i++)
            {
                for (int j = 0; j < N - i - 1; j++)
                {
                    delta[j, i] = delta[j + 1, i - 1] - delta[j, i - 1];
                }
            }
            Console.WriteLine("Bảng sai phân trung tâm:\n");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N - 1; j++)
                {
                    Console.Write(delta[i, j] + "   ");
                    if (j == N - 2) Console.Write("\n");
                }
            }
            double[] P = new double[N];
            for (int i = 1; i < N; i++)
            {
                double[] L = new double[i];
                for (int j = 0; j < i; j++)
                {
                    if (i % 2 != 0) L[j] = (1 - i) / 2 + j;
                    else L[j] = (2 - i) / 2 + j;
                }
                for (int j = N - 1; j >= N - i - 1; j--)
                {
                    if (i % 2 == 0) P[j] += NDT(L)[j - N + 1 + i] * delta[(N - i - 1) / 2, i - 1] / Giaithua(i);
                    else P[j] += NDT(L)[j - N + 1 + i] * delta[(N - i) / 2, i - 1] / Giaithua(i);
                }
            }
            P[N - 1] += y[(N - 1) / 2];
            Console.WriteLine("Hàm xấp xỉ: \n");
            for (int i = 0; i < N; i++)
            {
                Console.Write(P[i] + "t^" + (N - 1 - i));
                if (i != N) Console.Write("+");
            }
            Console.WriteLine("Với t=(x-x0)/h");
            Console.WriteLine("\nnhập x:");
            double T = Convert.ToDouble(Console.ReadLine());
            T = (T - x[(N - 1) / 2]) / (x[1] - x[0]);
            double Y = 0;
            for (int i = 0; i < N; i++)
            {
                Y += P[i] * Math.Pow(T, N - 1 - i);
            }
            Console.WriteLine(Y);
        }
        public static void Gauss2()
        {
        cp:
            Console.WriteLine("Nhập số điểm nội suy N (N lẻ)");
            int N = Convert.ToInt16(Console.ReadLine());
            if (N % 2 == 0) goto cp;
            double[] x = new double[N];
            double[] y = new double[N];
            double[,] delta = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập x" + i);
                x[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N - 1; i++)
            {
                delta[i, 0] = y[i + 1] - y[i];
            }
            for (int i = 1; i < N; i++)
            {
                for (int j = 0; j < N - i - 1; j++)
                {
                    delta[j, i] = delta[j + 1, i - 1] - delta[j, i - 1];
                }
            }
            Console.WriteLine("Bảng sai phân trung tâm:\n");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N - 1; j++)
                {
                    Console.Write(delta[i, j] + "   ");
                    if (j == N - 2) Console.Write("\n");
                }
            }
            double[] P = new double[N];
            for (int i = 1; i < N; i++)
            {
                double[] L = new double[i];
                for (int j = 0; j < i; j++)
                {
                    if (i % 2 != 0) L[j] = (1 - i) / 2 + j;
                    else L[j] = -i / 2 + j;
                }
                for (int j = N - 1; j >= N - i - 1; j--)
                {
                    if (i % 2 == 0) P[j] += NDT(L)[j - N + 1 + i] * delta[(N - i - 1) / 2, i - 1] / Giaithua(i);
                    else P[j] += NDT(L)[j - N + 1 + i] * delta[(N - i - 2) / 2, i - 1] / Giaithua(i);
                }
            }
            P[N - 1] += y[(N - 1) / 2];
            Console.WriteLine("Hàm xấp xỉ: \n");
            for (int i = 0; i < N; i++)
            {
                Console.Write(P[i] + "t^" + (N - 1 - i));
                if (i != N) Console.Write("+");
            }
            Console.WriteLine("Với t=(x-x0)/h");
            Console.WriteLine("\nnhập x:");
            double T = Convert.ToDouble(Console.ReadLine());
            T = (T - x[(N - 1) / 2]) / (x[1] - x[0]);
            double Y = 0;
            for (int i = 0; i < N; i++)
            {
                Y += P[i] * Math.Pow(T, N - 1 - i);
            }
            Console.WriteLine(Y);
        }
        public static void Sterling()
        {
        cp:
            Console.WriteLine("Nhập số điểm nội suy N (N lẻ)");
            int N = Convert.ToInt16(Console.ReadLine());
            if (N % 2 == 0) goto cp;
            double[] x = new double[N];
            double[] y = new double[N];
            double[,] delta = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập x" + i);
                x[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N - 1; i++)
            {
                delta[i, 0] = y[i + 1] - y[i];
            }
            for (int i = 1; i < N; i++)
            {
                for (int j = 0; j < N - i - 1; j++)
                {
                    delta[j, i] = delta[j + 1, i - 1] - delta[j, i - 1];
                }
            }
            Console.WriteLine("Bảng sai phân trung tâm:\n");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N - 1; j++)
                {
                    Console.Write(delta[i, j] + "   ");
                    if (j == N - 2) Console.Write("\n");
                }
            }
            double[] P = new double[N];
            for (int i = 1; i < N; i++)
            {
                double[] L = new double[i];
                if (i % 2 != 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        L[j] = (1 - i) / 2 + j;
                    }
                }
                else
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (j < i / 2) L[j] = (2 - i) / 2 + j;
                        else L[j] = (2 - i) / 2 + j - 1;
                    }
                }
                Console.WriteLine();
                for (int j = 0; j < i; j++)
                {
                    Console.WriteLine(L[j]);
                }
                for (int j = N - 1; j >= N - i - 1; j--)
                {
                    if (i % 2 == 0) P[j] += NDT(L)[j - N + 1 + i] * delta[(N - i - 1) / 2, i - 1] / Giaithua(i);
                    else
                    {
                        P[j] += NDT(L)[j - N + 1 + i] * (delta[(N - i) / 2, i - 1] + delta[(N - i - 2) / 2, i - 1]) / (2 * Giaithua(i));
                    }
                }
            }
            P[N - 1] += y[(N - 1) / 2];
            Console.WriteLine("Hàm xấp xỉ: \n");
            for (int i = 0; i < N; i++)
            {
                Console.Write(P[i] + "t^" + (N - 1 - i));
                if (i != N) Console.Write("+");
            }
            Console.WriteLine("Với t=(x-x0)/h");
            Console.WriteLine("\nnhập x:");
            double T = Convert.ToDouble(Console.ReadLine());
            T = (T - x[(N - 1) / 2]) / (x[1] - x[0]);
            double Y = 0;
            for (int i = 0; i < N; i++)
            {
                Y += P[i] * Math.Pow(T, N - 1 - i);
            }
            Console.WriteLine(Y);
        }
        public static void Bessel()
        {
        cp:
            Console.WriteLine("Nhập số điểm nội suy N (N chẵn)");
            int N = Convert.ToInt16(Console.ReadLine());
            if (N % 2 != 0) goto cp;
            double[] x = new double[N];
            double[] y = new double[N];
            double[,] delta = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập x" + i);
                x[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N - 1; i++)
            {
                delta[i, 0] = y[i + 1] - y[i];
            }
            for (int i = 1; i < N; i++)
            {
                for (int j = 0; j < N - i - 1; j++)
                {
                    delta[j, i] = delta[j + 1, i - 1] - delta[j, i - 1];
                }
            }
            Console.WriteLine("Bảng sai phân trung tâm:\n");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N - 1; j++)
                {
                    Console.Write(delta[i, j] + "   ");
                    if (j == N - 2) Console.Write("\n");
                }
            }
            double[] P = new double[N];
            for (int i = 1; i < N; i++)
            {
                double[] L = new double[i];
                for (int j = 0; j < i; j++)
                {
                    if (i % 2 != 0) L[j] = (1 - i) / 2 + j;
                    else L[j] = (2 - i) / 2 + j;
                }
                for (int j = N - 1; j >= N - i - 1; j--)
                {
                    if (i % 2 == 0) P[j] += NDT(L)[j - N + 1 + i] * delta[(N - i - 1) / 2, i - 1] / Giaithua(i);
                    else P[j] += NDT(L)[j - N + 1 + i] * delta[(N - i) / 2, i - 1] / Giaithua(i);
                }
            }
            P[N - 1] += y[(N - 2) / 2];
            for (int i = 1; i < N; i++)
            {
                double[] L = new double[i];
                for (int j = 0; j < i; j++)
                {
                    if (i % 2 != 0) L[j] = (1 - i) / 2 + j;
                    else L[j] = -i / 2 + j;
                }
                for (int j = N - 1; j >= N - i - 1; j--)
                {
                    if (i % 2 == 0) P[j] += NDT(L)[j - N + 1 + i] * delta[(N - i - 1) / 2, i - 1] / Giaithua(i);
                    else P[j] += NDT(L)[j - N + 1 + i] * delta[(N - i - 2) / 2, i - 1] / Giaithua(i);
                }
            }
            P[N - 1] += y[(N - 1) / 2];
            for (int i = 0; i < N; i++)
            {
                P[i] /= 2;
            }
            Console.WriteLine("Hàm xấp xỉ: \n");
            for (int i = 0; i < N; i++)
            {
                Console.Write(P[i] + "t^" + (N - 1 - i));
                if (i != N) Console.Write("+");
            }
            Console.WriteLine("Với t=(x-x0)/h");
            Console.WriteLine("\nnhập x:");
            double T = Convert.ToDouble(Console.ReadLine());
            T = (T - x[N / 2]) / (x[1] - x[0]);
            double Y = 0;
            for (int i = 0; i < N; i++)
            {
                Y += P[i] * Math.Pow(T, N - 1 - i);
            }
            Console.WriteLine(Y);
        }
        public static void NguocLagrange()
        {
            Console.WriteLine("Nhập số điểm nội suy");
            int N = Convert.ToInt16(Console.ReadLine());
            double[] x = new double[N];
            double[] y = new double[N];
            double[] delta = new double[N - 1];
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập x" + i);
                x[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N - 1; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    if (x[j] < x[i])
                    {
                        Swap(ref x[i], ref x[j]);
                        Swap(ref y[i], ref y[j]);
                    }
                }
            }
            for (int i = 0; i < N - 1; i++)
            {
                delta[i] = y[i + 1] - y[i];
            }
            int k = 0;
            ArrayList al = new ArrayList();
            al.Add(0);
            for (int i = k; i < N - 1; i++)
            {
                if (delta[i] * delta[k] < 0)
                {
                    k = i;
                    al.Add(k + 1);
                }
            }
            for (int i = 0; i < al.Count; i++)
            {
                Console.WriteLine(al[i]);
            }
            Console.WriteLine("Các khoảng đơn điệu: \n");
            Console.Write("(0;");
            for (int i = 1; i < al.Count; i++)
            {
                Console.Write((Convert.ToInt16(al[i]) - 1) + ")");
                if (i < al.Count) Console.Write("\n(" + al[i] + ";");
            }
            Console.Write(N - 1 + ")");
            al.Add(N);
            Console.WriteLine("\n Nhập giá trị y để f(x)=y");
            double Y = Convert.ToDouble(Console.ReadLine());
            for (int l = 1; l < al.Count; l++)
            {
                N = Convert.ToInt16(al[l]) - Convert.ToInt16(al[l - 1]);
                double[] L = new double[N];
                double[] M = new double[N];
                double[] P = new double[N];
                double[] tich = new double[N - 1];
                for (int i = 0; i < N; i++)
                {
                    L[i] = x[Convert.ToInt16(al[l - 1]) + i];
                    M[i] = y[Convert.ToInt16(al[l - 1]) + i];
                }
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (i != j) L[i] = L[i] / (M[i] - M[j]);
                    }
                }
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (i > j) tich[j] = M[j];
                        else if (i < j) tich[j - 1] = M[j];
                    }
                    for (int j = 0; j < N; j++)
                    {
                        P[j] += L[i] * NDT(tich)[j];
                    }
                }
                Console.WriteLine("\n Hàm ngược trong khoảng đơn điệu thứ " + l + "\n");
                for (int i = 0; i < N; i++)
                {
                    Console.Write(P[i] + "y^" + (N - 1 - i));
                    if (i != N) Console.Write("+");
                }
                double X = 0;
                for (int i = 0; i < N; i++)
                {
                    X += P[i] * Math.Pow(Y, N - 1 - i);
                }
                Console.WriteLine("\nx=" + X);
            }
        }
        public static void NguocNewton()
        {
            Console.WriteLine("Nhập số điểm nội suy");
            int N = Convert.ToInt16(Console.ReadLine());
            double[] x = new double[N];
            double[] y = new double[N];
            double[,] delta = new double[N, N];
            Console.WriteLine("Nhập x0:");
            x[0] = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập khoảng cách :");
            double h = Convert.ToDouble(Console.ReadLine());
            for (int i = 1; i < N; i++)
            {
                x[i] = x[i - 1] + h;
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N - 1; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    if (x[j] < x[i])
                    {
                        Swap(ref x[i], ref x[j]);
                        Swap(ref y[i], ref y[j]);
                    }
                }
            }
            for (int i = 0; i < N - 1; i++)
            {
                delta[i, 0] = y[i + 1] - y[i];
            }
            for (int i = 1; i < N; i++)
            {
                for (int j = 0; j < N - i - 1; j++)
                {
                    delta[j, i] = delta[j + 1, i - 1] - delta[j, i - 1];
                }
            }
            Console.WriteLine("Bảng sai phân:\n");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N - 1; j++)
                {
                    Console.Write(delta[i, j] + "   ");
                    if (j == N - 2) Console.Write("\n");
                }
            }
            /*
            int k = 0;
            ArrayList al = new ArrayList();
            al.Add(0);
            for (int i = k; i < N - 1; i++)
            {
                if (delta[i, 0] * delta[k, 0] < 0)
                {
                    k = i;
                    al.Add(k + 1);
                }
            }
            for (int i = 0; i < al.Count; i++)
            {
                Console.WriteLine(al[i]);
            }
            Console.WriteLine("Các khoảng đơn điệu: \n");
            Console.Write("(0;");
            for (int i = 1; i < al.Count; i++)
            {
                Console.Write((Convert.ToInt16(al[i]) - 1) + ")");
                if (i < al.Count) Console.Write("\n(" + al[i] + ";");
            }
            Console.Write(N - 1 + ")");
            al.Add(N);
            */
            Console.WriteLine("\n Nhập giá trị y để f(x)=y");
            double Y = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập độ chính xác epsilon");
            double epsilon = Convert.ToDouble(Console.ReadLine());
            /*
            for (int l = 1; l < al.Count; l++)
            {
                int i = Convert.ToInt16(al[l]);
                N = i - Convert.ToInt16(al[l - 1]);
                double t1 = (Y - y[i]) / delta[i, 0];
                double t2 = -999;
                while (Math.Abs(t2 - t1) > epsilon)
                {
                    t2 = (Y - y[i]) / delta[i, 0];
                    for(int j = 1; j < N - 1; j++)
                    {
                        double t3 = 1;
                        for(int m = 0; m <= j; m++)
                        {
                            t3 *= (t1 - m);
                        }
                        t2 -= (delta[i, j] / (Giaithua(j + 1) * delta[i, 0]));
                    }
                    Swap(ref t1, ref t2);
                }
                Console.WriteLine("\nx=" + (x[0] + h * t1));
            }
            */
            int lap = 0;
            double T = (Y - y[0]) / delta[0, 0];
            double t1 = (Y - y[0]) / delta[0, 0];
            double t2 = 0;
            do
            {
                if (lap != 0) t1 = t2;
                for (int i = 0; i < N - 2; i++)
                {
                    double tich = 1;
                    for (int j = 0; j < i + 2; j++)
                    {
                        tich *= (t1 - j);
                    }
                    t2 = T - ((delta[0, i + 1] * tich) / (Giaithua(i + 2) * delta[0, 0]));
                }
                lap++;
            } while (Math.Abs(t2 - t1) > epsilon);
            Console.WriteLine("x=" + (x[0] + h * t2));
        }
        public static void BPTT()
        {
            Console.WriteLine("Hàm y=ax^2+bx+c");
            Console.WriteLine("Nhập số điểm nội suy");
            int N = Convert.ToInt16(Console.ReadLine());

            double[] x = new double[N];
            double[] y = new double[N];
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập x" + i);
                x[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = Convert.ToDouble(Console.ReadLine());
            }
            /*
            double[] x =
            {
                -0.03,0.05,0.1,0.2,0.387,1.27,1.165,1.214,-0.25,-0.46,-0.25,-0.52,-0.57,-0.55,-0.62,-0.85,-0.9,-0.95,1.255,
                1.168,-1.416
            };
            double[] y =
            {
                1342,2210,2758,5850,6878,9282
            };
            */
            double[,] A = new double[3, 3];
            double[] B = new double[3];
            for (int i = 0; i < N; i++)
            {
                A[0, 0] += Math.Pow(x[i], 4);
                A[0, 1] += Math.Pow(x[i], 3);
                A[0, 2] += Math.Pow(x[i], 2);
                A[1, 1] += Math.Pow(x[i], 2);
                A[1, 2] += x[i];
                A[2, 2]++;
                B[0] += Math.Pow(x[i], 2) * y[i];
                B[1] += x[i] * y[i];
                B[2] += y[i];
            }
            A[1, 0] = A[0, 1];
            A[2, 0] = A[0, 2];
            A[2, 1] = A[1, 2];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //Console.WriteLine(A[i, j]);
                }
                Console.WriteLine(B[i]);
            }
            Console.WriteLine("Hàm xấp xỉ:");
            Console.WriteLine(Hept(A, B)[0] + "x^2 + " + Hept(A, B)[1] + "x + " + Hept(A, B)[0]);
            double s = 0;
            double[] Y = new double[N];
            for (int i = 0; i < N; i++)
            {
                Y[i] = Hept(A, B)[0] * Math.Pow(x[i], 2) + Hept(A, B)[1] * x[i] + Hept(A, B)[2];
                s += Math.Pow(y[i] - Y[i], 2);
            }
            s = Math.Sqrt(s / N);
            Console.WriteLine("Sai số: " + s);
        }
        public static void Gheptron2()
        {
            Console.WriteLine("Nhập số điẻm nội suy");
            int N = Convert.ToInt16(Console.ReadLine());
            double[] x = new double[N];
            double[] y = new double[N];
            double[] a = new double[N - 1];
            double[] b = new double[N - 1];
            double[] c = new double[N - 1];
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập x" + i);
                x[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N - 1; i++)
            {
                c[i] = y[i];
            }
            double[,] A = new double[2 * N - 2, 2 * N - 2];
            double[] B = new double[2 * N - 2];
            for (int i = 0; i < 2 * N - 2; i++)
            {
                for (int j = 0; j < 2 * N - 2; j++)
                {
                    if (i == j)
                    {
                        if (i % 2 != 0)
                        {
                            A[i, j] = 1;
                        }
                        else
                        {
                            A[i, j] = Math.Pow((x[(i + 2) / 2] - x[i / 2]), 2);
                        }
                    }
                    else if (i == j - 1)
                    {
                        if (i % 2 != 0)
                        {
                            A[i, j] = 0;
                        }
                        else
                        {
                            A[i, j] = x[(i + 2) / 2] - x[i / 2];
                        }
                    }
                    else if (i == j + 1)
                    {
                        if (i % 2 != 0)
                        {
                            A[i, j] = 2 * (x[(i + 1) / 2] - x[(i - 1) / 2]);
                        }
                        else
                        {
                            A[i, j] = 0;
                        }
                    }
                    else if ((i == j - 2) & (i % 2 != 0))
                    {
                        A[i, j] = -1;
                    }
                }
                if (i % 2 == 0)
                {
                    B[i] = y[(i + 2) / 2] - y[i / 2];
                }

            }
        home:
            Console.WriteLine("Chọn điều kiện biên");
            Console.WriteLine("1.S'(" + x[0] + ") = alpha");
            Console.WriteLine("2.S'(" + x[N - 1] + ") = belta");
            int key = Convert.ToInt16(Console.ReadLine());
            switch (key)
            {
                case 1:
                    Console.WriteLine("alpha=");
                    double alpha = Convert.ToDouble(Console.ReadLine());
                    for (int i = 0; i < 2 * N - 2; i++)
                    {
                        A[2 * N - 3, i] = 0;
                    }
                    A[2 * N - 3, 1] = 1;
                    B[2 * N - 3] = alpha;
                    break;
                case 2:
                    Console.WriteLine("belta=");
                    double belta = Convert.ToDouble(Console.ReadLine());
                    for (int i = 0; i < 2 * N - 2; i++)
                    {
                        A[2 * N - 3, i] = 0;
                    }
                    A[2 * N - 3, 2 * N - 4] = 2 * (x[N - 1] - x[N - 2]);
                    A[2 * N - 3, 2 * N - 3] = 1;
                    B[2 * N - 3] = belta;
                    break;
                default:
                    goto home;
            }
            Console.WriteLine();
            for (int i = 0; i < 2 * N - 2; i++)
            {
                for (int j = 0; j < 2 * N - 2; j++)
                {
                    Console.Write(A[i, j] + "\t  ");
                    if (j == 2 * N - 3)
                    {
                        Console.Write("  ||  " + B[i]);
                        Console.Write("\n");
                    }
                }
            }
            /*
            Console.WriteLine();
            for(int i=0;i < 2 * N - 2; i++)
            {
                Console.WriteLine(Hept(A, B)[i]);
            }
            */
            for (int i = 0; i < 2 * N - 2; i++)
            {
                if (i % 2 == 0) a[i / 2] = Hept(A, B)[i];
                else b[(i - 1) / 2] = Hept(A, B)[i];
            }
            for (int i = 0; i < N - 1; i++)
            {
                Console.WriteLine("Hàm ghép tại đoạn thứ " + (i + 1));
                Console.WriteLine(a[i] + "*(x-" + x[i] + ")^2+" + b[i] + "*(x-" + x[i] + ")+" + c[i]);
            }
        }
        public static void Gheptron3()
        {
            Console.WriteLine("Nhập số điểm nội suy");
            int N = Convert.ToInt16(Console.ReadLine());
            double[] x = new double[N];
            double[] y = new double[N];
            double[] a = new double[N - 1];
            double[] b = new double[N - 1];
            double[] c = new double[N - 1];
            double[] d = new double[N - 1];
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập x" + i);
                x[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N - 1; i++)
            {
                d[i] = y[i];
            }
            double[,] A = new double[3 * N - 3, 3 * N - 3];
            double[] B = new double[3 * N - 3];
            for (int i = 0; i < 3 * N - 3; i++)
            {
                for (int j = 0; j < 3 * N - 3; j++)
                {
                    if (i == j)
                    {
                        if (i % 3 == 0)
                        {
                            A[i, j] = Math.Pow((x[i / 3 + 1] - x[i / 3]), 3);
                        }
                        else if (i % 3 == 1)
                        {
                            A[i, j] = 2 * (x[(i + 2) / 3] - x[(i - 1) / 3]);
                        }
                    }
                    else if (i == j - 1)
                    {
                        if (i % 3 == 0)
                        {
                            A[i, j] = Math.Pow((x[i / 3 + 1] - x[i / 3]), 2);
                        }
                        else if (i % 3 == 1)
                        {
                            A[i, j] = 1;
                        }
                    }
                    else if (i == j - 2)
                    {
                        if (i % 3 == 0)
                        {
                            A[i, j] = x[i / 3 + 1] - x[i / 3];
                        }
                        else if (i % 3 == 2)
                        {
                            A[i, j] = -2;
                        }
                    }
                    else if (i == j - 4)
                    {
                        if (i % 3 == 1)
                        {
                            A[i, j] = -1;
                        }
                    }
                    else if (i == j + 1)
                    {
                        if (i % 3 == 1)
                        {
                            A[i, j] = 3 * Math.Pow((x[(i + 2) / 3] - x[(i - 1) / 3]), 2);
                        }
                        else if (i % 3 == 2)
                        {
                            A[i, j] = 2;
                        }
                    }
                    else if (i == j + 2)
                    {
                        if (i % 3 == 2)
                        {
                            A[i, j] = 6 * (x[(i + 1) / 3] - x[(i - 2) / 3]);
                        }
                    }
                }
                if (i % 3 == 0)
                {
                    B[i] = y[i / 3 + 1] - y[i / 3];
                }
            }
        home:
            Console.WriteLine("Chọn 2 trong 4 điều kiện biên");
            Console.WriteLine("1.S'(" + x[0] + ") = alpha1");
            Console.WriteLine("2.S''(" + x[0] + ") = alpha2");
            Console.WriteLine("3.S'(" + x[N - 1] + ") = belta1");
            Console.WriteLine("4.S''(" + x[N - 1] + ") = belta2");
            int key = Convert.ToInt16(Console.ReadLine());
            double alpha1, alpha2, belta1, belta2;
            switch (key)
            {
                case (12):
                    Console.WriteLine("alpha1=");
                    alpha1 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("alpha2=");
                    alpha2 = Convert.ToDouble(Console.ReadLine());
                    for (int i = 0; i < 3 * N - 3; i++)
                    {
                        A[3 * N - 5, i] = A[3 * N - 4, i] = 0;
                    }
                    A[3 * N - 5, 2] = 1;
                    A[3 * N - 4, 1] = 2;
                    B[3 * N - 5] = alpha1;
                    B[3 * N - 4] = alpha2;
                    break;
                case (13):
                    Console.WriteLine("alpha1=");
                    alpha1 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("belta1=");
                    belta1 = Convert.ToDouble(Console.ReadLine());
                    for (int i = 0; i < 3 * N - 3; i++)
                    {
                        A[3 * N - 5, i] = A[3 * N - 4, i] = 0;
                    }
                    A[3 * N - 5, 2] = 1;
                    A[3 * N - 4, 3 * N - 6] = 3 * Math.Pow(x[N - 1] - x[N - 2], 2);
                    A[3 * N - 4, 3 * N - 5] = 2 * (x[N - 1] - x[N - 2]);
                    A[3 * N - 4, 3 * N - 4] = 1;
                    B[3 * N - 5] = alpha1;
                    B[3 * N - 4] = belta1;
                    break;
                case (14):
                    Console.WriteLine("alpha1=");
                    alpha1 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("belta2=");
                    belta2 = Convert.ToDouble(Console.ReadLine());
                    for (int i = 0; i < 3 * N - 3; i++)
                    {
                        A[3 * N - 5, i] = A[3 * N - 4, i] = 0;
                    }
                    A[3 * N - 5, 2] = 1;
                    A[3 * N - 4, 3 * N - 6] = 6 * (x[N - 1] - x[N - 2]);
                    A[3 * N - 4, 3 * N - 5] = 2;
                    B[3 * N - 5] = alpha1;
                    B[3 * N - 4] = belta2;
                    break;
                case (23):
                    Console.WriteLine("alpha2=");
                    alpha2 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("belta1=");
                    belta1 = Convert.ToDouble(Console.ReadLine());
                    for (int i = 0; i < 3 * N - 3; i++)
                    {
                        A[3 * N - 5, i] = A[3 * N - 4, i] = 0;
                    }
                    A[3 * N - 5, 1] = 2;
                    A[3 * N - 4, 3 * N - 6] = 3 * Math.Pow(x[N - 1] - x[N - 2], 2);
                    A[3 * N - 4, 3 * N - 5] = 2 * (x[N - 1] - x[N - 2]);
                    A[3 * N - 4, 3 * N - 4] = 1;
                    B[3 * N - 5] = alpha2;
                    B[3 * N - 4] = belta1;
                    break;
                case (24):
                    Console.WriteLine("alpha2=");
                    alpha2 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("belta2=");
                    belta2 = Convert.ToDouble(Console.ReadLine());
                    for (int i = 0; i < 3 * N - 3; i++)
                    {
                        A[3 * N - 5, i] = A[3 * N - 4, i] = 0;
                    }
                    A[3 * N - 5, 1] = 2;
                    A[3 * N - 4, 3 * N - 6] = 6 * (x[N - 1] - x[N - 2]);
                    A[3 * N - 4, 3 * N - 5] = 2;
                    B[3 * N - 5] = alpha2;
                    B[3 * N - 4] = belta2;
                    break;
                case (34):
                    Console.WriteLine("belta1=");
                    belta1 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("belta2=");
                    belta2 = Convert.ToDouble(Console.ReadLine());
                    for (int i = 0; i < 3 * N - 3; i++)
                    {
                        A[3 * N - 5, i] = A[3 * N - 4, i] = 0;
                    }
                    A[3 * N - 5, 3 * N - 6] = 3 * Math.Pow(x[N - 1] - x[N - 2], 2);
                    A[3 * N - 5, 3 * N - 5] = 2 * (x[N - 1] - x[N - 2]);
                    A[3 * N - 5, 3 * N - 4] = 1;
                    A[3 * N - 4, 3 * N - 6] = 6 * (x[N - 1] - x[N - 2]);
                    A[3 * N - 4, 3 * N - 5] = 2;
                    B[3 * N - 5] = belta1;
                    B[3 * N - 4] = belta2;
                    break;
                default:
                    goto home;
            }
            Console.WriteLine();
            for (int i = 0; i < 3 * N - 3; i++)
            {
                for (int j = 0; j < 3 * N - 3; j++)
                {
                    Console.Write(A[i, j] + "\t  ");
                    if (j == 3 * N - 4)
                    {
                        Console.Write("  ||  " + B[i]);
                        Console.Write("\n");
                    }
                }
            }
            for (int i = 0; i < 3 * N - 3; i++)
            {
                if (i % 3 == 0) a[i / 3] = Hept(A, B)[i];
                else if (i % 3 == 1) b[(i - 1) / 3] = Hept(A, B)[i];
                else c[(i - 2) / 3] = Hept(A, B)[i];
            }
            for (int i = 0; i < N - 1; i++)
            {
                Console.WriteLine("Hàm ghép trơn tại đoạn thứ " + (i + 1));
                Console.WriteLine(a[i] + "*(x-" + x[i] + ")^3+" + b[i] + "*(x-" + x[i] + ")^2+" + c[i] + "*(x-" + x[i] + ")+" + d[i]);
            }
        }
        public static void Gheptron4()
        {
            Console.WriteLine("Nhập số điểm nội suy");
            int N = Convert.ToInt16(Console.ReadLine());
            double[] x = new double[N];
            double[] y = new double[N];
            double[] a = new double[N - 1];
            double[] b = new double[N - 1];
            double[] c = new double[N - 1];
            double[] d = new double[N - 1];
            double[] e = new double[N - 1];
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập x" + i);
                x[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i < N - 1; i++)
            {
                e[i] = y[i];
            }
            double[,] A = new double[4 * N - 4, 4 * N - 4];
            double[] B = new double[4 * N - 4];
            for (int i = 0; i < 4 * N - 4; i++)
            {
                for (int j = 0; j < 4 * N - 4; j++)
                {
                    if (i == j)
                    {
                        if (i % 4 == 0)
                        {
                            A[i, j] = Math.Pow(x[(i + 4) / 4] - x[i / 4], 4);
                        }
                        else if (i % 4 == 1)
                        {
                            A[i, j] = 3 * Math.Pow(x[(i + 3) / 4] - x[(i - 1) / 4], 2);
                        }
                        else if (i % 4 == 2)
                        {
                            A[i, j] = 2;
                        }
                    }
                    else if (i == j - 1)
                    {
                        if (i % 4 == 0)
                        {
                            A[i, j] = Math.Pow(x[(i + 4) / 4] - x[i / 4], 3);
                        }
                        else if (i % 4 == 1)
                        {
                            A[i, j] = 2 * (x[(i + 3) / 4] - x[(i - 1) / 4]);
                        }
                    }
                    else if (i == j - 2)
                    {
                        if (i % 4 == 0)
                        {
                            A[i, j] = Math.Pow(x[(i + 4) / 4] - x[i / 4], 2);
                        }
                        else if (i % 4 == 1)
                        {
                            A[i, j] = 1;
                        }
                        else if (i % 4 == 3)
                        {
                            A[i, j] = -6;
                        }
                    }
                    else if (i == j - 3)
                    {
                        if (i % 4 == 0)
                        {
                            A[i, j] = x[(i + 4) / 4] - x[i / 4];
                        }
                    }
                    else if (i == j - 4)
                    {
                        if (i % 4 == 2)
                        {
                            A[i, j] = -2;
                        }
                    }
                    else if (i == j - 6)
                    {
                        if (i % 4 == 1)
                        {
                            A[i, j] = -1;
                        }
                    }
                    else if (i == j + 1)
                    {
                        if (i % 4 == 1)
                        {
                            A[i, j] = 4 * Math.Pow(x[(i + 3) / 4] - x[(i - 1) / 4], 3);
                        }
                        else if (i % 4 == 2)
                        {
                            A[i, j] = 6 * (x[(i + 2) / 4] - x[(i - 2) / 4]);
                        }
                    }
                    else if (i == j + 2)
                    {
                        if (i % 4 == 2)
                        {
                            A[i, j] = 12 * Math.Pow(x[(i + 2) / 4] - x[(i - 2) / 4], 2);
                        }
                        else if (i % 4 == 3)
                        {
                            A[i, j] = 6;
                        }
                    }
                    else if (i == j + 3)
                    {
                        if (i % 4 == 3)
                        {
                            A[i, j] = 24 * (x[(i + 1) / 4] - x[(i - 3) / 4]);
                        }
                    }
                }
                if (i % 4 == 0) B[i] = y[(i + 4) / 4] - y[i / 4];
            }
            for (int i = 0; i < 4 * N - 4; i++)
            {
                A[4 * N - 6, i] = A[4 * N - 5, i] = 0;
            }
            Console.WriteLine("alpha =");
            double alpha = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("belta =");
            double belta = Convert.ToDouble(Console.ReadLine());
            //s1'
            A[4 * N - 6, 3] = 1;
            B[4 * N - 6] = alpha;
            //s1''
            A[4 * N - 5, 2] = 2;
            B[4 * N - 5] = belta;
            Console.WriteLine();
            for (int i = 0; i < 4 * N - 4; i++)
            {
                for (int j = 0; j < 4 * N - 4; j++)
                {
                    Console.Write(A[i, j] + "\t  ");
                    if (j == 4 * N - 5)
                    {
                        Console.Write("  ||  " + B[i]);
                        Console.Write("\n");
                    }
                }
            }
            for (int i = 0; i < 4 * N - 4; i++)
            {
                if (i % 4 == 0) a[i / 4] = Hept(A, B)[i];
                else if (i % 4 == 1) b[(i - 1) / 4] = Hept(A, B)[i];
                else if (i % 4 == 2) c[(i - 2) / 4] = Hept(A, B)[i];
                else c[(i - 3) / 4] = Hept(A, B)[i];
            }
            for (int i = 0; i < N - 1; i++)
            {
                Console.WriteLine("Hàm ghép trơn tại đoạn thứ " + (i + 1));
                Console.WriteLine(a[i] + "*(x-" + x[i] + ")^4+" + b[i] + "*(x-" + x[i] + ")^3+" + c[i] + "*(x-" + x[i] + ")^2+" + d[i] + "*(x-" + x[i] + ")+" + e[i]);
            }
        }
        public static void NewtonCotes()
        {
            Console.WriteLine("Nhập 2 cận a, b");
            Console.WriteLine("a=");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("b=");
            double b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập số đoạn chia khoảng [a,b]");
            int N = Convert.ToInt16(Console.ReadLine());
            double[] x = new double[N + 1];
            double[] y = new double[N + 1];
            double Hi;
            double temp = 0;
            double I = 0;
            double[] H = new double[N];
            x[0] = a;
            double h = (b - a) / N;
            for (int i = 1; i <= N; i++)
            {
                x[i] = x[i - 1] + h;
            }
            for (int i = 0; i <= N; i++)
            {
                Console.WriteLine("Nhập y" + i);
                y[i] = Convert.ToDouble(Console.ReadLine());
            }
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (j < i) H[j] = j;
                    else if (j > i) H[j] = j + 1;
                    else if (j == 0) H[j] = j + 1;
                    //Console.WriteLine(H[j]);
                    //if (j == N - 1) Console.WriteLine();
                }
                for (int k = 0; k <= N; k++)
                {
                    temp += Math.Pow(N, N - k + 1) * NDT(H)[k] / (N - k + 1);
                    //Console.WriteLine(temp);
                }
                Hi = (Math.Pow(-1, N - i) * temp) / (N * Giaithua(i) * Giaithua(N - i));
                I += Hi * y[i];
                temp = 0;
            }
            I *= (b - a);
            Console.WriteLine("tích phân trên [a,b] là " + I);
            Console.ReadKey();
        }
        public static void EulerHien()
        {
            Console.WriteLine("y'=f(x,y)");
            Console.WriteLine("Nhập x0");
            double x = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập y0 (y(" + x + ")=y0)");
            double y = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập h");
            double h = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập xN");
            double xN = Convert.ToDouble(Console.ReadLine());
            int N = Convert.ToInt16((xN - x) / h);
            int lap = 0;
            while (lap < N)
            {
                y += h * f(x, y);
                x += h;
                lap++;
                Console.WriteLine("y(x" + lap + ") = " + y);
            }
        }
        public static void EulerHT()
        {
            Console.WriteLine("Nhập x0");
            double x = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập y0 (y(" + x + ")=y0)");
            double y = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập h");
            double h = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập xN");
            double xN = Convert.ToDouble(Console.ReadLine());
            int N = Convert.ToInt16((xN - x) / h);
            int lap = 0;
            double Y;
            while (lap < N)
            {
                Y = y + h * f(x, y);
                y += (h / 2) * (f(x + h, Y) + f(x, y));
                x += h;
                lap++;
                Console.WriteLine("y(x" + lap + ") = " + y);
            }
        }
        public static void EulerAn()
        {
            Console.WriteLine("Nhập x0");
            double x = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập y0 (y(" + x + ")=y0)");
            double y = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập h");
            double h = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập xN");
            double xN = Convert.ToDouble(Console.ReadLine());
            int N = Convert.ToInt16((xN - x) / h);
            int lap = 0;
            double Y;
            while (lap < N)
            {
                Y = y + h * f(x, y);
                y += h * f(x + h, Y);
                x += h;
                lap++;
                Console.WriteLine("y(x" + lap + ") = " + y);
            }
        }
        public static void RK2()
        {
            Console.WriteLine("y'=f(x,y)");
            Console.WriteLine("Nhập x0");
            double x = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập y0 (y(" + x + ")=y0)");
            double y = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập h");
            double h = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập xN");
            double xN = Convert.ToDouble(Console.ReadLine());
            int N = Convert.ToInt16((xN - x) / h);
            Console.WriteLine("Nhập a2 (0<a2<=1)");
            int lap = 0;
        back:
            double a2 = Convert.ToDouble(Console.ReadLine());
            if ((a2 <= 0) || (a2 > 1)) goto back;
            double p2 = 1 / (2 * a2);
            double p1 = 1 - p2;
            double b21 = 1 / (2 * p2);
            double k1, k2;
            while (lap < N)
            {
                k1 = h * f(x, y);
                k2 = h * f(x + a2 * h, y + b21 * k1);
                y += (p1 * k1) + (p2 * k2);
                x += h;
                lap++;
                Console.WriteLine("y(x" + lap + ") = " + y);
            }
        }
        public static void RK3()
        {
            Console.WriteLine("y'=f(x,y)");
            Console.WriteLine("Nhập x0");
            double x = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập y0 (y(" + x + ")=y0)");
            double y = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập h");
            double h = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập xN");
            double xN = Convert.ToDouble(Console.ReadLine());
            int N = Convert.ToInt16((xN - x) / h);
            int key;
            int lap = 0;
            double k1, k2, k3;
        back:
            Console.WriteLine("1.R-K 3 thường dùng");
            Console.WriteLine("2.R-K 3 Heun");
            key = Convert.ToInt16(Console.ReadLine());
            switch (key)
            {
                case 1:
                    while (lap < N)
                    {
                        k1 = h * f(x, y);
                        k2 = h * f(x + h / 2, y + k1 / 2);
                        k3 = h * f(x + h, y - k1 + 2 * k2);
                        y += (k1 + 4 * k2 + k3) / 6;
                        x += h;
                        lap++;
                        Console.WriteLine("y(x" + lap + ") = " + y);
                    }
                    break;
                case 2:
                    while (lap < N)
                    {
                        k1 = h * f(x, y);
                        k2 = h * f(x + h / 3, y + k1 / 3);
                        k3 = h * f(x + 2 * h / 3, y + 2 * k2 / 3);
                        y += (k1 + 3 * k3) / 4;
                        x += h;
                        lap++;
                        Console.WriteLine("y(x" + lap + ") = " + y);
                    }
                    break;
                default:
                    goto back;
            }
        }
        public static void RK4()
        {
            Console.WriteLine("y'=f(x,y)");
            Console.WriteLine("Nhập x0");
            double x = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập y0 (y(" + x + ")=y0)");
            double y = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập h");
            double h = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập xN");
            double xN = Convert.ToDouble(Console.ReadLine());
            int N = Convert.ToInt16((xN - x) / h);
            int lap = 0;
            double k1, k2, k3, k4;
            while (lap < N)
            {
                k1 = h * f(x, y);
                k2 = h * f(x + h / 2, y + k1 / 2);
                k3 = h * f(x + h / 2, y + k2 / 2);
                k4 = h * f(x + h, y + k3);
                y += (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                x += h;
                lap++;
                Console.WriteLine("y(x" + lap + ") = " + y);
            }
        }
        public static void AdamsBashforth()
        {
            Console.WriteLine("y'=f(x,y)");
            Console.WriteLine("Nhập x0");
            double x0 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập y0 (y(" + x0 + ")=y0)");
            double y0 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập h");
            double h = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập xN");
            double xN = Convert.ToDouble(Console.ReadLine());
            int N = Convert.ToInt16((xN - x0) / h);
            double[] x = new double[N + 1];
            double[] y = new double[N + 1];
            x[0] = x0;
            for (int i = 1; i < N + 1; i++)
            {
                x[i] = x[i - 1] + h;
            }
            y[0] = y0;
            int key1;
        back:
            Console.WriteLine("Chọn bac công thức Adams:");
            key1 = Convert.ToInt16(Console.ReadLine());
            switch (key1)
            {
                case 2:
                    for (int i = 1; i < 2; i++)
                    {
                        double k1, k2, k3;
                        k1 = h * f(x[i - 1], y[i - 1]);
                        k2 = h * f(x[i - 1] + h / 2, y[i - 1] + k1 / 2);
                        k3 = h * f(x[i - 1] + h, y[i - 1] - k1 + 2 * k2);
                        y[i] = y[i - 1] + (k1 + 4 * k2 + k3) / 6;
                        Console.WriteLine("y(x" + i + ") = " + y[i]);
                    }
                    for (int i = 2; i < N + 1; i++)
                    {
                        y[i] = y[i - 1] + (3 * h / 2) * f(x[i - 1], y[i - 1]) - (h / 2) * f(x[i - 2], y[i - 2]);
                        Console.WriteLine("y(x" + i + ") = " + y[i]);
                    }
                    break;
                case 3:
                    for (int i = 1; i < 3; i++)
                    {
                        double k1, k2, k3;
                        k1 = h * f(x[i - 1], y[i - 1]);
                        k2 = h * f(x[i - 1] + h / 2, y[i - 1] + k1 / 2);
                        k3 = h * f(x[i - 1] + h, y[i - 1] - k1 + 2 * k2);
                        y[i] = y[i - 1] + (k1 + 4 * k2 + k3) / 6;
                        Console.WriteLine("y(x" + i + ") = " + y[i]);
                    }
                    for (int i = 3; i < N + 1; i++)
                    {
                        y[i] = y[i - 1] + (23 * h / 12) * f(x[i - 1], y[i - 1]) - (16 * h / 12) * f(x[i - 2], y[i - 2]) + (5 * h / 12) * f(x[i - 3], y[i - 3]);
                        Console.WriteLine("y(x" + i + ") = " + y[i]);
                    }
                    break;
                case 4:
                    for (int i = 1; i < 4; i++)
                    {
                        double k1, k2, k3;
                        k1 = h * f(x[i - 1], y[i - 1]);
                        k2 = h * f(x[i - 1] + h / 2, y[i - 1] + k1 / 2);
                        k3 = h * f(x[i - 1] + h, y[i - 1] - k1 + 2 * k2);
                        y[i] = y[i - 1] + (k1 + 4 * k2 + k3) / 6;
                        Console.WriteLine("y(x" + i + ") = " + y[i]);
                    }
                    for (int i = 4; i < N + 1; i++)
                    {
                        y[i] = y[i - 1] + (55 * h / 24) * f(x[i - 1], y[i - 1]) - (59 * h / 24) * f(x[i - 2], y[i - 2]) + (37 * h / 24) * f(x[i - 3], y[i - 3]) - (9 * h / 24) * f(x[i - 4], y[i - 4]);
                        Console.WriteLine("y(x" + i + ") = " + y[i]);
                    }
                    break;
                default:
                    goto back;
            }
        }
        public static void AdamsMoulton()
        {
            Console.WriteLine("y'=f(x,y)");
            Console.WriteLine("Nhập x0");
            double x0 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập y0 (y(" + x0 + ")=y0)");
            double y0 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập h");
            double h = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập xN");
            double xN = Convert.ToDouble(Console.ReadLine());
            int N = Convert.ToInt16((xN - x0) / h);
            double[] x = new double[N + 1];
            double[] y = new double[N + 1];
            x[0] = x0;
            for (int i = 1; i < N + 1; i++)
            {
                x[i] = x[i - 1] + h;
            }
            y[0] = y0;
            double Y;
            int key1;
            Console.WriteLine("Chọn bac công thức Adams:");
            key1 = Convert.ToInt16(Console.ReadLine());
            switch (key1)
            {
                case 2:
                    for (int i = 1; i < 2; i++)
                    {
                        double k1, k2, k3;
                        k1 = h * f(x[i - 1], y[i - 1]);
                        k2 = h * f(x[i - 1] + h / 2, y[i - 1] + k1 / 2);
                        k3 = h * f(x[i - 1] + h, y[i - 1] - k1 + 2 * k2);
                        y[i] = y[i - 1] + (k1 + 4 * k2 + k3) / 6;
                        Console.WriteLine("y(x" + i + ") = " + y[i]);
                    }
                    for (int i = 2; i < N + 1; i++)
                    {
                        Y = y[i - 1] + (3 * h / 2) * f(x[i - 1], y[i - 1]) - (h / 2) * f(x[i - 2], y[i - 2]);
                        y[i] = y[i - 1] + (h / 2) * (f(x[i], Y) + f(x[i - 1], y[i - 1]));
                        while (Math.Abs(Y - y[i]) > 0.001)
                        {
                            Y = y[i];
                            y[i] = y[i - 1] + (h / 2) * (f(x[i], Y) + f(x[i - 1], y[i - 1]));
                        }
                        Console.WriteLine("y(x" + i + ") = " + y[i]);
                    }
                    break;
                case 3:
                    for (int i = 1; i < 3; i++)
                    {
                        double k1, k2, k3;
                        k1 = h * f(x[i - 1], y[i - 1]);
                        k2 = h * f(x[i - 1] + h / 2, y[i - 1] + k1 / 2);
                        k3 = h * f(x[i - 1] + h, y[i - 1] - k1 + 2 * k2);
                        y[i] = y[i - 1] + (k1 + 4 * k2 + k3) / 6;
                        Console.WriteLine("y(x" + i + ") = " + y[i]);
                    }
                    for (int i = 3; i < N + 1; i++)
                    {
                        Y = y[i - 1] + (23 * h / 12) * f(x[i - 1], y[i - 1]) - (16 * h / 12) * f(x[i - 2], y[i - 2]) + (5 * h / 12) * f(x[i - 3], y[i - 3]);
                        y[i] = y[i - 1] + (h / 12) * (5 * f(x[i], Y) + 8 * f(x[i - 1], y[i - 1]) - f(x[i - 2], y[i - 2]));
                        while (Math.Abs(Y - y[i]) > 0.001)
                        {
                            Y = y[i];
                            y[i] = y[i - 1] + (h / 12) * (5 * f(x[i], Y) + 8 * f(x[i - 1], y[i - 1]) - f(x[i - 2], y[i - 2]));
                        }
                        Console.WriteLine("y(x" + i + ") = " + y[i]);
                    }
                    break;
                case 4:
                    for (int i = 1; i < 4; i++)
                    {
                        double k1, k2, k3;
                        k1 = h * f(x[i - 1], y[i - 1]);
                        k2 = h * f(x[i - 1] + h / 2, y[i - 1] + k1 / 2);
                        k3 = h * f(x[i - 1] + h, y[i - 1] - k1 + 2 * k2);
                        y[i] = y[i - 1] + (k1 + 4 * k2 + k3) / 6;
                        Console.WriteLine("y(x" + i + ") = " + y[i]);
                    }
                    for (int i = 4; i < N + 1; i++)
                    {
                        Y = y[i - 1] + (55 * h / 24) * f(x[i - 1], y[i - 1]) - (59 * h / 24) * f(x[i - 2], y[i - 2]) + (37 * h / 24) * f(x[i - 3], y[i - 3]) - (9 * h / 24) * f(x[i - 4], y[i - 4]);
                        y[i] = y[i - 1] + (h / 24) * (9 * f(x[i], Y) + 19 * f(x[i - 1], y[i - 1]) - 5 * f(x[i - 2], y[i - 2]) + f(x[i - 3], y[i - 3]));
                        while (Math.Abs(Y - y[i]) > 0.001)
                        {
                            Y = y[i];
                            y[i] = y[i - 1] + (h / 24) * (9 * f(x[i], Y) + 19 * f(x[i - 1], y[i - 1]) - 5 * f(x[i - 2], y[i - 2]) + f(x[i - 3], y[i - 3]));
                        }
                        Console.WriteLine("y(x" + i + ") = " + y[i]);
                    }
                    break;
            }
        }
        public static void BienBD()
        {
            //a=x[0], b=x[N]
            Console.WriteLine("Nhập N");
            int N = Convert.ToInt16(Console.ReadLine());
            double[] x = new double[N + 1];
            double[] y = new double[N + 1];
            double[] a = new double[N + 2];
            double[] A = new double[N + 1];
            double[] B = new double[N + 1];
            double[] C = new double[N + 1];
            double[,] E = new double[N + 1, N + 1];
            double[] F = new double[N + 1];
            Console.WriteLine("Nhập a");
            x[0] = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập b");
            x[N] = Convert.ToDouble(Console.ReadLine());
            double h = (x[N] - x[0]) / N;
            for (int i = 1; i < N; i++)
            {
                x[i] = x[i - 1] + h;
            }
            int key;
        back:
            Console.WriteLine("Nhập loại biên:");
            Console.WriteLine("1.Biên loại 1");
            key = Convert.ToInt16(Console.ReadLine());
            switch (key)
            {
                case 1:
                    Console.WriteLine("Nhập alpha");
                    y[0] = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Nhập belta");
                    y[N] = Convert.ToDouble(Console.ReadLine());
                    for (int i = 0; i < N + 1; i++)
                    {
                        a[i] = p(x[i] - (h / 2));
                    }
                    a[N + 1] = p(x[N] + (h / 2));
                    for (int i = 0; i < N + 1; i++)
                    {
                        A[i] = a[i] / (Math.Pow(h, 2));
                        B[i] = a[i + 1] / (Math.Pow(h, 2));
                        C[i] = (a[i] + a[i + 1] + Math.Pow(h, 2) * q(x[i])) / (Math.Pow(h, 2));
                    }
                    E[0, 0] = E[N, N] = 1;
                    F[0] = y[0];
                    F[N] = y[N];
                    for (int i = 1; i < N; i++)
                    {
                        for (int j = 0; j < N + 1; j++)
                        {
                            if (j == i - 1) E[i, j] = A[i];
                            else if (j == i) E[i, j] = -C[i];
                            else if (j == i + 1) E[i, j] = B[i];
                        }
                        F[i] = -f(x[i]);
                    }

                    Console.WriteLine();
                    for (int i = 0; i < N + 1; i++)
                    {
                        for (int j = 0; j < N + 1; j++)
                        {
                            Console.Write(E[i, j] + "   ");
                            if (j == N) Console.Write(F[i] + "\n");
                        }
                    }

                    Console.WriteLine();
                    for (int i = 0; i < N + 1; i++)
                    {
                        Console.WriteLine("y(x" + i + ") = " + Hept(E, F)[i]);
                    }
                    break;
                case 2:
                    Console.WriteLine("Nhập sigma1");
                    double sigma1 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Nhập sigma2");
                    double sigma2 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Nhập muy1");
                    double muy1 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Nhập muy2");
                    double muy2 = Convert.ToDouble(Console.ReadLine());
                    for (int i = 0; i < N + 1; i++)
                    {
                        a[i] = p(x[i] - (h / 2));
                    }
                    a[N + 1] = p(x[N] + (h / 2));
                    B[0] = a[1] / h;
                    C[0] = (a[1] + Math.Pow(h, 2) * q(x[0]) / 2 + sigma1 * h) / h;
                    A[N] = a[N] / h;
                    C[N] = (a[N] + Math.Pow(h, 2) * q(x[N]) / 2 + sigma2 * h) / h;
                    for (int i = 1; i < N; i++)
                    {
                        A[i] = a[i] / (Math.Pow(h, 2));
                        B[i] = a[i + 1] / (Math.Pow(h, 2));
                        C[i] = (a[i] + a[i + 1] + Math.Pow(h, 2) * q(x[i])) / (Math.Pow(h, 2));
                    }
                    E[0, 0] = -C[0];
                    E[0, 1] = B[0];
                    E[N, N - 1] = A[N];
                    E[N, N] = -C[N];
                    F[0] = -muy1;
                    F[N] = -muy2;
                    for (int i = 1; i < N; i++)
                    {
                        for (int j = 0; j < N + 1; j++)
                        {
                            if (j == i - 1) E[i, j] = A[i];
                            else if (j == i) E[i, j] = -C[i];
                            else if (j == i + 1) E[i, j] = B[i];
                        }
                        F[i] = -f(x[i]);
                    }

                    Console.WriteLine();
                    for (int i = 0; i < N + 1; i++)
                    {
                        for (int j = 0; j < N + 1; j++)
                        {
                            Console.Write(E[i, j] + "   ");
                            if (j == N) Console.Write(F[i] + "\n");
                        }
                    }
                    Console.WriteLine();
                    for (int i = 0; i < N + 1; i++)
                    {
                        for (int j = 0; j < N + 1; j++)
                        {
                            Console.Write(E[i, j] + "   ");
                            if (j == N) Console.Write(F[i] + "\n");
                        }
                    }

                    Console.WriteLine();
                    for (int i = 0; i < N + 1; i++)
                    {
                        Console.WriteLine("y(x" + i + ") = " + Hept(E, F)[i]);
                    }
                    break;
                default:
                    goto back;
            }
        }
        public static void TriRieng()
        {
            Console.WriteLine("Nhập N");
            int N = Convert.ToInt16(Console.ReadLine());
            double[] x = new double[N + 1];
            double[] y = new double[N + 1];
            double[] a = new double[N + 2];
            double[] A = new double[N + 1];
            double[] B = new double[N + 1];
            double[] C = new double[N + 1];
            double[,] E = new double[N + 1, N + 1];
            double[] F = new double[N + 1];
            Console.WriteLine("Nhập a");
            x[0] = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập b");
            x[N] = Convert.ToDouble(Console.ReadLine());
            double h = (x[N] - x[0]) / N;
            for (int i = 1; i < N; i++)
            {
                x[i] = x[i - 1] + h;
            }
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Bài tập phưong pháp số");
            Console.WriteLine("-------------------------------------");
            int key;
        home:
            Console.WriteLine("1.Sơ đồ Horner");
            Console.WriteLine("2.Nội suy = Lagrange 1");
            Console.WriteLine("3.<ERROR>Nội suy = Lagrange 2");
            Console.WriteLine("4.Nội suy = Newton mốc bất kì");
            Console.WriteLine("5.Nội suy = Newton mốc cách đều");
            Console.WriteLine("6.Nội suy = Gauss 1");
            Console.WriteLine("7.Nội suy = Gauss 2");
            Console.WriteLine("8.Nội suy = Sterling");
            Console.WriteLine("9.Nội suy = Bessel");
            Console.WriteLine("10.Nội suy ngược = Lagrange");
            Console.WriteLine("11.Nội suy ngược = Newton(mốc cách đều)");
            Console.WriteLine("12.Phg pháp bình phg tối thiểu");
            Console.WriteLine("13.Hàm ghép trơn bậc 2");
            Console.WriteLine("14.Hàm ghép trơn bậc 3");
            Console.WriteLine("15.Hàm ghép trơn bậc 4");
            Console.WriteLine("16.Tích phân Newton-Cotes");
            //Console.ReadKey();
            Console.WriteLine("17.Giải bài toán Cauchy = Euler hiện");
            Console.WriteLine("18.Giải bài toán Cauchy = Euler hinh thang");
            Console.WriteLine("19.Giải bài toán Cauchy = Euler ẩn");
            Console.WriteLine("20.Giải bài toán Cauchy = R-K bậc 2");
            Console.WriteLine("21.Giải bài toán Cauchy = R-K bậc 3");
            Console.WriteLine("22.Giải bài toán Cauchy = R-K bậc 4");
            Console.WriteLine("23.Giải bài toán Cauchy = Adams Bashforth");
            Console.WriteLine("24:Giải bài toán Cauchy = Adams Moulton");
            Console.WriteLine("25.Giải bài toán biên");
            key = Convert.ToInt16(Console.ReadLine());
            Console.ReadLine();
            switch (key)
            {
                case 1:
                    Horner();
                    break;
                case 2:
                    Lagrange1();
                    break;
                case 3:
                    Lagrange2();
                    break;
                case 4:
                    Newton();
                    break;
                case 5:
                    NewtonCD();
                    break;
                case 6:
                    Gauss1();
                    break;
                case 7:
                    Gauss2();
                    break;
                case 8:
                    Sterling();
                    break;
                case 9:
                    Bessel();
                    break;
                case 10:
                    NguocLagrange();
                    break;
                case 11:
                    NguocNewton();
                    break;
                case 12:
                    BPTT();
                    break;
                case 13:
                    Gheptron2();
                    break;
                case 14:
                    Gheptron3();
                    break;
                case 15:
                    Gheptron4();
                    break;
                case 16:
                    NewtonCotes();
                    break;
                case 17:
                    EulerHien();
                    break;
                case 18:
                    EulerHT();
                    break;
                case 19:
                    EulerAn();
                    break;
                case 20:
                    RK2();
                    break;
                case 21:
                    RK3();
                    break;
                case 22:
                    RK4();
                    break;
                case 23:
                    AdamsBashforth();
                    break;
                case 24:
                    AdamsMoulton();
                    break;
                case 25:
                    BienBD();
                    break;
                default:
                    Console.WriteLine("Nhập lại");
                    goto home;
            }
        }
    }
}
