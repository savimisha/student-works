#ifndef __SEPARATEDSETS_H__
#define __SEPARATEDSETS_H__
namespace KruskalLib
{
class SeparatedSets
{
	int count;
	int *p;
public:
	__declspec(dllexport) SeparatedSets(int);
	__declspec(dllexport) ~SeparatedSets();
	__declspec(dllexport) void Create(int);
	__declspec(dllexport) void Union(int, int);
	__declspec(dllexport) int WhichSet(int);
};
}
#endif