#ifndef __GRAPH_H__
#define __GRAPH_H__
namespace KruskalLib
{
	class Edge 
	{
	protected:
		int A, B;
	public:
		__declspec(dllexport) Edge();
		__declspec(dllexport) Edge(int, int);
		__declspec(dllexport) virtual ~Edge();
		__declspec(dllexport) virtual void print();
		__declspec(dllexport) int GetA();
		__declspec(dllexport) int GetB();
		__declspec(dllexport) void SetA(int);
		__declspec(dllexport) void SetB(int);
		__declspec(dllexport) virtual void SetWeight(double)=0;
	};

	class WeightedEdge: public Edge
	{
		double weight;
	public: 
		__declspec(dllexport) WeightedEdge();
		__declspec(dllexport) WeightedEdge(int, int, double);
		__declspec(dllexport) ~WeightedEdge();
		__declspec(dllexport) void print();
		__declspec(dllexport) double GetWeight();
		__declspec(dllexport) void SetWeight(double);
		__declspec(dllexport) WeightedEdge& operator=(WeightedEdge&);
	};
	
	class Graph 
	{
		int numVerteces;
		int numEdges;
		WeightedEdge **edges;
	public:
		__declspec(dllexport) Graph(int, int);
		__declspec(dllexport) ~Graph();
		__declspec(dllexport) void Generate();	
		__declspec(dllexport) WeightedEdge* GetEdge(int);
		__declspec(dllexport) WeightedEdge** GetAllEdges();
		__declspec(dllexport) int GetNumEdges();
		__declspec(dllexport) int GetNumVerteces();
	};
}

#endif