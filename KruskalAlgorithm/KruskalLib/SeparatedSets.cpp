#include "SeparatedSets.h"
#include <iostream>

namespace KruskalLib
{
	SeparatedSets::SeparatedSets(int _count)
	{
		count=_count;
		p=new int[count];
		for (int i=0; i<count; i++)
			p[i]=0;
	}
	SeparatedSets::~SeparatedSets()
	{
		count=0;
		delete[] p;
		p=0;
	}
	void SeparatedSets::Create(int who)
	{
		if (p[who-1] !=0) return;
		p[who-1]=who;
	}
	int SeparatedSets::WhichSet(int who)
	{
		return p[who-1];
	}
	void SeparatedSets::Union(int A, int B)
	{
		for (int i=0; i<count; i++)
			if (p[i]==B) p[i]=A;
	}
}

