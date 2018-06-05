#include "Graph.h"
#include "DHeapForEdges.h"
#include <iostream>
#define MIN(A,B) ((A) < (B) ? (A) : (B)) 
namespace KruskalLib
{
	//
	//DHeapForEdges
	//
	DHeapForEdges::DHeapForEdges(int _d, int _numEdges, WeightedEdge **_edges)
	{
		d=_d;
		numEdges=_numEdges;
		count=_numEdges;
		edges=new WeightedEdge*[numEdges];
		for (int i=0; i<numEdges; i++)
		{
			edges[i]=new WeightedEdge;
			*(edges[i])=*(_edges[i]);
		}
		this->Hilling();
	}
	DHeapForEdges::~DHeapForEdges()
	{
		if (edges == 0 ) return;
		for (int i=0; i<numEdges; i++)
			delete edges[i];
		delete[] edges;
		d=-1;
		numEdges=-1;
	}
	void DHeapForEdges::Change(int i, int j)
	{
		WeightedEdge tmp;
		tmp=*(edges[i]);
		*(edges[i])=*(edges[j]);
		*(edges[j])=tmp;
	}
	void DHeapForEdges::Up(int i)
	{
		int p;
		while (i>0)
		{
			p=(i-1)/d;
			if (edges[p]->GetWeight() > edges[i]->GetWeight()) 
			{
				Change(i,p);
				i=p;
			}
		}
	}
	int DHeapForEdges::MinChild(int i)
	{
		if (i*d+1 >= count) return -1;
		int left=i*d+1;
		int right=MIN(i*d+d, count-1);
		WeightedEdge min=*(edges[left]);
		int minId=left;
		for(int j=left+1; j<=right; j++)
		{
			if (edges[j]->GetWeight() < min.GetWeight()) 
			{
				min=*(edges[j]);
				minId=j;
			}
		}
		return minId;
	}
	void DHeapForEdges::Down(int i)
	{
		int c=MinChild(i);
		while (c!=-1 && edges[i]->GetWeight() > edges[c]->GetWeight()) 
		{
			Change(i,c);
			i=c;
			c=MinChild(i);
		}
	}
	void DHeapForEdges::Hilling()
	{
		for(int i=count-1; i>=0; i--)
			Down(i);
	}
	void DHeapForEdges::Ins(WeightedEdge *tmp)
	{
		if (count == numEdges) return; 
		count++;
		*(edges[count-1])=*tmp;
		Up(count);
	}
	void DHeapForEdges::Del(int i)
	{
		Change(i, count-1);
		count--;
		int p=(i-1)/d;
		if (edges[p]->GetWeight() > edges[i]->GetWeight()) Up(i); 
		else Down(i);
	}
	WeightedEdge* DHeapForEdges::GetMinWeight()
	{
		Change(0, count-1);
		count--;
		Down(0);
		return edges[count];
	}
	void DHeapForEdges::print()
	{
		for (int i=0; i<count; i++)
		{
			std::cout << edges[i]->GetWeight() <<' ';
			if (i%d == 0) std::cout<<std::endl;
		}

	}
	int DHeapForEdges::GetCount()
	{
		return count;
	}
}