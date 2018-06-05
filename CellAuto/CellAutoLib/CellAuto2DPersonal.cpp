#include "CellAuto2DPersonal.h"
#include "ListStack.h"
#include <math.h>
#include <iostream>
CellAuto2DPersonal::CellAuto2DPersonal(char *input, int _n, int _m) 
	:CellAuto2D(_n, _m)
{
	formula=new Postfix(input);
}

CellAuto2DPersonal::~CellAuto2DPersonal()
{
	delete formula;
}

int CellAuto2DPersonal::Operand(char x, int i, int j)
{
	if (x=='A') return currConf->conf[((j-2+m)%m)*n+i];
	if (x=='B') return currConf->conf[j*n+(i+2)%n];
	if (x=='C') return currConf->conf[((j+2+m)%m)*n+i];
	if (x=='D') return currConf->conf[j*n+(i-2+n)%n];
	if (x=='E') return currConf->conf[((j-1+m)%m)*n+(i-1+n)%n];
	if (x=='F') return currConf->conf[((j-1+m)%m)*n+i];
	if (x=='G') return currConf->conf[((j-1+m)%m)*n+(i+1)%n];
	if (x=='H') return currConf->conf[j*n+(i+1)%n];
	if (x=='I') return currConf->conf[((j+1)%m)*n+(i+1)%n];
	if (x=='J') return currConf->conf[((j+1)%m)*n+i];
	if (x=='K') return currConf->conf[((j+1)%m)*n+(i-1+n)%n];
	if (x=='L') return currConf->conf[j*n+(i-1+n)%n];
	if (x=='M') return currConf->conf[j*n+(i+n)%n];
	if (x==0) return 0;
	if (x==1) return 1;
	return -1;
}

int CellAuto2DPersonal::Operation(int A, int B, char x)
{
	if (x=='+') return (A+B)%2;
	if (x=='-') return abs(A-B)%2;
	if (x=='*') return A*B;
	if (x=='/')  
			if (B==0) return 0; else return A/B;
	return -1;
}

int CellAuto2DPersonal::GetNextState(int i, int j)
{
	ListStack *stack=new ListStack;
	for (int k=0; k<formula->postsize; k++)
	{
		if (formula->output[k]>=65 && formula->output[k]<=77)
			stack->Put(formula->output[k]);
		if (formula->output[k]=='+' || formula->output[k]=='-' 
			|| formula->output[k]=='*' || formula->output[k]=='/') 
		{
			char tmp1=stack->Get(), tmp2=stack->Get();
			int operand1=Operand(tmp1, i, j);
			int operand2=Operand(tmp2, i, j);
			int operation=Operation(operand1, operand2, formula->output[k]);
			stack->Put((char)operation);
		}
	}
	int result=((int)stack->Get())%2;
	delete stack;
	return result;
}