#include "AVLTbasedPQ.h"

namespace KruskalLib
{
	AVLTbasedPQ::AVLTbasedPQ(int numEdges, WeightedEdge **edges)
	{
		tree=new AVLTree;
		count=numEdges;
		AVLTreeNode **nodes=new AVLTreeNode*[numEdges];
		for (int i=0; i<numEdges; i++)
		{
			nodes[i]=new AVLTreeNode(i,edges[i]);
			tree->insert(nodes[i]);
		}
	}

	AVLTbasedPQ::~AVLTbasedPQ()
	{
		delete tree;
	}
	void AVLTbasedPQ::Put(WeightedEdge *edge)
	{
		AVLTreeNode *node=new AVLTreeNode(count,edge);
		tree->insert(node);
		count++;
	}

	WeightedEdge* AVLTbasedPQ::Get()
	{
		if (count == 0) return 0;
		TreeNode *tmp=tree->searchMinOfTree();
		WeightedEdge *tmpEdge=new WeightedEdge;; 
		*tmpEdge=*(tmp->GetEdge());
		tree->remove((AVLTreeNode*)tmp);
		count--;
		return tmpEdge;
	}
	int AVLTbasedPQ::isEmpty()
	{
		if (count == 0) return 0;
		return 1;
	}
}