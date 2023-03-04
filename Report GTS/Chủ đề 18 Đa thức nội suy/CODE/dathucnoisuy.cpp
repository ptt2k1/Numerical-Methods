#include<bits/stdc++.h>
using namespace std;
int n=-1,N;
float x[10001],y[10001],X[10001],Y[10001],kq[10001];
float Van[1001][1001];
void kiemtratontai()
{
    for(int i=0;i<=N;i++)
    {
        bool check = true;
        for(int j=i+1;j<=N;j++)
        {
            if(X[i]==X[j])
            {
                check = false;
            }
        }
        if(check==true)
        {
            n++;
            x[n] = X[i];
            y[n] = Y[i];
        }
    }
}


void taoVandemonde()
{
    for(int i=0;i<=n;i++)
    {
        Van[i][0] = 1;
        for(int j=1;j<=n;j++)
        {
            float tg = x[i];
            Van[i][j] = 1;
            for(int k=1;k<=j;k++)
            {
                Van[i][j] *=tg;
            }
        }
    }
}
void giaimatranVan()
{
    float temp;
    for(int j=0;j<=n-1;j++)
    {
        for(int i=j+1;i<=n;i++)
        {
            temp = Van[i][j] / Van[j][j];
            for(int k=0;k<=n+1;k++)
            {
                Van[i][k] -= Van[j][k]*temp;
            }
        }
    }
    for(int i=n;i>=0;i--)
    {
        float s=0;
        for(int j=n;j>=i;j--)
        {
            s += Van[i][j] * kq[j];
        }
        kq[i] = (Van[i][n+1] - s)/Van[i][i];
    }

}
float tinhgiatri(float c)
{
    float b[10001],gt;
    b[n] = kq[n];
    for(int i=n-1;i>=0;i--)
    {
        b[i] = b[i+1] * c + kq[i];
    }
    gt = b[0];
    return gt;
}
int main()
{
    freopen("inp.txt","r",stdin);
    freopen("out.txt","w",stdout);
    cin >> N;
    for(int i=0;i<=N;i++)
    {
        cin >> X[i] >> Y[i];
    }
    kiemtratontai();
    cout << "----------------------------------------"; cout << endl;
    cout << "Cac moc noi suy ban dau la:";cout << endl;
    for(int i=0;i<=n;i++)
    {
        cout << setw(8) << x[i] << "    "<< setprecision(7) << y[i] <<"  ";
        cout << endl;
    }
    cout << "----------------------------------------"; cout << endl;
    taoVandemonde();
    cout << "Ma tran Vandemonde tao boi cac moc noi suy:";cout<<endl;
    for(int i=0;i<=n;i++)
    {
        Van[i][n+1] = y[i];
    }
    for(int i=0;i<=n;i++)
    {
        for(int j=0;j<=n;j++)
        {
            cout << setw(12) << setprecision(7) << Van[i][j];
        }
        cout << endl;
    }
    cout << "----------------------------------------"; cout << endl;
    giaimatranVan();
    cout << "Ma tran mo rong cua hpt duoc chuyen thanh ma tran bac thang nhu sau:"; cout << endl;
    for(int i=0;i<=n;i++)
    {
        for(int j=0;j<=n;j++)
        {
            cout << setw(8) << setprecision(7) <<Van[i][j]<< "  ";
        }
        cout << endl;
    }
    cout << "----------------------------------------"; cout << endl;
    cout << "Da thuc noi suy la: f(x) = ";
    for(int i=0;i<=n;i++)
    {
            if(i==0)  cout << "a" << i<<" + ";
            else if(i==1) cout << "a" << i << "x" << " + ";
            else if(i==n) cout << "a" << i << "x^"<<i;
            else cout << "a" << i << "x^"<<i << " + ";
    }
    cout << endl;
    for(int i=0;i<=n;i++) cout << setw(4) << setprecision(8) <<"x"<<i<<"= "<<kq[i]<<"  ";
    cout << endl;
    cout << "----------------------------------------"; cout << endl;
    float q;
    cin >> q;
    cout << "Tinh gia tri cua bieu thuc tai diem x = " << q << " la:";
    cout << endl << setprecision(10) << tinhgiatri(q);

}
