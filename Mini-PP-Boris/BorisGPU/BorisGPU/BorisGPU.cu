#include "BorisGPU.cuh"
#include "Vector3.cuh"


__constant__ double LIGHTSPEED = 29979245800.0;
__constant__ double PI = 3.14159265358979323846;
__constant__ double EPS = 0.05;


__host__ __device__ double Det(Vector3 &a, Vector3 &b, Vector3 &c)
{
	return a.x*(b.y*c.z-b.z*c.y)-b.x*(a.y*c.z-a.z*c.y)+c.x*(a.y*b.z-a.z*b.y);
}

__global__ void Boris_kernel(Particle *parts, Vector3 E, Vector3 B, double dt)
{
	int j = blockIdx.x*blockDim.x + threadIdx.x;
	Vector3 p_old, u_old, u_minus, u_new, p_new, V_new, r_new;
	Vector3 a,b,c,d;
	double Y_old, delta, factor;
	Vector3 u_plus;
	p_old = (parts[j].m/sqrt(1-(parts[j].V.SqNorm()/(LIGHTSPEED*LIGHTSPEED))))*parts[j].V;
	u_old = (1/(parts[j].m*LIGHTSPEED))*p_old;
	Y_old = sqrt(1+u_old.SqNorm());
	factor = parts[j].q*(dt)/(2*Y_old*LIGHTSPEED*parts[j].m);
	u_minus = u_old+(parts[j].q*dt/(2*parts[j].m*LIGHTSPEED))*E;
			
	a.x = 1;
	a.y = factor*B.z;
	a.z = -factor*B.y;
	b.x = -factor*B.z;
	b.y = 1;
	b.z = factor*B.x;
	c.x = factor*B.y;
	c.y = -factor*B.x;
	c.z = 1;
	d.x = u_minus.x+factor*(u_minus.y*B.z-u_minus.z*B.y);
	d.y = u_minus.y+factor*(u_minus.z*B.x-u_minus.x*B.z);
	d.z = u_minus.z+factor*(u_minus.x*B.y-u_minus.y*B.x);

	delta = Det(a,b,c);
	u_plus.x = Det(d,b,c)/delta;
	u_plus.y = Det(a,d,c)/delta;
	u_plus.z = Det(a,b,d)/delta;
	
	u_new = u_plus+factor*Y_old*E;
	p_new = parts[j].m*LIGHTSPEED*u_new;
	V_new = 1/(parts[j].m*sqrt(1+p_new.SqNorm()/(parts[j].m*parts[j].m*LIGHTSPEED*LIGHTSPEED)))*p_new;
	r_new = parts[j].r+V_new*dt;
	parts[j].r = r_new;
	parts[j].V = V_new;
		
}

void BorisGPU(Particle *parts, Vector3 E, Vector3 B, double dt, int N, int Count, int BlockSize)
{

	int nb = Count * sizeof(Particle);
	Particle *partsDev = NULL; 
	
	cudaMalloc((void**)&partsDev, nb);
	
	cudaMemcpy( partsDev, parts, nb, cudaMemcpyHostToDevice);
	dim3 blocks = dim3(Count/BlockSize,1,1);
	dim3 threads = dim3(BlockSize,1,1);
	for (int i = 0; i < N; i++)
		Boris_kernel<<<blocks, threads>>> (partsDev, E, B, dt);

	cudaDeviceSynchronize();

	cudaMemcpy(parts, partsDev, nb, cudaMemcpyDeviceToHost);

	cudaFree(partsDev);
}