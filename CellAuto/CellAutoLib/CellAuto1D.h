#ifndef __CELLAUTO1D_H__
#define __CELLAUTO1D_H__

#include "CellAuto.h"
#include "TData.h"

class CellAuto1D: public CellAuto 
{
protected:
	int n;
	virtual int GetNextState(int)=0;
	TDataRoot *QueueConf;
public:
	CellAuto1D(int, int);
	virtual ~CellAuto1D();
	virtual void GetNextConf();
	virtual void SetCurrConf();
	virtual void SetTestConf();
	virtual void printCurrConf();
	virtual void SetInitialConf(Configuration*);
	virtual Configuration* OutCurrConf();
	void SaveCurrConf();
	TDataRoot* GetQueuePointer();
};

class CellAuto1DNeumann: public CellAuto1D
{
protected: 
	virtual int GetNextState(int);
public:
	CellAuto1DNeumann(int, int);
	~CellAuto1DNeumann();
};
class CellAuto1DMvon: public CellAuto1D
{
protected: 
	virtual int GetNextState(int);
public:
	CellAuto1DMvon(int, int);
	~CellAuto1DMvon();
};

#endif