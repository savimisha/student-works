#ifndef __CELLAUTO_H__
#define __CELLAUTO_H__

#include "Configuration.h"

class CellAuto 
{
protected:
	Configuration *nextConf, *currConf;
public:
	CellAuto();
	virtual ~CellAuto();
	int GetState(int);
	virtual void SetCurrConf()=0;
	virtual Configuration* OutCurrConf()=0;
	virtual void SetInitialConf(Configuration*)=0;
	virtual void GetNextConf()=0;
	virtual void SetTestConf()=0;
	virtual void printCurrConf()=0;	
};

#endif