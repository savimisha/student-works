#ifndef __AVLTREE_H__
#define __AVLTREE_H__
#include "BinarySearchTree.h"
#include "Graph.h"
namespace KruskalLib
{
	class AVLTreeNode: public TreeNode 
	{
	private:
		int balance;
		friend class AVLTree; 
	public:
		__declspec(dllexport) AVLTreeNode(int, WeightedEdge*);
		__declspec(dllexport) virtual ~AVLTreeNode();
		__declspec(dllexport) int GetBalance();
		__declspec(dllexport) void SetBalance(int);
		__declspec(dllexport) static int depth(AVLTreeNode *root);
		__declspec(dllexport) void updateBalance();
		__declspec(dllexport) AVLTreeNode* GetLeft();
		__declspec(dllexport) AVLTreeNode* GetRight();
		__declspec(dllexport) AVLTreeNode* GetParent();
		__declspec(dllexport) AVLTreeNode& operator=(const AVLTreeNode&);
	};

	class AVLTree: public BinarySearchTree
	{
	public:
		__declspec(dllexport) AVLTree();
		__declspec(dllexport) virtual ~AVLTree();

		__declspec(dllexport) static void SingleRightRotate(AVLTreeNode*&);
		__declspec(dllexport) static void SingleLeftRotate(AVLTreeNode*&);
		__declspec(dllexport) static void DoubleLeftRotate(AVLTreeNode*&);
		__declspec(dllexport) static void DoubleRightRotate(AVLTreeNode*&);

		__declspec(dllexport) void insert(AVLTreeNode*);
		__declspec(dllexport) void AVLinsert(AVLTreeNode*&, AVLTreeNode*, AVLTreeNode*, bool&);

		//__declspec(dllexport) void rightTreeBalance(AVLTreeNode*&);
		//__declspec(dllexport) void leftTreeBalance(AVLTreeNode*&);

		__declspec(dllexport) void remove(AVLTreeNode*);
		__declspec(dllexport) void AVLremove(AVLTreeNode*&, AVLTreeNode*, bool&);
		__declspec(dllexport) void autoBalance(AVLTreeNode*&);

	};
}

#endif