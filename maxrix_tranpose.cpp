#include <mpi.h>
#include <iostream>
using namespace std;

void Transpose(int rows, int columns, double *A_temp, double *B_temp)
{
	int i,j;
	for(i = 0; i < columns; i++)
		for(j = 0; j < rows; j++)
			B_temp[j + rows*i] = A_temp[i + columns*j];
}

int main(int argc, char **argv)
{
	int Rank, Procs;
	const int N=10, M=8;
	double *Matrix, *MatrixT;
	MPI_Init(&argc, &argv); 
	MPI_Comm_size(MPI_COMM_WORLD, &Procs);
    MPI_Comm_rank(MPI_COMM_WORLD, &Rank);
    
    
    if (Rank == 0) 
    {
		Matrix=new double[N*M];
		for (int i=0; i<N*M; i++)
		Matrix[i]=i+1;
	}
    double Time;
    
    int M_local=M/Procs;
    double *Temp=new double[N*M_local]; 
    double *TempT=new double[N*M_local];
    if (Rank==0) MatrixT=new double[N*M];   
	MPI_Datatype VectorType, VectorType1;
	
	MPI_Type_vector(N, M_local, M, MPI_DOUBLE, &VectorType); 
	
	 
    MPI_Type_commit(&VectorType);
    MPI_Type_create_resized(VectorType, 0, M_local*sizeof(double), &VectorType1);
    MPI_Type_commit(&VectorType1);
    double TempTime=MPI_Wtime();
	
	MPI_Scatter(Matrix, 1, VectorType1, Temp, N*M_local, MPI_DOUBLE, 0, MPI_COMM_WORLD);
	
    Transpose(N,M_local,Temp,TempT);   
    
	MPI_Gather(TempT, M_local*N,MPI_DOUBLE, 
     MatrixT, M_local*N, MPI_DOUBLE, 0, MPI_COMM_WORLD); 
     
    double LocalTime=MPI_Wtime()-TempTime;     
    MPI_Reduce(&LocalTime, &Time, 1, MPI_DOUBLE, 
					MPI_MAX, 0, MPI_COMM_WORLD);
	
    if (Rank==0)
     {
		for (int i=0; i<N*M; i++)
		{
			if (i%M==0 && i!=0) cout << endl;
			cout << Matrix[i] << ' ';
			
		}
		cout << endl;
		cout << endl;
		for (int i=0; i<N*M; i++)
		{
			if (i%N==0 && i!=0) cout << endl;
			cout << MatrixT[i] << ' ';
		}
		cout << endl; 
		cout << "time: " << Time << endl;
	 }
     
    MPI_Type_free(&VectorType);
    MPI_Type_free(&VectorType1);
	MPI_Finalize();
	delete []Temp;
	delete []TempT;
	if (Rank==0)
	{
		delete []Matrix;
		delete []MatrixT;
	}
	
	return 0;
}
