using System;
using System.IO;

namespace newtonCotez
{
    class Program
    {
        static double TinhGiaTri(double[] a, double value)
        {
            double b = a[0];
            for (int i = 1; i < a.Length - 1; i++)
                b = b * value + a[i];
            return b;
        }

        #region tinh he so da thuc P = t(t- 1) ... (t - (n-1))
        static double[] hoocnerNhan(int n)
        {
            double[] a = new double[n + 2];
            double[] temp = new double[n + 2];
            a[0] = 1;
            a[1] = 0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= i; j++)
                    temp[j] = a[j];
                a[i + 1] = 0;
                for (int j = 1; j <= i + 1; j++)
                    a[j] = a[j] - i * temp[j - 1];
            }
            return a;
        }
        #endregion

        static double[] tinhA_i(int n)
        {
            double[] A_i = new double [n + 1];
            double[] a = hoocnerNhan(n);
            double[] F = new double[a.Length + 1];
            double coeff;
            double temp1, temp2;

            for (int i = 0; i < a.Length; i++)
                Console.Write("{0} ", a[i]);

            for (int i = 0; i <= n/2; i++)
            {
                temp1 = 0;
                temp2 = 0;
                coeff = TinhGiaTri(a, i);

                Console.WriteLine("\n he so la: {0}", coeff);

                for (int j = 0; j < F.Length - 1; j++)
                    F[j] = a[j]/(F.Length - j);
                F[F.Length - 2] = 0;
                F[F.Length - 1] = 0;
                Console.WriteLine("\nGia tri cua F.leng la : {0}", F.Length);
                //tính giá trị tích phân tại t = n và t = 0
                // Console.WriteLine("\nGia tri F[n] = {0} \nF[1] = {1}", hoocnerChia(F,n)[F.Length - 1], hoocnerChia(F,0)[F.Length - 1]);
                

                //for (int j = 0; j < F.Length)

               // Console.WriteLine("\nGia tri cua F.leng la : {0}", F.Length);
                // Console.WriteLine("\nGia tri coeff = {0}", coeff);
                // Console.WriteLine("\nGia tri A[{0}] = {1}", i, A_i[i]);
                // Console.WriteLine("\n----------------------------------------------------------");
                A_i[n - i] = A_i[i];
            }
            return A_i;
        }

        static void Main(string[] args)
        {
            string fileInput = @"input.txt"; //Doc file
            StreamWriter sWrite = new StreamWriter("output.txt"); //ghi vao file

            string[] data;
            string[] dataX;
            string[] dataY;
            double[] A_i;
            double a, b, integralValue = 0, h;

            if (System.IO.File.Exists(fileInput))
            {
                data = System.IO.File.ReadAllLines(fileInput);

                #region phiên bản dạng file tex cho 2 dòng x và y
                dataX = data[0].Split(" ");
                dataY = data[1].Split(" ");
                #endregion

                #region phiên bản dạng file tex cho 2 cột x và y
                #endregion
                
                a = Convert.ToDouble(dataX[0]);
                b = Convert.ToDouble(dataX[1]);

                Console.WriteLine(dataY.Length);
                h = (b - a) / (dataY.Length - 1);
                A_i = tinhA_i(dataY.Length - 1);

                // for (int i = 0; i < A_i.Length; i++)
                //     Console.WriteLine("gia tri A_{0} = {1}", i, A_i[i]);
                
                // Console.ReadLine();
                // for (int i = 0; i <= dataY.Length - 1; i++)
                //     integralValue += A_i[i] * Convert.ToDouble(dataY[i]);
                
                // Console.WriteLine("\nGia tri tich phan tu a -> b cua f(x) la: {0}", integralValue * h);

                sWrite.Flush(); 
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
    }
}