syms x;
s=input('Nhap sai so gioi han: ');
key=0;
y=input('Nhap ham f(x)=@(x)...: ');
while key==0
    disp('Chon vung khao sat cho x (vi du: chon tu -10 -> 10, buoc nhay 0.1)');
    min=input('Nhap can duoi x: ');
    max=input('Nhap can tren x: ');
    d=input('Nhap buoc nhay: ');
    t=min:d:max;
    f=y(t);
    f1=matlabFunction(diff(y(x)));
    f2=matlabFunction(diff(f1(x)));
    h=f2(t);
    disp('Ta co Do thi:');
    plot(t,f,'-b',t,h,'--r');
    title('Do thi f(x)(duong mau xanh) và f"(x)(duong mau do)');
    axis on;
    grid on;
    key=input('Quan sat do thi, neu da thoa man, nhap 1, neu chua, nhap 0: ');
end
disp('Tu do thi, uoc luong khoang phan ly nghiem [a,b] sao cho thoa man dieu kien va so nghiem cua phuong trinh');
sn=input('Nhap so nghiem phuong trinh co the co: ');
alpha=zeros(1,sn);
for i=1:1:sn
    disp('Tim nghiem thu ');
    disp(i);
    a=input('Nhap a: ');
    b=input('Nhap b: ');
    if f2((a+b)/2)*y(a)>0
        x0=a;
        x1=a-y(a)/f1(a);
    else
        x0=b;
        x1=b-y(b)/f1(b);
    end
    f1m=matlabFunction(abs(f1(x)));
    m1=fminbnd(f1m,a,b);
    e=abs(y(x1)./m1);
    while e>s
        x0=x1;
        x1=x0-y(x0)/f1(x0);
        e=abs(y(x1)./m1);
    end
    disp('Nghiem gan dung: ');
    alpha(1,i)=x1;
    alpha(1,i)
end
disp('Cac nghiem: ')
alpha
    