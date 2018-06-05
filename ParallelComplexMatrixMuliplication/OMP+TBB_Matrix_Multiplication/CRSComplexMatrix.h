#ifndef _CRS_H_
#define _CRS_H_
#define ZERO_IN_CRS 0.00001
#define MAX_VAL 10
#include <iostream>
struct CRSComplexMatrix
{
	int N;
	int NZ;
	double *valueRe;
	double *valueIm;
	int *col;
	int *rowIndex;
};

void deleteCRSComplexMatrix(CRSComplexMatrix&);
void transposeCRSComplexMatrix(CRSComplexMatrix, CRSComplexMatrix&, double&);
void InitializeCRSComplexMatrix(int, int, CRSComplexMatrix&);
void GenerateRegularCRSComplexMatrix(int, int, int, CRSComplexMatrix&);
int multiplicateCRSComplexMatrix(CRSComplexMatrix, CRSComplexMatrix, CRSComplexMatrix&, double&);
int multiplicateCRSComplexMatrixOMP(CRSComplexMatrix, CRSComplexMatrix, CRSComplexMatrix&, double&, int);
int multiplicateCRSComplexMatrixTBB(CRSComplexMatrix, CRSComplexMatrix, CRSComplexMatrix&, double&, int);
int SparseMKLMult(CRSComplexMatrix A, CRSComplexMatrix B, CRSComplexMatrix &C, double &time);
double CRSComplexMatrixDiff(CRSComplexMatrix A, CRSComplexMatrix B);
std::ostream& operator << (std::ostream& os, const CRSComplexMatrix& mat);
#endif