#include <iostream>
using namespace std;

#include "CellAuto2D.h"

CellAuto2D::CellAuto2D(int _n, int _m) 
{
	n=_n;
	m=_m;
	currConf=new Configuration(n*m);
	nextConf=new Configuration(n*m);
}

CellAuto2D::~CellAuto2D()
{
	m=0;
	n=0;
	delete currConf;
	delete nextConf;
}

void CellAuto2D::SetCurrConf()
{
	Configuration *tmp;
	tmp=currConf;
	currConf=nextConf;
	nextConf=tmp;
}


CellAuto2DNeumann::CellAuto2DNeumann(int _n, int _m) 
	: CellAuto2D(_n, _m)
{
}

CellAuto2DNeumann::~CellAuto2DNeumann()
{
}

void CellAuto2D::GetNextConf()
{
	int k=0;
	for(int j=0; j<m; j++)
		for(int i=0; i<n; i++)
		{
			nextConf->conf[k++]=GetNextState(i,j);
		}
}


void CellAuto2D::SetTestConf()
{
	for(int i=0; i<m*n; i++)
		currConf->conf[i]=0;
	currConf->conf[m/2*n+n/2]=1;
	currConf->conf[(m/2-1)*n+n/2]=1;
	currConf->conf[(m/2+1)*n+n/2]=1;
	currConf->conf[(m/2-1)*n+n/2-1]=1;
	currConf->conf[(m/2-1)*n+n/2+1]=1;
	currConf->conf[(m/2-2)*n+n/2]=1;
	currConf->conf[(m/2+2)*n+n/2]=1;
	currConf->conf[(m/2+1)*n+n/2-1]=1;
	currConf->conf[(m/2+1)*n+n/2+1]=1;
}

void CellAuto2D::printCurrConf()
{
	for(int j=0;j<m;j++)
	{
		for(int i=0; i<n; i++)
			cout << currConf->conf[j*n+i] << ' ';
		cout << endl;
	}
	cout << endl;
}

int CellAuto2DNeumann::GetNextState(int i, int j) 
{
	return (currConf->conf[j*n+(i-1+n)%n]+currConf->conf[j*n+(i+1)%n]
	+currConf->conf[((j-1+m)%m)*n+i]+currConf->conf[((j+1)%m)*n+i])%2;
}

int CellAuto2DMoore::GetNextState(int i, int j) 
{
	return (currConf->conf[j*n+(i-1+n)%n]+currConf->conf[j*n+(i+1)%n]+currConf->conf[((j-1+m)%m)*n+i]+currConf->conf[((j+1)%m)*n+i]
	+currConf->conf[((j-1+m)%m)*n+(i-1+n)%n]+currConf->conf[((j-1+m)%m)*n+(i+1)%n]+currConf->conf[((j+1)%m)*n+(i-1+n)%n]
	+currConf->conf[((j+1)%m)*n+(i+1)%n])%2;
}

CellAuto2DMoore::CellAuto2DMoore(int _n, int _m) 
	: CellAuto2D(_n, _m)
{
}

CellAuto2DMoore::~CellAuto2DMoore()
{
}

int CellAuto2DMvon::GetNextState(int i, int j) 
{
	return (currConf->conf[j*n+(i-1+n)%n]+currConf->conf[j*n+(i+1)%n]+currConf->conf[((j-1+m)%m)*n+i]+currConf->conf[((j+1)%m)*n+i]
	+currConf->conf[((j-1+m)%m)*n+(i-1+n)%n]+currConf->conf[((j-1+m)%m)*n+(i+1)%n]+currConf->conf[((j+1)%m)*n+(i-1+n)%n]
	+currConf->conf[((j+1)%m)*n+(i+1)%n]+currConf->conf[j*n+(i-2+n)%n]+currConf->conf[j*n+(i+2)%n]
	+currConf->conf[((j-2+m)%m)*n+i]+currConf->conf[((j+2+m)%m)*n+i])%2;
}

CellAuto2DMvon::CellAuto2DMvon(int _n, int _m) 
	: CellAuto2D(_n, _m)
{
}

CellAuto2DMvon::~CellAuto2DMvon()
{
}

void CellAuto2D::SetInitialConf(Configuration* conf)
{
	memcpy(currConf->conf, conf->conf, sizeof(int)*n*m);
}

Configuration* CellAuto2D::OutCurrConf()
{
	Configuration* tmp=new Configuration(n*m);
	memcpy(tmp->conf, currConf->conf, sizeof(int)*n*m);
	return tmp;
}