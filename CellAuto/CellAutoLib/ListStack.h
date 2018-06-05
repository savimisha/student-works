#ifndef __LISTSTACK_H__
#define __LISTSTACK_H__
#include "TDataCom.h"
#include "List.h"

class ListStack: public TDataCom
{
private:
	List *pList;
public:
	ListStack();
	~ListStack();
	void Put(TData);
	TData Get();
	int isFull();
	int isEmpty();
};


#endif