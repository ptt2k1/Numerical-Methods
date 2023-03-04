using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoiSuyLagrange
{
    class Program
    {
        #region tinh he so da thuc P = (x - x0)(x- x1) ... (x - x_(n-1))
        static double[] hoocnerNhan(double[] x, int n)
        {
            double[] temp = new double[n+1];
            double[] a = new double[n+1];
            a[n - 1] = 1;
            a[n] = - x[0];
            for (int i = 1; i <= n - 1; i++)
            {
                for (int j = n - 1 - i; j <= n - 1; j++)
                    temp[j] = a[j + 1];
                temp[n] = 0;
                for (int j = n; j >= n - i ; j--)
                    a[j] = temp[j] - x[i] * temp[j - 1];
                a[n - i - 1] = 1;
            }
            return a;
        }
        #endregion

        #region tinh he so da thuc Q bac n - 2 tai c voi P(x) = Q(x)(x - c) + P(c), P da thuc bac n - 1
        // => Gia tri cua da thuc P tai value = result [n - 1];
        static double[] hoocnerChia(double[] a, int soLuong, double value)
        {
            //o day: n dai dien cho la n - 1 phan tu x[i]
            double[] b = new double[soLuong];
            b[0] = a[0];
            for (int i = 1; i < soLuong; i++)
                b[i] = b[i - 1] * value + a[i];
            return b;
        }
        #endregion

        #region tinh he so cua da thuc noi suy Lagrange theo cong thuc 1: tong (y_i/D_i)*theta_i
        static double[] noiSuyLagrange1(double[] x, double[] y, int n)
        {
            double[] omega = new double[n];
            double[] a = new double[n];
            double D;
            double k;
            double[] temp = new double[n - 1];
            int m;
            for (int j = 0; j < n; j++)
                a[j] = 0;
            for (int i = 0; i < n; i++) //vong for nay duyet qua tat ca gia tri x_i
            {
                //gan mang temp = x de tinh tich (x - x_m) m != i
                m = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        temp[m] = x[j];
                        m ++;
                    }
                }
                omega = hoocnerNhan(temp, n - 1);
                D = hoocnerChia(omega, n, x[i])[n - 1]; //Gia tri dao ham tinh theo hoocner tai x[i]
                k = y[i]/D;
                for (int j = 0; j < n; j++)
                    a[j] += k * omega[j];
            }
            return a;
        }
        #endregion

        #region Tinh he so cua da thuc noi suy Lagrange theo cong thuc 2: tong cua (y_i/D_i)*(omega(x)/(x - x_i))
        static double[] noiSuyLagrange2(double[] x, double[] y, int n)
        {
            double[] a = new double[n];
            double[] omega = new double[n + 1];
            double[] phi = new double[n + 1];
            double D;
            double k;
            phi = hoocnerNhan(x, n);
            for (int j = 0; j < n; j++)
                a[j] = 0;
            for (int i = 0; i < n; i++)
            {
                omega = hoocnerChia(phi, n + 1, x[i]);
                D = hoocnerChia(omega, n, x[i])[n - 1];
                k = y[i]/D;
                for (int j = 0; j < n; j++)
                    a[j] += k * omega[j];
            }
            return a;
        }
        #endregion

        static void Main(string[] args)
        {
            string fileInput = @"input.txt";
            string[] data;
            string[] dataX;
            string[] dataY;
            int n;
            double[] x;
            double[] y;
            double[] daThucNoiSuy1;
            double[] daThucNoiSuy2;

            if (System.IO.File.Exists(fileInput))
            {
                data = System.IO.File.ReadAllLines(fileInput);
                
                dataX = data[0].Split(" ");
                dataY = data[1].Split(" ");

                n = dataX.Length;
                x = new double[n];
                y = new double[n];
                daThucNoiSuy1 = new double[n];
                daThucNoiSuy2 = new double[n];
                for (int i = 0; i < n; i++)
                {
                    x[i] = Convert.ToDouble(dataX[i]);
                    y[i] = Convert.ToDouble(dataY[i]);
                }

                daThucNoiSuy1 = noiSuyLagrange1(x, y, n);
                daThucNoiSuy2 = noiSuyLagrange2(x, y, n);


                StreamWriter sWrite = new StreamWriter("output.txt");
                sWrite.WriteLine("Da thuc bac {0} \nHe so cua da thuc noi suy theo cong thuc 1 la: ", n - 1);
                for (int i = 0; i < n; i++)
                    sWrite.Write("{0} \t", daThucNoiSuy1[i]);

                sWrite.WriteLine("\n\nDa thuc bac {0} \nHe so cua da thuc noi suy theo cong thuc 2 la: ", n - 1);
                for (int i = 0; i < n; i++)
                    sWrite.Write("{0} \t", daThucNoiSuy2[i]);

                sWrite.WriteLine("\n\nThu lai: ");
                for (int i = 0; i < n; i++)
                {
                    sWrite.WriteLine("\n\nTai x = {0}", x[i]);
                    sWrite.WriteLine("Gia tri P(x) - y = {0}", hoocnerChia(daThucNoiSuy2, n, x[i])[n - 1] - y[i]);
                }
                sWrite.Flush(); 
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
    }
}
