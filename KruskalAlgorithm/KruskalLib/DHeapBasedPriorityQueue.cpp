#include "PriorityQueue.h"
#include "DHeapBasedPriorityQueue.h"
#include "DHeapForEdges.h"
namespace KruskalLib
{
	DHeapBasedPriorityQueue::DHeapBasedPriorityQueue(int _d, int _numEdges, WeightedEdge **_edges)
	{
		dheap=new DHeapForEdges(_d,_numEdges,_edges);
		dheap->Hilling();
	}
	DHeapBasedPriorityQueue::~DHeapBasedPriorityQueue()
	{
		delete dheap;
	}
	void DHeapBasedPriorityQueue::Put(WeightedEdge *tmp)
	{
		dheap->Ins(tmp);
	}
	WeightedEdge* DHeapBasedPriorityQueue::Get()
	{
		return dheap->GetMinWeight();
	}
	int DHeapBasedPriorityQueue::isEmpty()
	{
		return dheap->GetCount();
	}
}