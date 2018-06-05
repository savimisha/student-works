#ifndef __BORISGPU_CUH__
#define __BORISGPU_CUH__

#include "Vector3.cuh"
#include "cuda.h"



class Particle 
{
public:
	Vector3 r, V;
	double m, q;
	__host__ __device__ Particle(Vector3 r, Vector3 V, double m, double q)
	{
		this->r=r;
		this->V=V;
		this->m=m;
		this->q=q;
	}
	__host__ __device__ Particle()
	{
	}
};

void BorisGPU(Particle *parts, Vector3 E, Vector3 B, double dt, int N, int Count, int BlockSize);

#endif