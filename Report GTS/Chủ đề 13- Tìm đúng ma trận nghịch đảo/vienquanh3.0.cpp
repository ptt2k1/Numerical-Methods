#include "mymath.h" 
// coi fabs(a) < 1e-10 thì a=0 
matran henshin(matran B11, matran B12, matran B21, double b22){
	matran henshin(B11.gr()+1,B11.gr()+1);
	for(int i=1; i<=B11.gr(); i++){
		for(int j=1; j<=B11.gr(); j++){
			henshin.gmatran(i,j,B11.omatran(i,j)); 
		} 
	} 	
	for(int i=1; i<= B12.gr(); i++){
		henshin.gmatran(i,B11.gr()+1,B12.omatran(i,1)); 
	} 
	for(int i=1; i<= B21.gc(); i++){
		henshin.gmatran(B11.gr()+1,i,B21.omatran(1,i)); 
	} 	
	henshin.gmatran(B11.gr()+1,B11.gr()+1,b22);
	return henshin; 
} 
matran chuyenvi(matran another)
 {
 	matran T(another.gc(),another.gr());
 	for(int e=1;e<=another.gc();e++)
	 for(int f=1;f<=another.gr();f++)
	  T.gmatran(e,f,another.omatran(f,e));
	return T;
  }  
matran A12(matran another,int n){
	matran A12(n-1,1);
	for(int i=1; i<=n-1; i++){
		A12.gmatran(i,1,another.omatran(i,n)); 
	} 
	return A12; 
} 
matran A21(matran another,int n){
	matran A21(1,n-1);
	for(int i=1; i<=n-1; i++){
		A21.gmatran(1,i,another.omatran(n,i)); 
	} 
	return A21; 	 
} 
matran A22(matran another, int n){
	matran A22(1,1); 
	A22.gmatran(1,1,another.omatran(n,n));
	return A22; 
}
int main()
{
	cout<<fixed<<setprecision(5);
	int m,k=0;
	double n,p; 
    FILE *f = fopen("D:\\matran.txt","rt"); //Nhap du lieu tu file
    fscanf(f,"%d",&m); 
    matran input(m,m), output(2,2), chuyenvi1(m,m), B11(2,2), B12(2,1),B21(1,2),X(2,1),Y(1,2),theta(1,1),tmp(m,m);
    for(int i=1; i<=m; i++){
    	for(int j=1; j<=m; j++){
    		fscanf(f,"%lf",&n);
    		input.gmatran(i,j,n);
		}
	}
	fclose(f); 
	tmp=input; 
	p=input.omatran(1,1)*input.omatran(2,2)-input.omatran(1,2)*input.omatran(2,1);
	if(fabs(p)==0){
		chuyenvi1= chuyenvi(input); 
		tmp= chuyenvi1*input; 
		k=1; 
		p=tmp.omatran(1,1)*tmp.omatran(2,2)-tmp.omatran(1,2)*tmp.omatran(2,1);		
	}	  
	output.gmatran(1,1,tmp.omatran(2,2)/p);
	output.gmatran(2,2,tmp.omatran(1,1)/p);	
	output.gmatran(1,2,-tmp.omatran(1,2)/p);
	output.gmatran(2,1,-tmp.omatran(2,1)/p);
	for(int i=3;i<=input.gr();i++){
		X= output*A12(tmp,i);
		Y= A21(tmp,i)*output;
		theta= A22(tmp,i)- Y*A12(tmp,i); 
		if(fabs(theta.omatran(1,1))<1e-10){
			if(k==1){
				cout<<"Ma tran suy bien :))) "<<endl; 
				return 0; 
			} 
			else{
				cout<<"ma tran dau vao co dinh thuc con chinh bang 0"<<endl; 
			    chuyenvi1= chuyenvi(tmp);
			    tmp= chuyenvi1*tmp;
			    k=1;
			    i=2;
		        p=tmp.omatran(1,1)*tmp.omatran(2,2)-tmp.omatran(1,2)*tmp.omatran(2,1);			 
		        if(fabs(p)<1e-12){
		        	cout<<"ma tran suy bien :(("<<endl; 
		        	return 0; 
		    	} 
	         	output.gmatran(1,1,tmp.omatran(2,2)/p);
             	output.gmatran(2,2,tmp.omatran(1,1)/p);	
             	output.gmatran(1,2,-tmp.omatran(1,2)/p);
             	output.gmatran(2,1,-tmp.omatran(2,1)/p);		
             	continue; 
	    	} 	
		} 
		B11= output+ (X*Y)*(1/theta.omatran(1,1));
		B12= (-1*X )*(1/theta.omatran(1,1));
		B21= (-1*Y)*(1/theta.omatran(1,1));		
		output = henshin(B11,B12,B21, (1/theta.omatran(1,1))) ;
		cout<<"Ma tran nghich dao cua ma tran con chinh cap "<<i<<endl;
		output.in(); 
		cout<<"------------------------------------------------------------------"<<endl; 
	}
	if(k==0){
		cout<<"Ma tran nghich dao la"<<endl;
		output.in(); 
	} 
	else{
		output= output* chuyenvi1; 
		cout<<"ma tran nghich dao la "<<endl; 
		output.in(); 
	} 
	cout<<"check"<<endl;
	input= output*input;
	input.in();
 	FILE *v = fopen("D:\\ketqua.txt", "wt"); /// Luu ket qua vao file
 	fputs("vien quanh 3.0 \n",v); 
	for(int i=1;i<=output.gr();i++)
	{
	 for(int j=1;j<=output.gc();j++)
    {
	 fprintf(v,"%Lf",output.omatran(i,j)) ;	 	  
	 fputs(" ",v) ;
    }	
     fputs("\n",v);	
	}  	   
	  return 0; 
 } 
