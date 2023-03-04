#include <bits/stdc++.h>
using namespace std; 
class vecto
{
	private:
	vector<long double> ker;
	public:
	vecto (); 
	vecto(long double a[],int n); 
	vecto(int i); 
	void pbvecto(long double k); 
	void in(); 
	long double ovecto(int i);
	int size(); 
	void gvecto(int i, long double k); 
	vecto operator + (vecto another);
	vecto operator - (vecto another); 
	vecto operator * (long double k); 
	friend vecto operator *(long double k,vecto another); 
	void pobvecto();
	void reset(); 
 } ;
vecto::vecto(){
	ker.push_back(0); 
} 
vecto::vecto(long double a[], int n){
	for(int i=0;i<=n-1;i++){
		ker.push_back(a[i]) ;
	} 
} 
void vecto::reset(){
	for(int i=0;i<=this->size()-1;i++){
		this->gvecto(i,0); 
	} 
} 
vecto::vecto(int i){
 	for(int k=1;k<=i;k++)
	 ker.push_back(0); 	
}
void vecto::pbvecto(long double k){
	ker.push_back(k); 
}
void vecto::in(){
	for(int i=0;i<=ker.size()-1;i++)
	cout<< ker[i] << " ";
	cout<< endl; 
}
int vecto::size(){
	return ker.size(); 
} 
void vecto::gvecto(int i, long double k){
	if (i>this->size()-1)
	return; 
	ker[i]=k; 
} 
vecto vecto::operator +(vecto another){
	int t,a,max,min;
	t=this->size();
	a=another.size();
	if(a>=t){
		max=a;
		min=t; 
	}
	else{
		max=t;
		min=a; 
	}
	vecto tong(max);
	for(int i=0;i<=max-1;i++){
		if(i<=min-1){
			tong.ker[i]=this->ker[i]+another.ker[i]; 
		}
		else{
			if(max==a) 
			tong.ker[i]=another.ker[i];
			else
			tong.ker[i]=this->ker[i]; 
		} 
	} 
	return tong; 
} 
vecto vecto::operator -(vecto another){
	int t,a,max,min;
	t=this->size();
	a=another.size();
	if(a>=t){
		max=a;
		min=t; 
	}
	else{
		max=t;
		min=a; 
	}
	vecto tong(max);
	for(int i=0;i<=max-1;i++){
		if(i<=min-1){
			tong.ker[i]=this->ker[i]-another.ker[i]; 
		}
		else{
			if(max==a) 
			tong.ker[i]=-another.ker[i];
			else
			tong.ker[i]=this->ker[i]; 
		} 
	} 
	return tong; 
} 
vecto operator *(long double k, vecto another){
	vecto nhan(another.size()) ;
	for(int i=0;i<=another.size()-1;i++)
	nhan.ker[i]=k*another.ker[i];
	return nhan; 
} 
vecto vecto:: operator *(long double k){
	vecto nhan(this->size()) ;
	for(int i=0;i<=this->size()-1;i++)
	nhan.ker[i]=k*this->ker[i];
	return nhan; 
} 
long double vecto::ovecto(int i){
	return this->ker[i];
}
void vecto::pobvecto(){
	this->ker.pop_back();
}
long double tvh(vecto another1, vecto another2){
	long double S=0;
	if(another1.size()!=another2.size()){
		cout<<"Hai vecto khong cung kich co"<<endl;
		return 0; 
	} 
	else{
		for(int i=0;i<=another1.size()-1;i++){
			S=S+another1.ovecto(i)*another2.ovecto(i); 
		} 
		return S; 
	} 
}
//////////////////////////////////////// Dinh nghia da thuc  
class dathuc : public vecto{
	public:
	dathuc (): vecto(){
	} 
	dathuc(int i): vecto(i){ 
	};
	dathuc(long double a[], int n): vecto(a,n){
	} ;
	void chuanhoa();
	int deg();
	void in();
	void changevt(vecto another); 
	long double calc(long double k);
	long double daoham(long double k); 
	dathuc operator *(long double k); 
	friend dathuc operator * (long double k, dathuc another); 
	dathuc operator / (dathuc another);
    dathuc operator + (dathuc another);
	dathuc operator - (dathuc another);
	dathuc operator * (dathuc another); 
};
void dathuc::changevt(vecto another){
	for(int i=0;i<=another.size()-1;i++){
		this->pbvecto(another.ovecto(i)); 
	} 
} 
dathuc dathuc::operator *(dathuc another){
	int h=this->deg()+another.deg()+1;
	vecto nhan(h), tmp(h); 
	dathuc nhan1(0); 
	long double k; 
	for(int i=0;i<=this->deg();i++){
		tmp.reset(); 
		for(int j=0;j<=another.deg();j++){
			k=this->ovecto(i)*another.ovecto(j);
			tmp.gvecto(i+j,k); 
		} 
		nhan=nhan+tmp; 
	}	
	nhan1.changevt(nhan); 
	return nhan1; 
} 
dathuc dathuc::operator *(long double k){
	dathuc nhan(0);
	for(int i=0;i<=this->deg();i++)
	nhan.pbvecto(k*this->ovecto(i));
	return nhan; 
} 
dathuc operator *(long double k, dathuc another){
	return another*k; 
}
dathuc dathuc::operator +(dathuc another){
	int t,a,max,min;
	t=this->deg(); 
	a=another.deg();
	if(a>=t){
		max=a;
		min=t; 
	}
	else{
		max=t;
		min=a; 
	}
	dathuc tong(max+1);
	for(int i=0;i<=max+1;i++){
		if(i<=min){
			tong.gvecto(i,this->ovecto(i)+another.ovecto(i));
		}
		else{
			if(max==a){
				tong.gvecto(i,another.ovecto(i)) ;						
			}

			else{
				tong.gvecto(i,this->ovecto(i)) ;										
			}
		} 
	} 
	return tong; 	
}
dathuc dathuc::operator -(dathuc another){
	int t,a,max,min;
	t=this->deg();
	a=another.deg();
	if(a>=t){
		max=a;
		min=t; 
	}
	else{
		max=t;
		min=a; 
	}
	dathuc tong(0);
	for(int i=0;i<=max;i++){
		if(i<=min){
			tong.pbvecto(this->ovecto(i)-another.ovecto(i));
		}
		else{
			if(max==a) 
			tong.pbvecto(-another.ovecto(i)) ;
			else
			tong.pbvecto(this->ovecto(i)); 
		} 
	} 
	tong.chuanhoa() ;
	return tong; 	
} 
dathuc dathuc::operator /(dathuc another){
	dathuc thuong(this->deg()), tmp(0);
	long double k, tmp1,h;
	this->chuanhoa();
	another.chuanhoa();
	if(this->deg()<another.deg())
	return *(this);
	if(another.deg()>1){
		cout<<"da thuc chia khong phai don thuc "<<endl;
		return *(this);
	}
	else{
		if(another.ovecto(another.deg())!=1){
			k=another.ovecto(another.deg());
			another= (1/k)*another ;
			*(this)= (1/k)**(this);
		}
		k=-another.ovecto(0);
		tmp1=this->ovecto(this->deg()) ;
		thuong.gvecto(this->deg()-1,tmp1);
		for (int i=this->deg()-1;i>=1;i--){
			tmp1=this->ovecto(i)+k*tmp1;
			thuong.gvecto(i-1,tmp1); 
		} 
		return thuong; 
	}
}
long double dathuc::daoham(long double k){
	dathuc thuong(this->deg()), chia(2);
	chia.gvecto(0,-k);
	chia.gvecto(1,1);
	thuong=*(this)/chia;
	return thuong.calc(k); 
} 
void dathuc::chuanhoa(){
	while(this->ovecto(this->size()-1)==0 && (this->size()>1))
	this->pobvecto();
}
int dathuc::deg(){
	this->chuanhoa();
	return this->size()-1;
}
void dathuc::in(){
	for(int i=0;i<=this->deg();i++){
		if(i==0)
		cout<<this->ovecto(i)<< " ";
		else{
			if(this->ovecto(i)>0)
			cout<<"+"<<this->ovecto(i)<<"x^"<<i<<" ";
			if(this->ovecto(i)<0)
			cout<<this->ovecto(i)<<"x^"<<i<<" ";
		}
	}
}
long double dathuc::calc(long double k){
	long double S=this->ovecto(this->deg());
	for(int i=this->deg();i>=1;i--){
		S=this->ovecto(i-1)+k*S;
	}
	return S; 
} 
///////////////////////////////////////// Dinh nghia ma tran 
class matran{
	private:
	vector <vecto> ker;
	int row;
	int col; 
	public:
	matran(int row, int col); 
	int gr();
	int gc(); 
	long double omatran(int i, int j); 
	void gmatran(int i, int j, long double k) ;
	void in(); 
	matran operator +(matran another); 
	matran operator -(matran another); 
	matran operator *(long double k);
	friend matran operator *(long double k, matran another); 
	matran operator *(matran another); 
	vecto operator *(vecto another); 
	void addrow( int i);
	void addcol( int i); 
	vecto gvectoc(int i);
	vecto gvector(int i); 
}; 
vecto matran::gvectoc(int i){
	return this->ker[i-1]; 
}
vecto matran:: gvector(int i){
	vecto hang(this->gc());
	for(int e=0;e<=hang.size()-1;e++){
		hang.gvecto(e, this->omatran(i,e+1)); 
	} 
	return hang; 
} 
void matran::addrow(int i){
	if(i<1){
		cout<<" Addrow ???"; 
	} 
	else{
		this->row=this->row+i;
		for(int e=0;e<=this->col-1;e++){
			for(int f=1;f<=i;f++){
				this->ker[e].pbvecto(0); 
			} 
		} 
	} 
} 
void matran::addcol(int i){
	if(i<1){
		cout<<" Addcol???"; 
	} 
	else{
		vecto v(this->row);
		for(int e=1;e<=i;e++){
			this->ker.push_back(v); 
		}
	} 
} 
matran::matran(int row, int col){
	this->row=row;
	this->col=col; 
	vecto a(row);
	for(int i=0;i<=col-1;i++){
		this->ker.push_back(a); 
	} 
} 
int matran::gr(){
	return  this->row; 
} 
int matran::gc(){
	return this->col; 
} 
void matran:: in(){
	for(int i=0;i<=row-1;i++) {
		for (int j=0;j<=col-1;j++){
			cout<< ker[j].ovecto(i)<<"  "; 
		} 
		cout<<endl; 
	}
} 
long double matran:: omatran(int i, int j){
	return ker[j-1].ovecto(i-1) ;
} 
void matran:: gmatran(int i, int j, long double k){
	ker[j-1].gvecto(i-1,k); 
} 
matran matran::operator +(matran another){
	int hang,cot,minc,minh;
	if(this->gc()>=another.gc()){
		cot=this->gc(); 
		minc= another.gc(); 
	} 
	else {
		cot=another.gc(); 
		minc=this->gc(); 
	} 
	if(this->gr()>=another.gr()){
		hang=this->gr(); 
		minh=another.gr(); 
	} 
	else{
		hang=another.gr(); 
		minh=this->gr(); 
	} 
	matran tong(hang,cot) ;
	for(int i=0;i<=cot-1;i++){
		if(i<=minc-1){
			tong.ker[i]=this->ker[i]+another.ker[i]; 
		} 
		else{
			if(cot==this->col)
			tong.ker[i]=tong.ker[i]+this->ker[i];
			else
			tong.ker[i]=tong.ker[i]+another.ker[i];			 
		} 
	} 
	return  tong; 
} 
matran matran::operator *(long double k){
	matran nhan(this->row,this->col);
	for(int i=0;i<=col-1;i++){
		nhan.ker[i]=k*this->ker[i]; 
	} 
	return nhan; 
} 
matran operator *(long double k, matran another){
	return another*k; 
} 
matran matran::operator -(matran another){
	return *(this)+(-1)*another; 
} 
matran matran::operator *(matran another){
	int cot= col, hang= another.gr(), cotn=another.gc(), hangn= row;
	long double S=0; 
	matran nhan(hangn,cotn); 
	if(cot!=hang){
		cout<<" kich co khong hop le"<<endl;
		return *(this);
	} 
	else{
		for(int i=0;i<=hangn-1;i++){
			for(int j=0;j<=cotn-1;j++){
				for(int e=0;e<=cot-1;e++){
					S=S+ ker[e].ovecto(i)*another.ker[j].ovecto(e); 
				} 
				nhan.ker[j].gvecto(i,S) ;
				S=0; 
			} 
		} 
		return nhan; 
	} 
}
vecto matran::operator *(vecto another){
	if(this->gc()!=another.size()){
		cout<<"Kich thuoc khong hop le"<<endl;
		return another; 
	} 
	else{
		vecto nhan(this->gr());
		long double S=0;
		for(int i=1; i<=this->gr(); i++){
			for(int j=1; j<=this->gc(); j++){
				S= S+ this->omatran(i,j)*another.ovecto(j-1); 
			} 
			nhan.gvecto(i-1,S);
			S=0; 
		} 
		return nhan; 
	} 
} 
long double chuanvc(matran another){
	long double tmp=0, max=0;
	if (another.gc()!=another.gr()){
		cout<<"Phai la ma tran vuong"<<endl;
		return -1; 
	} 
	else{
		for(int i=1;i<=another.gr();i++){
			for(int j=1;j<=another.gc();i++){
				tmp=tmp+fabs(another.omatran(i,j)); 
			} 
			if(max<tmp){
				max=tmp; 
			} 
			tmp=0; 
		} 
		return max; 
	} 
} 
long double chuanvc(vecto another){
	long double max=0;
	for(int i=0;i<=another.size()-1;i++){
		if(max<fabs(another.ovecto(i))){
			max=fabs(another.ovecto(i)); 
		} 
	} 
	return max; 
} 
long double chuan1(matran another){
	long double tmp=0, max=0;
	if(another.gc()!=another.gr()){
		cout<<"Phai la ma tran vuong";
		return -1; 
	} 
	else{
		for(int i=1;i<=another.gr();i++){
			for(int j=1;j<=another.gr();j++){
				tmp=tmp+fabs(another.omatran(i,j)); 
			} 
			if(max<tmp){
				max=tmp; 
			} 
			tmp=0; 
		} 
		return max; 
	} 
} 
long double chuan1(vecto another){
	long double max=0;
	for(int i=0;i<=another.size()-1;i++){
		max=max +fabs(another.ovecto(i)); 
	} 
	return max; 
} 
bool check (matran another){
	int hang=another.gr();
	int cot=another.gc();
	if(cot!=2){
		cout<<"matran input khong dung kich thuoc"<<endl;
		return false; 
	} 	
	long double k= another.omatran(2,1)- another.omatran(1,1);
	for(int i=3;i<=another.gr();i++){
		if((another.omatran(i,1)-another.omatran(i-1,1))!=k)
		return false;
	}
	return true;
}

matran BTH(matran another){
	int hang=another.gr();
	int cot=another.gc();
	if(cot!=2){
		cout<<"matran input khong dung kich thuoc"<<endl;
		return another; 
	} 
	else{
		matran table(hang,hang+1); 
		for(int i=1;i<=hang;i++){
			for(int j=1;j<=2;j++){
				table.gmatran(i,j,another.omatran(i,j)); 
			} 
		} 
		for(int i=1;i<=hang;i++){
			for(int j=3;j<=hang+1;j++){
				if(j-1>i){
					break; 
				}
				else{
					table.gmatran(i,j,((table.omatran(i,j-1)-table.omatran(i-1,j-1))/(table.omatran(i,1)-table.omatran(i-(j-2),1)))); 
				} 
			} 
		} 
		return table; 
	} 
} 
bool checkvc(matran another){
	long double S=0;
	for (int i=1; i<=another.gr(); i++){
		S=0; 
		for(int j=1; j<=another.gc(); j++){
			if(j==i){
				continue; 
			} 
			else{
				S= S+fabs(another.omatran(i,j)) ;
			} 
		} 
		if(S>=another.omatran(i,i)){
			return false; 
		} 
	}
	return true; 
} 
bool check1(matran another){
	long double S=0;
	for (int i=1; i<=another.gc(); i++){
		S=0; 
		for(int j=1; j<=another.gr(); j++){
			if(j==i){
				continue; 
			} 
			else{
				S= S+fabs(another.omatran(i,j)) ;
			} 
		} 
		if(S>=another.omatran(i,i)){
			return false; 
		} 
	}
	return true; 	
} 
/////////////////////////////////// dinh nghia mot so ham thong dung 
class hamsin{ ////// dinh nghia sin(x) 
	private:
	long double ker;
	public:
	hamsin(); 
	hamsin(long double k);
	void in();
	void gsin(long double k);
	long double osin();
	long double  calc(long double k); 
}; 
hamsin::hamsin(){
	ker=0; 
} 
hamsin::hamsin(long double k){
	ker=k; 
}
void hamsin::in(){
	cout<<"sin("<<ker<<"x)"; 
} 
void hamsin::gsin(long double k){
	ker=k; 
} 
long double hamsin::osin(){
	return ker; 
}
long double hamsin::calc(long double k){
	return sin(ker*k); 
} 
class hamcos{ ////// dinh nghia cos(x) 
	private:
	long double ker;
	public:
	hamcos(); 
	hamcos(long double k);
	void in();
	void gcos(long double k);
	long double ocos();
	long double  calc(long double k); 
}; 
hamcos::hamcos(){
	ker=0; 
} 
hamcos::hamcos(long double k){
	ker=k; 
} 
void hamcos::in(){
	cout<<"cos("<<ker<<"x)"; 
} 
void hamcos::gcos(long double k){
	ker=k; 
} 
long double hamcos::ocos(){
	return ker; 
}
long double hamcos::calc(long double k){
	return cos(ker*k); 
} 
class hame { ///////// dinh nghia ham e^x 
	private:
	long double ker;
	public:
	hame(); 
	hame(long double k);
	void in();
	void ge(long double k);
	long double oe();
	long double calc(long double k); 
}; 
hame::hame(){
	ker=0; 
} 
hame::hame(long double k){
	ker=k; 
} 
void hame::in(){
	cout<<"e^("<<ker<<"x)"; 
} 
void hame::ge(long double k){
	ker=k; 
} 
long double hame::oe(){
	return ker; 
} 
long double hame::calc(long double k){
	return exp(ker*k); 
} 
class hamln{ //// dinh nghia ham ln(x) 
	private:
	long double ker;
	public:
	hamln(long double k) ;
	void in();
	void gln(long double k); 
	long double oln();
	long double calc(long double  k);
}; 
hamln::hamln(long double k){
	ker=k; 
}
void hamln::in(){
	cout<<"ln("<<ker<<"x)"; 
} 
void hamln::gln(long double k){
	ker=k; 
} 
long double hamln::oln(){
	return ker; 
} 
long double hamln::calc(long double k){
	return log(k*ker); 
} 
