#include <iostream>
using namespace std;

#include "TData.h"

TDataRoot::TDataRoot(int _size)
{
	size=_size;
	mem=new TElem[size];
	count=0;
}
TDataRoot::~TDataRoot()
{
	size=0;
	delete mem;
	count=0;
}

int TDataRoot::isFull()
{
	if (size==count) 
	{
		SetRetCode(DataFull);
		return DataFull;
	}
	return DataOK;
}

int TDataRoot::isEmpty()
{
	if (count==0) 
	{
		SetRetCode(DataFull);
		return DataEmpty;
	}
	return DataOK;
}

TStack::TStack(int _size) : TDataRoot(_size)
{
	Hi=-1;
}

TStack::~TStack()
{
	Hi=-1;
}

void TStack::Put(const TElem &elem)
{
	if (isFull()==DataFull) 
	{
		SetRetCode(DataFull);
		return;
	}
	Hi=GetNextIndex(Hi);
	mem[Hi]=elem; 
	++count;
	SetRetCode(DataOK);
}

TElem TStack::Get()
{
	if(isEmpty()==DataEmpty) 
	{
		SetRetCode(DataEmpty);
		return NULL;
	}
	TElem temp;
	temp=mem[Hi];
	Hi--;
	count--;
	SetRetCode(DataOK);
	return temp;
}

int TStack::GetNextIndex(int id)
{
	return ++id;
}

TQueue::TQueue(int _size) : TStack(_size)
{
	Li=0;
}
TQueue::~TQueue()
{
	Li=0;
}


int TQueue::GetNextIndex(int id)
{
	return (++id)%size; 
}

TElem TQueue::Get()
{
	if (isEmpty()==DataEmpty)
	{
		SetRetCode(DataEmpty);
		return NULL;
	}
	TElem temp;
	temp=mem[Li];
	Li=GetNextIndex(Li);
	count--;
	SetRetCode(DataOK);
	return temp;
}

int TDataRoot::GetCount() 
{
	return count;
}

