#include <bits/stdc++.h>
#define  step 1.0e-6
#define  pi   3.14159265
#define e   2.718281828459
using namespace std;
//--------------------------//
int dem=0;
double f( double x)
{
    return pow(e,x*x-2*x-1)+x-2*x*x*x;
}
//-----------------------------------------------//
double x( double a, double b, string s)
{
    double i, xmax, xmin;
    xmax=a; xmin=a; i=a;
    do
    {
        if (f(i) < f(xmin))
            {
                xmin=i;
            }
        if (f(i) > f(xmax))
            {
                xmax=i;
            }
        i+=step;
        dem++;
    }
    while (i<=b);

    if    (s=="max") return xmax;
    else             return xmin;
}
//-----------------------------------------------//
double f1( double a, double b, string s)
{
    double i, f1max, f1min;
    f1max=f(a); f1min=f(a); i=a;
    do
    {
        if (f(i) < f1min)
            {
                f1min=f(i);
            }
        if (f(i) > f1max)
            {
                f1max=f(i);
            }
        i+=step;
    }
    while (i<=b);

    if    (s=="max") return f1max;
    else             return f1min;
}
//----------------------------------------------//
int main()
{
    double a=-10, b=10;
    printf("Min cua f(x) trong khoang [%3.2f,%2.2f] tai: (%2.5f, %2.5f)\n",a,b,x(a,b,"min"),f1(a,b,"min"));
    printf("Max cua f(x) trong khoang [%3.2f,%2.2f] tai: (%2.5f, %2.5f)\n",a,b,x(a,b,"max"),f1(a,b,"max"));
    printf("So lan duyet: %d",dem);
}
