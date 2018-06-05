#include <iostream>
#include <conio.h>
#include "CellAuto2D.h"
#include "CellAuto1D.h"
using namespace std;
#define WAITKEY _getch();
void ConfiguringAndDrawing1D(int, int, int, int);
void ConfiguringAndDrawing2D(int, int, int, int, char*);
int main(int argc, char *argv[])
{
	if (argc!=6) 
	{
		cout << "Incorrect parameters\n";
		WAITKEY
		return -1;
	}
	int n=atoi(argv[2]);
	int m=atoi(argv[3]);
	if (n<=0 || m<=0) 
	{
		cout << "Incorrect parameters\n";
		WAITKEY
		return -1;
	}
	int Surround=atoi(argv[4]);
	int OptionConf=-1;
	if (strcmp(argv[5], "t")==0) OptionConf=0;
	if (strcmp(argv[5], "r")==0) OptionConf=1;
	if (strcmp(argv[5], "p")==0) OptionConf=2;
	if (OptionConf==-1) 
	{
		cout << "Incorrect parameters\n";
		WAITKEY
		return -1;
	}
	if (strcmp(argv[1], "-1D")==0)
	{
		if (Surround<0 || Surround>1) 
		{
			cout << "Incorrect parameters\n";
			WAITKEY
			return -1;
		}
		ConfiguringAndDrawing1D(n, Surround, OptionConf, m);
	}
	if (strcmp(argv[1], "-2D")==0)
	{
		if (Surround<0 || Surround>3) 
		{
			cout << "Incorrect parameters\n";
			WAITKEY
			return -1;
		}
		char *formula=0;
		if (Surround==3) 
		{
			formula=new char[100];
			cout << "    A\n";
			cout << "  E F G\n";
			cout << "D L M H B\n";
			cout << "  K J I\n";
			cout << "    C\n";
			cout << "Please, enter the formula using symbols A-M and +, -, *, /, (, ):\n";
			cin >> formula;
			cout << endl;
			int i=0;
			while (formula[i]!='\0') 
			{
				if ((formula[i]<65 || formula[i]>77) && formula[i]!='+' && formula[i]!='-' 
					&& formula[i]!='*' && formula[i]!='/' && formula[i]!='(' && formula[i]!=')')
				{
					cout << "Incorrect formula\n";
					WAITKEY
					return -1;
				}
				i++;
			}
		}
		ConfiguringAndDrawing2D(n, m, Surround, OptionConf, formula);
	}
	return 0;
}

