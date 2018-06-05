#ifndef __BSTBASEDPQ_H__
#define __BSTBASEDPQ_H__
#include "PriorityQueue.h"
#include "BinarySearchTree.h"
#include "Graph.h"
namespace KruskalLib
{
	class BSTbasedPQ: public PriorityQueue
	{
		int count;
		BinarySearchTree *tree;
	public:
		__declspec(dllexport) BSTbasedPQ(int, WeightedEdge**);
		__declspec(dllexport) virtual ~BSTbasedPQ();
		__declspec(dllexport) virtual void Put(WeightedEdge*);
		__declspec(dllexport) virtual WeightedEdge* Get();
		__declspec(dllexport) virtual int isEmpty();

	};
}

#endif