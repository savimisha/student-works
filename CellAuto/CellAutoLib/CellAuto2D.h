#ifndef __CELLAUTO2D_H__
#define __CELLAUTO2D_H__

#include "CellAuto.h"

class CellAuto2D: public CellAuto
{
protected:
	int n;
	int m;
	virtual int GetNextState(int, int)=0;
public:
	CellAuto2D(int, int);
	virtual ~CellAuto2D();
	virtual void GetNextConf();
	virtual void SetCurrConf();
	virtual void SetTestConf();
	virtual void printCurrConf();
	virtual void SetInitialConf(Configuration*);
	virtual Configuration* OutCurrConf();
};

class CellAuto2DNeumann: public CellAuto2D 
{
protected:
	virtual int GetNextState(int, int);
public:
	virtual ~CellAuto2DNeumann();
	CellAuto2DNeumann(int, int);
};

class CellAuto2DMoore: public CellAuto2D 
{
protected:
	virtual int GetNextState(int, int);
public:
	virtual ~CellAuto2DMoore();
	CellAuto2DMoore(int, int);
};

class CellAuto2DMvon: public CellAuto2D 
{
protected:
	virtual int GetNextState(int, int);
public:
	virtual ~CellAuto2DMvon();
	CellAuto2DMvon(int, int);
};


#endif