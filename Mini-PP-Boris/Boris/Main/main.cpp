#include "Boris.h"
#include "Vector3.h"
#include <iostream>
#include <ctime>
#include <omp.h>
using namespace std;



int main(int argc, char **argv)
{
	const int Count = 1000000;
	double m = 9.10938291e-8;
	double q = -4.80320427e-5;
	Vector3 r = Vector3(1,0,0);
	Vector3 V = Vector3(0,2,0);
	Vector3 E = Vector3(1,2,3);
	Vector3 B = Vector3(1,2,3);
	
	Particle *W = new Particle[Count];
	for (int i=0; i<Count; i++)
		W[i] = Particle(r,V,m,q);
	double dt = 3;
	int N = 1000;
	//unsigned int start =  clock(); 
	double start = omp_get_wtime();
	Boris(W,E,B,dt,N,Count);
	double end = omp_get_wtime();
    //unsigned int end = clock(); 

	//unsigned int time = end - start;
    double time = end - start;
	cout << time << endl;

	return 0;
}