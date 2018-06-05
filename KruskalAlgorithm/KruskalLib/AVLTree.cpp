#include "BinarySearchTree.h"
#include "AVLTree.h"
#include "Graph.h"
#include <stdlib.h>
#define max(a,b) ((a) > (b) ? (a) : (b))
namespace KruskalLib
{
	AVLTreeNode::AVLTreeNode(int _id, WeightedEdge *_edge) 
		:TreeNode(_id, _edge)
	{
		balance=0;
	}

	AVLTreeNode::~AVLTreeNode()
	{
		balance=0;
	}
	int AVLTreeNode::GetBalance()
	{
		return balance;
	}
	void AVLTreeNode::SetBalance(int tmp)
	{
		balance=tmp;
	}

	int AVLTreeNode::depth(AVLTreeNode *root)
	{
		int h1=0, h2=0;
        if(root==0) return 0;
        if(root->left)
		{
			h1=depth((AVLTreeNode*)root->left);
		}
        if(root->right)
		{
			h2=depth((AVLTreeNode*)root->right);
		}
        return(max(h1,h2)+1);
	}
	void AVLTreeNode::updateBalance()
	{
		int a=depth((AVLTreeNode*)this->left), b=depth((AVLTreeNode*)this->right);
		balance = b-a;
	}
	AVLTreeNode& AVLTreeNode::operator=(const AVLTreeNode& B)
	{
		this->id=B.id;
		this->parent=B.parent;
		this->left=B.left;
		this->right=B.right;
		this->balance=B.balance;
		*(this->edge)=*(B.edge);
		return *this;
	}
	AVLTreeNode* AVLTreeNode::GetLeft()
	{
		return (AVLTreeNode*)this->left;
	}
	AVLTreeNode* AVLTreeNode::GetRight()
	{
		return (AVLTreeNode*)this->right;
	}
	AVLTreeNode* AVLTreeNode::GetParent()
	{
		return (AVLTreeNode*)this->parent;
	}

	AVLTree::AVLTree() : BinarySearchTree()
	{
	}
	AVLTree::~AVLTree()
	{
	}

	void AVLTree::SingleLeftRotate(AVLTreeNode*& root)
	{
		if (root == 0) return;
		AVLTreeNode *right = root->GetRight();
		AVLTreeNode *tempParent=root->GetParent();
		root->SetRight(right->GetLeft());
		if (right->GetLeft() != 0) right->GetLeft()->SetParent(root);
		right->SetLeft(root);
		root->SetParent(right);
		if (right->GetBalance() == 1) 
		{
			root->SetBalance(0);
			right->SetBalance(0);
		}
		else 
		{
			root->SetBalance(1);
			right->SetBalance(-1);
		}
		root = right;
		root->SetParent(tempParent);
	}
	void AVLTree::SingleRightRotate(AVLTreeNode*& root)
	{
		if (root == 0) return;
		AVLTreeNode *left = root->GetLeft();
		AVLTreeNode *tempParent=root->GetParent();
		root->SetLeft(left->GetRight());
		if (left->GetRight() != 0) left->GetRight()->SetParent(root);
		left->SetRight(root);
		root->SetParent(left);
		if (left->GetBalance() == -1) 
		{
			root->SetBalance(0);
			left->SetBalance(0);
		}
		else 
		{
			root->SetBalance(-1);
			left->SetBalance(1);
			
		}
		root = left;
		root->SetParent(tempParent);		
	}

	void AVLTree::DoubleRightRotate(AVLTreeNode*& root)
	{
		if (root == 0) return;
		AVLTreeNode *left = root->GetLeft();
		AVLTreeNode *tempParent=root->GetParent();
		AVLTreeNode *right = left->GetRight();
		left->SetRight(right->GetLeft());
		if (right->GetLeft() != 0) right->GetLeft()->SetParent(left);
		right->SetLeft(left);
		left->SetParent(right);
		root->SetLeft(right->GetRight());
		if (right->GetRight() != 0) right->GetRight()->SetParent(root);
		right->SetRight(root);
		root->SetParent(right);
		if (right->GetBalance() == -1)
		{
			root->SetBalance(1);
			left->SetBalance(0);
		}
		if (right->GetBalance() == 0)
		{
			root->SetBalance(0);
			left->SetBalance(0);
		}
		if (right->GetBalance() == 1)
		{
			root->SetBalance(0);
			left->SetBalance(-1);
		}
		right->SetBalance(0);
		root = right;
		root->SetParent(tempParent);
	}
	void AVLTree::DoubleLeftRotate(AVLTreeNode*& root)
	{
		if (root == 0) return;
		AVLTreeNode *right = root->GetRight();
		AVLTreeNode *tempParent=root->GetParent();
		AVLTreeNode *left = right->GetLeft();
		right->SetLeft(left->GetRight());
		if (left->GetRight() != 0) left->GetRight()->SetParent(right);
		left->SetRight(right);
		right->SetParent(left);
		root->SetRight(left->GetLeft());
		if (left->GetLeft() != 0) left->GetLeft()->SetParent(root);
		left->SetLeft(root);
		root->SetParent(left);
		if (left->GetBalance() == -1)
		{
			root->SetBalance(0);
			right->SetBalance(1);
		}
		if (left->GetBalance() == 0)
		{
			root->SetBalance(0);
			right->SetBalance(0);
		}
		if (left->GetBalance() == 1)
		{
			root->SetBalance(-1);
			right->SetBalance(0);
		}
		left->SetBalance(0);
		root = left;
		root->SetParent(tempParent);
	}
	void AVLTree::AVLinsert(AVLTreeNode* &root, AVLTreeNode *node, AVLTreeNode *parent, bool &needBalance)
	{
		if (root == 0)
		{
			root=node;
			node->SetParent(parent);
			needBalance=true;
			return;
		}
		bool needCurrBalance;
		if (node->GetEdge()->GetWeight() < root->GetEdge()->GetWeight()) 
		{
			AVLinsert((AVLTreeNode*&)root->left,node,root,needCurrBalance);
			if (needCurrBalance)
				{
				switch (root->GetBalance())
				{
				case -1:
				{
					
					if (((AVLTreeNode*)root->left)->balance == -1)
						SingleRightRotate(root);
					else DoubleRightRotate(root);
					needBalance=false;
					break;
				}
				case 0:
				{
					root->SetBalance(-1);
					needBalance=true;
					break;
				}
				case 1:
				{
					root->SetBalance(0);
					needBalance=false;
					break;
				}
			}
			} 
			else needBalance=false;
		} 
		else if (node->GetEdge()->GetWeight() > root->GetEdge()->GetWeight()) 
		{
			AVLinsert((AVLTreeNode*&)root->right,node,root,needCurrBalance);
			if (needCurrBalance)
			{
				switch (root->GetBalance())
				{
				case -1:
				{
					root->SetBalance(0);
					needBalance=false;
					break;
				}
				case 0: 
				{
					root->SetBalance(1);
					needBalance=true;
					break;
				}
				case 1:
				{
					
					if (((AVLTreeNode*)root->right)->balance == 1)
						SingleLeftRotate(root);
					else DoubleLeftRotate(root);
					needBalance=false;
					
					break;
				}
				}
			}
			else needBalance=false;
		}
	}
	void AVLTree::insert(AVLTreeNode *node)
	{
		bool needBalance;
		AVLinsert((AVLTreeNode*&)this->root,node,0,needBalance);
	}
	void AVLTree::AVLremove(AVLTreeNode*& root, AVLTreeNode *node, bool &needBalance)
	{
		if (root == 0 || node == 0) return;
		bool needCurrBalance;
		if (node->GetEdge()->GetWeight() < root->GetEdge()->GetWeight()) 
		{
			AVLremove((AVLTreeNode*&)root->left,node,needCurrBalance);
			if (needCurrBalance)
				{
				switch (root->GetBalance())
				{
				case -1:
				{
					
					root->SetBalance(0);
					needBalance=false;
					break;
				}
				case 0:
				{
					root->SetBalance(1);
					needBalance=true;
					break;
				}
				case 1:
				{	
					if ((AVLTreeNode*)root->right != 0)
					{
					if (((AVLTreeNode*)root->right)->balance == 0)
						SingleLeftRotate(root);
					else if (((AVLTreeNode*)root->right)->balance == 1)
						SingleLeftRotate(root);
					else DoubleLeftRotate(root); 
					needBalance=false;
					} else root->SetBalance(0);
					break;
				}
			}
			} 
			else needBalance=false;
			return;
		} 
		if (node->GetEdge()->GetWeight() > root->GetEdge()->GetWeight()) 
		{
			AVLremove((AVLTreeNode*&)root->right,node,needCurrBalance);
			if (needCurrBalance)
			{
				switch (root->GetBalance())
				{
				case -1:
				{
					if (((AVLTreeNode*)root->left)->balance == 0)
						SingleRightRotate(root);
					else if (((AVLTreeNode*)root->left)->balance == -1)
						SingleRightRotate(root);
					else DoubleRightRotate(root); 
					
					
					needBalance=false;
					break;
				}
				case 0: 
				{
					root->SetBalance(-1);
					needBalance=true;
					break;
				}
				case 1:
				{
					needBalance=false;
					root->SetBalance(0);
					break;
				}
				}
			}
			else needBalance=false;

			return;
		}
		if (root == node) 
		{
			AVLTreeNode *parent;
			if (root->GetLeft() == 0 && root->GetRight() == 0) 
			{
				parent=root->GetParent();
				AVLTreeNode *tmp=root;
				if (parent != 0)
				{
					if (parent->GetLeft() == tmp) parent->left=0;
					if (parent->GetRight() == tmp) parent->right=0;
				} 
				delete tmp;
				root=0;
			}  
			else if (root->GetLeft() != 0 && root->GetRight() == 0) 
			{
				parent=root->GetParent();
				AVLTreeNode *tmp=root;
				if (parent != 0)
				{
					if (parent->GetLeft() == tmp) 
					{
						parent->SetLeft(tmp->GetLeft());
						parent->GetLeft()->SetParent(parent);
					}
					if (parent->GetRight() == tmp) 
					{
						parent->SetRight(tmp->GetLeft());
						parent->GetRight()->SetParent(parent);
					}
				}
				else 
				{
					tmp->GetLeft()->SetParent(0);
					root=tmp->GetLeft();
				}
				delete tmp;
			}
			else if (root->GetLeft() == 0 && root->GetRight() != 0) 
			{
				parent=root->GetParent();
				AVLTreeNode *tmp=root;
				if (parent != 0)
				{
					if (parent->GetLeft() == tmp) 
					{
						parent->SetLeft(root->GetRight());
						parent->GetLeft()->SetParent(parent);
					}
					if (parent->GetRight() == tmp) 
					{
						parent->SetRight(root->GetRight());
						parent->GetRight()->SetParent(parent);
					}
				} 
				else 
				{
						tmp->GetRight()->SetParent(0);
						root=tmp->GetRight();
				}
				delete tmp;
			}
			else if (root->GetLeft() != 0 && root->GetRight() != 0) 
			{
				AVLTreeNode *maxRightOfLeft=root->GetLeft();
				while (maxRightOfLeft->GetRight() != 0)
						maxRightOfLeft=maxRightOfLeft->GetRight();
				WeightedEdge *tempEdge=root->GetEdge();
				int tempId=root->GetId();
				root->SetEdge(maxRightOfLeft->GetEdge());
				root->SetId(maxRightOfLeft->GetId());
				maxRightOfLeft->SetEdge(tempEdge);
				maxRightOfLeft->SetId(tempId);
				bool tmp;
				AVLremove((AVLTreeNode*&)root->left,maxRightOfLeft, tmp);
			}
		}
		
		
	}
	void AVLTree::remove(AVLTreeNode *node)
	{
		bool needBalance;
		AVLremove((AVLTreeNode*&)this->root,node,needBalance);
	}

	void AVLTree::autoBalance(AVLTreeNode*& root)
	{
		if (root == 0) return;
		root->updateBalance();
		if (root->balance == -2)
			{
				if (((AVLTreeNode*)root->left)->balance == -1)
					SingleRightRotate(root);
				else DoubleRightRotate(root);
			} 
		if (root->balance == 2)
			{
				if (((AVLTreeNode*)root->right)->balance == 1)
					SingleLeftRotate(root);
				else DoubleLeftRotate(root);
			}
		autoBalance((AVLTreeNode*&)root->left);
		autoBalance((AVLTreeNode*&)root->right);
	}

}