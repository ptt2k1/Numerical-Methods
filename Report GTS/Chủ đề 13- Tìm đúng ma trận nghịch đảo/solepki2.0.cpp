#include "mymath.h"
// // Coi fabs(a) < 1e-5 thì a=0 
int kiemtra(matran another) // kiem tra ma tran doi xung 
{
	for (int i=1;i<=another.gr();i++)
	 for(int j=1;j<=i;j++)
	 if (another.omatran(i,j)!=another.omatran(j,i))
	 return 1;
	 return 0; 
 } 
matran chuyenvi(matran another) // tao ma tran chuyen vi 
 {
 	matran T(another.gc(),another.gr());
 	for(int e=1;e<=another.gc();e++)
	 for(int f=1;f<=another.gr();f++)
	  T.gmatran(e,f,another.omatran(f,e));
	return T;
  }  
 double nhan(double x,double y,int sign)
 {
 	if( sign==1)
	 return -x*y;
	else
	 return x*y; 
 }
 double chia(double x, double y,int sign)
 {
 	if(sign==1)
	 return -x/y;
	else
	 return x/y; 
  } 
 void khaican(int i, double k, matran &another, int a[]) // tinh cac phan tu tren duong cheo cua ma tran tam giac tren 
 {
 	double S= k;
	for(int e=1;e<=i-1;e++)
	 S=S-nhan(another.omatran(e,i),another.omatran(e,i),a[e]);  
	 if (S<0)
	 {
	    a[i]=1;
	    another.gmatran(i,i,sqrt(-1*S));
	}
	 else 
	 {
	 	a[i]=0;
	    another.gmatran(i,i,sqrt(S));	 	
	  } 
 }
 void findSij(int i,int j,double h,matran &another,int a[]) // tinh cac phan tu cua ma tran tam giac tren 
 {
	for(int k=1;k<=i-1;k++)
	h=h-nhan(another.omatran(k,i),another.omatran(k,j),a[k]);
	another.gmatran(i,j,chia(h,another.omatran(i,i),a[i])); 
  }  
 void findSijdao(int i,int j,matran &another, matran &another2,int a[]) // tinh các phan tu cua ma tran tam giac tren nghich dao 
 {
 	double S=0;
	for(int e=i;e<=j-1;e++)
	S=S-nhan(another.omatran(i,e),another2.omatran(e,j),a[e]) ;
	another.gmatran(i,j,chia(S,another2.omatran(j,j),a[j])); 
}  
 int main()
  {
  	cout<<fixed<<setprecision(5);
	int m,k;
	double n; 
    FILE *f = fopen("D:\\matran.txt", "rt"); //Nhap du lieu tu file
    fscanf(f,"%d",&m); 
    matran input(m,m), output(m,m), chuyenvi1(m,m), tmp(m,m), S(m,m),Sdao(m,m), tmp2(1,1);
    int a[m+1]; 
    for(int i=1; i<=m; i++){
    	for(int j=1; j<=m; j++){
    		fscanf(f,"%lf",&n);
    		input.gmatran(i,j,n);
		}
	}
	fclose(f); 
	tmp=input;
    printf("Ma tran dau vao la \n") ;
    input.in();
    cout<<"----------------------------------------"<<endl;
	if  (kiemtra(input)==0)// Kiem tra ma tran lien ket va khoi tao neu can  
	 {
	 	printf("Ma tran dau vao la ma tran doi xung \n");
	 }
	else 
	 {
	 	chuyenvi1= chuyenvi(input);
	 	tmp= chuyenvi1* tmp;
	 	cout<<"ma tran moi la:"<<endl;
	 	tmp.in();
	  } 
    cout<<"----------------------------------------"<<endl;
	khaican(1,tmp.omatran(1,1),S,a); 
		for(int j=2;j<=input.gr();j++) 
		{
	      S.gmatran(1,j,chia(tmp.omatran(1,j),S.omatran(1,1),a[1]));
	    } 
		for(int f=2;f<=input.gr();f++)// tinh cac hang duoi cua ma tran tam giac tren 
		{
			khaican(f,tmp.omatran(f,f),S,a);
			if(fabs(S.omatran(f,f))<=1e-5)
			{
				k=1;
				break; 
			 } 
			for(int i=f+1;i<=input.gc();i++)
			{  
				findSij(f,i,tmp.omatran(f,i),S,a); 
			}
		 } 
	if(k!=1)// in ra ma tran tam giac tren neu co 
	{
		printf("Ma tran tam giac tren la: \n"); 
		for(int i=1;i<=S.gr();i++)
		{
		 for(int j=1;j<=S.gr();j++)
		 {
		 	if(a[i]==1)
			 printf("%.4Lfi  ",S.omatran(i,j));
			if(a[i]==0)
			 printf("%.4Lf  ",S.omatran(i,j)); 
		  } 		
		 printf("\n");	
		}
	 }
	printf("\n"); 
    cout<<"----------------------------------------"<<endl;	 
	if (k==1){
		printf("Ma tran ban dau suy bien nen khong co ma tran nghich dao\n");
		return 0;	
	}

	else
	{
		for(int i=1;i<=S.gr();i++)
		Sdao.gmatran(i,i,chia(1,S.omatran(i,i),a[i]));
		for(int i=1;i<=input.gr();i++)
		 for(int j=i+1;j<=input.gr();j++)		 
		 findSijdao(i,j,Sdao,S,a); // tinh các phan tu cua ma tran tam giac tren nghich dao 
		 printf("Ma tran nghich dao cua ma tran tam giac tren la \n") ;
		for(int i=1;i<=input.gr();i++)//In ma tran nghich dao tam giac tren  
		{
		 for(int j=1;j<=input.gr();j++)
		 {
		 	if(a[j]==1)
			 printf("%.4Lfi  ",Sdao.omatran(i,j));
			if(a[j]==0)
			 printf("%.4Lf  ",Sdao.omatran(i,j)); 
		  } 		
		 printf("\n");	
		}
	 }
    cout<<"----------------------------------------"<<endl;	 
	if (kiemtra(input)==0)
	{
		chuyenvi1=chuyenvi(Sdao); 
		for(int i=1;i<=input.gr(); i++){
			for(int j=1;j<=input.gr(); j++){
				double S=0;
				for(int e=1;e<=input.gr();e++){
					S=S + nhan(Sdao.omatran(i,e),chuyenvi1.omatran(e,j),a[e]);
				} 
				output.gmatran(i,j,S); 	
			} 
		} 
    	 printf("Ma tran nghich dao la \n") ;
    	 output.in(); 
    	 tmp= output*input;
		 cout<<"check \n"<<endl;
		 tmp.in(); 
}
	else 
	{
		tmp2=chuyenvi(Sdao); 			
		for(int i=1;i<=input.gr(); i++){
			for(int j=1;j<=input.gr(); j++){
				double S=0;
				for(int e=1;e<=input.gr();e++){
					S=S + nhan(Sdao.omatran(i,e),tmp2.omatran(e,j),a[e]);
				} 
				tmp.gmatran(i,j,S); 	
			} 
		} 		
		output=tmp*chuyenvi1; 	
		 printf("Ma tran nghich dao la \n") ;
		 output.in(); 
    	 tmp= output*input;
		 cout<<"check \n"<<endl;
		 tmp.in(); 		 
		 	 
	 } 
	FILE *z = fopen("D:\\ketqua.txt", "wt");
	fputs("Cholesky \n",z); 	
	for(int i=1;i<=output.gr();i++)
	{
	 for(int j=1;j<=output.gc();j++)
    {
	 fprintf(z,"%Lf",output.omatran(i,j)) ;	 
	 fputs(" ",z) ;
    }	
     fputs("\n",z);	
	}		 
	fclose(f); 
	return 0;		 
 }
