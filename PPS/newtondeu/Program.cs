using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtoncachdeu
{
    class Program
    {
        static double[] saiphantien(double[] y, int n)
        {
            double[] temp = new double[n]; 
            double[] spt = new double[n];
            for(int i = 0; i<n;  i++)
            {
                temp[i] = y[i];
            }
            spt[0] = y[0];
            for(int i = 1; i<n; i++)
            {
                for(int j = 0; j<n-1; j++)
                {
                    temp[j] = temp[j+1] - temp[j];
                }
                spt[i] = temp[0];
            }
            return spt;
        }
        static double[] saiphanlui(double[] y, int n)
        {
            double[] temp = new double[n]; 
            double[] spl = new double[n];
            for(int i = 0; i<n;  i++)
            {
                temp[i] = y[i];
            }
            spl[0] = y[n-1];
            for(int i = 1; i<n; i++)
            {
                for(int j = 0; j<n-1; j++)
                {
                    temp[j] = temp[j+1] - temp[j];
                }
                spl[i] = temp[n-i-1];
            }
            return spl;
        }
      
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
        static int giaithua(int a)
        {
            int gt = 1;
            for(int i= 1; i<=a; i++)
            gt = gt*i;
            return gt;
        }
        #region Tinh ham noi suy
        static double[] noisuytien (double[] spt, int n)//n = 6
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
                    temp[j] = j;
                    check++;
                }
                tich = hoocnerNhan(temp,check);

                for(int j =n-1; j>=0 ; j--)
                { 
                   
                    a[j] += (spt[i+1] * tich[check])/giaithua(i+1);
                    check--;
                    if(check<0) break;
                }
                check = 0;
            }
            a[n-1] += spt[0];
            return a;
        }
        static double[] noisuylui (double[] spl, int n)//n = 6
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
                    temp[j] = -j;
                    check++;
                }
                tich = hoocnerNhan(temp,check);

                for(int j =n-1; j>=0 ; j--)
                { 
                   
                    a[j] += (spl[i+1] * tich[check])/giaithua(i+1);
                    check--;
                    if(check<0) break;
                }
                check = 0;
            }
            a[n-1] += spl[0];
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
            double h;
            double[] daThucNoiSuy;
            double[] spt;
            double[] spl;

            if (System.IO.File.Exists(fileInput))
            {
                data = System.IO.File.ReadAllLines(fileInput);
                
                dataX = data[0].Split(" ");
                dataY = data[1].Split(" ");

                n = dataX.Length;
                x = new double[n];
                y = new double[n];
                double[] chia = new double[n];
                daThucNoiSuy = new double[n];
                for (int i = 0; i < n; i++)
                {
                    x[i] = Convert.ToDouble(dataX[i]);
                    y[i] = Convert.ToDouble(dataY[i]);
                }
                h = x[1] - x[0];
                StreamWriter sWrite = new StreamWriter("output.txt");
                spt = new double[n]; spt = saiphantien(y,n);
                spl = new double[n]; spl = saiphanlui(y,n);
                sWrite.WriteLine("Day sai phan tien:");
                for(int i = 0; i<n; i++)
                {
                    sWrite.Write(spt[i] + "  ");
                }
                sWrite.Write("\n");
                 sWrite.WriteLine("Day sai phan lui:");
                for(int i = 0; i<n; i++)
                {
                    sWrite.Write(spl[i] + "  ");
                }
                sWrite.Write("\n");
                daThucNoiSuy = noisuytien(spt,n);

                sWrite.WriteLine("He so cua da thuc noi suy tien la: ");
                for (int i = 0; i < n; i++)
                    sWrite.Write("{0} \t", daThucNoiSuy[i]);

                sWrite.WriteLine("\n\nThu lai: ");
                for (int i = 0; i < n; i++)
                {
                    sWrite.WriteLine("\n\nTai x = {0}", x[i]);
                    chia = hoocnerChia(daThucNoiSuy, n, (x[i]-x[0])/h);
                    sWrite.WriteLine("\n Da thuc sau khi chia");
                    for(int j = 0; j<n; j++)
                        sWrite.Write(chia[j]+" ");
                    sWrite.WriteLine("\nGia tri P(x) = {0}", chia[n-1]);
                }
                sWrite.WriteLine("\n\nTai x = {0}", 45);
                    sWrite.WriteLine("Gia tri P(x) = {0}", hoocnerChia(daThucNoiSuy, n, (45-x[0])/h)[n-1]);

                sWrite.WriteLine("\n");
                
                daThucNoiSuy = noisuylui(spl,n);
                sWrite.WriteLine("He so cua da thuc noi suy lui la: ");
                for (int i = 0; i < n; i++)
                    sWrite.Write("{0} \t", daThucNoiSuy[i]);

                sWrite.WriteLine("\n\nThu lai: ");
                for (int i = 0; i < n; i++)
                {
                    sWrite.WriteLine("\n\nTai x = {0}", x[i]);
                    chia = hoocnerChia(daThucNoiSuy, n, (x[i]-x[n-1])/h);
                    sWrite.WriteLine("\n Da thuc sau khi chia");
                    for(int j = 0; j<n; j++)
                        sWrite.Write(chia[j]+" ");
                    sWrite.WriteLine("\nGia tri P(x) = {0}", chia[n-1]);
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
