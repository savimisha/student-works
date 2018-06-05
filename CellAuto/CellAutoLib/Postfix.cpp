#include "Postfix.h"
#include "ListStack.h"
#include <iostream>
Postfix::Postfix(char *input)
{
	postsize=0;
	ConvertToPostfix(input);
	std::cout << "Postfix formula: ";
	for (int i=0; i<postsize; i++)
		std::cout << output[i];
	std::cout << std::endl;
	
}
Postfix::~Postfix()
{
	delete output;
}

int Postfix::Priority(char x)
{
	if (x=='/') return 3;
	if (x=='*') return 2;
	//if ((x=='*')||(x=='/')) return 2;
	if ((x=='+')||(x=='-')) return 1;
	if ((x=='(')||(x==')')) return 0;
	return -1;
}


void Postfix::ConvertToPostfix(char *input)
{
	int size=0;
	while (input[size]!='\0')
		size++;
	ListStack *stack1=new ListStack;
	ListStack *stack2=new ListStack;
	for (int i=0; i<size; i++)
	{
		if (input[i]>=65 && input[i]<=77)
			stack1->Put(input[i]);
		if (input[i]=='+' || input[i]=='-' || input[i]=='*' || input[i]=='/' || input[i]=='(') 
		{
			if (stack2->isEmpty()!=DataEmpty)
			{
				char tmp=stack2->Get();
				if (Priority(input[i])<Priority(tmp)  && input[i]!='(') 
					stack1->Put(tmp);
				else 
					stack2->Put(tmp);
				stack2->Put(input[i]);
			} else 
				stack2->Put(input[i]);
		}
		if (input[i]==')') 
		{
			char tmp=stack2->Get();
			while (tmp!='(') 
			{
				stack1->Put(tmp);
				tmp=stack2->Get();
			}
		}
	}
	while (stack2->isEmpty()!=DataEmpty)
		stack1->Put(stack2->Get());
	int brackets=0;
	for(int i=0; i<size; i++)
		if (input[i]=='(' || input[i]==')') brackets++;
	char *result=new char[size-brackets];
	for(int i=size-brackets-1; i>=0; i--)
		result[i]=stack1->Get();
	output=result;
	postsize=size-brackets;
	delete stack1;
	delete stack2;
}