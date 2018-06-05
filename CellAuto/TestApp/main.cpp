#include <conio.h>
#include "gtest\gtest.h"
#include "CellAuto2D.h"
#include "CellAuto2DPersonal.h"
#include "CellAuto1D.h"
#include "TData.h"
#include "List.h"
#include "ListStack.h"
#include "Postfix.h"
#include <iostream>
using namespace std;

TEST(CellAuto2D, NeumannGetNextState)
{
	CellAuto2D* object=new CellAuto2DNeumann(3, 3);
	Configuration* start=new Configuration(9);
	start->conf[1]=1;
	start->conf[3]=1;
	start->conf[5]=1;
	start->conf[7]=1;
	object->SetInitialConf(start);
	object->GetNextConf();
	object->SetCurrConf();
	Configuration* finish=object->OutCurrConf();
	for(int i=0; i<9; i++)
	{
		if (i%2==1) EXPECT_EQ(1, finish->conf[i]);
		if (i%2==0) EXPECT_EQ(0, finish->conf[i]);
	}	
	delete object;
	delete start;
}
TEST(CellAuto2D, MooreGetNextState)
{
	CellAuto2D* object=new CellAuto2DNeumann(3, 3);
	Configuration* start=new Configuration(9);
	start->conf[1]=1;
	start->conf[3]=1;
	start->conf[5]=1;
	start->conf[7]=1;
	object->SetInitialConf(start);
	object->GetNextConf();
	object->SetCurrConf();
	Configuration* finish=object->OutCurrConf();
	for(int i=0; i<9; i++)
	{
		if (i%2==1) EXPECT_EQ(1, finish->conf[i]);
		if (i%2==0) EXPECT_EQ(0, finish->conf[i]);
	}		
	delete object;
	delete start;
}
TEST(CellAuto2D, MvonGetNextState)
{
	CellAuto2D* object=new CellAuto2DMvon(3, 3);
	Configuration* start=new Configuration(9);
	start->conf[1]=1;
	start->conf[3]=1;
	start->conf[5]=1;
	start->conf[7]=1;
	object->SetInitialConf(start);
	object->GetNextConf();
	object->SetCurrConf();
	Configuration* finish=object->OutCurrConf();
	for(int i=0; i<9; i++)
		EXPECT_EQ(0, finish->conf[i]);	
	delete object;
	delete start;
}

TEST(CellAuto1D, NeumannGetNextState)
{
	CellAuto1D* object=new CellAuto1DNeumann(4, 1);
	Configuration* start=new Configuration(4);
	start->conf[0]=1;
	start->conf[3]=1;
	object->SetInitialConf(start);
	object->GetNextConf();
	object->SetCurrConf();
	Configuration* finish=object->OutCurrConf();
	for(int i=0; i<4; i++)
		EXPECT_EQ(1, finish->conf[i]);
	delete object;
	delete start;
}

TEST(CellAuto1D, MvonGetNextState)
{
	CellAuto1D* object=new CellAuto1DMvon(4, 1);
	Configuration* start=new Configuration(4);
	start->conf[0]=1;
	start->conf[3]=1;
	object->SetInitialConf(start);
	object->GetNextConf();
	object->SetCurrConf();
	Configuration* finish=object->OutCurrConf();
	for(int i=0; i<4; i++)
		EXPECT_EQ(1, finish->conf[i]);
	delete object;
	delete start;
}

TEST(List, Test)
{
	List* object=new List;
	object->add('a');
	object->add('b');
	object->add('c');
	object->add('d');
	object->remove('a'); 
	object->remove('d');
	EXPECT_EQ('c', object->getfirst());
	object->remove('c');
	EXPECT_EQ('b', object->getfirst());
	object->remove('b');
	EXPECT_EQ('_', object->getfirst());
	delete object;
}

TEST(ListStack, Test)
{
	ListStack *object=new ListStack;
	object->Put('a');
	object->Put('b');
	object->Put('c');
	object->Put('d');
	EXPECT_EQ('d',object->Get());
	EXPECT_EQ('c',object->Get());
	EXPECT_EQ('b',object->Get());
	EXPECT_EQ('a',object->Get());
	EXPECT_EQ('_',object->Get());

	object->Put('e');
	EXPECT_EQ('e',object->Get());
	delete object;
}

TEST(Postfix, Test)
{
	Postfix *object=new Postfix("(A+B)*(C+D)-L/F");
	EXPECT_EQ('A', object->output[0]);
	EXPECT_EQ('B', object->output[1]);
	EXPECT_EQ('+', object->output[2]);
	EXPECT_EQ('C', object->output[3]);
	EXPECT_EQ('D', object->output[4]);
	EXPECT_EQ('+', object->output[5]);
	EXPECT_EQ('*', object->output[6]);
	EXPECT_EQ('L', object->output[7]);
	EXPECT_EQ('F', object->output[8]);
	EXPECT_EQ('/', object->output[9]);
	EXPECT_EQ('-', object->output[10]);
	delete object;
}

TEST(CellAuto2D, PersonalGetNextState)
{
	CellAuto2D* object=new CellAuto2DPersonal("L+F+H+J",3, 3);
	Configuration* start=new Configuration(9);
	start->conf[1]=1;
	start->conf[3]=1;
	start->conf[5]=1;
	start->conf[7]=1;
	object->SetInitialConf(start);
	object->GetNextConf();
	object->SetCurrConf();
	Configuration* finish=object->OutCurrConf();
	for(int i=0; i<9; i++)
	{
		if (i%2==1) EXPECT_EQ(1, finish->conf[i]);
		if (i%2==0) EXPECT_EQ(0, finish->conf[i]);
	}	
	delete object;
	delete start;
}


int main(int argc, char **argv)
{	
	testing::InitGoogleTest(&argc, argv);
	RUN_ALL_TESTS();
	_getch();
	return 0;
}