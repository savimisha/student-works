#ifndef __POSTFIX_H__
#define __POSTFIX_H__

class Postfix
{
private:
	void ConvertToPostfix(char*);
	int Priority(char);
public:
	char *output;
	int postsize;
	Postfix(char*);
	~Postfix();
};


#endif