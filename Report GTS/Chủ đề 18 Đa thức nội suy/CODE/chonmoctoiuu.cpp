#include<bits/stdc++.h>
#include<math.h>
using namespace std;
#define pi 3.14159265358979323846
float x[100001];
float n;
int main()
{
    freopen("inp.txt","w",stdin);
    freopen("out.txt","w",stdout);
    cin >> n;

    for(int i=0;i<=n;i++)
    {
        x[i] = (cos(((2*i+1) / (2*n+2)) * pi))/pow(2,n);
    }

    for(int i=0;i<=n;i++) cout << x[i]<<" ";
}
