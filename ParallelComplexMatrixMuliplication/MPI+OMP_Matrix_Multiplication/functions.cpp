#include <cstdlib>
#include <ctime>
#include <vector>
#include <cmath>
#include <iostream>
#include "CRSComplexMatrix.h"


void deleteCRSComplexMatrix(CRSComplexMatrix &M)
{
	M.N = 0;
	M.NZ = 0;
	delete[] M.valueRe;
	delete[] M.valueIm;
	delete[] M.col;
	delete[] M.rowIndex;
}

void transposeCRSComplexMatrix(CRSComplexMatrix M, CRSComplexMatrix &out, double &time)
{
	clock_t start = clock();
	InitializeCRSComplexMatrix(M.N, M.NZ, out);
	out.rowIndex[0] = 0;
	for (int i = 0; i < M.N + 1; i++)
		out.rowIndex[i] = 0;

	for (int i = 0; i < M.NZ; i++) 
		out.rowIndex[M.col[i] + 1]++;

	int S = 0, tmp = 0;
	for (int i = 1; i <= M.N; i++) 
	{ 
		tmp = out.rowIndex[i]; 
		out.rowIndex[i] = S; 
		S = S + tmp; 
	}

	int Col, IIndex, RIndex;
	double VRe, VIm;
	for (int i = 0; i < M.N; i++) 
	{ 
		Col = i; 
		for (int j = M.rowIndex[i]; j < M.rowIndex[i + 1]; j++) 
		{ 
			VRe = M.valueRe[j]; 
			VIm = M.valueIm[j];
			RIndex = M.col[j]; 
			IIndex = out.rowIndex[RIndex + 1]; 
			out.valueRe[IIndex] = VRe; 
			out.valueIm[IIndex] = VIm; 
			out.col[IIndex] = Col; 
			out.rowIndex[RIndex + 1]++; 
		} 
	}
	clock_t finish = clock(); 
	time = (double)(finish - start) / CLOCKS_PER_SEC;
}




void InitializeCRSComplexMatrix(int N, int NZ, CRSComplexMatrix &mtx)
{
	mtx.N = N;
	mtx.NZ = NZ;
	mtx.col = new int[NZ];
	mtx.rowIndex = new int[N + 1];
	mtx.valueRe = new double[NZ];
	mtx.valueIm = new double[NZ];
}

bool isSrandCalled = false; 
double next()
{
	return ((double)rand() / (double)RAND_MAX); 
}

void GenerateRegularCRSComplexMatrix(int seed, int N, int cntInRow, CRSComplexMatrix& mtx) 
{ 
	int i, j, k, f, tmp, notNull, c; 
	if (!isSrandCalled) 
	{ 
		srand(seed); 
		isSrandCalled = true; 
	} 
	notNull = cntInRow * N; 
	InitializeCRSComplexMatrix(N, notNull, mtx); 
	for(i = 0; i < N; i++) 
	{ 
		for(j = 0; j < cntInRow; j++) 
		{ 
			do 
			{ 
				mtx.col[i * cntInRow + j] = rand() % N; 
				f = 0; 
				for (k = 0; k < j; k++) 
					if (mtx.col[i * cntInRow + j] == mtx.col[i * cntInRow + k]) 
						f = 1; 
			} while (f == 1); 
		} 
		for (j = 0; j < cntInRow - 1; j++) 
			for (k = 0; k < cntInRow - 1; k++) 
				if (mtx.col[i * cntInRow + k] > mtx.col[i * cntInRow + k + 1]) 
				{ 
					tmp = mtx.col[i * cntInRow + k]; 
					mtx.col[i * cntInRow + k] = mtx.col[i * cntInRow + k + 1]; 
					mtx.col[i * cntInRow + k + 1] = tmp; 
				} 
	} 
	for (i = 0; i < cntInRow * N; i++)
	{
		double a = next();
		mtx.valueRe[i] = a * MAX_VAL; 
		mtx.valueIm[i] = next() * MAX_VAL; 
	} 
	c = 0; 
	for (i = 0; i <= N; i++) 
	{ 
		mtx.rowIndex[i] = c; 
		c += cntInRow; 
	} 
}



int multiplicateCRSComplexMatrix(CRSComplexMatrix A, CRSComplexMatrix B, CRSComplexMatrix &C, double &time) 
{ 
	if (A.N != B.N) 
		return 1; 
	int N = A.N; 
	std::vector<int> columns; 
	std::vector<double> valuesRe; 
	std::vector<double> valuesIm;
	std::vector<int> row_index;

	clock_t start = clock();
	int NZ = 0; 
	row_index.push_back(0); 
	for (int i = 0; i < N; i++) 
	{ 
		for (int j = 0; j < N; j++) 
		{ 
			double sumRe, sumIm ; 
			int ks = A.rowIndex[i]; 
			int ls = B.rowIndex[j]; 
			int kf = A.rowIndex[i + 1] - 1; 
			int lf = B.rowIndex[j + 1] - 1; 
			while ((ks <= kf) && (ls <= lf)) 
			{ 
				if (A.col[ks] < B.col[ls]) 
					ks++; 
				else 
					if (A.col[ks] > B.col[ls]) 
						ls++; 
					else 
					{ 
						sumRe += A.valueRe[ks] * B.valueRe[ls] - A.valueIm[ks] * B.valueIm[ls]; 
						sumIm += A.valueIm[ks] * B.valueRe[ks] + A.valueRe[ks] * B.valueIm[ks];
						ks++; 
						ls++; 
					} 
			}
			if (fabs(sumRe) > ZERO_IN_CRS || fabs(sumIm) > ZERO_IN_CRS) 
			{ 
				columns.push_back(j); 
				valuesRe.push_back(sumRe); 
				valuesIm.push_back(sumIm);
				NZ++; 
			} 
		} 
		row_index.push_back(NZ); 
	} 
	InitializeCRSComplexMatrix(N, NZ, C);
	for (unsigned int j = 0; j < columns.size(); j++) 
	{ 
		C.col[j] = columns[j]; 
		C.valueRe[j] = valuesRe[j]; 
		C.valueIm[j] = valuesIm[j]; 
	} 
	for(int i = 0; i <= N; i++) 
		C.rowIndex[i] = row_index[i];

	clock_t finish = clock(); 
	time = (double)(finish - start) / CLOCKS_PER_SEC;

	return 0;
}

std::ostream& operator << (std::ostream& os, const CRSComplexMatrix& mat)
{
	os << '[' << std::endl;
	for (int i = 0; i < mat.NZ; i++)
	{
		os << " " << "(" << mat.valueRe[i] << ", " << mat.valueIm[i] << ")" << ", ";
	}
	os << std::endl << ']';

	os << std::endl;

	os << '[' << std::endl;
	for (int i = 0; i < mat.NZ; i++)
		os << " " << mat.col[i] << ", ";
	os << std::endl << ']';

	os << std::endl;

	os << '[' << std::endl;
	for (int i = 0; i < mat.N + 1; i++)
		os << " " << mat.rowIndex[i] << ", ";
	os << std::endl << ']';

	return os;
}


