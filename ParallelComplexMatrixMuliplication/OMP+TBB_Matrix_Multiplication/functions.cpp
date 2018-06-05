#include <cstdlib>
#include <ctime>
#include <vector>
#include <cmath>
#include <tbb/parallel_for.h>
#include <tbb/blocked_range.h>
#include <tbb/partitioner.h>
#include <tbb/task_scheduler_init.h>
#include <omp.h>
#include "CRSComplexMatrix.h"
#include "Multiplicator.h"
#include <mkl.h>
#include <iostream>

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
 
double next()
{
	return ((double)rand() / (double)RAND_MAX); 
}

void GenerateRegularCRSComplexMatrix(int seed, int N, int cntInRow, CRSComplexMatrix& mtx) 
{ 
	int i, j, k, f, tmp, notNull, c; 
	notNull = cntInRow * N; 
	InitializeCRSComplexMatrix(N, notNull, mtx); 
	srand(seed);
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

double CRSComplexMatrixDiff(CRSComplexMatrix A, CRSComplexMatrix B) 
{
	if (A.N != B.N) return 1; 
	int n = A.N; 
	int i, j; 
	for (i = 0; i < A.NZ; i++) 
		A.col[i]++; 
	for (i = 0; i < B.NZ; i++) 
		B.col[i]++; 
	for (j = 0; j <= A.N; j++) 
	{ 
		A.rowIndex[j]++; 
		B.rowIndex[j]++; 
	} 

	char trans = 'N'; 
	int request; 
	int sort = 8; 
	int nzmax = -1; 
	int info; 
	clock_t start = clock();
	int *rowIndex = new int[n + 1];
	int *col = 0;
	double *valueIm = 0;
	double *valueRe = 0;
	request = 1; 
	double beta = -1.0;
	mkl_dcsradd(&trans, &request, &sort, &n, &n, A.valueRe, A.col, A.rowIndex, &beta, B.valueRe, B.col, B.rowIndex, valueRe, col, rowIndex, &nzmax, &info); 
	int nzc1 = rowIndex[n] - 1;
	mkl_dcsradd(&trans, &request, &sort, &n, &n, A.valueIm, A.col, A.rowIndex, &beta, B.valueIm, B.col, B.rowIndex, valueIm, col, rowIndex, &nzmax, &info);
	int nzc2 = rowIndex[n] - 1;

	int nzc = std::fmax(nzc1, nzc2);
	valueRe = new double[nzc]; 
	valueIm = new double[nzc];
	col = new int[nzc]; 
	request = 2; 
	mkl_dcsradd(&trans, &request, &sort, &n, &n, A.valueRe, A.col, A.rowIndex, &beta, B.valueRe, B.col, B.rowIndex, valueRe, col, rowIndex, &nzmax, &info);
	mkl_dcsradd(&trans, &request, &sort, &n, &n, A.valueIm, A.col, A.rowIndex, &beta, B.valueIm, B.col, B.rowIndex, valueIm, col, rowIndex, &nzmax, &info);

	double maxRe = 0, maxIm = 0;
	for (int i = 0; i < n; i++)
		for (int j = rowIndex[i]; j < rowIndex[i + 1]; j++)
		{
			if (maxRe < valueRe[j]) maxRe = valueRe[j];
			if (maxIm < valueIm[j]) maxIm = valueIm[j];
		}

	return std::fmaxf(maxRe, maxIm);
}

int SparseMKLMult(CRSComplexMatrix A, CRSComplexMatrix B, CRSComplexMatrix &C, double &time)
{
	if (A.N != B.N)
		return 1;
	int n = A.N;
	int i, j;
	for (i = 0; i < A.NZ; i++)
		A.col[i]++;
	for (i = 0; i < B.NZ; i++)
		B.col[i]++;
	for (j = 0; j <= n; j++)
	{
		A.rowIndex[j]++;
		B.rowIndex[j]++;
	}
	char trans = 'N';
	int request;
	int sort = 8;
	int nzmax = -1;
	int info;
	clock_t start = clock();
	C.rowIndex = new int[n + 1];
	request = 1;
	C.valueIm = 0;
	C.valueRe = 0;
	C.col = 0;

	CRSComplexMatrix ReRe, ImIm, ImRe, ReIm; 


	ReRe.rowIndex = new int[n + 1];
	ImIm.rowIndex = new int[n + 1];
	ImRe.rowIndex = new int[n + 1];
	ReIm.rowIndex = new int[n + 1];
	ReRe.valueRe = 0;
	ReRe.valueIm = 0;
	ReRe.col = 0;
	ImIm.valueRe = 0;
	ImIm.valueIm = 0;
	ImIm.col = 0;
	ImRe.valueRe = 0;
	ImRe.valueIm = 0;
	ImRe.col = 0;
	ReIm.valueRe = 0;
	ReIm.valueIm = 0;
	ReIm.col = 0;


	mkl_dcsrmultcsr(&trans, &request, &sort, &n, &n, &n, A.valueRe, A.col, A.rowIndex, B.valueRe, B.col, B.rowIndex, ReRe.valueRe, ReRe.col, ReRe.rowIndex, &nzmax, &info);
	mkl_dcsrmultcsr(&trans, &request, &sort, &n, &n, &n, A.valueIm, A.col, A.rowIndex, B.valueIm, B.col, B.rowIndex, ImIm.valueRe, ImIm.col, ImIm.rowIndex, &nzmax, &info);
	mkl_dcsrmultcsr(&trans, &request, &sort, &n, &n, &n, A.valueRe, A.col, A.rowIndex, B.valueIm, B.col, B.rowIndex, ReIm.valueRe, ReIm.col, ReIm.rowIndex, &nzmax, &info);
	mkl_dcsrmultcsr(&trans, &request, &sort, &n, &n, &n, A.valueIm, A.col, A.rowIndex, B.valueRe, B.col, B.rowIndex, ImRe.valueRe, ImRe.col, ImRe.rowIndex, &nzmax, &info);

	int nzc = ReRe.rowIndex[n] - 1;
	ReRe.NZ = nzc;
	ReRe.col = new int[nzc];
	ReRe.valueRe = new double[nzc];

	nzc = ImIm.rowIndex[n] - 1;
	ImIm.NZ = nzc;
	ImIm.col = new int[nzc];
	ImIm.valueRe = new double[nzc];

	nzc = ReIm.rowIndex[n] - 1;
	ReIm.NZ = nzc;
	ReIm.col = new int[nzc];
	ReIm.valueRe = new double[nzc];

	nzc = ImRe.rowIndex[n] - 1;
	ImRe.NZ = nzc;
	ImRe.col = new int[nzc];
	ImRe.valueRe = new double[nzc];

	request = 2;
	mkl_dcsrmultcsr(&trans, &request, &sort, &n, &n, &n, A.valueRe, A.col, A.rowIndex, B.valueRe, B.col, B.rowIndex, ReRe.valueRe, ReRe.col, ReRe.rowIndex, &nzmax, &info);
	mkl_dcsrmultcsr(&trans, &request, &sort, &n, &n, &n, A.valueIm, A.col, A.rowIndex, B.valueIm, B.col, B.rowIndex, ImIm.valueRe, ImIm.col, ImIm.rowIndex, &nzmax, &info);
	mkl_dcsrmultcsr(&trans, &request, &sort, &n, &n, &n, A.valueRe, A.col, A.rowIndex, B.valueIm, B.col, B.rowIndex, ReIm.valueRe, ReIm.col, ReIm.rowIndex, &nzmax, &info);
	mkl_dcsrmultcsr(&trans, &request, &sort, &n, &n, &n, A.valueIm, A.col, A.rowIndex, B.valueRe, B.col, B.rowIndex, ImRe.valueRe, ImRe.col, ImRe.rowIndex, &nzmax, &info);


	int *colCRe = 0;
	int *colCIm = 0;
	int *rowCRe = new int[n + 1];
	int *rowCIm = new int[n + 1];
	double *valueCRe = 0;
	double *valueCIm = 0;
	request = 1;
	double beta = -1.0;
	mkl_dcsradd(&trans, &request, &sort, &n, &n, ReRe.valueRe, ReRe.col, ReRe.rowIndex, &beta, ImIm.valueRe, ImIm.col, ImIm.rowIndex, C.valueRe, colCRe, rowCRe, &nzmax, &info);
	beta = 1.0;
	mkl_dcsradd(&trans, &request, &sort, &n, &n, ImRe.valueRe, ImRe.col, ImRe.rowIndex, &beta, ReIm.valueRe, ReIm.col, ReIm.rowIndex, C.valueIm, colCIm, rowCIm, &nzmax, &info);
	int nzcRe = rowCRe[n] - 1;
	int nzcIm = rowCIm[n] - 1;

	valueCRe = new double[nzcRe];
	valueCIm = new double[nzcIm];
	colCRe = new int[nzcRe];
	colCIm = new int[nzcIm];
	request = 2;
	beta = -1.0;
	mkl_dcsradd(&trans, &request, &sort, &n, &n, ReRe.valueRe, ReRe.col, ReRe.rowIndex, &beta, ImIm.valueRe, ImIm.col, ImIm.rowIndex, valueCRe, colCRe, rowCRe, &nzmax, &info);
	beta = 1.0;
	mkl_dcsradd(&trans, &request, &sort, &n, &n, ImRe.valueRe, ImRe.col, ImRe.rowIndex, &beta, ReIm.valueRe, ReIm.col, ReIm.rowIndex, valueCIm, colCIm, rowCIm, &nzmax, &info);

	nzc = std::fmax(nzcRe, nzcIm);
	C.N = n;
	C.NZ = nzc;
	C.valueRe = valueCRe;
	C.valueIm = valueCIm;
	C.rowIndex = rowCRe;
	C.col = colCRe;

	deleteCRSComplexMatrix(ReRe);
	deleteCRSComplexMatrix(ImIm);
	deleteCRSComplexMatrix(ImRe);
	deleteCRSComplexMatrix(ReIm);

	clock_t finish = clock();
	for (i = 0; i < A.NZ; i++)
		A.col[i]--;
	for (i = 0; i < B.NZ; i++)
		B.col[i]--;
	for (i = 0; i < C.NZ; i++)
		C.col[i]--;
	for (j = 0; j <= n; j++)
	{
		A.rowIndex[j]--;
		B.rowIndex[j]--;
		C.rowIndex[j]--;
	}
	time = double(finish - start) / double(CLOCKS_PER_SEC);
	return 0;
}


int multiplicateCRSComplexMatrixOMP(CRSComplexMatrix A, CRSComplexMatrix B, CRSComplexMatrix &C, double &time, int numThreads) 
{ 
	if (A.N != B.N)
		return 1; 
	int N = A.N; 
	int j;
	clock_t start = clock(); 
	std::vector<int>* columns = new std::vector<int>[N]; 
	std::vector<double> *valuesRe = new std::vector<double>[N]; 
	std::vector<double> *valuesIm = new std::vector<double>[N]; 
	int* row_index = new int[N + 1]; 
	memset(row_index, 0, sizeof(int) * N); 
	omp_set_num_threads(numThreads);

#pragma omp parallel for private(j)
	for (int i = 0; i < N; i++) 
	{ 
		for (j = 0; j < N; j++) 
		{ 
			double sumRe = 0, sumIm = 0; 
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
						sumIm += A.valueIm[ks] * B.valueRe[ls] + A.valueRe[ks] * B.valueIm[ls];
						ks++; 
						ls++; 
					} 
			}
			if (fabs(sumRe) > ZERO_IN_CRS || fabs(sumIm) > ZERO_IN_CRS) 
			{ 
				columns[i].push_back(j); 
				valuesRe[i].push_back(sumRe); 
				valuesIm[i].push_back(sumIm);
				row_index[i]++;
			} 
		} 
	}
	int NZ = 0; 
	for(int i = 0; i < N; i++) 
	{ 
		int tmp = row_index[i]; 
		row_index[i] = NZ; 
		NZ += tmp; 
	} 
	row_index[N] = NZ; 
	InitializeCRSComplexMatrix(N, NZ, C); 
	int count = 0; 
	for (int i = 0; i < N; i++) 
	{ 
		int size = columns[i].size(); 
		memcpy(&C.col[count], &columns[i][0], size * sizeof(int)); 
		memcpy(&C.valueRe[count], &valuesRe[i][0], size * sizeof(double)); 
		memcpy(&C.valueIm[count], &valuesIm[i][0], size * sizeof(double)); 
		count += size; 
	} 
	memcpy(C.rowIndex, &row_index[0], (N + 1) * sizeof(int)); 
	delete [] row_index; 
	delete [] columns; 
	delete [] valuesRe; 
	delete [] valuesIm;
	clock_t finish = clock(); 
	time = (double)(finish - start) / CLOCKS_PER_SEC; 
	return 0; 
}

int multiplicateCRSComplexMatrixTBB(CRSComplexMatrix A, CRSComplexMatrix B, CRSComplexMatrix &C, double &time, int numThreads) 
{ 
	if (A.N != B.N)
		return 1; 
	int N = A.N; 
	int i, j, k; 
	clock_t start = clock(); 
	std::vector<int>* columns = new std::vector<int>[N]; 
	std::vector<double> *valuesRe = new std::vector<double>[N]; 
	std::vector<double> *valuesIm = new std::vector<double>[N]; 
	int* row_index = new int[N + 1]; 
	memset(row_index, 0, sizeof(int) * N); 
	int grainsize = 10; 
	tbb::task_scheduler_init init(numThreads);
	tbb::parallel_for(tbb::blocked_range<int>(0, A.N, grainsize), MultiplicatorCRSComplexMatrix(A, B, columns, valuesRe, valuesIm, row_index));

	int NZ = 0; 
	for(i = 0; i < N; i++) 
	{ 
		int tmp = row_index[i]; 
		row_index[i] = NZ; 
		NZ += tmp; 
	} 
	row_index[N] = NZ; 
	InitializeCRSComplexMatrix(N, NZ, C); 
	int count = 0; 
	for (i = 0; i < N; i++) 
	{ 
		int size = columns[i].size(); 
		memcpy(&C.col[count], &columns[i][0], size * sizeof(int)); 
		memcpy(&C.valueRe[count], &valuesRe[i][0], size * sizeof(double));
		memcpy(&C.valueIm[count], &valuesIm[i][0], size * sizeof(double));
		count += size; 
	} 
	memcpy(C.rowIndex, &row_index[0], (N + 1) * sizeof(int)); 
	delete [] row_index; 
	delete [] columns; 
	delete [] valuesRe; 
	delete [] valuesIm; 
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