#ifndef __AVLTBASEDPQ_H__
#define __AVLTBASEDPQ_H__
#include "BinarySearchTree.h"
#include "Graph.h"
#include "AVLTree.h"
#include "PriorityQueue.h"
namespace KruskalLib
{
	class AVLTbasedPQ: public PriorityQueue
	{
		int count;
		AVLTree *tree;
	public:
		__declspec(dllexport) AVLTbasedPQ(int, WeightedEdge**);
		__declspec(dllexport) virtual ~AVLTbasedPQ();
		__declspec(dllexport) virtual void Put(WeightedEdge*);
		__declspec(dllexport) virtual WeightedEdge* Get();
		__declspec(dllexport) virtual int isEmpty();
	};
}
#endif