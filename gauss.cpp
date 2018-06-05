#include <mpi.h>
#include <iostream>
#include <math.h>
using namespace std;

const double X1[1] = {0};
const double Q1[1] = {2};
const double X2[2] = {-0.5773502692, 0.5773502692};
const double Q2[2] = {1, 1};
const double X3[3] = {-0.7745966692, 0, 0.7745966692};
const double Q3[3] = {0.5555555556, 0.8888888889, 0.5555555556};
const double X4[4] = {-0.8611363116, -0.3399810436, 0.3399810436, 0.8611363116};
const double Q4[4] = {0.3478548451, 0.6521451549, 0.6521451549, 0.3478548451};
const double X5[5] = {-0.9061798459, -0.5384693101, 0, 0.5384693101, 0.9061798459};
const double Q5[5] = {0.2369268851, 0.4786286705, 0.5688888889, 0.4786286705, 0.2369268851};

double getQ(int n, int k)
{
	switch(n)
	{
		case 1: return Q1[k];
		case 2: return Q2[k];
		case 3: return Q3[k];
		case 4: return Q4[k];
		case 5: return Q5[k];
	}
}
double getX(int n, int k)
{
	switch(n)
	{
		case 1: return X1[k];
		case 2: return X2[k];
		case 3: return X3[k];
		case 4: return X4[k];
		case 5: return X5[k];
	}
}

double F1(double *p, int dim)
{
	if (p == NULL || p == 0) return -1;
	double sum = 0;
	for (int i = 0; i < dim; i++)
		sum += p[i]*p[i];
	return sum;
}

int main(int argc, char **argv)
{
	int n = 5, dim = 9;
	int Rank, Procs;
	MPI_Init(&argc, &argv); 
	MPI_Comm_size(MPI_COMM_WORLD, &Procs);
    MPI_Comm_rank(MPI_COMM_WORLD, &Rank);
	int localCount = (int)pow(n, dim) / Procs;
	int end = (int)pow(n, dim) % Procs; 
	double q = 1, sum = 0;
	double *point = new double[dim];
	double LocalTime, Time;
	double TempTime = MPI_Wtime();
	for(int i = Rank*localCount; i < (Rank+1)*localCount; i++)
	{
		q = 1;
		for(int j = 1; j <= dim; j++)
		{
			point[j-1] = getX(n, (i%(int)pow(n,dim-j+1))/(int)pow(n,dim-j) );
			q *= getQ(n, (i%(int)pow(n,dim-j+1))/(int)pow(n,dim-j) );
		}
		sum += q * F1(point, dim);	
	}
	
	if (end != 0 && Rank == Procs-1)
	{
		for(int i = Procs*localCount; i < pow(n, dim); i++)
		{
			q = 1;
			for(int j = 1; j <= dim; j++)
			{
				point[j-1] = getX(n, (i%(int)pow(n,dim-j+1))/(int)pow(n,dim-j) );
				q *= getQ(n, (i%(int)pow(n,dim-j+1))/(int)pow(n,dim-j) );
			}
			sum += q * F1(point, dim);		
		}
	}

	double result = 0;
	MPI_Reduce(&sum, &result, 1, MPI_DOUBLE, MPI_SUM, 0, MPI_COMM_WORLD);
	LocalTime=MPI_Wtime()-TempTime;   
	MPI_Reduce(&LocalTime, &Time, 1, MPI_DOUBLE, 
				MPI_MAX, 0, MPI_COMM_WORLD);
				
	if (Rank == 0) cout << result << endl; 
	if (Rank == 0) cout << "Time: " << Time << endl;

	
	MPI_Finalize();
	return 0;
}
