#ifndef __DHEAP_H__
#define __DHEAP_H__
#include "Graph.h"
#include <iostream>
namespace KruskalLib
{
	class DHeapForEdges 
	{
		int d;
		int numEdges;
		int count;
		WeightedEdge **edges;
	public: 
		__declspec(dllexport) DHeapForEdges(int,int, WeightedEdge**);
		__declspec(dllexport) ~DHeapForEdges();
		__declspec(dllexport) void Change(int, int);
		__declspec(dllexport) void Up(int);
		__declspec(dllexport) int MinChild(int);
		__declspec(dllexport) void Down(int);
		__declspec(dllexport) void Del(int);
		__declspec(dllexport) void Ins(WeightedEdge*);
		__declspec(dllexport) void Hilling();
		__declspec(dllexport) WeightedEdge* GetMinWeight();
		__declspec(dllexport) void print();
		__declspec(dllexport) int GetCount();
	};

}
#endif