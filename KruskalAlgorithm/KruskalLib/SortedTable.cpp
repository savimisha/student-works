#include "SortedTable.h"
namespace KruskalLib
{
	TabRecord::TabRecord()
	{
		id=-1;
		edge=0;
	}
	TabRecord::~TabRecord()
	{
		id=-1;
		edge=0;
	}
	WeightedEdge* TabRecord::GetEdge()
	{
		return edge;
	}
	void TabRecord::SetEdge(WeightedEdge *tmp)
	{
		edge=tmp;
	}
	void TabRecord::SetId(int _id)
	{
		id=_id;
	}

	Table::Table()
	{
		maxSize=0;
		count=0;
		currPos=0;
	}
	Table::~Table()
	{
		maxSize=0;
		count=0;
		currPos=0;
	}

	int Table::isEmpty()
	{
		if (count != 0) return 1;
		return 0;
	}
	int Table::isFull()
	{
		if (count < maxSize) return 1;
		return 0;
	}
	void Table::Reset()
	{
		currPos=0;
	}
	void Table::Next()
	{
		if (!isTableEnded()) currPos++;
	}
	int Table::isTableEnded()
	{
		if (currPos >= maxSize) return 0;
		return 1;
	}
	void Table::SetMax(int k)
	{
		maxSize=k;
	}
	ScanTable::ScanTable(int max) : Table()
	{
		maxSize=max;
		records=new TabRecord*[maxSize];
	}
	ScanTable::ScanTable(WeightedEdge **edges, int numEdges) : Table()
	{
		maxSize=numEdges;
		records=new TabRecord*[maxSize];
		for (int i=0; i<maxSize; i++)
		{
			records[i]=new TabRecord;
			records[i]->SetEdge(edges[i]);
			records[i]->SetId(i);
		}
	}
	ScanTable::~ScanTable()
	{
		if (records!=0)
		{
		for(int i=0; i<maxSize; i++)
			delete records[i];
		delete records;
		}
	}


	void ScanTable::insert(TabRecord* rec)
	{
		if (isFull()==0) return;
		records[count++]=rec;
	}


}