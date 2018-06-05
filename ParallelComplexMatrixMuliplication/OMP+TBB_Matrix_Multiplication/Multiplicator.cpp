#include "Multiplicator.h"
#include <tbb/parallel_for.h>
#include <tbb/blocked_range.h>
#include <tbb/partitioner.h>

void MultiplicatorCRSComplexMatrix::operator()(const tbb::blocked_range<int>& r) const 
{ 
	int N = A.N;
	int j;
	for (int i = r.begin(); i < r.end(); i++)
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
	/*int begin = r.begin(); 
	int end = r.end(); 
	int N = A.N; 
	int i, j, k; 
	int *temp = new int[N]; 
	for (i = begin; i < end; i++) 
	{ 
		memset(temp, -1, N * sizeof(int)); 

		int ind1 = A.rowIndex[i], ind2 = A.rowIndex[i + 1]; 
		for (j = ind1; j < ind2; j++) 
		{ 
			int col = A.col[j]; 
		temp[col] = j; 

		} 
		for (j = 0; j < N; j++) 
		{ 
			double sumRe = 0, sumIm = 0; 
			int ind3 = B.rowIndex[j], 
				ind4 = B.rowIndex[j + 1]; 
			for (k = ind3; k < ind4; k++) 
			{ 
				int bcol = B.col[k]; 
				int aind = temp[bcol]; 
				if (aind != -1) 
				{
					sumRe += A.valueRe[aind] * B.valueRe[k] - A.valueIm[aind] * B.valueIm[k]; 
					sumIm += A.valueIm[aind] * B.valueRe[k] + A.valueRe[aind] * B.valueIm[k]; 
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
	} delete [] temp; */
}