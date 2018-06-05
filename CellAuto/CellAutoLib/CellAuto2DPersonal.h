#ifndef __CELLAUTO2DPERSONAL_H__
#define __CELLAUTO2DPERSONAL_H__
#include "CellAuto2D.h"
#include "Postfix.h"

class CellAuto2DPersonal: public CellAuto2D
{
protected: 
	Postfix *formula;
	virtual int GetNextState(int, int);
	int Operation(int, int, char);
	int Operand(char, int, int);
public:
	CellAuto2DPersonal(char*, int, int);
	virtual ~CellAuto2DPersonal();
};


#endif