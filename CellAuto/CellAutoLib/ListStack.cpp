#include "ListStack.h"

ListStack::ListStack()
{
	pList=new List;
}

ListStack::~ListStack()
{
	delete pList;
}

int ListStack::isEmpty()
{
	if (pList->isEmpty()==0) 
	{
		SetRetCode(DataEmpty);
		return DataEmpty;
	}
	return DataOK;
}

int ListStack::isFull()
{
	int tmp=pList->add('~');
	if (tmp!=0) return DataFull;
	pList->remove('~');
	return DataOK;
}

TData ListStack::Get()
{
	if (isEmpty()==DataEmpty) return '_'; 
	TData tmp=pList->getfirst();
	pList->removefirst();
	return tmp;
}

void ListStack::Put(TData _info)
{
	if (isFull()==DataFull) return;
	pList->add(_info);
}