#include "mymath.h" 
// Coi fabs(a) < 1e-10 thì a=0 
struct phantugiai{
	double giatri; 
	int hanggoc;
	int cotgoc;
	int hangtam;
	int cottam;	
};
typedef struct phantugiai phantugiai; 
phantugiai  X; 
void xoahang(int i,int j,matran &another, matran &R) // Xoa hang co phan tu giai 
 {
 	for (int e=i+1; e<=another.gr()-j; e++){
 		for(int f=1; f<=another.gc()-j; f++){
 			another.gmatran(e-1,f,another.omatran(e,f));
 			R.gmatran(e-1,1,R.omatran(e,1));
		 } 
	 }	 
 }
int xoacot(int i,int j, matran &another, matran &C) // Xoa cot co phan tu giai 
 {
 	for(int e=i+1; e<=another.gc()-j; e++){
 		for(int f=1; f<=another.gr()-j; f++){
 			another.gmatran(f,e-1,another.omatran(f,e));
			C.gmatran(f,e-1,C.omatran(1,e)); 
		 } 
	 }	 
 }
void transform(int i,int j, int k, matran &another){ // Bien doi ma tran A
 	for (int e=1;e<i;e++)
	 {
	 	double tam=another.omatran(e,j);
		for (int f=1; f<=another.gc()-k; f++){
			another.gmatran(e,f,another.omatran(e,f)-tam/another.omatran(i,j)*another.omatran(i,f));
		}
	  } 
	for (int e=i+1; e<=another.gc()-k; e++)
	{
		double tam1= another.omatran(e,j);
		for (int f=1; f<=another.gc()-k; f++){
			another.gmatran(e,f,another.omatran(e,f)-tam1/another.omatran(i,j)*another.omatran(i,f));			
		} 
	 } 	
}

void transform(int i, int j, matran &another, matran &another1)// Bien doi ma tran B va ma tran don vi 
  {
  	for (int e=1;e<= another.gr();e++)
	 {
	 	double tam= another.omatran(e,j);
	 	if (e==i){
	 		continue; 
		 } 
		for (int f=1; f<=another.gc(); f++) 
		{
		    another.gmatran(e,f,another.omatran(e,f)-tam/another.omatran(i,j)*another.omatran(i,f));
		    another1.gmatran(e,f,another1.omatran(e,f)-tam/another.omatran(i,j)*another1.omatran(i,f));		    
		}
	  } 	 
   } 
phantugiai find(int i, matran another, matran R, matran C){ // goi tim phan tu troi
	phantugiai X; 
	double MAX=0; 
    MAX= another.omatran(1,1); 
	X.hangtam= 1;
	X.cottam= 1;
	X.hanggoc= R.omatran(1,1);
	X.cotgoc= C.omatran(1,1); 
	for(int e=1; e<=another.gr()-i; e++){
		for(int f=1; f<=another.gc()-i; f++){
			if(fabs(another.omatran(e,f))==1){
				X.giatri= another.omatran(e,f);
				X.hangtam= e;
				X.cottam= f;
				X.hanggoc= R.omatran(e,1);
				X.cotgoc= C.omatran(1,f);
				return X;	 
			}
			if(fabs(MAX)< fabs(another.omatran(e,f))){
				MAX= another.omatran(e,f); 
				X.giatri= another.omatran(e,f);
				X.hangtam= e;
				X.cottam= f;
				X.hanggoc= R.omatran(e,1);
				X.cotgoc= C.omatran(1,f);				
			} 
		}
	}
	X.giatri= MAX; 
	return X; 	
}
void chuanhoa( matran &another, matran &another1){ // chuan hoa ma tran B va E
	for(int i=1; i<=another.gr(); i++){
		int a= 0;		
		if(fabs(another.omatran(i,i))>1e-10){
			double tmp= another.omatran(i,i);		
			for(int j=1; j<=another.gc(); j++){
				another.gmatran(i,j,another.omatran(i,j)/tmp);
				another1.gmatran(i,j,another1.omatran(i,j)/tmp);				
			}
		}
		else{
			double tmp;
			for(int e=1; e<=another.gc(); e++){
				if(fabs(another.omatran(i,e))>1e-10){
					a=e;
					break;
				}
			}
			for(int e=1; e<=another.gc(); e++){
				tmp= another.omatran(i,e);
				another.gmatran(i,e,another.omatran(a,e));
				another.gmatran(a,e,tmp);
				tmp= another1.omatran(i,e);
				another1.gmatran(i,e,another1.omatran(a,e));
				another1.gmatran(a,e,tmp);				
			}
			i--;	
		}	
    }
	return ; 
} 
int main(){
	cout<<fixed<<setprecision(5);
	int m,a=0;
	double n; 
    FILE *f = fopen("D:\\matran.txt", "rt"); //Nhap du lieu tu file
    fscanf(f,"%d",&m); 
    matran input(m,m), output(m,m), tmp(m,m), tmp2(m,m), R(m,1), C(1,m);
    for(int i=1; i<=m; i++){
    	R.gmatran(i,1,i); 
    	C.gmatran(1,i,i);  
    	for(int j=1; j<=m; j++){
    		fscanf(f,"%lf",&n);
    		input.gmatran(i,j,n);
    		if(i==j){
    			output.gmatran(i,i,1); 
			} 
		}
	}
	fclose(f);  
	tmp2= input;	
	tmp= input; 
	while (a<input.gr()){	
		X= find(a, tmp, R, C);
		if(fabs(X.giatri)<1e-10){
			cout<<" Ma tran suy bien :)))) "<<endl;
			return 0; 
		} 	
		transform(X.hangtam, X.cottam, a, tmp);
		transform(X.hanggoc, X.cotgoc, tmp2,output); 
		xoacot(X.cottam,a,tmp,C);			
		xoahang(X.hangtam,a,tmp,R);
		a++; 
		cout<<"Lan bien doi thu "<<a<<endl; 
		cout<<"Ma tran B la"<<endl; tmp2.in();
		cout<<"Ma tran E la"<<endl; output.in();
		cout<<"-----------------------------------------------"<<endl;
	} 
	chuanhoa(tmp2,output);
	cout<<"Sau khi chuan hoa"<<endl<<"Ma tran B la"<<endl;
	tmp2.in();
	cout<<"Ma tran nghich dao la"<<endl;
	output.in();
  	FILE *k = fopen("D:\\ketqua.txt", "wt");
  	fputs("gaussjordan3.0 \n",k); 
	for(int i=1; i<=output.gr(); i++){
		for(int j=1; j<=output.gc(); j++){
			fprintf(k,"%.6Lf",output.omatran(i,j)) ;
			fputs(" ",k) ;
		}
		fputs("\n",k);	
	}	
	return 0; 
 }


