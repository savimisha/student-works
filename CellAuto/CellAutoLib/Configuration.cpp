#include <iostream>
using namespace std;

#include "Configuration.h"

Configuration::Configuration(int _size)
{
	size=_size;
	conf=new int[size];
	memset(conf, 0, sizeof(int)*size);
}

Configuration::~Configuration()
{
	delete[] conf;
}