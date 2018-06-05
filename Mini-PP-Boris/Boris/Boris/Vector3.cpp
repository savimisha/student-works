#include "Vector3.h"
#include <iostream>

Vector3::Vector3()
{
	x=0; y=0; z=0;
}

Vector3::Vector3(double _x, double _y, double _z)
{
	x=_x; y=_y; z=_z;
}
Vector3 Vector3::operator+(const Vector3 &B)
{
	Vector3 C;
	C.x=this->x+B.x;
	C.y=this->y+B.y;
	C.z=this->z+B.z;
	return C;
}
Vector3 Vector3::operator-(const Vector3 &B)
{
	Vector3 C;
	C.x=this->x-B.x;
	C.y=this->y-B.y;
	C.z=this->z-B.z;
	return C;
}
double Vector3::length() const
{
	return sqrt(this->x*this->x+this->y*this->y+this->z*this->z);
}
Vector3 Vector3::operator*(const double &a)
{
	Vector3 temp;
	temp.x=this->x*a;
	temp.y=this->y*a;
	temp.z=this->z*a;
	return temp;
}
Vector3 Vector3::operator*(const Vector3 &a)
{
	Vector3 temp;
	temp.x = this->y*a.z-a.y*this->z;
	temp.y = -this->x*a.z+a.x*this->z;
	temp.z = this->x*a.y-a.x*this->y;
	return temp;
}
Vector3& Vector3::operator=(const Vector3 &A)
{
	this->x=A.x;
	this->y=A.y;
	this->z=A.z;
	return *this;

}
double Vector3::SqNorm() const
{
	return this->x*this->x+this->y*this->y+this->z*this->z;
}
Vector3 operator*(const double &a, const Vector3 &A)
{
	Vector3 temp;
	temp.x=A.x*a;
	temp.y=A.y*a;
	temp.z=A.z*a;
	return temp;
}
std::ostream& operator<< (std::ostream &s, const Vector3 &A)
{
	std::cout << '(' << A.x << ',' << A.y << ',' << A.z << ')' ;
	return s;
}