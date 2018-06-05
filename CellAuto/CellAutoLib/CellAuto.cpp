#include <iostream>
using namespace std;

#include "CellAuto.h"

CellAuto::CellAuto()
{
	currConf=0;
	nextConf=0;
}

CellAuto::~CellAuto()
{
	currConf=0;
	nextConf=0;
}

int CellAuto::GetState(int k)
{
	return currConf->conf[k];
}
