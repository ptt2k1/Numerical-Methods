using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bessel
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
            if(n == 1) return a;
            else
            {
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
        }
            
        static double[] hoocnerChia(double[] a, int soLuong, double value)

        {
            double[] b = new double[soLuong];
            b[0] = a[0];
            for (int i = 1; i < soLuong; i++)
                b[i] = b[i - 1] * value + a[i];
            return b;
        }
        static double[] heso(double[] y, int n) 
        {
            double[] dau = new double[n];
            double[] cuoi = new double[n];
            double[] hs = new double[n];
            double[] temp = new double[n];// mảng lưu y theo thứ tự kết nạp
            int k = 0;
            for(int i = 0; i<n; i++)
            {
                if(i%2 != 0) {temp[i] = y[(n-1)/2+k];}
                else {temp[i] = y[(n-1)/2-k]; k++;}
                Console.Write(temp[i] + " ");
            }
            for(int i = 0; i<n; i++)
            {
                if(i==0) 
                {
                    dau[i] = temp[i]; cuoi[i] = temp[i];
                }
                else
                {
                    if(i%2 != 0)
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
                        hs[i-1] = (dau[i-1]+cuoi[i-1])/2;
                        hs[i] = dau[i]; 
                    }
                    else
                    {
                        double p = dau[0];
                        double q;
                        dau[0] = temp[i];
                        for(int j = 1; j<=i; j++)
                        {
                            q = dau[j];
                            dau[j] = p - dau[j-1];
                            p = q;
                        }
                        cuoi[i] = dau[i];
                    }
                } 
            }
            for(int i = 0; i<n; i++)
            {
                hs[i]= hs[i]/giaithua(i);
            }
            return hs;
        }
        static double[] hamnoisuy(double[] hs, int n)
        {
            double[] a = new double[n];
            double[] chan = new double[n];
            double[] le = new double[n];
            double[] temp = new double[n];
            double[] tich = new double[(n)/2];
            
            int check =0;
            for(int i = 0; i<n; i++)
            {
                if(i%2 == 0)
                    chan[i/2] = hs[i];
                else
                    le[(i-1)/2] = hs[i];
            }
             for(int i = 0; i<n; i++)
            {
                a[i] = 0;
            }
            for(int i = 0; i<(n)/2; i++) 
            {
                for(int j = 0; j<=i; j++)
                {
                    if(j == 0) {temp[j] = 0; check++;}
                    else
                    {
                        temp[j] = (double)((2*j-1)*(2*j-1))/4;
                        check++;
                    }
                    
                }
                tich = hoocnerNhan(temp,check);
                for(int k = tich.Length-1; k>0; k--) //tạo mảng con của tich, chỉ tiến hành nhân với mảng con
                        tich[k] = tich[k-1];
                for(int k = 0; k<tich.Length; k++)
                    Console.Write(tich[k]+" ");
                Console.WriteLine("\n");
                tich[0] =0;
                for(int j =n-1; j>=0 ; j--)
                { 
                    if(j%2 == 0)
                    {a[j+1] += (chan[i] * tich[check]);check--;}
                    else {a[j-1] += le[i]*tich[check];}
                    if(check<0) break;
                }
                check = 0;
            }
            return a;
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
            double[] hs;
            double[] f;

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
                hs = heso(y,n);
                f = new double[n]; f = hamnoisuy(hs,n);

                StreamWriter sWrite = new StreamWriter("output.txt");
                
                sWrite.WriteLine("Day he so Sterling chua nhan:");
                for(int i = 0; i<n; i++)
                {
                    sWrite.Write(hs[i] + "  ");
                }
                sWrite.Write("\n");
                
                sWrite.WriteLine("He so cua da thuc noi suy Sterling la: ");
                for (int i = 0; i < n; i++)
                    sWrite.Write("{0} \t", f[i]);

                sWrite.WriteLine("\n\nThu lai: ");
                for (int i = 0; i < n; i++)
                {
                    sWrite.WriteLine("\n\nTai x = {0}", x[i]);
                    chia = hoocnerChia(f, n, (x[i]-x[(n-1)/2])/h-0.5);
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
