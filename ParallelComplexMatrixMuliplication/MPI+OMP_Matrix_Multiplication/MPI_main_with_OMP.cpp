#include "CRSComplexMatrix.h"
#include <iostream>
#include <mpi.h>
#include <omp.h>
#include <vector>

#define NZ_ELEMENTS 0
#define VALUESRE_SEND_1 1
#define VALUESIM_SEND_1 2
#define VALUESRE_SEND_2 3
#define VALUESIM_SEND_2 4

#define COLS_SEND_1 5
#define COLS_SEND_2 6

#define ROW_IND_SEND_1 7
#define ROW_IND_SEND_2 8
#define COLLECT_ROW_IND 9


int main(int argc, char **argv)
{
	if (argc != 4) return -1;
	int N = atoi(argv[1]);
	int cntInRow = atoi(argv[2]);
	int NZ = N * cntInRow;
	int numOMPThreads = atoi(argv[3]);
	omp_set_num_threads(numOMPThreads);
	int Rank, Procs;
	double timeTranspose;
	MPI_Init(&argc, &argv); 
	MPI_Comm_size(MPI_COMM_WORLD, &Procs);
    MPI_Comm_rank(MPI_COMM_WORLD, &Rank);
	
	CRSComplexMatrix A, B, M;
	CRSComplexMatrix BT;
	if (Rank == 0)
	{
		InitializeCRSComplexMatrix(N, NZ, A);
		InitializeCRSComplexMatrix(N, NZ, B);
		GenerateRegularCRSComplexMatrix(10, N, cntInRow, A);
		GenerateRegularCRSComplexMatrix(100, N, cntInRow, B);
		transposeCRSComplexMatrix(B, BT, timeTranspose);
	}
	
	double start, end;

	start = MPI_Wtime();
	
	int rowsToWorkWith = N / Procs; 

	int nzElementsToWorkWith;
	MPI_Status status;

	if (Rank == 0)
	{
		nzElementsToWorkWith = A.rowIndex[rowsToWorkWith] - A.rowIndex[0];
		for (int i = 1; i < Procs; i++)
		{
			nzElementsToWorkWith = A.rowIndex[(i+1)*rowsToWorkWith] - A.rowIndex[i*rowsToWorkWith];
			MPI_Send(&nzElementsToWorkWith, 1, MPI_INT, i, NZ_ELEMENTS, MPI_COMM_WORLD);
		}
	}
	else 
	{
		MPI_Recv(&nzElementsToWorkWith, 1, MPI_INT, 0, NZ_ELEMENTS, MPI_COMM_WORLD, &status);
	}

	double *values1Re = new double[nzElementsToWorkWith];
	double *values1Im = new double[nzElementsToWorkWith];
	double *values2Re = new double[NZ];
	double *values2Im = new double[NZ];

	int* cols1 = new int[nzElementsToWorkWith];
	int* cols2 = new int[NZ];

	int* rowInd1 = new int[rowsToWorkWith+1];
	int* rowInd2 = new int[N + 1];

	if (Rank == 0)
	{
			for (int i = 1; i < Procs; i++)
			{
				MPI_Send(&A.valueRe[A.rowIndex[i*rowsToWorkWith]], A.rowIndex[(i+1)*rowsToWorkWith] -  A.rowIndex[i*rowsToWorkWith], MPI_DOUBLE, i, VALUESRE_SEND_1, MPI_COMM_WORLD);
				MPI_Send(&A.valueIm[A.rowIndex[i*rowsToWorkWith]], A.rowIndex[(i+1)*rowsToWorkWith] -  A.rowIndex[i*rowsToWorkWith], MPI_DOUBLE, i, VALUESIM_SEND_1, MPI_COMM_WORLD);

				MPI_Send(BT.valueRe, NZ, MPI_DOUBLE, i, VALUESRE_SEND_2, MPI_COMM_WORLD);
				MPI_Send(BT.valueIm, NZ, MPI_DOUBLE, i, VALUESIM_SEND_2, MPI_COMM_WORLD);

				MPI_Send(&A.col[A.rowIndex[i*rowsToWorkWith]], A.rowIndex[(i+1)*rowsToWorkWith] -  A.rowIndex[i*rowsToWorkWith], MPI_INT, i, COLS_SEND_1, MPI_COMM_WORLD);
				MPI_Send(BT.col, NZ, MPI_INT, i, COLS_SEND_2, MPI_COMM_WORLD);

				MPI_Send(&A.rowIndex[i], rowsToWorkWith + 1, MPI_INT, i, ROW_IND_SEND_1, MPI_COMM_WORLD);
				MPI_Send(BT.rowIndex, N + 1, MPI_INT, i, ROW_IND_SEND_2, MPI_COMM_WORLD);
			}
	}

	if (Rank == 0)
	{
		memcpy(values1Re, &A.valueRe[A.rowIndex[0]], sizeof(double)*nzElementsToWorkWith);
		memcpy(values1Im, &A.valueIm[A.rowIndex[0]], sizeof(double)*nzElementsToWorkWith);
		memcpy(values2Re, BT.valueRe, sizeof(double)*NZ);
		memcpy(values2Im, BT.valueIm, sizeof(double)*NZ);
		memcpy(cols1, &A.col[A.rowIndex[0]],  sizeof(int)*nzElementsToWorkWith);
		memcpy(cols2, BT.col,  sizeof(int)*NZ);
		memcpy(rowInd1, &A.rowIndex[0], sizeof(int)*(rowsToWorkWith + 1));
		memcpy(rowInd2, BT.rowIndex, sizeof(int)*(N + 1));
	}
	else
	{
		MPI_Recv(values1Re, nzElementsToWorkWith, MPI_DOUBLE, 0, VALUESRE_SEND_1, MPI_COMM_WORLD, &status);
		MPI_Recv(values1Im, nzElementsToWorkWith, MPI_DOUBLE, 0, VALUESIM_SEND_1, MPI_COMM_WORLD, &status);
		MPI_Recv(values2Re, NZ, MPI_DOUBLE, 0, VALUESRE_SEND_2, MPI_COMM_WORLD, &status);
		MPI_Recv(values2Im, NZ, MPI_DOUBLE, 0, VALUESIM_SEND_2, MPI_COMM_WORLD, &status);

		MPI_Recv(cols1, nzElementsToWorkWith, MPI_INT, 0, COLS_SEND_1, MPI_COMM_WORLD, &status);
		MPI_Recv(cols2, NZ, MPI_INT, 0, COLS_SEND_2, MPI_COMM_WORLD, &status);

		MPI_Recv(rowInd1, rowsToWorkWith + 1, MPI_INT, 0, ROW_IND_SEND_1, MPI_COMM_WORLD, &status);
		MPI_Recv(rowInd2, N + 1, MPI_INT, 0, ROW_IND_SEND_2, MPI_COMM_WORLD, &status);
	}

	int correction = rowInd1[0];
	for(int i = 0; i < rowsToWorkWith + 1; i++)
		rowInd1[i] -= correction;



	std::vector<int> *columns = new std::vector<int>[rowsToWorkWith]; 
	std::vector<double> *valuesRe = new std::vector<double>[rowsToWorkWith]; 
	std::vector<double> *valuesIm = new std::vector<double>[rowsToWorkWith];
	int* row_index = new int[rowsToWorkWith + 1];
	memset(row_index, 0, sizeof(int)*(rowsToWorkWith+1));
	int j;
#pragma omp parallel for private(j)
	for (int i = 0; i < rowsToWorkWith; i++) 
	{ 
		for (j = 0; j < N; j++) 
		{ 
			double sumRe = 0, sumIm = 0; 
			int ks = rowInd1[i]; 
			int ls = rowInd2[j]; 
			int kf = rowInd1[i + 1] - 1; 
			int lf = rowInd2[j + 1] - 1; 
			while ((ks <= kf) && (ls <= lf)) 
			{ 
				if (cols1[ks] < cols2[ls]) 
					ks++; 
				else 
					if (cols1[ks] > cols2[ls]) 
						ls++; 
					else 
					{ 
						sumRe += values1Re[ks] * values2Re[ls] - values1Im[ks] * values2Im[ls]; 
						sumIm += values1Im[ks] * values2Re[ls] + values1Re[ks] * values2Im[ls];
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

	int NZnew = 0; 
	for(int i = 0; i < rowsToWorkWith; i++) 
	{ 
		int tmp = row_index[i]; 
		row_index[i] = NZnew; 
		NZnew += tmp; 
	} 
	row_index[rowsToWorkWith] = NZnew;



	double *values3Re = new double[NZnew];
	double *values3Im = new double[NZnew];
	int* cols3 = new int[NZnew];
	int* rowInd3 = new int[rowsToWorkWith + 1];

	int count = 0; 
	for (int i = 0; i < rowsToWorkWith; i++) 
	{ 
		int size = columns[i].size(); 
		memcpy(&cols3[count], &columns[i][0], size * sizeof(int)); 
		memcpy(&values3Re[count], &valuesRe[i][0], size * sizeof(double)); 
		memcpy(&values3Im[count], &valuesIm[i][0], size * sizeof(double)); 
		count += size; 
	} 
	memcpy(rowInd3, &row_index[0], (rowsToWorkWith + 1) * sizeof(int)); 
	delete [] row_index; 
	delete [] columns; 
	delete [] valuesRe;
	delete [] valuesIm;




	int *rcounts = NULL;
		if (Rank == 0)
			rcounts = new int[Procs];
	MPI_Gather(&NZnew, 1, MPI_INT, rcounts, 1, MPI_INT, 0, MPI_COMM_WORLD);
	int* displs = NULL;
		int nnz = -1;
		if (Rank == 0)
		{
			displs = new int[Procs];
			displs[0] = 0;
			for (int i = 1; i < Procs; i++)
				displs[i] = displs[i - 1] + rcounts[i-1];

			nnz = displs[Procs - 1] + rcounts[Procs - 1];
		}

		double *resValueRE = NULL;
		double *resValueIM = NULL;
		int *resCol = NULL, *resRowInd = NULL;

		if (Rank == 0)
		{
			resValueRE = new double[nnz];
			resValueIM = new double[nnz];
			resCol = new int[nnz];
			resRowInd = new int[N + 1];
		}

		MPI_Gatherv(values3Re, NZnew, MPI_DOUBLE, resValueRE, rcounts, displs, MPI_DOUBLE, 0, MPI_COMM_WORLD);
		MPI_Gatherv(values3Im, NZnew, MPI_DOUBLE, resValueIM, rcounts, displs, MPI_DOUBLE, 0, MPI_COMM_WORLD);
		MPI_Gatherv(cols3, NZnew, MPI_INT, resCol, rcounts, displs, MPI_INT, 0, MPI_COMM_WORLD);

		if(Rank != 0)
			MPI_Send(rowInd3, rowsToWorkWith + 1, MPI_INT, 0, COLLECT_ROW_IND, MPI_COMM_WORLD);

		if (Rank == 0)
		{
			memcpy(resRowInd, rowInd3, sizeof(int)*(rowsToWorkWith + 1));

			int* rowIndBuffer = new int[rowsToWorkWith + 1];

			for(int i = 1; i < Procs; i++)
			{
				MPI_Recv(rowIndBuffer, rowsToWorkWith + 1, MPI_INT, i, COLLECT_ROW_IND, MPI_COMM_WORLD, &status);
				for(int j = 0; j < rowsToWorkWith + 1; j++)
					rowIndBuffer[j] += resRowInd[i*(rowsToWorkWith + 1) - i];
				memcpy(&resRowInd[i*rowsToWorkWith], rowIndBuffer, sizeof(int)*(rowsToWorkWith + 1));
			}

			delete rowIndBuffer;
		}

		M.N = N;
		M.NZ = nnz;
		M.col = resCol;
		M.rowIndex = resRowInd;
		M.valueRe = resValueRE;
		M.valueIm = resValueIM;
	end = MPI_Wtime();
	
	if (Rank == 0)
	{
		std::cout << end - start << std::endl;
		//std::cout << A;
		//std::cout << B;
		//std::cout << M;
	}
	if (Rank == 0)
	{
		deleteCRSComplexMatrix(A);
		deleteCRSComplexMatrix(B);
		deleteCRSComplexMatrix(BT);
		deleteCRSComplexMatrix(M);
	}
	
	MPI_Finalize();
	return 0;
}

