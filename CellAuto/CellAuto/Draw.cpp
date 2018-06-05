#include "CellAuto2D.h"
#include "CellAuto1D.h"
#include "CellAuto2DPersonal.h"
#include "TData.h"
#include <iostream>
#include <opencv2/opencv.hpp>
#include <time.h>
using namespace cv;
using namespace std;

#define ESC_KEY 27
#define ENTER_KEY 13
#define SPACE_KEY 32
#define DELAY 100

int width=500, height=500;
const char* winName = "CellAuto";
const int lineThick=1;
Mat image;
struct DataForMouseClick
{
	int n;
	int m;
	Configuration* conf;
};

void RandomConfiguring(Configuration* conf, int n, int m)
{
	srand(time(NULL));
	for(int i=0; i<n*m; i++)
	{
		conf->conf[i]=rand()%2;
	}
}

void onMouseClick(int eventId, int x, int y, int flags, void *userdata)
{
	if (eventId != CV_EVENT_LBUTTONDOWN) return;
	DataForMouseClick* data=(DataForMouseClick*)userdata;
	int widthCell=(width-(data->n+1)*lineThick)/data->n, heightCell=(height-(data->m+1)*lineThick)/data->m;
    int xLTAngle = (x / (widthCell + lineThick)) * (widthCell + lineThick) + lineThick;
    int yLTAngle = (y / (heightCell + lineThick)) * (heightCell + lineThick) + lineThick;
	if (data->conf->conf[y/(heightCell+lineThick)*data->n+x/(widthCell+lineThick)]==0)
	{
		rectangle(image, cvRect(xLTAngle, yLTAngle, widthCell, heightCell), CV_RGB(0, 100, 0), CV_FILLED);
		data->conf->conf[y/(heightCell+lineThick)*data->n+x/(widthCell+lineThick)]=1;
		//cout << "conf[" << y/(heightCell+lineThick)*data->n+x/(widthCell+lineThick) << "]=1\n";
	} 
	else
	{
		rectangle(image, cvRect(xLTAngle, yLTAngle, widthCell, heightCell), CV_RGB(255, 255, 255), CV_FILLED);
		data->conf->conf[y/(heightCell+lineThick)*data->n+x/(widthCell+lineThick)]=0;
		//cout << "conf[" << y/(heightCell+lineThick)*data->n+x/(widthCell+lineThick) << "]=0\n";
	}
	imshow(winName, image);
}
void onMouseClick1D(int eventId, int x, int y, int flags, void *userdata)
{
	if (eventId != CV_EVENT_LBUTTONDOWN) return;
	DataForMouseClick* data=(DataForMouseClick*)userdata;
	int widthCell=(width-(data->n+1)*lineThick)/data->n;
	int xLTAngle = (x / (widthCell + lineThick)) * (widthCell + lineThick) + lineThick;
	if (y>widthCell+2*lineThick) return;
	if (data->conf->conf[x/(widthCell+lineThick)]==0)
	{
		rectangle(image, cvRect(xLTAngle, 1, widthCell, widthCell), CV_RGB(0, 100, 0), CV_FILLED);
		//cout << "conf[" << x/(widthCell+lineThick) << "]=1\n";
		data->conf->conf[x/(widthCell+lineThick)]=1;
	}
	else
	{
		rectangle(image, cvRect(xLTAngle, 1, widthCell, widthCell), CV_RGB(255, 255, 255), CV_FILLED);
		//cout << "conf[" << x/(widthCell+lineThick) << "]=0\n";
		data->conf->conf[x/(widthCell+lineThick)]=0;
	}
	imshow(winName, image);
}

void Lines(int n, int m)
{
	int widthCell=(width-(n+1)*lineThick)/n, heightCell=(height-(m+1)*lineThick)/m;
	int begin = 0;
	for (int i = 0; i <= n; i++)
	{
		begin = i * (widthCell + lineThick);
		line(image, cvPoint(begin, 0), cvPoint(begin, height), CV_RGB(0, 0, 0), 
						lineThick);
	}
	begin = 0;
	for (int i = 0; i <= m; i++)
	{
		begin = i * (heightCell + lineThick);
		line(image, cvPoint(0, begin), cvPoint(width, begin), CV_RGB(0, 0, 0), 
					lineThick);
	}
}
int PersonalConfiguring2D(int n, int m, Configuration* conf)
{
	int key=0;
	namedWindow(winName,1);
	DataForMouseClick data;
	data.n=n;
	data.m=m;
	data.conf=conf;
	int widthCell=(width-(n+1)*lineThick)/n, heightCell=(height-(m+1)*lineThick)/m;
	int xLTAngle=0;
	int yLTAngle=0;
	cvSetMouseCallback(winName, onMouseClick, (void*)(&data));		
	image.release();
	width=widthCell*n+lineThick*(n+1);
	height=heightCell*m+lineThick*(m+1);
	image.create(width,height,CV_32FC3);
	rectangle(image, cvRect(0, 0, width, height), CV_RGB(255, 255, 255), CV_FILLED);
	Lines(n,m);
	imshow(winName,image);
	while (key!=ENTER_KEY && key!=ESC_KEY) 
	{
		key=waitKey();
	}
	if (key==ESC_KEY) { image.release(); destroyAllWindows(); return -1; }
	image.release();
	return 0;
}
void LinesFor1D(int n, int k)
{
	int widthCell=(width-(n+1)*lineThick)/n;
	int begin = 0;
	for (int i = 0; i <= n; i++)
	{
		begin = i * (widthCell + lineThick);
		line(image, cvPoint(begin, k*(widthCell+lineThick)+lineThick), 
			cvPoint(begin, k*(widthCell+lineThick)+widthCell), CV_RGB(0, 0, 0), 
						lineThick);
	}
	line(image, cvPoint(0, k*(widthCell+lineThick)), cvPoint(width, k*(widthCell+lineThick)), CV_RGB(0, 0, 0), 
					lineThick);
	line(image, cvPoint(0, (k+1)*(widthCell+lineThick)), cvPoint(width, (k+1)*(widthCell+lineThick)), CV_RGB(0, 0, 0), 
					lineThick);

}
int PersonalConfiguring1D(int n, int qsize, Configuration* conf)
{
	int key=0;
	namedWindow(winName,1);
	DataForMouseClick data;
	data.n=n;
	data.m=qsize;
	data.conf=conf;
	int widthCell=(width-(n+1)*lineThick)/n;
	int xLTAngle=0;
	int yLTAngle=0;
	cvSetMouseCallback(winName, onMouseClick1D, (void*)(&data));		
	width=widthCell*n+lineThick*(n+1);
	height=widthCell*qsize+lineThick*(qsize+1);
	image.create(height,width,CV_32FC3);
	rectangle(image, cvRect(0, 0, width, height), CV_RGB(255, 255, 255), CV_FILLED);
	LinesFor1D(n,0);
	imshow(winName,image);
	while (key!=ENTER_KEY && key!=ESC_KEY && key!=SPACE_KEY) 
	{
		key=waitKey();
	}
	if (key==ESC_KEY) { image.release(); destroyAllWindows(); return -1; }
	return 0;
	rectangle(image, cvRect(0, 0, width, height), CV_RGB(255, 255, 255), CV_FILLED);
}



void Draw1D(CellAuto1D* object, int n, int qsize)
{
	int key=0;
	namedWindow(winName,1);
	int widthCell=(width-(n+1)*lineThick)/n;
	int xLTAngle=0;
	int yLTAngle=0;
	TDataRoot*pQueue=object->GetQueuePointer();
	bool show=false;
	Configuration* tmp;
	width=widthCell*n+lineThick*(n+1);
	height=widthCell*qsize+lineThick*(qsize+1);
	image.create(height,width,CV_32FC3);
	rectangle(image, cvRect(0, 0, width, height), CV_RGB(255, 255, 255), CV_FILLED); 
	while (key!=ESC_KEY) 
	{
		switch (key)
		{
		case ENTER_KEY: { show=true; break; }
		case SPACE_KEY: { show=false; break; }
		default: { break; }
		}
		if (pQueue->isFull()==DataFull) rectangle(image, cvRect(0, 0, width, height), CV_RGB(255, 255, 255), CV_FILLED);
		object->SaveCurrConf();
		for(int i=0; i<pQueue->GetCount(); i++)
		{
			tmp=pQueue->Get();
			for(int j=0; j<n; j++)
			{
				if (tmp->conf[j]==1)
				{
					xLTAngle = j*(widthCell + lineThick) + lineThick;
					yLTAngle = i*(widthCell + lineThick) + lineThick;
					rectangle(image, cvRect(xLTAngle, yLTAngle, widthCell, widthCell), CV_RGB(0, 100, 0), CV_FILLED);
				}
			}
			LinesFor1D(n, i);
			pQueue->Put(tmp);
			
		}
		object->GetNextConf();
		object->SetCurrConf();
		imshow(winName,image);
		if (show==true) key=waitKey(DELAY);
		if (show==false) key=waitKey();
	}
	image.release();
	destroyAllWindows();
}

void Draw2D(CellAuto2D* object, int n, int m)
{
	int key=0;
	namedWindow(winName,1);
	int widthCell=(width-(n+1)*lineThick)/n, heightCell=(height-(m+1)*lineThick)/m;
	int xLTAngle=0;
	int yLTAngle=0;
	bool show=false;
	while (key!=ESC_KEY) 
	{
		switch (key)
		{
		case ENTER_KEY: { show=true; break; }
		case SPACE_KEY: { show=false; break; }
		default: { break; }
		}
		image.release();
		width=widthCell*n+lineThick*(n+1);
		height=heightCell*m+lineThick*(m+1);
		image.create(width,height,CV_32FC3);
		rectangle(image, cvRect(0, 0, width, height), CV_RGB(255, 255, 255), CV_FILLED);
		Lines(n,m);
		for(int i=0; i<n*m; i++)
		{
			if (object->GetState(i)==1)
			{
				xLTAngle = (i%n)*(widthCell + lineThick) + lineThick;
				yLTAngle = (i/n)*(heightCell + lineThick) + lineThick;
				rectangle(image, cvRect(xLTAngle, yLTAngle, widthCell, heightCell), CV_RGB(0, 100, 0), CV_FILLED);
			} 
		}
		imshow(winName,image);
		object->GetNextConf();
		object->SetCurrConf();
		if (show==true) key=waitKey(DELAY);
		if (show==false) key=waitKey();
	}
	image.release();
	destroyAllWindows();
}

void ConfiguringAndDrawing1D(int n, int OptionSurround, int OptionConf, int qsize)
{
	Configuration conf(n);
	CellAuto1D *object;
	switch (OptionSurround)
	{
	case 0: 
		{
			object=new CellAuto1DNeumann(n, qsize);
			break;
		}
	case 1: 
		{
			object=new CellAuto1DMvon(n, qsize);
			break;
		}
	default: 
		{
			return;
		}
	}
	switch (OptionConf)
	{
	case 0: 
		{  
			object->SetTestConf();
			break;
		}
	case 1: 
		{
			RandomConfiguring(&conf, n, 1);
			object->SetInitialConf(&conf);
			break; 
		}
	case 2: 
		{ 
			int tmp=PersonalConfiguring1D(n, qsize, &conf);
			if (tmp==-1) { delete object; return; }
			object->SetInitialConf(&conf);
			break; 
		}
	default: 
		{ 
			delete object; 
			cout << "Incorrect parameters\n";
			return; 
		}
	}
	
	Draw1D(object, n, qsize);
	delete object;
}

void ConfiguringAndDrawing2D(int n, int m, int OptionSurround, int OptionConf, char *formula)
{
	Configuration conf(n*m);
	CellAuto2D *object;
	switch (OptionSurround)
	{
	case 0: 
		{
			object=new CellAuto2DNeumann(n, m);
			break;
		}
	case 1: 
		{
			object=new CellAuto2DMoore(n, m);
			break;
		}
	case 2: 
		{
			object=new CellAuto2DMvon(n, m);
			break;
		}
	case 3: 
		{
			object=new CellAuto2DPersonal(formula, n, m);
			break;
		}
	default: 
		{
			return;
		}
	}
	switch (OptionConf)
	{
	case 0: 
		{  
			object->SetTestConf();
			break;
		}
	case 1: 
		{
			RandomConfiguring(&conf, n, m);
			object->SetInitialConf(&conf);
			break; 
		}
	case 2: 
		{ 
			int tmp=PersonalConfiguring2D(n,m, &conf);
			if (tmp==-1) { delete object; return; }
			object->SetInitialConf(&conf);
			break; 
		}
	default: 
		{ 
			delete object; 
			cout << "Incorrect parameters\n";
			return; 
		}
	}
	
	Draw2D(object, n, m);
	delete object;
}



