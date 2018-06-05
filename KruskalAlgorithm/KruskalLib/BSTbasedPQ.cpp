#include "BSTbasedPQ.h"
namespace KruskalLib
{
	BSTbasedPQ::BSTbasedPQ(int numEdges, WeightedEdge **edges)
	{
		tree=new BinarySearchTree;
		count=numEdges;
		TreeNode **nodes=new TreeNode*[numEdges];
		for (int i=0; i<numEdges; i++)
		{
			nodes[i]=new TreeNode(i,edges[i]);
			tree->insert(nodes[i]);
		}
	}
	BSTbasedPQ::~BSTbasedPQ()
	{
		delete tree;
	}
	void BSTbasedPQ::Put(WeightedEdge *edge)
	{
		TreeNode *node=new TreeNode(count,edge);
		tree->insert(node);
		count++;
	}
	WeightedEdge* BSTbasedPQ::Get()
	{
		if (count == 0) return 0;
		TreeNode *tmp=tree->searchMinOfTree();
		WeightedEdge *tmpedge=tmp->GetEdge();
		tree->remove(tmp);
		count--;
		return tmpedge;
	}
	int BSTbasedPQ::isEmpty()
	{
		if (count == 0) return 0;
		return 1;
	}
}