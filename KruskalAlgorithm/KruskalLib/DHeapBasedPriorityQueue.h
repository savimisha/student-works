#ifndef __DHEAPBASEDPRIORITYQUEUE_H__
#define __DHEAPBASEDPRIORITYQUEUE_H__
#include "Graph.h"
#include "DHeapForEdges.h"
#include "PriorityQueue.h"
namespace KruskalLib
{
class DHeapBasedPriorityQueue: public PriorityQueue
	{
		DHeapForEdges *dheap;
	public:
		__declspec(dllexport) DHeapBasedPriorityQueue(int,int,WeightedEdge**);
		__declspec(dllexport) virtual ~DHeapBasedPriorityQueue();
		__declspec(dllexport) virtual void Put(WeightedEdge*);
		__declspec(dllexport) virtual WeightedEdge* Get();
		__declspec(dllexport) int isEmpty();
	};
}
#endif