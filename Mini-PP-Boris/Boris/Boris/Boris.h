#ifndef __BORIS_H__
#define __BORIS_H__

#include "Vector3.h"
__declspec(dllexport) const double LIGHTSPEED = 29979245800.0;
__declspec(dllexport) const double PI = 3.14159265358979323846;
__declspec(dllexport) const double EPS = 0.05;
double Det(Vector3&, Vector3&, Vector3&);


class Particle 
{
public:
	Vector3 r, V;
	double m, q;
	Particle(Vector3 r, Vector3 V, double m, double q)
	{
		this->r=r;
		this->V=V;
		this->m=m;
		this->q=q;
	}
	Particle()
	{
	}
};
__declspec(dllexport) void Boris(Particle *parts, Vector3 E, Vector3 B, double dt, int N, int Count);
__declspec(dllexport) void Boris_2(Particle *parts, Vector3 E, Vector3 B, double dt, int N, int Count);

#endif