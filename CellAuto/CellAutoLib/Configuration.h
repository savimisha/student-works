#ifndef __CONFIGURATION_H__
#define __CONFIGURATION_H__

class Configuration 
{
protected: 
	int size; 
public:
	Configuration(int);
	~Configuration();
	int *conf;
};

#endif