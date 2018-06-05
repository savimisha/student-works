#ifndef _CRS_H_
#define _CRS_H_
#define ZERO_IN_CRS 0.0000001
#define MAX_VAL 10
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
#endif