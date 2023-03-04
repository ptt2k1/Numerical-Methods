#include<bits/stdc++.h>
using namespace std;
float a[10001],b[10001],x;
int n;
float gt;
// Tinh gia tri cua da thuc tai 1 diem

float tinhgiatri(float c)
{
    b[n] = a[n];
    for(int i=n-1;i>=0;i--)
    {
        b[i] = b[i+1] * c + a[i];
    }
    gt = b[0];
    return gt;
}

// Tinh gia tri dao ham cua da thuc tai 1 diem

float tinhdaoham(float c)
{
    float d[10001];
    for(int i=n;i>=0;i--)
    {
        d[i-1] = i * a[i];
    }
    b[n-1] = d[n-1];
    for(int i=n-2;i>=0;i--)
    {
        b[i] = b[i+1] * c + d[i];
    }
    gt = b[0];
    return gt;
}

// Tim da thuc thuong va phan du khi lay da thuc chia cho don thuc

void chiadathuc(float c)
{
    b[n] = a[n];
    for(int i = n-1; i>=0; i--)
    {
        b[i] = b[i+1] * c + a[i];
    }
    for(int i=n;i>=1;i--) cout << b[i]<<" ";
    cout << b[0];

}
//Tim da thuc tich khi lay da thuc chia cho don thuc

void nhandathuc(float c)
{
    b[n+1] = a[n];
    b[n] = a[n];
    b[-1] = - a[0]*c;
    for(int i=n-1;i>=0;i--)
    {
        b[i] = a[i] - a[i+1]*c;
    }
    for(int i=n;i>=-1;i--) cout << b[i] << " ";
}

int main()
{
    freopen("inp.txt","r",stdin);
    freopen("out.txt","w",stdout);
    int t;
    cin >> n;
    for(int i=n;i>=0;i--) cin >> a[i];
    cin >> x >> t;
    if(t==1)
    {
        cout << "Gia tri cua da thuc f(x) tai diem x = " << x <<" la "<<tinhgiatri(x);
    }
    if(t==2)
    {
        cout << "Gia tri cua dao ham cua da thuc tai diem x = " << x << " la "<<tinhdaoham(x);
    }
    if(t==3)
    {
        chiadathuc(x);
    }
    if(t==4)
    {
        nhandathuc(x);
    }
}
