#include "BinarySearchTree.h"
#include <iostream>
namespace KruskalLib
{
	//
	//TreeNode
	//
	TreeNode::TreeNode(int _id, WeightedEdge *_edge)
	{
		id=_id;
		edge=new WeightedEdge;
		*edge=*_edge;
		parent=0;
		left=0;
		right=0;
	}
	TreeNode::~TreeNode()
	{
		parent=0;
		id=-1;
		delete edge;
		left=0;
		right=0;
	}
	TreeNode* TreeNode::GetLeft()
	{
		return this->left;
	}
	TreeNode* TreeNode::GetRight()
	{
		return this->right;
	}
	TreeNode* TreeNode::GetParent()
	{
		return this->parent;
	}
	WeightedEdge* TreeNode::GetEdge()
	{
		return this->edge;
	}
	void TreeNode::SetLeft(TreeNode *tmp)
	{
		left=tmp;
	}
	void TreeNode::SetRight(TreeNode *tmp)
	{
		right=tmp;
	}
	void TreeNode::SetParent(TreeNode *tmp)
	{
		parent=tmp;
	}
	void TreeNode::SetEdge(WeightedEdge* tmp)
	{
		edge=tmp;
	}
	void TreeNode::SetId(int tmp)
	{
		id=tmp;
	}
	int TreeNode::GetId()
	{
		return id;
	}
	//
	//BinarySearchTree
	//
	BinarySearchTree::BinarySearchTree()
	{
		root=0;
	}
	BinarySearchTree::~BinarySearchTree()
	{
		removeTree(this->root);
	}
	void BinarySearchTree::removeTree(TreeNode *root)
	{
		if (root != NULL || root != 0)
		{
			removeTree(root->GetLeft());
			removeTree(root->GetRight());
			delete root;
		}
	}
	TreeNode* BinarySearchTree::searchMin(TreeNode* current)
	{
		current = root;
		while (current->GetLeft() != 0)
		{
			current = current->GetLeft();
		}
		return current;
	}
	TreeNode* BinarySearchTree::searchMinOfTree()
	{
		TreeNode *current = root;
		while (current->GetLeft() != 0)
		{
			current = current->GetLeft();
		}
		return current;
	}
	TreeNode* BinarySearchTree::searchNext(TreeNode* current)
	{
		TreeNode *result=0;
		if (current->GetRight()!=0)
		{
			result=searchMin(current);
			return result;
		}
		result=current->GetParent();
		TreeNode *tmp=current;
		while (result != 0 && tmp == result->GetRight())
		{
			tmp = result;
			result = result->GetParent();
		}
		return result;
	}
	void BinarySearchTree::insert(TreeNode *node)
	{
		if (root==0) 
		{
			root=node;
			return;
		}
		TreeNode *x = root, *y;
		while (x != 0)
		{
			y = x;
			if (node->GetEdge()->GetWeight() < x->GetEdge()->GetWeight()) x = x->GetLeft();
			else x = x->GetRight();
		}
		node->SetParent(y);
		if (node->GetEdge()->GetWeight() < y->GetEdge()->GetWeight()) y->SetLeft(node);
		else y->SetRight(node);
	}
	void BinarySearchTree::remove(TreeNode *node)
	{
		TreeNode *y = 0, *x = 0;
		if (node->GetLeft() != 0 && node->GetRight() != 0)
		{
			y = searchNext(node);
		}
		else
		{
			y = node; 
		}
		if (y->GetLeft() != 0)
		{
			x = y->GetLeft();
		}
		else
		{
			x = y->GetRight();
		}
		if (x != 0) x->SetParent(y->GetParent());
		if (y->GetParent() != 0)
		{
			if ( y == y->GetParent()->GetLeft() ) y->GetParent()->SetLeft(x);
			else  y->GetParent()->SetRight(x); 
		}
		if (y != node)
		{
			node->SetId(y->GetId());
			delete node->GetEdge();
			node->SetEdge(y->GetEdge());
		}
		if (y==root) root=x;
	}
	void BinarySearchTree::print(TreeNode* tmp)
	{
		if (tmp==0) return;
		std::cout << tmp->GetEdge()->GetWeight() << ' ';
		if (tmp->GetLeft()!=0) print(tmp->GetLeft());
		if (tmp->GetRight()!=0) print (tmp->GetRight());
	}
	TreeNode*& BinarySearchTree::GetRoot()
	{
		return root;
	}
}