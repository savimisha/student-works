#ifndef __LIST_H__
#define __LIST_H__

typedef char TData;

class Node 
{
public:
	TData info;
	Node *pNext;
	Node(TData);
	~Node();
};

class List 
{
private:
	Node *pBeg;
public:
	List();
	~List();
	int add(TData);
	TData getfirst();
	void remove(TData);
	void removefirst();
	int isEmpty();
};



#endif