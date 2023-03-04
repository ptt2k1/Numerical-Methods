using System;
using System.IO; //Thu vien su dung thao tac voi file
using System.Collections.Generic; //Thu vien su dung List
using System.Linq; //Thu vien su dung List

namespace NoiSuyTrungTam
{
    class Program
    {
        #region tinh he so da thuc Q bac n - 2 tai c voi P(x) = Q(x)(x - c) + P(c), P da thuc bac n - 1
        static double[] hoocnerChia(List<double> a, double value)
        {
            int n = a.Count();
            //o day: n dai dien cho la n - 1 phan tu x[i]
            double[] b = new double[n];
            b[0] = a[0];
            for (int i = 1; i < n; i++)
                b[i] = b[i - 1] * value + a[i];
            return b;
        }
        #endregion      

        static List<double> hoocnerNhanBoSung(List<double> coeffPolyTich, double t)
        {
            int n = coeffPolyTich.Count(); //la so he so co trong da thuc tich
            if (n == 0)
            {
                coeffPolyTich.Add(1);
                coeffPolyTich.Add(-t);
            }
            else if (n >= 2)
            {
                coeffPolyTich.Add(0);
                n = n + 1;
                List<double> temp = new List<double>(coeffPolyTich); //De sao chep toan bo du lieu tu coeffPolyTich sang temp

                for (int i = 1; i < n; i++)
                    coeffPolyTich[i] = coeffPolyTich[i] - t * temp[i - 1];

                temp.Clear(); //xoa bo nho trong List
            }
            return coeffPolyTich;
        }

        #region Noi suy trung tam Gauss
        static List<List<double>> saiPhan(List<List<double>> f, double yk, double x_0, double xk)
        {
            /*
            mang f[][] la mang 2 chieu co 2 hang:
            + hang 0: chua cac phan tu tren duong cheo chinh
            + hang 1: chua cac phan tu thuoc hang cuoi cung cua ma tran
            */
            if (xk > x_0)
            {
                List<double> temp = new List<double>(f[0]);
                f[0].Add(0);
                f[0][0] = yk;
                for (int i = 1; i < f[0].Count(); i++)
                    f[0][i] = f[0][i - 1] - temp[i - 1];

                f[1].Add(f[0][f[0].Count() - 1]); 
                temp.Clear();
            }
            else
            {
                List<double> temp = new List<double>(f[1]);
                f[1].Add(0);
                f[1][0] = yk;
                for (int i = 1; i < f[1].Count(); i++)
                    f[1][i] = temp[i - 1] - f[1][i - 1];

                f[0].Add(f[1][f[1].Count() - 1]); 
                temp.Clear();
            }
            return f;
        }

        static (List<double>, List<double>, List<List<double>>, double) noiSuyTrungTamGauss(List<double> heSoDaThuc, List<double> coeffTich, List<List<double>> f, int k, double x0, double xk, double yk, ref int giaiThua_k)
        {
            double c;
            if (k == 0)
            {
                heSoDaThuc.Add(yk);
                List<double> temp1 = new List<double>();
                List<double> temp2 = new List<double>();
                f.Add(temp1);
                f.Add(temp2);
                f[0].Add(yk);
                f[1].Add(yk);

                giaiThua_k = 1;
            }
            else
            {
                List<double> heSoDaThucCu = new List<double>(heSoDaThuc); //sao chep toan bo heSoDaThuc sang heSoDaThucCu
                saiPhan(f, yk, x0, xk);

                giaiThua_k *= heSoDaThuc.Count();
                if (k > 0)
                    k = k - 1; 
                
                hoocnerNhanBoSung(coeffTich, - k);

                c = f[1][f[1].Count() - 1] / giaiThua_k;
                heSoDaThuc.Add(0);
                for (int i = 1; i < heSoDaThuc.Count(); i++)
                    heSoDaThuc[i] = heSoDaThucCu[i - 1] + c * coeffTich[i];
                heSoDaThuc[0] = c;

                heSoDaThucCu.Clear();
            }
            return (heSoDaThuc, coeffTich, f, giaiThua_k); 
        }
        #endregion

        static void Main(string[] args)
        {
            string fileInput = @"input.txt"; //Doc file
            StreamWriter sWrite = new StreamWriter("output.txt"); //ghi vao file

            string[] data;
            string[] dataX;
            string[] dataY;
            int n, chiSo, k, m, luaChon, answers;
            int indexMin;
            int giaiThua_k = 1;
            string temp;
            double h, x_0, x_k, y_k;

            List<double> heSoDaThuc = new List<double>();
            List<List<double>> f = new List<List<double>>();
            List<double> coeffTich = new List<double>();
            List<double> x = new List<double>();
            List<double> y = new List<double>();

            if (System.IO.File.Exists(fileInput))
            {
                data = System.IO.File.ReadAllLines(fileInput);

                dataX = data[0].Split(" ");
                dataY = data[1].Split(" ");

                //Sap xep mang du lieu theo thu tu tang dan
                for (int i = 0; i < dataX.Length; i++)
                {
                    indexMin = i;
                    for (int j = i + 1; j < dataX.Length; j++)
                    {
                        if (Convert.ToDouble(dataX[j]) < Convert.ToDouble(dataX[indexMin]))
                            indexMin = j;
                    }
                    if (indexMin != i)
                    {
                        temp = dataX[i];
                        dataX[i] = dataX[indexMin];
                        dataX[indexMin] = temp;

                        temp = dataY[i];
                        dataY[i] = dataY[indexMin];
                        dataY[indexMin] = temp;
                    }
                }

                h = Convert.ToDouble(dataX[1]) - Convert.ToDouble(dataX[0]);
                /*Kiem tra dieu kien xem day du lieu co cach deu ko

                */

                Console.Write("\n\nDa thuc noi suy trung tam Gauss 1: \nChon moc noi suy ban dau x voi x thuoc ({0}, {1}), voi khoang cach giua cac moc noi suy h = {2} \nx = ", dataX[0], dataX[dataX.Length - 1], h);
                x_0 = Convert.ToDouble(Console.ReadLine());

                m = Convert.ToInt32((x_0 - Convert.ToDouble(dataX[0])) / h);

                while(x_0 - (m * h + Convert.ToDouble(dataX[0])) > 1e-10 && x_0 > Convert.ToDouble(dataX[dataX.Length - 1]) && x_0 < Convert.ToDouble(dataX[0]))
                {
                    Console.Write("\n\nMoc noi suy chon khong hop le! \nChon lai moc noi suy ban dau x voi x thuoc ({0}, {1}), voi khoang cach giua cac moc noi suy h = {2}: \nx = ", dataX[0], dataX[dataX.Length - 1], h);
                    x_0 = Convert.ToDouble(Console.ReadLine());
                    m = Convert.ToInt32((x_0 - Convert.ToDouble(dataX[0])) / h);
                }

                //so moc noi suy co the duoc chon
                if (m <= dataX.Length/2)
                    n = 2 * m + 1;
                else
                    n = 2 * (dataX.Length - m) + 1;
                if (n > dataX.Length)  
                    n = dataX.Length;

                Console.Write("\n\nNhap vao so moc noi suy muon su dung, so moc co the chon la SO LE thuoc [{0}, {1}] \nn = ", 1, n);
                luaChon = Convert.ToInt32(Console.ReadLine());
                while(luaChon % 2 == 0 || luaChon > n || luaChon < 0)
                {
                    Console.Write("\n\nSo moc noi suy ban chon khong hop le! \nNhap vao so moc noi suy muon su dung, so moc co the chon la SO LE thuoc [{0}, {1}] \nn = ", 1, n);
                    luaChon = Convert.ToInt32(Console.ReadLine());
                }

                //sap xep du lieu theo thu tu them moc noi suy Gauss 1
                for (int i = 0; i < luaChon; i++)
                {
                    if (i % 2 == 1)
                    {
                        k = (i + 1)/2;
                        chiSo = m + k;
                        x.Add(Convert.ToDouble(dataX[chiSo]));
                        y.Add(Convert.ToDouble(dataY[chiSo]));
                    }
                    else
                    {
                        k = -(i + 1)/2;
                        chiSo = m + k;
                        x.Add(Convert.ToDouble(dataX[chiSo]));
                        y.Add(Convert.ToDouble(dataY[chiSo]));
                    }
                }

                sWrite.WriteLine("\nDa thuc noi suy trung tam theo cong thuc Gauss 1 voi phep doi bien t = (x - x0)/h");
                sWrite.WriteLine("\nMoc noi suy ban dau duoc chon la (x[{0}], y[{0}]) = ({1}, {2}):",m, x[0], y[0]);
                
                for (int i = 0; i < luaChon; i++)
                {
                    if (i % 2 == 1)
                        k = (i + 1)/2;
                    else
                        k = -(i + 1)/2;

                    noiSuyTrungTamGauss(heSoDaThuc, coeffTich, f, k, x_0, x[i], y[i], ref giaiThua_k);

                    sWrite.WriteLine("\n\n--------------------------------------------------------------------------------------");
                    sWrite.WriteLine("\nDa thuc moi sau khi them moc noi suy x[{0}] = {1},  y[{0}] = {2}: ", m + k, x[i], y[i]);
                    
                    sWrite.Write("Tap cac moc noi suy: \t");
                    for (int j = 0; j <= i; j++)
                        sWrite.Write("({0}, {1}) ; \t", x[j], y[j]);

                    sWrite.Write("\nDa thuc bac {0}: \np(t) = : ", i);
                    for (int j = 0; j < heSoDaThuc.Count(); j++)
                        sWrite.Write("{0} * x^{1} \t + \t", heSoDaThuc[j], heSoDaThuc.Count() - 1 - j);
                      
                    sWrite.WriteLine("\n\nThu lai ");
                    for (int j = 0; j <= i; j++)
                    {
                        sWrite.WriteLine("Tai du lieu x = {0}", x[j]);
                        sWrite.WriteLine("Pn(x) - y = {0}", hoocnerChia(heSoDaThuc, (x[j] - x[0])/h)[heSoDaThuc.Count() - 1] - y[j]);
                    }
                }

                Console.Write("\nBan co muon thuc hien them moc noi suy? \n0: khong \n1: co \nLua chon: ");
                answers = Convert.ToInt32(Console.ReadLine());
                while(answers == 1)
                {
                    for (int i = 1; i <= 2; i++)
                    {
                        luaChon = luaChon + 1;
                        Console.WriteLine("\nNhap vao moc noi suy moi thu 1: ");
                        Console.Write("\nx = ");
                        x_k = Convert.ToDouble(Console.ReadLine());
                        Console.Write("\ny = ");
                        y_k = Convert.ToDouble(Console.ReadLine());
                        m = Convert.ToInt32((x_k - x_0)/h);
                        if(x_k - (m*h + x_0) >= 1e-10)
                            
                        noiSuyTrungTamGauss(heSoDaThuc, coeffTich, f, luaChon, x_0, x[i], y[i], ref giaiThua_k);
                        
                    }
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
