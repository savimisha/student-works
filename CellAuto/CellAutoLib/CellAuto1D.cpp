#include <iostream>
using namespace std;

#include "CellAuto1D.h"


CellAuto1D::CellAuto1D(int _n, int _qsize)
{
	n=_n;
	currConf=new Configuration(n);
	nextConf=new Configuration(n);
	QueueConf=new TQueue(_qsize);
}

CellAuto1D::~CellAuto1D()
{
	n=0;
	delete currConf; 
	delete nextConf;
	for(int i=0; i<QueueConf->GetCount(); i++)
		delete QueueConf->Get();
}

void CellAuto1D::GetNextConf()
{
	for(int i=0; i<n; i++)
		nextConf->conf[i]=GetNextState(i);
}

void CellAuto1D::SetTestConf()
{
	currConf->conf[n/2]=1;
	//currConf->conf[n/2+2]=1;
	//currConf->conf[n/2-2]=1;
}
void CellAuto1D::printCurrConf()
{
	for(int i=0; i<n; i++)
		cout << currConf->conf[i] << ' ';
	cout << endl;
}
void CellAuto1D::SetInitialConf(Configuration* conf)
{
	memcpy(currConf->conf, conf->conf, sizeof(int)*n);
}

int CellAuto1DNeumann::GetNextState(int k)
{
	return (currConf->conf[(k-1+n)%n]+currConf->conf[(k+1)%n])%2;
}
CellAuto1DNeumann::CellAuto1DNeumann(int _n, int _qsize) 
	:CellAuto1D(_n, _qsize)
{
}
CellAuto1DNeumann::~CellAuto1DNeumann()
{
}
int CellAuto1DMvon::GetNextState(int k)
{
	return (currConf->conf[(k-1+n)%n]+currConf->conf[(k+1)%n]+
		currConf->conf[(k-2+n)%n]+currConf->conf[(k+2)%n])%2;
}
CellAuto1DMvon::CellAuto1DMvon(int _n, int _qsize) 
	:CellAuto1D(_n, _qsize)
{
}
CellAuto1DMvon::~CellAuto1DMvon()
{
}
Configuration* CellAuto1D::OutCurrConf()
{
	Configuration* tmp=new Configuration(n);
	memcpy(tmp->conf, currConf->conf, sizeof(int)*n);
	return tmp;
}

void CellAuto1D::SetCurrConf()
{
	Configuration *tmp;
	tmp=currConf;
	currConf=nextConf;
	nextConf=tmp;
}

void CellAuto1D::SaveCurrConf()
{
	Configuration* tmp=new Configuration(n);
	memcpy(tmp->conf, currConf->conf, sizeof(int)*n);
	if (QueueConf->isFull()) delete QueueConf->Get();
	QueueConf->Put(tmp);
}

TDataRoot* CellAuto1D::GetQueuePointer()
{
	return QueueConf;
}
