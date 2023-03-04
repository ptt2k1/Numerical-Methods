using System;
using System.IO; //Thu vien su dung thao tac voi file
using System.Collections.Generic; //Thu vien su dung List
using System.Collections; //Thu vien su dung ArrayList
using System.Linq; //Thu vien su dung List

namespace NoiSuyNewTonMocCachDeu
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

        #region Cong thuc noi suy new ton voi cong thuc sai phan tien
        static List<double> saiPhanTien(List<double> delta, double yk)
        {
            /*
            Mang f se co 1 hang: luu cac gia tri hang cheo chinh nguoc chua ca phan tu y
            */
            List<double> temp = new List<double>(delta);
            delta[0] = yk;
            delta.Add(0);
            for (int i = 1; i < delta.Count(); i++)
                delta[i] = Convert.ToDouble(delta[i - 1]) - Convert.ToDouble(temp[i - 1]); 

            temp.Clear();
            return delta;
        }
        
        static (List<double>, List<double>, List<double>, int) daThucNoiSuyNewTonTien(List<double> heSoDaThuc, List<double> delta, List<double> coeffPolyTich, int k, double yk, ref int giaiThua_k)
        {	
            if (k >= 1)
            {
                double c;
                List<double> heSoDaThucCu = new List<double>(heSoDaThuc); //sao chep toan bo heSoDaThuc sang heSoDaThucCu
                saiPhanTien(delta, yk);
                hoocnerNhanBoSung(coeffPolyTich, Convert.ToDouble(k - 1));

                giaiThua_k = giaiThua_k * k;
                c = Convert.ToDouble(delta[delta.Count() - 1]) / giaiThua_k;
                heSoDaThuc.Add(0);
                for (int i = 1; i < heSoDaThuc.Count(); i++)
                    heSoDaThuc[i] = heSoDaThucCu[i - 1] + c * coeffPolyTich[i];
                heSoDaThuc[0] = c;

                heSoDaThucCu.Clear();
            }
            return (heSoDaThuc, delta, coeffPolyTich, giaiThua_k);
        }
        #endregion


        #region cong thuc noi suy newton voi cong thuc sai phan lui
        static List<double> saiPhanLui(List<double> Lambda, double yk)
        {
            //gia tri k >= 1, vi se chi su dung sai phan khi mang co tu 2 phan tu tro len
            /*
            Mang f se chua: luu cac gia tri hang cheo chinh nguoc
            */
            List<double> temp = new List<double>(Lambda);
            Lambda[0] = yk;
            Lambda.Add(0);
            for (int i = 1; i < Lambda.Count(); i++)
                Lambda[i] = Convert.ToDouble(temp[i - 1]) - Convert.ToDouble(Lambda[i - 1]); 

            temp.Clear();
            return Lambda;
        }
        
        static (List<double>, List<double>, List<double>, int) daThucNoiSuyNewTonLui(List<double> heSoDaThuc, List<double> Lambda, List<double> coeffPolyTich, int k, double yk, ref int giaiThua_k)
        {	
            double c;
            if (k >= 1)
            {
                List<double> heSoDaThucCu = new List<double>(heSoDaThuc); //sao chep toan bo heSoDaThuc sang heSoDaThucCu
                saiPhanLui(Lambda, yk);
                hoocnerNhanBoSung(coeffPolyTich, Convert.ToDouble(1 - k));

                giaiThua_k = giaiThua_k * k;
                c = Convert.ToDouble(Lambda[Lambda.Count() - 1]) / giaiThua_k;
                heSoDaThuc.Add(0);
                for (int i = 1; i < heSoDaThuc.Count(); i++)
                    heSoDaThuc[i] = heSoDaThucCu[i - 1] + c * coeffPolyTich[i];
                heSoDaThuc[0] = c;

                heSoDaThucCu.Clear();
            }
            return (heSoDaThuc, Lambda, coeffPolyTich, giaiThua_k);
        }
        #endregion


        static void Main(string[] args)
        {
            string fileInput = @"input.txt"; //Doc file
            StreamWriter sWrite = new StreamWriter("output.txt"); //ghi vao file

            string[] data;
            string[] dataX;
            string[] dataY;
            int n;
            int giaiThua_k;
            int indexMin;
			double yk;
            double h;
            string temp;

            List<double> heSoDaThuc = new List<double>();
            List<double> coeffPolyTich = new List<double>();
            List<double> delta = new List<double>();

            List<double> Lambda = new List<double>();

            if (System.IO.File.Exists(fileInput))
            {
                data = System.IO.File.ReadAllLines(fileInput);
                
                dataX = data[0].Split(" ");
                dataY = data[1].Split(" ");

                n = dataX.Length;

                //Sap xep mang du lieu theo thu tu tang dan
                for (int i = 0; i < n; i++)
                {
                    indexMin = i;
                    for (int j = i + 1; j < n; j++)
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

                sWrite.WriteLine("\nDa thuc noi suy Newton moc cach deu tinh theo cong thuc sai phan TIEN \n\n");

                heSoDaThuc.Add(Convert.ToDouble(dataY[0]));
                delta.Add(Convert.ToDouble(dataY[0]));
                giaiThua_k = 1;
                
                sWrite.WriteLine("\nDa thuc noi suy ban dau voi moc noi suy ban dau x[0] = {0}, y[0] = {1} la P(x) = {1}", dataX[0], dataY[0]);

                for (int i = 1; i < n; i++)
                {
                    yk = Convert.ToDouble(dataY[i]);
                    daThucNoiSuyNewTonTien(heSoDaThuc, delta, coeffPolyTich, i, yk, ref giaiThua_k);

                    // sWrite.WriteLine("\n--------------------------------------------------------------------------------------");
                    // sWrite.WriteLine("\n\nDa thuc moi sau khi them moc noi suy x[{0}] = {1},  y[{0}] = {2}: ", i, dataX[i], dataY[i]);
                    
                    // sWrite.Write("Tap cac moc noi suy: \t");
                    // for (int j = 0; j <= i; j++)
                    //     sWrite.Write("({0}, {1}) ; \t", dataX[j], dataY[j]);

                    sWrite.WriteLine("\nDa thuc bac {0} voi he so cua da thuc la: ", i);
                    for (int j = 0; j < heSoDaThuc.Count(); j++)
                        sWrite.Write("{0} * x^{1} \t + \t", heSoDaThuc[j], heSoDaThuc.Count() - 1 - j);
                      
                    // sWrite.WriteLine("\n\nThu lai ");
                    // for (int j = 0; j <= i; j++)
                    // {
                    //     sWrite.WriteLine("Tai du lieu x{0} = {1}", j, dataX[j]);
                    //     sWrite.WriteLine("Pn(x) - y = {0}", hoocnerChia(heSoDaThuc, j)[heSoDaThuc.Count() - 1] - Convert.ToDouble(dataY[j]));
                    // }
                }
                sWrite.WriteLine("\nPn(x)= {0}", hoocnerChia(heSoDaThuc, (1893 - 1880)/5)[heSoDaThuc.Count() - 1]);

                // coeffPolyTich.Clear();
                // heSoDaThuc.Clear();



                // sWrite.WriteLine("\n\n---------------------------------------------------------------------------------------------\n");
                // sWrite.WriteLine("\nDa thuc noi suy Newton tinh theo cong thuc sai phan LUI ");
                
                // heSoDaThuc.Add(Convert.ToDouble(dataY[n - 1]));
                // Lambda.Add(Convert.ToDouble(dataY[n - 1]));
                // giaiThua_k = 1;
                // h = Convert.ToDouble(dataX[1]) - Convert.ToDouble(dataX[0]);
                    
                // sWrite.WriteLine("\nDa thuc noi suy ban dau voi moc noi suy ban dau x[0] = {0}, y[0] = {1} la P(x) = {1}", dataX[n - 1], dataY[n - 1]);

                // for (int i = 1; i < n; i++)
                // {
                //     yk = Convert.ToDouble(dataY[n - 1 - i]);
                //     daThucNoiSuyNewTonLui(heSoDaThuc, Lambda, coeffPolyTich, i, yk, ref giaiThua_k);

                //     // sWrite.WriteLine("\n-------------------------------------------------------------------------------------------");
                //     // sWrite.WriteLine("\n\nDa thuc moi sau khi them moc noi suy x[{0}] = {1},  y[{0}] = {2}: ", n - 1 - i, dataX[n - 1 - i], dataY[n - 1 - i]);
                    
                //     // sWrite.Write("Tap cac moc noi suy: \t");
                //     // for (int j = 0; j <= i; j++)
                //     //     sWrite.Write("({0}, {1}) ; \t", dataX[n - 1 - j], dataY[n - 1 - j]);

                //     sWrite.WriteLine("\nDa thuc bac {0} voi he so cua da thuc la: ", i);
                //     for (int j = 0; j < heSoDaThuc.Count(); j++)
                //         sWrite.Write("{0} * x^{1} \t + \t", heSoDaThuc[j], heSoDaThuc.Count() - 1 - j);
                      
                //     sWrite.WriteLine("\n\nThu lai ");
                //     // for (int j = 0; j <= i; j++)
                //     // {
                //     //     sWrite.WriteLine("Tai du lieu x{0} = {1}", j, dataX[n - 1 - j]);
                //     //     sWrite.WriteLine("Pn(x) - y = {0}", hoocnerChia(heSoDaThuc, - j)[heSoDaThuc.Count() - 1] - Convert.ToDouble(dataY[n - 1 - j]));
                //     // }
                // }
                // sWrite.WriteLine("\nPn(x)= {0}", hoocnerChia(heSoDaThuc, (1922 - 1880)/5)[heSoDaThuc.Count() - 1]);
                sWrite.Flush(); 
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
    }
}
