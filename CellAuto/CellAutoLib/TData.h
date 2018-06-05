#ifndef __TDATA_H__
#define __TDATA_H__

#include "Configuration.h"
#include "TDataCom.h"

typedef Configuration* TElem;


class TDataRoot:public TDataCom
{
protected: 
	TElem *mem;
	int size;
	int count;
public:
	TDataRoot(int);
	virtual ~TDataRoot();
	virtual TElem Get()=0;
	virtual void Put(const TElem&)=0;
	virtual int isFull();
	virtual int isEmpty();
	int GetCount();
};
class TStack:public TDataRoot
{
protected:
	int Hi;
	virtual int GetNextIndex(int);
public:
	TStack(int);
	virtual ~TStack();
	virtual void Put(const TElem&);
	virtual TElem Get();
};

class TQueue:public TStack
{
protected:
	int Li;
	virtual int GetNextIndex(int);
public:
	TQueue(int);
	virtual ~TQueue();
	virtual TElem Get();
};
#endif