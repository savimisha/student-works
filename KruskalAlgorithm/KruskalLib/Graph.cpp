#include <time.h>
#include "Graph.h"
#include <iostream>
namespace KruskalLib 
{
//
//Edge
//
Edge::Edge()
{
	A=-1;
	B=-1;
}
Edge::Edge(int _A, int _B)
{
	A=_A;
	B=_B;
}
Edge::~Edge()
{
	A=-1; 
	B=-1;
}
void Edge::print()
{
	std::cout << A << ' ' << B << std::endl;
}
int Edge::GetA() 
{
	return A;
}
int Edge::GetB() 
{
	return B;
}
void Edge::SetA(int _A)
{
	A=_A;
}
void Edge::SetB(int _B)
{
	B=_B;
}
//
//WeightedEdge
//
WeightedEdge::WeightedEdge()
{
	A=-1;
	B=-1;
	weight=-1;
}
WeightedEdge::WeightedEdge(int _A, int _B, double _weight)
{
	A=_A;
	B=_B;
	weight=_weight;
}
WeightedEdge::~WeightedEdge()
{
	weight=-1;
}
void WeightedEdge::print()
{
	std::cout << A << ' ' << B << ' ' << weight << std::endl;
}
double WeightedEdge::GetWeight()
{
	return weight;
}
void WeightedEdge::SetWeight(double _weight)
{
	weight=_weight;
}
//
//Graph
//
Graph::Graph(int _numVerteces, int _numEdges)
{
	numVerteces=_numVerteces;
	numEdges=_numEdges;
	edges=new WeightedEdge*[numEdges];
	for (int i=0; i<numEdges; i++)
		edges[i]=new WeightedEdge;
}
Graph::~Graph()
{
	for (int i=0; i<numEdges; i++)
		delete edges[i];
	delete[] edges;
	numVerteces=0;
	numEdges=0;
}


template <typename T>
int Isset(T *arr, int n, T k)
{
	for (int i=0; i<n; i++)
	{
		if (arr[i]==k)
		{
			return 0;
		}
	}
	return 1;
}
int IssetEdge(WeightedEdge **edges, int numEdges, int A, int B)
{
	for (int i=0; i<numEdges; i++)
	{
		if (edges[i]->GetA() == A && edges[i]->GetB() == B || edges[i]->GetA() == B && edges[i]->GetB() == A) 
		{
			return 0;
		}
	}
	return 1;
}
void Graph::Generate()
{
	srand(time(0));
	double rand3; 
	int *tmp=new int[numVerteces];
	int rand2; 
	int rand1=rand()%numVerteces+1;
	tmp[0]=rand1;
	edges[0]->SetA(rand1);
	for (int i=0; i<numVerteces-1; i++)
	{
	do
	{
		rand2=rand()%numVerteces+1;
	}
	while (Isset<int>(tmp,numVerteces,rand2) == 0);
	edges[i]->SetB(rand2);
	if (i!=numVerteces-2) edges[i+1]->SetA(rand2);
	tmp[i+1]=rand2;
	}


	delete tmp;
	for (int i=numVerteces-1; i<numEdges; i++)
	{
		do
		{
			do 
			{
				rand1=rand() % (numVerteces)+1;
				rand2=rand() % (numVerteces)+1;
			} 
			while (rand1==rand2);
		}
		while (IssetEdge(edges, numEdges, rand1, rand2) == 0);
		(edges[i])->SetA(rand1);
		(edges[i])->SetB(rand2);
	}
	double *tmp1=new double[numEdges];
	for (int i=0; i<numEdges; i++)
	{
		do {
			rand3=double(rand()%101)+double(rand())/RAND_MAX;
		} while (Isset<double>(tmp1,numEdges,rand3) == 0);
		tmp[i]=rand3;
		(edges[i])->SetWeight(rand3);
	}
}
WeightedEdge* Graph::GetEdge(int i)
{
	return edges[i];
}
WeightedEdge& WeightedEdge::operator=(WeightedEdge &SecondOperand)
{
	if (this == &SecondOperand) return *this;
	this->A=SecondOperand.A;
	this->B=SecondOperand.B;
	this->weight=SecondOperand.weight;
	return *this;
}
WeightedEdge** Graph::GetAllEdges()
{
	return edges;
}
int Graph::GetNumEdges()
{
	return numEdges;
}
int Graph::GetNumVerteces()
{
	return numVerteces;
}
}