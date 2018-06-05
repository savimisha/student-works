#include "TDataCom.h"
TDataCom::TDataCom()
{
	RetCode=DataEmpty;
}

TDataCom::~TDataCom()
{
	RetCode=DataEmpty;
}
int TDataCom::GetRetCode()
{
	return RetCode;
}
void TDataCom::SetRetCode(int code)
{
	RetCode=code;
}