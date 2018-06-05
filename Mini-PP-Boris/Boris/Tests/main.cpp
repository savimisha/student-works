#include <iostream>
#include "Vector3.h"
#include "Boris.h"
#include "gtest/gtest.h"
using namespace std;

TEST(Boris, Static_field)
{
	

	const int K = 3;
	double m = 9.10938291e-8;
	double q = -4.80320427e-5;
	Vector3 B = Vector3(0,0,0);
	Vector3 r = Vector3(0,0,0);
	Vector3 V = Vector3(0,0,0);
	int N=100;
	Particle *W = new Particle(r,V,m,q);

	Vector3 E[K];
	E[0].x = 1000;
	E[1].y = 1000;
	E[2].z = 1000;

	double dt = m*LIGHTSPEED/(q*E[0].x*N);

	Vector3 r_final[K];
	r_final[0].x = m*LIGHTSPEED*LIGHTSPEED*(sqrt(2.0)-1)/(q*E[0].x);
	r_final[1].y = m*LIGHTSPEED*LIGHTSPEED*(sqrt(2.0)-1)/(q*E[0].x);
	r_final[2].z = m*LIGHTSPEED*LIGHTSPEED*(sqrt(2.0)-1)/(q*E[0].x);
	
	Vector3 p_final[K];
	p_final[0].x = m*LIGHTSPEED;
	p_final[1].y = m*LIGHTSPEED;
	p_final[2].z = m*LIGHTSPEED;

	Vector3 r_out[K], p_out[K];

	for(int i=0; i<K; i++)
	{
		Boris_2(W,E[i],B,dt,N,1);
		r_out[i]=W->r;
		p_out[i]=(m/sqrt(1 - W->V.SqNorm()/(LIGHTSPEED*LIGHTSPEED)))*W->V;
		*W=Particle(r,V,m,q);
	}


	double err_r[K];
	for (int i=0; i<K; i++)
		err_r[i]=(r_out[i]-r_final[i]).length()/r_final[i].length();

	double err_p[K];
	for (int i=0; i<K; i++)
		err_p[i]=(p_out[i]-p_final[i]).length()/p_final[i].length();


	for (int i=0; i<K; i++)
	{
		cout << "r_final=" << r_final[i] << endl;
		cout << "r_out=" << r_out[i] << endl;
		cout << "Error=" << err_r[i] << endl;
		cout << "p_final=" << p_final[i] << endl;
		cout << "p_out=" << p_out[i] << endl;
		cout << "Error=" << err_p[i] << endl;
		cout << endl;
	}
	cout << "EPS=" << EPS << endl;
	for (int i=0; i<K; i++)
	{
		EXPECT_TRUE(err_r[i] < EPS);
		EXPECT_TRUE(err_p[i] < EPS);
	}

}

TEST(Boris, Magnetic_field)
{
	const int K=3;
	double m=9.10938291e-8;
	double q=-4.80320427e-5;
	Vector3 r=Vector3(0,0,0);
	Vector3 E=Vector3(0,0,0);
	int N=100;
	

	double B_0=1000;
	Vector3 B[K];
	B[0].z=B_0;
	B[1].x=B_0;
	B[2].y=B_0;
	double p_0=1;
	Vector3 p[K];
	p[0].x=p_0;
	p[1].y=p_0;
	p[2].z=p_0;

	Vector3 V[K];
	double gamma = sqrt(1 + (p_0/(m*LIGHTSPEED)) * (p_0/(m*LIGHTSPEED)));
	V[0].x=p_0/(m*gamma);
	V[1].y=p_0/(m*gamma);
	V[2].z=p_0/(m*gamma);

	Particle W[K];
	for (int i=0; i<K; i++)
		W[i]=Particle(r,V[i],m,q);
	
	double dt=PI*m*LIGHTSPEED*gamma/(abs(q)*B_0*N);

	Vector3 r_final[K];
	r_final[0].y=-2*p_0*LIGHTSPEED/(q*B_0);
	r_final[1].z=-2*p_0*LIGHTSPEED/(q*B_0); 
	r_final[2].x=-2*p_0*LIGHTSPEED/(q*B_0);

	Vector3 p_final[K];
	p_final[0].x=-p_0;
	p_final[1].y=-p_0; 
	p_final[2].z=-p_0;

	Vector3 r_out[K], p_out[K];

	for(int i=0; i<K; i++)
	{
		Boris_2(&(W[i]),E,B[i],dt,N,1);
		r_out[i]=W[i].r;
		p_out[i]=(m/sqrt(1 - W->V.SqNorm()/(LIGHTSPEED*LIGHTSPEED)))*W[i].V;
	}

	double err_r[K];
	for (int i=0; i<K; i++)
		err_r[i]=(r_out[i]-r_final[i]).length()/r_final[i].length();

	double err_p[K];
	for (int i=0; i<K; i++)
		err_p[i]=(p_out[i]-p_final[i]).length()/p_final[i].length();


	for (int i=0; i<K; i++)
	{
		cout << "r_final=" << r_final[i] << endl;
		cout << "r_out=" << r_out[i] << endl;
		cout << "Error=" << err_r[i] << endl;
		cout << "p_final=" << p_final[i] << endl;
		cout << "p_out=" << p_out[i] << endl;
		cout << "Error=" << err_p[i] << endl;
		cout << endl;
	}
	cout << "EPS=" << EPS << endl;
	for (int i=0; i<K; i++)
	{
		EXPECT_TRUE(err_r[i] < EPS);
		EXPECT_TRUE(err_p[i] < EPS);
	}
}



int main(int argc, char** argv)
{
	::testing::InitGoogleTest(&argc, argv);
	return RUN_ALL_TESTS();
}