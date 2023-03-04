#include<bits/stdc++.h>

using namespace std;

#define maxn 202

int n;

struct matran{
  double K[maxn][maxn];

};

struct Vector{
    double H[maxn];
    Vector()
    {
        H[0]=0;
    }
};

int f[maxn];


matran A;

Vector B;

int cmp(int x,int y)
{
    return (f[x]<f[y]);
}

int id[maxn];

matran sapxep(matran x)
{
    for(int i=1;i<=n;i++) id[i]=i;

    for(int i=1;i<=n;i++)
    {
     int root=1;
        for(int j=1;j<=n;j++)
            if(abs(x.K[i][j])>abs(x.K[i][root])) root=j;
        f[i]=root;
    }

    matran y;

    sort(id+1,id+1+n,cmp);

    f[n+1]=-1;

    for(int i=1;i<=n;i++) if(f[id[i]]==f[id[i+1]])
    {
        for(int j=1;j<=n;j++) id[j]=j;

        for(int j=1;j<=n;j++)
        {
            int root=1;
            for(int t=1;t<=n;t++)
                if(abs(x.K[t][j])>abs(x.K[root][j])) root=t;

            f[j]=root;
        }

        sort(id+1,id+1+n,cmp);

        for(int j=1;j<=n;j++) if(f[id[j]]==f[id[j+1]]) return x;

        for(int j=1;j<=n;j++)
            for(int t=1;t<=n;t++)
            y.K[t][j]=x.K[t][id[j]];

        return y;
    }

    for(int i=1;i<=n;i++)
        for(int j=1;j<=n;j++)
            y.K[i][j]=x.K[id[i]][j];

    Vector C;

    for(int i=1;i<=n;i++)
        C.H[i]=B.H[id[i]];

    B=C;

    return y;
}

int CalDig(matran x)
{
    int p=0;

    for(int i=1;i<=n;i++)
    {
        double ans=0;
        for(int j=1;j<=n;j++)  ans+=abs(x.K[i][j]);

        if(2*abs(x.K[i][i])>ans) p++;
    }

    if(p==n) return 1;

    p=0;

    for(int j=1;j<=n;j++)
    {
        double ans=0;
        for(int i=1;i<=n;i++) ans+=abs(x.K[i][j]);

        if(2*abs(x.K[j][j])>ans) p++;
    }
    if(p==n) return 2;
    return 0;
}

matran setAtoC(matran x,int p)
{
    matran C;
    if(p==1)
    for(int i=1;i<=n;i++)
        for(int j=1;j<=n;j++)
            if(i!=j) C.K[i][j]=-(x.K[i][j]/x.K[i][i]);
            else C.K[i][j]=0;
    else
    for(int i=1;i<=n;i++)
        for(int j=1;j<=n;j++)
    {
        C.K[i][j]=-x.K[i][j]/x.K[j][j];
        if(i==j) C.K[i][j]=0;
    }

    return C;
}

Vector setBtoD(Vector B,matran x,int p)
{
    Vector D;
    for(int i=1;i<=n;i++) D.H[i]=B.H[i]/x.K[i][i];

    if(p==1) return D;
    else return B;
}

double getNorm(Vector D,int p)
{
    double max1=D.H[1],ans=0;
    for(int i=1;i<=n;i++) {max1=max(max1,abs(D.H[i]));ans+=abs(D.H[i]);}

    if(p==2) return ans;
    return max1;
}


double getY1(matran C,int p)
{
    double y=-1000000000;

    for(int i=1;i<=n;i++)
    {
        double q=0,p=0;
        for(int j=i;j<=n;j++) q+=abs(C.K[i][j]);
        for(int j=1;j<=i-1;j++) p+=abs(C.K[i][j]);

        y=max(y,q/(1-p));
    }

    if(p==1) return y;

    y=-1000000000;

    for(int j=1;j<=n;j++)
    {
        double p=0,q=0;

        for(int i=1;i<=j;i++) q+=abs(C.K[i][j]);
        for(int i=j+1;i<=n;i++) p+=abs(C.K[i][j]);

        y=max(y,q/(1-p));
    }

    return y;
}

typedef pair<double,double> ii;

double getY2(matran C)
{

    double y=-1000000000,s=-1000000000;
    for(int j=1;j<=n;j++)

    {
        double q=0,p=0;
        for(int i=j+1;i<=n;i++) p+=abs(C.K[i][j]);

         s=max(s,p);
    }

    return (double)(1/(1-s));
}

Vector operator - (Vector a,Vector b)
{
    Vector x;

    for(int i=1;i<=n;i++)
        x.H[i]=a.H[i]-b.H[i];

    return x;
}


Vector SeidelLoop(matran B,Vector D,int p,double eps,Vector Xo,double y,double s)
{
    double eps1,eps2;
    eps1=pow((double)10,log10(eps))*0.5;
    eps2=eps-eps1;

    int cnt=0;

    Vector X=Xo,X1;

    do
    {
        X1=X;
        for(int i=1;i<=n;i++)
        {
            X.H[i]=0;
            for(int j=1;j<=i-1;j++)
                X.H[i]+=X.H[j]*B.K[i][j];
            for(int j=i;j<=n;j++) X.H[i]+=B.K[i][j]*X1.H[j];
            X.H[i]+=D.H[i];
        }
     cnt++;
    }
    while (s*y/(1-y)*getNorm(X-X1,p)>eps2);
    cout<<"|   So lan lap: "<<cnt<<"                     "<<endl;

    cout<<"|-------------------------------------";
    cout<<endl;

    return X;
}

Vector X4;

void khangg(matran A,Vector B,double espp,int p)
{
    double max1=-1000000;

    matran C=setAtoC(A,p);
    Vector D=setBtoD(B,A,p);

    for(int i=1;i<=n;i++)
        for(int j=1;j<=n;j++)
        if(i==j) max1=max(max1,1/A.K[i][j]);

    X4=SeidelLoop(C,D,p,espp,X4,getY1(C,p),((p==1)?1:(getY2(C)*max1)));

    for(int i=1;i<=n;i++)
        cout << fixed << setprecision(13)<<"|   Ngiem thu "<<i<<": "<<((p==1)?X4.H[i]:(X4.H[i]*(1/A.K[i][i])))<<"      "<<endl;

        for(int i=1;i<=n;i++) X4.H[i]=((p==1)?X4.H[i]:(X4.H[i]*(1/A.K[i][i])));

    cout<<"|-------------------------------------";

    cout<<endl;
}

int main()
{
    freopen("gauss-seidel.INP","r",stdin);
    freopen("gauss-seidel.OUT","w",stdout);

    cin>>n;

    double espp;cin>>espp;

    for(int i=1;i<=n;i++)
        for(int j=1;j<=n;j++) cin>>A.K[i][j];

    for(int i=1;i<=n;i++) cin>>B.H[i];

    int p=CalDig(A);

   for(int i=1;i<=n;i++) X4.H[i]=rand() % 100 + 1;

   cout<<"X0 ban dau: "<<endl;
    for(int i=1;i<=n;i++) cout<<X4.H[i]<<endl;

    //for(int i=1;i<=n;i++) X4.H[i]=i;

    cout<<endl;

    cout<<"|-------------------------------------"<<endl;

    cout<<"|   chuan: "<<p<<"                          "<<endl;

    cout<<"|-------------------------------------";
    cout<<endl;

    if(p==0) {cout<<"Not diagonally dominant matrix";return 1;}
    khangg(A,B,espp,p);

    Vector S;

    for(int i=1;i<=n;i++)
    {
     S.H[i]=0;
    for(int j=1;j<=n;j++)
        S.H[i]=S.H[i]+X4.H[j]*A.K[i][j];
    }

    cout<<"|   KIEM TRA LAI!"<<"                     "<<endl;

    for(int i=1;i<=n;i++) cout<<"|   B["<<i<<"] : "<<S.H[i]<<"            "<<endl;
    cout<<"|-------------------------------------";
}
