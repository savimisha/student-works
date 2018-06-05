#include <iostream>
#include "gtest\gtest.h"
#include "SeparatedSets.h"
#include "BinarySearchTree.h"
#include "AVLTree.h"
using namespace testing;
using namespace KruskalLib;
TEST(SeparatedSets, CreateAndWhichSet)
{
	SeparatedSets *SS=new SeparatedSets(3);
	SS->Create(1);
	SS->Create(2);
	SS->Create(3);
	EXPECT_EQ(1, SS->WhichSet(1));
	EXPECT_EQ(2, SS->WhichSet(2));
	EXPECT_EQ(3, SS->WhichSet(3));
	delete SS;
}
TEST(SeparatedSets, Union)
{
	SeparatedSets *SS=new SeparatedSets(3);
	SS->Create(1);
	SS->Create(2);
	SS->Create(3);
	SS->Union(1,2);
	EXPECT_EQ(1,SS->WhichSet(1));
	EXPECT_EQ(1,SS->WhichSet(2));
	EXPECT_EQ(3,SS->WhichSet(3));
}
TEST(BinarySearchTree, insert_and_searchMin)
{
	WeightedEdge *edge1=new WeightedEdge;
	edge1->SetWeight(1);
	WeightedEdge *edge2=new WeightedEdge;
	edge2->SetWeight(2);
	WeightedEdge *edge3=new WeightedEdge;
	edge3->SetWeight(3);
	WeightedEdge *edge4=new WeightedEdge;
	edge4->SetWeight(0.5);
	BinarySearchTree *T=new BinarySearchTree;
	TreeNode *node1=new TreeNode(1,edge2);
	TreeNode *node2=new TreeNode(2,edge1);
	TreeNode *node3=new TreeNode(3,edge3);
	TreeNode *node4=new TreeNode(4,edge4);
	T->insert(node1);
	T->insert(node2);
	T->insert(node3);
	T->insert(node4);
	TreeNode *tmp=T->searchMin(node1);
	EXPECT_EQ(0.5,tmp->GetEdge()->GetWeight());
	T->remove(node4);
	tmp=T->searchMin(node1);
	EXPECT_EQ(1,tmp->GetEdge()->GetWeight());
}

int main(int argc, char **argv) 
{
	//::testing::InitGoogleTest(&argc, argv);
	//RUN_ALL_TESTS();

	AVLTree *T=new AVLTree();
	WeightedEdge *edge1=new WeightedEdge;
	edge1->SetWeight(1);
	WeightedEdge *edge2=new WeightedEdge;
	edge2->SetWeight(2);
	WeightedEdge *edge3=new WeightedEdge;
	edge3->SetWeight(3);
	WeightedEdge *edge4=new WeightedEdge;
	edge4->SetWeight(0.5);
	WeightedEdge *edge5=new WeightedEdge;
	edge5->SetWeight(0.6);
	WeightedEdge *edge6=new WeightedEdge;
	edge6->SetWeight(0.7);
	WeightedEdge *edge7=new WeightedEdge;
	edge7->SetWeight(2.5);
	WeightedEdge *edge8=new WeightedEdge;
	edge8->SetWeight(2.6);
	WeightedEdge *edge9=new WeightedEdge;
	edge9->SetWeight(2.7);
	AVLTreeNode *node1=new AVLTreeNode(1,edge2);
	AVLTreeNode *node2=new AVLTreeNode(2,edge1);
	AVLTreeNode *node3=new AVLTreeNode(3,edge3);
	AVLTreeNode *node4=new AVLTreeNode(4,edge4);
	AVLTreeNode *node5=new AVLTreeNode(5,edge5);
	AVLTreeNode *node6=new AVLTreeNode(6,edge6);
	AVLTreeNode *node7=new AVLTreeNode(8,edge7);
	AVLTreeNode *node8=new AVLTreeNode(9,edge8);
	AVLTreeNode *node9=new AVLTreeNode(10,edge9);
	T->insert(node1);
	T->insert(node2);
	T->insert(node3);
	T->insert(node4);
	T->insert(node5);
	T->insert(node6);
	T->insert(node7);
	T->insert(node8);
	T->insert(node9);

	T->remove(node1);
	

	T->print(T->GetRoot());

	std::cout << std::endl;

	std::cin.get();
	return 0;
}