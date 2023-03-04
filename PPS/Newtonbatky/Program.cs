using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtonbatky
{
    class Program
    {
        #region tinh ty sai phan cac cap
        static double tysaiphan(double[] x, double[] y, int d,int e)
        {
            if(e == d) return y[e];
            if(e - d == 1) return (y[e]- y[e-1])/(x[e]-x[e-1]);
            return (tysaiphan(x,y,d+1,e)-tysaiphan(x,y,d,e-1))/(x[e]- x[d]);
        }
        static double[] tinhtsp(double[] x, double[] y, int n)
        {
            double[] tsp = new double[n];
            for(int i = 0; i<n; i++)
            {
                tsp[i] = tysaiphan(x,y,0,i);
            }
            return tsp;
        }
        #endregion

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
        static double[] hoocnerChia(double[] a, int soLuong, double value)
        {
            double[] b = new double[soLuong];
            b[0] = a[0];
            for (int i = 1; i < soLuong; i++)
                b[i] = b[i - 1] * value + a[i];
            return b;
        }
        #region Tinh ham noi suy
        static double[] fx (double[] x, double[] y, double[] tsp, int n)//n = 5
        {
            double[] a = new double[n];
            double[] temp = new double[n];
            double[] tich = new double[n];
            int check = 0;
            for(int i = 0; i<n; i++)
            {
                a[i] = 0;
            }
            for(int i = 0; i<n-1; i++)
            {
                for(int j = 0; j<=i; j++)
                {
                    temp[j] = x[j];
                    check++;
                }
                tich = hoocnerNhan(temp,check);

                for(int j =n-1; j>=0 ; j--)
                { 
                   
                    a[j] += tsp[i+1] * tich[check];
                    check--;
                    if(check<0) break;
                }
                check = 0;

            }
            a[n-1] += y[0];
            return a;
        }
        #endregion
        #region Tinh sai lech R
        static double[] sailech( double[] x, int n, double k)
        {
            double[] r = new double[n+1];
            double[] omega = new double[n+1];
            omega = hoocnerNhan(x,n);
            for(int i = 0; i<n+1; i++)
            {
                r[i] = k*omega[i];
            }
            return r;
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
            double[] f;
            double[] r;
            double[] daThucNoiSuy;

            if (System.IO.File.Exists(fileInput))
            {
                data = System.IO.File.ReadAllLines(fileInput);
                
                dataX = data[0].Split(" ");
                dataY = data[1].Split(" ");

                n = dataX.Length;
                x = new double[n];
                y = new double[n];
                f = new double[n];
                r = new double[n+1];
                double[] chia = new double[n+1];
                daThucNoiSuy = new double[n+1];
                for (int i = 0; i < n; i++)
                {
                    x[i] = Convert.ToDouble(dataX[i]);
                    y[i] = Convert.ToDouble(dataY[i]);
                }
                StreamWriter sWrite = new StreamWriter("output.txt");
                double[] tsp = new double[n];
                tsp = tinhtsp(x,y,n);
                sWrite.WriteLine("Day ty sai phan:");
                for(int i = 0; i<n; i++)
                {
                    sWrite.Write(tsp[i] + "  ");
                }
                sWrite.Write("\n");

                f = fx(x, y, tsp, n);
                r = sailech(x,n,tsp[n-1]);
                for(int i = 0; i<n+1; i++)
                {
                    if(i == 0)
                        daThucNoiSuy[i] = r[i];
                    else
                        daThucNoiSuy[i] = r[i] + f[i-1];
                }
                sWrite.WriteLine("He so cua da thuc noi suy la: ");
                for (int i = 0; i < n; i++)
                    sWrite.Write("{0} \t", f[i]);
                sWrite.Write("\n");
                sWrite.WriteLine("He so cua da thuc sai lech la: ");
                for (int i = 0; i < n+1; i++)
                    sWrite.Write("{0} \t", r[i]);
                sWrite.Write("\n");
                sWrite.WriteLine("He so cua da thuc la: ");
                for (int i = 0; i < n+1; i++)
                    sWrite.Write("{0} \t", daThucNoiSuy[i]);

                sWrite.WriteLine("\n\nThu lai: ");
                for (int i = 0; i < n; i++)
                {
                    chia = hoocnerChia(daThucNoiSuy, n+1, x[i]);
                    sWrite.WriteLine("\n\nTai x = {0}", x[i]);
                    sWrite.WriteLine("\n Da thuc sau khi chia");
                    for(int j = 0; j<=n; j++)
                        sWrite.Write(chia[j]+" ");
                    sWrite.WriteLine("\nGia tri P(x) = {0}", chia[n]);
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
