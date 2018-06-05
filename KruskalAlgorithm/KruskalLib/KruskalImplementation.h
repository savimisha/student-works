#ifndef __KRUSKALIMPLEMENTATION_H__
#define __KRUSKALIMPLEMENTATION_H__
#include "Graph.h"
#include "PriorityQueue.h"
namespace KruskalLib
{
	 class KruskalImplemetation
	{
	public:
		__declspec(dllexport) static void GiveMeTree(Graph*, PriorityQueue*, Graph*);
	};
}
#endif