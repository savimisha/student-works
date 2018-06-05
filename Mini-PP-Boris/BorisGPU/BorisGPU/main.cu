#include "BorisGPU.cuh"
#include <iostream>
using namespace std;


int main()
{
	/*
	setlocale(LC_ALL, "Russian");
	cudaDeviceProp deviceProp;
	cudaGetDeviceProperties(&deviceProp, 0);
	printf("Имя устройства: %s\n", deviceProp.name);
	printf("Макс количество потоков в блоке: %d\n", deviceProp.maxThreadsPerBlock);
	printf("Максимальная размерность потока: x = %d, y = %d, z = %d\n",
      deviceProp.maxThreadsDim[0],
      deviceProp.maxThreadsDim[1],
      deviceProp.maxThreadsDim[2]);
	printf("Максимальный размер сетки: x = %d, y = %d, z = %d\n", 
      deviceProp.maxGridSize[0], 
      deviceProp.maxGridSize[1], 
      deviceProp.maxGridSize[2]); 
	 */


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
	int BlockSize = 1000;
	unsigned int start =  clock(); 
	BorisGPU(W,E,B,dt,N,Count,BlockSize);
	unsigned int end = clock(); 
	unsigned int time = end - start;
	
	cout << "Time: " << time/1000.0 << endl;

	/*for (int i=0; i<Count; i++)
	{
		cout << '(' << W[i].r.x << ", " << W[i].r.y << ", " << W[i].r.z << ')';
		cout << ' ';
		cout << '(' << W[i].V.x << ", " << W[i].V.y << ", " << W[i].V.z << ')' << endl;
	}*/

	cout << '(' << W[Count-1].r.x << ", " << W[Count-1].r.y << ", " << W[Count-1].r.z << ')';
	cout << ' ';
	cout << '(' << W[Count-1].V.x << ", " << W[Count-1].V.y << ", " << W[Count-1].V.z << ')' << endl;

	return 0;
}