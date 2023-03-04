using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauss
{
    class Program
    {
        static int giaithua(int a)
        {
            if(a == 0) return 1;
            else
            {
                int gt = 1;
                for(int i= 1; i<=a; i++)
                gt = gt*i;
                return gt;
            }
        }
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
        static double[] hoocnerChia(double[] a, int soLuong, double value)

        {
            double[] b = new double[soLuong];
            b[0] = a[0];
            for (int i = 1; i < soLuong; i++)
                b[i] = b[i - 1] * value + a[i];
            return b;
        }
        static double[] hesogauss1(double[] y, int n) 
        {
            double[] dau = new double[n];
            double[] cuoi = new double[n];
            double[] hs = new double[n];
            double[] temp = new double[n];// mảng lưu y theo thứ tự kết nạp
            int k = 0;
            double p,q;
            for(int i = 0; i<n; i++)
            {
                if(i%2 != 0) {temp[i] = y[(n-1)/2+k];}
                else {temp[i] = y[(n-1)/2-k]; k++;}
            }
            for(int i = 0; i<n; i++)
            {
                if(i==0) 
                {
                    hs[i] = temp[i]; dau[i] = temp[i]; cuoi[i] = temp[i];
                }
                else
                {
                    if(i%2 != 0)
                    {
                        p = cuoi[0];
                        cuoi[0] = temp[i];
                        for(int j = 1; j<=i; j++)
                        {
                            q = cuoi[j];
                            cuoi[j] = cuoi[j-1] -p;
                            p = q;
                        }
                        dau[i] =cuoi[i];
                        hs[i] = cuoi[i];
                    }
                    else
                    {
                        p = dau[0];
                        dau[0] = temp[i];
                        for(int j = 1; j<=i; j++)
                        {
                            q = dau[j];
                            dau[j] = p - dau[j-1];
                            p = q;
                        }
                        cuoi[i] = dau[i];
                        hs[i] = dau[i];
                    }
                } 
            }
            for(int i = 0; i<n; i++)
            {
                hs[i]= hs[i]/giaithua(i);
            }
            return hs;
        }
        static double[] hesogauss2(double[] y, int n) 
        {
            double[] dau = new double[n];
            double[] cuoi = new double[n];
            double[] hs = new double[n];
            double[] temp = new double[n];// mảng lưu y theo thứ tự kết nạp
            int k = 0;
            for(int i = 0; i<n; i++)
            {
                if(i%2 != 0) {temp[i] = y[(n-1)/2-k];}
                else {temp[i] = y[(n-1)/2+k]; k++;}
            }
            for(int i = 0; i<n; i++)
            {
                if(i==0) 
                {
                    hs[i] = temp[i]; dau[i] = temp[i]; cuoi[i] = temp[i];
                }
                else
                {
                    if(i%2 == 0)
                    {
                        double p = cuoi[0];
                        double q;
                        cuoi[0] = temp[i];
                        for(int j = 1; j<=i; j++)
                        {
                            q = cuoi[j];
                            cuoi[j] = cuoi[j-1] -p;
                            p = q;
                        }
                        dau[i] =cuoi[i];
                        hs[i] = cuoi[i];
                    }
                    else
                    {
                        double p = dau[0];
                        double q;
                        dau[0] = temp[i];
                        for(int j = 1; j<=i; j++)
                        {
                            q = dau[j];
                            dau[j] = p -dau[j-1];
                            p = q;
                        }
                        cuoi[i] = dau[i];
                        hs[i] = dau[i];
                    }
                } 
            }
            for(int i = 0; i<n; i++)
            {
               hs[i]= hs[i]/giaithua(i);
            }
            return hs;
        }
        static double[] gauss1 (double[] hs, int n)
        {
            double[] a = new double[n];
            double[] temp = new double[n];
            double[] tich = new double[n];
            int check = 0;
            int k = 0;
            for(int i = 0; i<n; i++)
            {
                a[i] = 0;
            }
            for(int i = 0; i<n-1; i++)
            {
                for(int j = 0; j<i+1; j++)
                {
                        if(j %2 != 0)
                        {
                            k++;
                            temp[j] = k;
                        }
                        
                        else { temp[j] = -k;}
                    check++;
                }
                k = 0;
                tich = hoocnerNhan(temp,check);

                for(int j =n-1; j>=0 ; j--)
                { 
                   
                    a[j] += (hs[i+1] * tich[check]);
                    check--;
                    if(check<0) break;
                }
                check = 0;
            }
            a[n-1] += hs[0];
            return a;
        }
        static double[] gauss2 (double[] hsl, int n)
        {
            double[] a = new double[n];
            double[] temp = new double[n];
            double[] tich = new double[n];
            int check = 0;
            int k = 0;
            for(int i = 0; i<n; i++)
            {
                a[i] = 0;
            }
            for(int i = 0; i<n-1; i++)
            {
                for(int j = 0; j<i+1; j++)
                {
                        if(j %2 != 0)
                        {
                            k++;
                            temp[j] = -k;
                        }
                        
                        else { temp[j] = k;}
                        //Console.Write(temp[j]+" ");
                    check++;
                }
                //Console.Write("\n");
                k = 0;
                tich = hoocnerNhan(temp,check);

                for(int j =n-1; j>=0 ; j--)
                { 
                   
                    a[j] += (hsl[i+1] * tich[check]);
                    check--;
                    if(check<0) break;
                }
                check = 0;
            }
            a[n-1] += hsl[0];
            return a;
        }
        static double[] daoham(double[] a, int n)
        {
            double[] dh = new double[n-1];
            for(int i = 0; i<n-1; i++)
            dh[i] = a[i]*(n-i-1);
            return dh;
        }
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
            double[] hs1;
            double[] hs2;
            double[] g1;
            double[] g2;
            double[] dh;


            if (System.IO.File.Exists(fileInput))
            {
                data = System.IO.File.ReadAllLines(fileInput);
                
                dataX = data[0].Split(" ");
                dataY = data[1].Split(" ");

                n = dataX.Length;
                x = new double[n];
                y = new double[n];
                double[] chia = new double[n];
                for (int i = 0; i < n; i++)
                {
                    x[i] = Convert.ToDouble(dataX[i]);
                    y[i] = Convert.ToDouble(dataY[i]);
                }
                h = x[1] - x[0];
                hs1 = hesogauss1(y,n);
                g1 = new double[n]; g1 = gauss1(hs1,n);
                hs2 = hesogauss2(y,n); 
                g2 = new double[n]; g2 = gauss2(hs2,n);

                StreamWriter sWrite = new StreamWriter("output.txt");
                
                sWrite.WriteLine("Day he so Gauss 1 chua nhan:");
                for(int i = 0; i<n; i++)
                {
                    sWrite.Write(hs1[i] + "  ");
                }
                sWrite.Write("\n");

                sWrite.WriteLine("He so cua da thuc noi suy Gauss 1 la: ");
                for (int i = 0; i < n; i++)
                    sWrite.Write("{0} \t", g1[i]);

                sWrite.WriteLine("\n\nThu lai: ");
                for (int i = 0; i < n; i++)
                {
                    sWrite.WriteLine("\n\nTai x = {0}", x[i]);
                    chia = hoocnerChia(g1, n, (x[i]-x[(n-1)/2])/h);
                    sWrite.WriteLine("\n Da thuc sau khi chia");
                    for(int j = 0; j<n; j++)
                        sWrite.Write(chia[j]+" ");
                    sWrite.WriteLine("\nGia tri P(x) = {0}", chia[n-1]);
                }


                sWrite.WriteLine("\n\nTai x = {0}", 1.38);
                sWrite.WriteLine("Gia tri P(x) = {0}", hoocnerChia(g1, n, (1.38-x[(n-1)/2])/h)[n-1]);
                dh = new double[n-1];
                dh = daoham(g1,n);
                sWrite.WriteLine("He so cua da thuc dao ham: ");
                for (int i = 0; i < n-1; i++)
                    sWrite.Write("{0} \t", dh[i]);
                sWrite.WriteLine("\n\nDao Ham Tai x = {0}", 1.38);
                sWrite.WriteLine("Gia tri dh = {0}", hoocnerChia(dh, n-1, 1.38)[n-2]);

                


                sWrite.WriteLine("\n \n \n");
                sWrite.WriteLine("Day he so Gauss 2 chua nhan:");
                for(int i = 0; i<n; i++)
                {
                    sWrite.Write(hs2[i] + "  ");
                }
                sWrite.Write("\n");

                sWrite.WriteLine("He so cua da thuc noi suy Gauss 2 la: ");
                for (int i = 0; i < n; i++)
                    sWrite.Write("{0} \t", g2[i]);

                sWrite.WriteLine("\n\nThu lai: ");
                for (int i = 0; i < n; i++)
                {
                    sWrite.WriteLine("\n\nTai x = {0}", x[i]);
                    chia = hoocnerChia(g2, n, (x[i]-x[(n-1)/2])/h);
                    sWrite.WriteLine("\n Da thuc sau khi chia");
                    for(int j = 0; j<n; j++)
                        sWrite.Write(chia[j]+" ");
                    sWrite.WriteLine("\nGia tri P(x) = {0}", chia[n-1]);
                }
                sWrite.WriteLine("\n");
                sWrite.Flush(); 
            }
            else    
            {
                Console.WriteLine("File does not exist");
            }
            
        }

    }
}
