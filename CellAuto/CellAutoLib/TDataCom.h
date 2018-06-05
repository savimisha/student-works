#ifndef __TDATACOM_H__
#define __TDATACOM_H__

#define DataOK 0
#define DataFull 1
#define DataEmpty 2

class TDataCom 
{
protected: 
	int RetCode;
	void SetRetCode(int);
public:
	TDataCom();
	virtual ~TDataCom();
	int GetRetCode();
};
#endif