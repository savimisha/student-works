#include "KruskalImplementation.h"
#include "SeparatedSets.h"
#include "DHeapBasedPriorityQueue.h"
#include "Graph.h"
#include "DHeapForEdges.h"
#include "PriorityQueue.h"
#include <iostream>
using namespace std;
namespace KruskalLib
{
	void KruskalImplemetation::GiveMeTree(Graph* g, PriorityQueue *q, Graph *T)
	{
		int numEdges=g->GetNumEdges();
		int numVerteces=g->GetNumVerteces();
		SeparatedSets SS(numVerteces);
		for (int i=1; i<=numVerteces; i++)
			SS.Create(i);
		WeightedEdge *e;
		int set1=0, set2=0;
		int k=0;
		int A,B;
		while (q->isEmpty() != 0 && k!=numVerteces-1)
		{
			e=q->Get();
			A=e->GetA();
			B=e->GetB();
			set1=SS.WhichSet(A);
			set2=SS.WhichSet(B);
			if (set1 != set2)
			{
				SS.Union(set1,set2);
				*(T->GetAllEdges())[k]=*e;
				k++;
			}
			
		}
	}
}