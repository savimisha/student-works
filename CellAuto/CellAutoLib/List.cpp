#include "List.h"
Node::Node(TData _info)
{
	info=_info;
	pNext=0;
}

Node::~Node()
{
}

List::List()
{
	pBeg=0;
}
List::~List()
{
	if (pBeg!=0) 
	{	
		Node *tmp;
		while (pBeg->pNext!=0)
		{
			tmp=pBeg->pNext;
			delete pBeg;
			pBeg=tmp;
		}
	}
}

int List::add(TData _info)
{
	if (pBeg==0) 
	{
		pBeg=new Node(_info);
		return 0;
	}
	/*Node *tmp=pBeg;
	while (tmp->pNext!=0)
		tmp=tmp->pNext;
	tmp->pNext=new Node(_info);
	tmp->pNext->pNext=0;*/

	Node *tmp=new Node(_info);
	if (tmp==0) return -1;
	tmp->pNext=pBeg;
	pBeg=tmp;
	return 0;
}

TData List::getfirst()
{
	if (pBeg==0) return '_';
	/*Node *tmp=pBeg;
	while (tmp->pNext!=0)
		tmp=tmp->pNext;
	return tmp->info;*/
	return pBeg->info;
}

void List::removefirst()
{
	if (pBeg==0) return;
	Node *tmp=pBeg->pNext;
	delete pBeg;
	pBeg=tmp;
}
void List::remove(TData _info)
{
	Node *pCurr, *pPrev;
	pCurr=pBeg; 
	pPrev=0;
	while (pCurr!=0 && pCurr->info!=_info)
	{
		pPrev=pCurr;
		pCurr=pCurr->pNext;
	}
	if (pCurr->pNext==0 && pCurr->info!=_info) return;
	if (pPrev==0) 
	{
		pBeg=pBeg->pNext;
		delete pCurr;
		return;
	}
	pPrev->pNext=pCurr->pNext;
	delete pCurr; 
}

int List::isEmpty()
{
	if (pBeg==0) return 0; 
	else return -1;
}