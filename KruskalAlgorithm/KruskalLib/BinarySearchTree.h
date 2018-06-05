#ifndef __BINARYSEARCHTREE_H__
#define __BINARYSEARCHTREE_H__
#include "Graph.h"
namespace KruskalLib
{
	class TreeNode
	{
	protected:
		int id;
		TreeNode *parent;
		TreeNode *left;
		TreeNode *right;
		WeightedEdge *edge;
	public:
		__declspec(dllexport) TreeNode(int, WeightedEdge*);
		__declspec(dllexport) virtual ~TreeNode();
		__declspec(dllexport) TreeNode* GetLeft();
		__declspec(dllexport) TreeNode* GetRight();
		__declspec(dllexport) TreeNode* GetParent();
		__declspec(dllexport) int GetId();
		__declspec(dllexport) void SetId(int);
		__declspec(dllexport) WeightedEdge* GetEdge();
		__declspec(dllexport) void SetParent(TreeNode*);
		__declspec(dllexport) void SetLeft(TreeNode*);
		__declspec(dllexport) void SetRight(TreeNode*);
		__declspec(dllexport) void SetEdge(WeightedEdge*);

	};
	class BinarySearchTree
	{
	protected:
		TreeNode *root;
	public:
		__declspec(dllexport) BinarySearchTree();
		__declspec(dllexport) ~BinarySearchTree();
		__declspec(dllexport) void insert(TreeNode*);
		__declspec(dllexport) TreeNode* searchNext(TreeNode*);
		__declspec(dllexport) TreeNode* searchMin(TreeNode*);
		__declspec(dllexport) TreeNode* searchMinOfTree();
		__declspec(dllexport) void remove(TreeNode*);
		__declspec(dllexport) void print(TreeNode*);
		__declspec(dllexport) void removeTree(TreeNode*);
		__declspec(dllexport) TreeNode*& GetRoot();
	};
}

#endif