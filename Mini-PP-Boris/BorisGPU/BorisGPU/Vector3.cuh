#ifndef __VECTOR3_CUH__
#define __VECTOR3_CUH__

#include "cuda.h"

class Vector3
{
public:
	double x;
	double y;
	double z;
	__host__ __device__ Vector3();
	__host__ __device__ Vector3(double _x, double _y, double _z);
	__host__ __device__ Vector3 operator+(const Vector3 &B);
	__host__ __device__ Vector3 operator-(const Vector3 &B);
	__host__ __device__ double length() const;
	__host__ __device__ double SqNorm() const;
	__host__ __device__ Vector3 operator*(const double &a);
	__host__ __device__ Vector3 operator*(const Vector3 &a);
	__host__ __device__ Vector3& operator=(const Vector3 &A);
	__host__ __device__ friend Vector3 operator*(const double &a, const Vector3 &A);
};

__device__ int abc();

#endif