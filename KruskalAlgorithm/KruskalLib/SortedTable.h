#ifndef __SORTEDTABLE_H__
#define __SORTEDTABLE_H__
#include "Graph.h"
namespace KruskalLib
{
class TabRecord 
{
	int id;
	WeightedEdge *edge;
public:
	TabRecord();
	~TabRecord();
	WeightedEdge* GetEdge();
	void SetEdge(WeightedEdge*);
	void SetId(int);
};

class Table
{
protected:
	int maxSize;
	int count;
	int currPos;
public:
	Table();
	virtual ~Table();
	int isFull();
	int isEmpty();
	void Reset();
	void Next();
	int isTableEnded();
	void SetMax(int);
	virtual void insert(TabRecord*)=0;
	virtual TabRecord* findRecord(int)=0;
	virtual void deleteRecord(int)=0;
};

class ScanTable: public Table
{
protected:
	TabRecord **records;
public:
	ScanTable(int);
	ScanTable(WeightedEdge **, int);
	virtual ~ScanTable();
	virtual void insert(TabRecord*);
	virtual TabRecord* findRecord(int);
	virtual void deleteRecord(int);
};

}



#endif