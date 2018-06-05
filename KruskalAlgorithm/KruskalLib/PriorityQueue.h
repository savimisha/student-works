#ifndef __PRIORITYQUEUE_H__
#define __PRIORITYQUEUE_H__
#include "Graph.h"
namespace KruskalLib
{
	class PriorityQueue
		{
		public:
			__declspec(dllexport) PriorityQueue() {};
			__declspec(dllexport) virtual ~PriorityQueue() {};
			__declspec(dllexport) virtual void Put(WeightedEdge*)=0;
			__declspec(dllexport) virtual WeightedEdge* Get()=0;
			__declspec(dllexport) virtual int isEmpty()=0;
		};
}
#endif