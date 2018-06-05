#include "CRSComplexMatrix.h"
#include <iostream>

int main(int argc, char **argv)
{
	if (argc < 4 || argc > 5) return -1;
	int N = atoi(argv[1]); 
	int cntInRow = atoi(argv[2]);
	int numThreads = atoi(argv[4]);
	int NZ = N * cntInRow;
	CRSComplexMatrix A, B, BT, M;
	InitializeCRSComplexMatrix(N, NZ, A);
	InitializeCRSComplexMatrix(N, NZ, B);
	GenerateRegularCRSComplexMatrix(10, N, cntInRow, A);
	GenerateRegularCRSComplexMatrix(100, N, cntInRow, B);
	double timeTranspose, timeMult;
	transposeCRSComplexMatrix(B, BT, timeTranspose);
	std::cout << "Transpose time = " << timeTranspose << std::endl;
	if (atoi(argv[3]) == 1)
		multiplicateCRSComplexMatrixOMP(A, BT, M, timeMult, numThreads);
	if (atoi(argv[3]) == 2)
		multiplicateCRSComplexMatrixTBB(A, BT, M, timeMult, numThreads);
	CRSComplexMatrix C;
	std::cout << "Multiplication time = " << timeMult << std::endl;

	SparseMKLMult(A, B, C, timeMult);
	std::cout << "MKL multiplication time = " << timeMult << std::endl;

	//std::cout << "The matrixes difference = " << CRSComplexMatrixDiff(C, M) << std::endl;

	//std::cout << C << std::endl;
	//std::cout << M << std::endl;

	deleteCRSComplexMatrix(A);
	deleteCRSComplexMatrix(B);
	deleteCRSComplexMatrix(BT);
	deleteCRSComplexMatrix(M);
	return 0;
}