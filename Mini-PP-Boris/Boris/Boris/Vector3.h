#ifndef __VECTOR3_H__
#define __VECTOR3_H__
#include <iostream>
class Vector3
{
public:
	double x;
	double y;
	double z;
	__declspec(dllexport) Vector3();
	__declspec(dllexport) Vector3(double _x, double _y, double _z);
	__declspec(dllexport) Vector3 operator+(const Vector3 &B);
	__declspec(dllexport) Vector3 operator-(const Vector3 &B);
	__declspec(dllexport) double length() const;
	__declspec(dllexport) double SqNorm() const;
	__declspec(dllexport) Vector3 operator*(const double &a);
	__declspec(dllexport) Vector3 operator*(const Vector3 &a);
	__declspec(dllexport) Vector3& operator=(const Vector3 &A);
	__declspec(dllexport) friend std::ostream& operator<< (std::ostream &s, const Vector3 &A);
	__declspec(dllexport) friend Vector3 operator*(const double &a, const Vector3 &A);
};
#endif
