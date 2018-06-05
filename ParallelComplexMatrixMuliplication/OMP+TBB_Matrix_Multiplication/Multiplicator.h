#ifndef _MULTIPLICATOR_H_
#define _MULTIPLICATOR_H_
#include <vector>
#include "CRSComplexMatrix.h"
#include <tbb/parallel_for.h>
#include <tbb/blocked_range.h>
#include <tbb/partitioner.h>
class MultiplicatorCRSComplexMatrix 
{ 
	CRSComplexMatrix A, B; 
	std::vector<int>* columns; 
	std::vector<double>* valuesRe; 
	std::vector<double>* valuesIm; 
	int *row_index; 
public: 
	MultiplicatorCRSComplexMatrix(CRSComplexMatrix& _A, CRSComplexMatrix& _B, std::vector<int>* &_columns, std::vector<double>* &_valuesRe, std::vector<double>* &_valuesIm , int *_row_index) 
		: A(_A), B(_B), columns(_columns), valuesRe(_valuesRe), valuesIm(_valuesIm), row_index(_row_index) 
	{
	} 
	void operator()(const tbb::blocked_range<int>& r) const;
};

#endif