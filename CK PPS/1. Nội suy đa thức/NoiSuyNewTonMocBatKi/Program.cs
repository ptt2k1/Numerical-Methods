using System;
using System.IO; //Thu vien su dung thao tac voi file
using System.Collections.Generic; //Thu vien su dung List
using System.Collections; //thu vien su dung ArrayList 
using System.Linq; //Thu vien su dung List
using System.Threading; //Thu vien dung lenh sleep
using System.Text;
using System.Threading.Tasks;

namespace NoiSuyNewTon
{
    class Program
    {
        #region tinh he so da thuc Q bac n - 2 tai c voi P(x) = Q(x)(x - c) + P(c), P da thuc bac n - 1
        // => Gia tri cua da thuc P tai value = result [n - 1];
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

        //them du lieu xk va yk vao mang x va y truoc, co nghia la ham tySaiPhan se su dung them moc noi suy (xk, yk) va xk = x[k - 1], yk = y[k - 1]
        //Moi lan goi ham tySaiPhanCapk thi mang x va y da duoc them phan tu xk, yk vao cuoi 2 mang roi
        static List<ArrayList> tySaiPhanCapk(List<ArrayList> f, List<double> x, List<double> y)
        {
            int k = x.Count();
            //gia tri k >= 2, vi se chi su dung ty sai phan khi mang co tu 2 phan tu tro len
            if(k >= 2)
            {
                int index = k - 2;
                ArrayList temp = new ArrayList();
                double a;
                temp.Add((y[k - 1] - y[k - 2]) / (x[k - 1] - x[k - 2]));
                // Moi lan goi ham tySaiPhanCapk thi f se them 1 hang temp
                f.Add(temp);
                for (int j = 1; j <= k - 2; j++)
                {
                    a = (Convert.ToDouble(f[index + 1 - j][j- 1]) - Convert.ToDouble(f[index - j][j - 1])) / (x[k - 1] - x[index - j]);
                    f[index - j].Add(a);
                }
            }
            return f;
        }

        /*
        hoocner nhan cai tien se tinh he so cua da thuc (x - xi) voi i = 0 -> n
        Y tuong la: khi tinh he so cua da thuc (x - xi) = (x - xk)(x - xn) voi k = 0 -> n - 1, 
        se su dung he so da biet cua da thuc truoc (x - xi) voi i = 0 -> n - 1
        mang a la mang chua 

        pn = a0 + a1x + a2x^2 + ... + an x^n
        */
        //su dung ham hoocnerNhan bo sung nay voi so phan tu >= 2
        static List<double> hoocnerNhanBoSung(List<double> coeffPolyTich, double xk)
        {
            int n = coeffPolyTich.Count(); //la so he so co trong da thua tich, n = 2 <=> x - x0
            if (n == 0)
            {
                coeffPolyTich.Add(1);
                coeffPolyTich.Add(-xk);
            }
            else if (n >= 2)
            {
                coeffPolyTich.Add(0);
                n = n + 1;
                List<double> temp = new List<double>(coeffPolyTich); //De sao chep toan bo du lieu tu coeffPolyTich sang temp

                for (int i = 1; i < n; i++)
                    coeffPolyTich[i] = coeffPolyTich[i] - xk * temp[i - 1];
            }
            return coeffPolyTich;
        }
        
        static (List<double>, List<ArrayList>, List<double>) daThucNoiSuyNewTon(List<double> heSoDaThuc, List<ArrayList> f, List<double> coeffPolyTich, List<double> x, List<double> y, double xk, double yk)
        {	
			x.Add(xk);
			y.Add(yk);
            int k = x.Count();
            double c;
            if (k >= 2)
            {
                List<double> heSoDaThucCu = new List<double>(heSoDaThuc); //sao chep toan bo heSoDaThuc sang heSoDaThucCu
                tySaiPhanCapk(f, x, y);
                c = Convert.ToDouble(f[0][f.Count() - 1]);
                hoocnerNhanBoSung(coeffPolyTich, x[k - 2]);
                heSoDaThuc.Add(0);
                for (int i = 1; i < k; i++)
                    heSoDaThuc[i] = heSoDaThucCu[i - 1] + c * coeffPolyTich[i];
                heSoDaThuc[0] = c;
            }
            return (heSoDaThuc, f, coeffPolyTich);
        }

          static void Main(string[] args)
        {
            string fileInput = @"input.txt"; //Doc file
            StreamWriter sWrite = new StreamWriter("output.txt"); //ghi vao file

            string[] data;
            string[] dataX;
            string[] dataY;
            int n;
			double xk;
			double yk;
            List<double> heSoDaThuc = new List<double>();
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            List<double> coeffPolyTich = new List<double>();
            List<ArrayList> f = new List<ArrayList>();

            if (System.IO.File.Exists(fileInput))
            {
                data = System.IO.File.ReadAllLines(fileInput);
                
                dataX = data[0].Split(" ");
                dataY = data[1].Split(" ");

                n = dataX.Length;
                x.Add(Convert.ToDouble(dataX[0]));
                y.Add(Convert.ToDouble(dataY[0]));
                heSoDaThuc.Add(y[0]);

                sWrite.WriteLine("\nDa thuc noi suy ban dau voi moc noi suy ban dau x[0] = {0}, y[0] = {1} la P(x) = {1}", x[0], y[0]);

                for (int i = 1; i < n; i++)
                {
                    xk = Convert.ToDouble(dataX[i]);
                    yk = Convert.ToDouble(dataY[i]);
                    daThucNoiSuyNewTon(heSoDaThuc,f, coeffPolyTich, x, y, xk, yk);

                    sWrite.WriteLine("\n-------------------------------------------------------------------");
                    sWrite.WriteLine("\n\nDa thuc moi sau khi them moc noi suy x[{0}] = {1},  y[{0}] = {2}: ", i, x[i], y[i]);
                    
                    sWrite.Write("Tap cac moc noi suy: \t");
                    for (int j = 0; j < x.Count(); j++)
                        sWrite.Write("({0}, {1}) ; \t", x[j], y[j]);

                    sWrite.WriteLine("\nDa thuc bac {0} voi he so cua da thuc la: ", i);
                    for (int j = 0; j < heSoDaThuc.Count(); j++)
                        sWrite.Write("{0} * x^{1} \t", heSoDaThuc[j], heSoDaThuc.Count() - 1 - j);
                      
                    sWrite.WriteLine("\n\nThu lai ");
                    for (int j = 0; j < heSoDaThuc.Count(); j++)
                    {
                        sWrite.WriteLine("Tai du lieu x{0} = {1}", j, x[j]);
                        sWrite.WriteLine("Pn(x) - y = {0}", hoocnerChia(heSoDaThuc, x[j])[heSoDaThuc.Count() - 1] - y[j]);
                    }
                }
                // // sWrite.WriteLine("Tai du lieu x{0} = {1}", j, x[j]);
                sWrite.WriteLine("\nPn(x)= {0}", hoocnerChia(heSoDaThuc, 1922)[heSoDaThuc.Count() - 1]);
                // // sWrite.WriteLine("\nDa thuc bac {0} voi he so cua da thuc la: ", i);
                //     for (int j = 0; j < heSoDaThuc.Count(); j++)
                       
                sWrite.Flush(); 
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
    }
}
