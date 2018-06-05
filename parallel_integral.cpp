#include <mpi.h>
#include <iostream>
using namespace std;

double func(double x)
{
	return 4*1/(1+x*x);
}

int main(int argc, char** argv) 
{
    int Rank, Procs;
	double a=0, b=1;
	int N=1000000;
	double step=(b-a)/N;
	double Result=0, TempRes=0;
	double TempStTime, TempEndTime, stTime, endTime;
    MPI_Init(&argc, &argv); 
	TempStTime=MPI_Wtime();
	
    MPI_Comm_size(MPI_COMM_WORLD, &Procs);
    MPI_Comm_rank(MPI_COMM_WORLD, &Rank);
    
    int TrapezeCount=N/Procs;
    int End=TrapezeCount;
    if (Rank==Procs-1) End=TrapezeCount+N%Procs;
   
    for (int i=0; i<End; i++)
    {
		TempRes+=((func(a+Rank*TrapezeCount*step+i*step)+
		func(a+Rank*TrapezeCount*step+(i+1)*step))/2)*(step);
	}
	
	MPI_Reduce(&TempRes, &Result, 1, MPI_DOUBLE, 
					MPI_SUM, 0, MPI_COMM_WORLD);

	TempEndTime=MPI_Wtime();
		
	MPI_Reduce(&TempStTime, &stTime, 1, MPI_DOUBLE, 
					MPI_MIN, 0, MPI_COMM_WORLD);
	MPI_Reduce(&TempEndTime, &endTime, 1, MPI_DOUBLE, 
					MPI_MAX, 0, MPI_COMM_WORLD);
	if (Rank == 0)
	{
		cout.precision(15);
		cout << "Pi=" << Result << endl;
		cout.precision(4);
		cout << "Parts: " << N << endl;
		cout << "Calculation time: " << endTime-stTime << endl;
	}
    
    MPI_Finalize();
    return 0;
}
