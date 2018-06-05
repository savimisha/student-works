#pragma once
#include "Graph.h"
#include "KruskalImplementation.h"
#include "PriorityQueue.h"
#include "DHeapForEdges.h"
#include "DHeapBasedPriorityQueue.h"
#include "BinarySearchTree.h"
#include "BSTbasedPQ.h"
#include "AVLTbasedPQ.h"
#include <stdlib.h>
#include <time.h>
namespace KruskalAlgorithm {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	using namespace Microsoft::Glee::Drawing;

	/// <summary>
	/// Summary for Form1
	/// </summary>
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:
		Form1(void)
		{
			countOfRedEdges=0;
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~Form1()
		{
			if (mygraph!=NULL) delete mygraph;
			if (SpanningTree!=NULL) delete SpanningTree;
			if (components)
			{
				delete components;
			}
		}

	protected: 
	private: KruskalLib::Graph *mygraph;
	private: Graph ^MSAGLgraph;
	private: array<Edge^> ^MSAGLgraphEdges;
	private: KruskalLib::Graph *SpanningTree;
	private: int countOfRedEdges;
	private: Microsoft::Glee::GraphViewerGdi::GViewer^  gViewer;
	private: System::Windows::Forms::TextBox^  CountOfVertecestextBox;
	private: System::Windows::Forms::TextBox^  CountOfEdgestextBox;
	private: System::Windows::Forms::Label^  CountOfVerteceslabel;
	private: System::Windows::Forms::Label^  CountOfEdgeslabel;
	private: System::Windows::Forms::Button^  NextStepbutton;
	private: System::Windows::Forms::Button^  GenerateGraphbutton;


	private: System::Windows::Forms::RadioButton^  DHeapBasedPQradioButton;
	private: System::Windows::Forms::RadioButton^  BinarySearchTreeBasedPQradioButton;
	private: System::Windows::Forms::Button^  ShowFullbutton;
	private: System::Windows::Forms::Button^  ClearGraphbutton;
	private: System::Windows::Forms::MenuStrip^  TopMenumenuStrip;
	private: System::Windows::Forms::ToolStripMenuItem^  TopMenutoolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^  TopMenuAboutToolStripMenuItem;
	private: System::Windows::Forms::RadioButton^  AVLTreeBasedPQradioButton;
	private: System::Windows::Forms::TextBox^  textBox1;



	private: System::ComponentModel::IContainer^  components;









	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>


#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			System::ComponentModel::ComponentResourceManager^  resources = (gcnew System::ComponentModel::ComponentResourceManager(Form1::typeid));
			this->gViewer = (gcnew Microsoft::Glee::GraphViewerGdi::GViewer());
			this->GenerateGraphbutton = (gcnew System::Windows::Forms::Button());
			this->CountOfVertecestextBox = (gcnew System::Windows::Forms::TextBox());
			this->CountOfEdgestextBox = (gcnew System::Windows::Forms::TextBox());
			this->CountOfVerteceslabel = (gcnew System::Windows::Forms::Label());
			this->CountOfEdgeslabel = (gcnew System::Windows::Forms::Label());
			this->NextStepbutton = (gcnew System::Windows::Forms::Button());
			this->DHeapBasedPQradioButton = (gcnew System::Windows::Forms::RadioButton());
			this->BinarySearchTreeBasedPQradioButton = (gcnew System::Windows::Forms::RadioButton());
			this->ShowFullbutton = (gcnew System::Windows::Forms::Button());
			this->ClearGraphbutton = (gcnew System::Windows::Forms::Button());
			this->TopMenumenuStrip = (gcnew System::Windows::Forms::MenuStrip());
			this->TopMenutoolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->TopMenuAboutToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->AVLTreeBasedPQradioButton = (gcnew System::Windows::Forms::RadioButton());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->TopMenumenuStrip->SuspendLayout();
			this->SuspendLayout();
			// 
			// gViewer
			// 
			this->gViewer->AsyncLayout = false;
			this->gViewer->AutoScroll = true;
			this->gViewer->BackColor = System::Drawing::Color::LightGray;
			this->gViewer->BackgroundImageLayout = System::Windows::Forms::ImageLayout::None;
			this->gViewer->BackwardEnabled = false;
			this->gViewer->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->gViewer->ForwardEnabled = true;
			this->gViewer->Graph = nullptr;
			this->gViewer->Location = System::Drawing::Point(12, 12);
			this->gViewer->MouseHitDistance = 0.05;
			this->gViewer->Name = L"gViewer";
			this->gViewer->NavigationVisible = true;
			this->gViewer->PanButtonPressed = true;
			this->gViewer->SaveButtonVisible = true;
			this->gViewer->Size = System::Drawing::Size(550, 550);
			this->gViewer->TabIndex = 0;
			this->gViewer->ZoomF = 1;
			this->gViewer->ZoomFraction = 0.5;
			this->gViewer->ZoomWindowThreshold = 0.05;
			// 
			// GenerateGraphbutton
			// 
			this->GenerateGraphbutton->BackColor = System::Drawing::Color::LawnGreen;
			this->GenerateGraphbutton->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 12, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->GenerateGraphbutton->Location = System::Drawing::Point(629, 93);
			this->GenerateGraphbutton->Name = L"GenerateGraphbutton";
			this->GenerateGraphbutton->Size = System::Drawing::Size(146, 52);
			this->GenerateGraphbutton->TabIndex = 1;
			this->GenerateGraphbutton->Text = L"Generate graph";
			this->GenerateGraphbutton->UseVisualStyleBackColor = false;
			this->GenerateGraphbutton->Click += gcnew System::EventHandler(this, &Form1::GenerateGraphbutton_Click);
			// 
			// CountOfVertecestextBox
			// 
			this->CountOfVertecestextBox->Location = System::Drawing::Point(710, 27);
			this->CountOfVertecestextBox->Name = L"CountOfVertecestextBox";
			this->CountOfVertecestextBox->Size = System::Drawing::Size(100, 20);
			this->CountOfVertecestextBox->TabIndex = 2;
			// 
			// CountOfEdgestextBox
			// 
			this->CountOfEdgestextBox->Location = System::Drawing::Point(710, 57);
			this->CountOfEdgestextBox->Name = L"CountOfEdgestextBox";
			this->CountOfEdgestextBox->Size = System::Drawing::Size(100, 20);
			this->CountOfEdgestextBox->TabIndex = 3;
			// 
			// CountOfVerteceslabel
			// 
			this->CountOfVerteceslabel->AutoSize = true;
			this->CountOfVerteceslabel->BackColor = System::Drawing::Color::LightGray;
			this->CountOfVerteceslabel->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 10, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->CountOfVerteceslabel->ForeColor = System::Drawing::Color::Black;
			this->CountOfVerteceslabel->Location = System::Drawing::Point(568, 24);
			this->CountOfVerteceslabel->Name = L"CountOfVerteceslabel";
			this->CountOfVerteceslabel->Size = System::Drawing::Size(141, 17);
			this->CountOfVerteceslabel->TabIndex = 4;
			this->CountOfVerteceslabel->Text = L"Count of verteces:";
			// 
			// CountOfEdgeslabel
			// 
			this->CountOfEdgeslabel->AutoSize = true;
			this->CountOfEdgeslabel->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 10, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->CountOfEdgeslabel->ForeColor = System::Drawing::Color::Black;
			this->CountOfEdgeslabel->Location = System::Drawing::Point(568, 57);
			this->CountOfEdgeslabel->Name = L"CountOfEdgeslabel";
			this->CountOfEdgeslabel->Size = System::Drawing::Size(123, 17);
			this->CountOfEdgeslabel->TabIndex = 5;
			this->CountOfEdgeslabel->Text = L"Count of edges:";
			// 
			// NextStepbutton
			// 
			this->NextStepbutton->BackColor = System::Drawing::Color::DeepSkyBlue;
			this->NextStepbutton->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 12, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->NextStepbutton->Location = System::Drawing::Point(571, 272);
			this->NextStepbutton->Name = L"NextStepbutton";
			this->NextStepbutton->Size = System::Drawing::Size(118, 43);
			this->NextStepbutton->TabIndex = 6;
			this->NextStepbutton->Text = L"Next step";
			this->NextStepbutton->UseVisualStyleBackColor = false;
			this->NextStepbutton->Click += gcnew System::EventHandler(this, &Form1::NextStepbutton_Click);
			// 
			// DHeapBasedPQradioButton
			// 
			this->DHeapBasedPQradioButton->AutoSize = true;
			this->DHeapBasedPQradioButton->Checked = true;
			this->DHeapBasedPQradioButton->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 9, System::Drawing::FontStyle::Bold, 
				System::Drawing::GraphicsUnit::Point, static_cast<System::Byte>(204)));
			this->DHeapBasedPQradioButton->Location = System::Drawing::Point(571, 161);
			this->DHeapBasedPQradioButton->Name = L"DHeapBasedPQradioButton";
			this->DHeapBasedPQradioButton->Size = System::Drawing::Size(92, 19);
			this->DHeapBasedPQradioButton->TabIndex = 7;
			this->DHeapBasedPQradioButton->TabStop = true;
			this->DHeapBasedPQradioButton->Text = L"DHeap PQ";
			this->DHeapBasedPQradioButton->UseVisualStyleBackColor = true;
			// 
			// BinarySearchTreeBasedPQradioButton
			// 
			this->BinarySearchTreeBasedPQradioButton->AutoSize = true;
			this->BinarySearchTreeBasedPQradioButton->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 9, System::Drawing::FontStyle::Bold, 
				System::Drawing::GraphicsUnit::Point, static_cast<System::Byte>(204)));
			this->BinarySearchTreeBasedPQradioButton->Location = System::Drawing::Point(571, 186);
			this->BinarySearchTreeBasedPQradioButton->Name = L"BinarySearchTreeBasedPQradioButton";
			this->BinarySearchTreeBasedPQradioButton->Size = System::Drawing::Size(164, 19);
			this->BinarySearchTreeBasedPQradioButton->TabIndex = 8;
			this->BinarySearchTreeBasedPQradioButton->Text = L"Binary search tree PQ";
			this->BinarySearchTreeBasedPQradioButton->UseVisualStyleBackColor = true;
			// 
			// ShowFullbutton
			// 
			this->ShowFullbutton->BackColor = System::Drawing::Color::DeepSkyBlue;
			this->ShowFullbutton->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 12, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->ShowFullbutton->Location = System::Drawing::Point(710, 272);
			this->ShowFullbutton->Name = L"ShowFullbutton";
			this->ShowFullbutton->Size = System::Drawing::Size(115, 43);
			this->ShowFullbutton->TabIndex = 9;
			this->ShowFullbutton->Text = L"Show full";
			this->ShowFullbutton->UseVisualStyleBackColor = false;
			this->ShowFullbutton->Click += gcnew System::EventHandler(this, &Form1::ShowFullbutton_Click);
			// 
			// ClearGraphbutton
			// 
			this->ClearGraphbutton->BackColor = System::Drawing::Color::Red;
			this->ClearGraphbutton->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 8, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->ClearGraphbutton->Location = System::Drawing::Point(568, 533);
			this->ClearGraphbutton->Name = L"ClearGraphbutton";
			this->ClearGraphbutton->Size = System::Drawing::Size(84, 29);
			this->ClearGraphbutton->TabIndex = 10;
			this->ClearGraphbutton->Text = L"Clear graph";
			this->ClearGraphbutton->UseVisualStyleBackColor = false;
			this->ClearGraphbutton->Click += gcnew System::EventHandler(this, &Form1::ClearGraphbutton_Click);
			// 
			// TopMenumenuStrip
			// 
			this->TopMenumenuStrip->BackColor = System::Drawing::Color::LightGray;
			this->TopMenumenuStrip->Items->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(1) {this->TopMenutoolStripMenuItem});
			this->TopMenumenuStrip->Location = System::Drawing::Point(0, 0);
			this->TopMenumenuStrip->Name = L"TopMenumenuStrip";
			this->TopMenumenuStrip->RightToLeft = System::Windows::Forms::RightToLeft::Yes;
			this->TopMenumenuStrip->Size = System::Drawing::Size(848, 24);
			this->TopMenumenuStrip->TabIndex = 11;
			this->TopMenumenuStrip->Text = L"menuStrip1";
			// 
			// TopMenutoolStripMenuItem
			// 
			this->TopMenutoolStripMenuItem->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(1) {this->TopMenuAboutToolStripMenuItem});
			this->TopMenutoolStripMenuItem->Font = (gcnew System::Drawing::Font(L"Segoe UI", 9, System::Drawing::FontStyle::Bold));
			this->TopMenutoolStripMenuItem->Name = L"TopMenutoolStripMenuItem";
			this->TopMenutoolStripMenuItem->Size = System::Drawing::Size(24, 20);
			this->TopMenutoolStripMenuItem->Text = L"\?";
			// 
			// TopMenuAboutToolStripMenuItem
			// 
			this->TopMenuAboutToolStripMenuItem->BackColor = System::Drawing::Color::LightGray;
			this->TopMenuAboutToolStripMenuItem->Name = L"TopMenuAboutToolStripMenuItem";
			this->TopMenuAboutToolStripMenuItem->Size = System::Drawing::Size(108, 22);
			this->TopMenuAboutToolStripMenuItem->Text = L"About";
			this->TopMenuAboutToolStripMenuItem->Click += gcnew System::EventHandler(this, &Form1::TopMenuAboutToolStripMenuItem_Click);
			// 
			// AVLTreeBasedPQradioButton
			// 
			this->AVLTreeBasedPQradioButton->AutoSize = true;
			this->AVLTreeBasedPQradioButton->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 9, System::Drawing::FontStyle::Bold, 
				System::Drawing::GraphicsUnit::Point, static_cast<System::Byte>(204)));
			this->AVLTreeBasedPQradioButton->Location = System::Drawing::Point(571, 211);
			this->AVLTreeBasedPQradioButton->Name = L"AVLTreeBasedPQradioButton";
			this->AVLTreeBasedPQradioButton->Size = System::Drawing::Size(101, 19);
			this->AVLTreeBasedPQradioButton->TabIndex = 12;
			this->AVLTreeBasedPQradioButton->TabStop = true;
			this->AVLTreeBasedPQradioButton->Text = L"AVL tree PQ";
			this->AVLTreeBasedPQradioButton->UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			this->textBox1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 8.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->textBox1->Location = System::Drawing::Point(710, 330);
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(115, 20);
			this->textBox1->TabIndex = 13;
			this->textBox1->Text = L" time";
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->BackColor = System::Drawing::Color::LightGray;
			this->ClientSize = System::Drawing::Size(848, 575);
			this->Controls->Add(this->textBox1);
			this->Controls->Add(this->AVLTreeBasedPQradioButton);
			this->Controls->Add(this->ClearGraphbutton);
			this->Controls->Add(this->ShowFullbutton);
			this->Controls->Add(this->BinarySearchTreeBasedPQradioButton);
			this->Controls->Add(this->DHeapBasedPQradioButton);
			this->Controls->Add(this->NextStepbutton);
			this->Controls->Add(this->CountOfEdgeslabel);
			this->Controls->Add(this->CountOfVerteceslabel);
			this->Controls->Add(this->CountOfEdgestextBox);
			this->Controls->Add(this->CountOfVertecestextBox);
			this->Controls->Add(this->GenerateGraphbutton);
			this->Controls->Add(this->gViewer);
			this->Controls->Add(this->TopMenumenuStrip);
			this->Icon = (cli::safe_cast<System::Drawing::Icon^  >(resources->GetObject(L"$this.Icon")));
			this->MainMenuStrip = this->TopMenumenuStrip;
			this->Name = L"Form1";
			this->Text = L"Find the minimal spanning tree in the graph";
			this->Load += gcnew System::EventHandler(this, &Form1::Form1_Load);
			this->TopMenumenuStrip->ResumeLayout(false);
			this->TopMenumenuStrip->PerformLayout();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void GenerateGraphbutton_Click(System::Object^  sender, System::EventArgs^  e) {

				gViewer->Graph = nullptr;
				if (System::String::IsNullOrEmpty(CountOfVertecestextBox->Text) == true
					|| System::String::IsNullOrEmpty(CountOfEdgestextBox->Text) == true) 
				{
					MessageBox::Show("ERROR: no parameters");
					return;
				}
				int numVerteces=Convert::ToInt32(CountOfVertecestextBox->Text); 
				int numEdges=Convert::ToInt32(CountOfEdgestextBox->Text); 
				if (numVerteces > numEdges)
				{
					MessageBox::Show("ERROR: incorrect parameters");
					return;
				}
				if (numEdges > (numVerteces*(numVerteces-1))/2 )
				{
					MessageBox::Show("ERROR: incorrect parameters");
					return;
				}
				mygraph=new KruskalLib::Graph(numVerteces,numEdges);
				mygraph->Generate();
				countOfRedEdges=0;
				MSAGLgraph = gcnew Graph("graph");
				for (int i=1; i<=numVerteces; i++)
				{
					 MSAGLgraph->AddNode(Convert::ToString(i));
					 MSAGLgraph->FindNode(Convert::ToString(i))->Attr->Fillcolor=Microsoft::Glee::Drawing::Color::Lime;
				}

				KruskalLib::WeightedEdge *tmpEdge;
				MSAGLgraphEdges=gcnew array<Edge^>(numEdges);
                for(int i=0; i<numEdges; i++)
				{
					tmpEdge=mygraph->GetEdge(i);
					MSAGLgraphEdges[i]=MSAGLgraph->AddEdge(Convert::ToString(tmpEdge->GetA()), Convert::ToString(tmpEdge->GetWeight())->Remove(5), Convert::ToString(tmpEdge->GetB()));
					MSAGLgraphEdges[i]->EdgeAttr->ArrowHeadAtTarget=Microsoft::Glee::Drawing::ArrowStyle::None; 
					//MSAGLgraphEdges[i]->EdgeAttr->Fontcolor=Microsoft::Glee::Drawing::Color::Green;
				}	
				MSAGLgraph->GraphAttr->Backgroundcolor=Microsoft::Glee::Drawing::Color::LightGray;
				gViewer->Graph = MSAGLgraph;
			 }
	private: System::Void Form1_Load(System::Object^  sender, System::EventArgs^  e) {
			 }
private: System::Void ShowFullbutton_Click(System::Object^  sender, System::EventArgs^  e) {
			 if (mygraph == NULL) 
			 {
				 MessageBox::Show("ERROR: graph doesn't generated");
				 return;
			 }
			KruskalLib::PriorityQueue *queue=NULL;
			if (DHeapBasedPQradioButton->Checked == true)
			{
				queue=new KruskalLib::DHeapBasedPriorityQueue(2,mygraph->GetNumEdges(),mygraph->GetAllEdges());
			}
			if (BinarySearchTreeBasedPQradioButton->Checked == true) 
			{
				queue=new KruskalLib::BSTbasedPQ(mygraph->GetNumEdges(),mygraph->GetAllEdges());
			}
			if (AVLTreeBasedPQradioButton->Checked == true)
			{
				queue=new KruskalLib::AVLTbasedPQ(mygraph->GetNumEdges(),mygraph->GetAllEdges());
			}
			if (queue == NULL) return;
			SpanningTree=new KruskalLib::Graph(mygraph->GetNumVerteces(), mygraph->GetNumVerteces()-1);
			System::Diagnostics::Stopwatch ^time=gcnew System::Diagnostics::Stopwatch();
			time->Start();
			KruskalLib::KruskalImplemetation::GiveMeTree(mygraph,queue,SpanningTree);
			time->Stop();
			textBox1->Text=Convert::ToString(time->Elapsed);
			delete queue;
			KruskalLib::WeightedEdge *tmpEdge1;
			Edge ^tmpEdge2;
			int j=0;
			for (int i=0; i<SpanningTree->GetNumEdges(); i++)
			{
				j=0;
				tmpEdge1=SpanningTree->GetEdge(i);
				do 
				{
					tmpEdge2=MSAGLgraphEdges[j];
					j++;
				} while  (System::String::Compare(tmpEdge2->SourceNode->Attr->Label, Convert::ToString(tmpEdge1->GetA())) != 0 
					||  System::String::Compare(tmpEdge2->TargetNode->Attr->Label, Convert::ToString(tmpEdge1->GetB())) != 0) ;
				tmpEdge2->EdgeAttr->Color=Microsoft::Glee::Drawing::Color::Red;
				tmpEdge2->EdgeAttr->LineWidth=4;
			}
			gViewer->Graph = MSAGLgraph;

		 }

private: System::Void ClearGraphbutton_Click(System::Object^  sender, System::EventArgs^  e) {
			 gViewer->Graph=gcnew Graph("graph");
			 gViewer->Graph->GraphAttr->Backgroundcolor=Microsoft::Glee::Drawing::Color::LightGray;
			 MSAGLgraph=gcnew Graph("graph");
			 delete mygraph; 
			 mygraph=NULL;
			 countOfRedEdges=0;
		 }
private: System::Void NextStepbutton_Click(System::Object^  sender, System::EventArgs^  e) {
			if (mygraph == NULL) 
			{
				MessageBox::Show("ERROR: graph doesn't generated");
				return;
			}
			if (countOfRedEdges==0)
			{
				KruskalLib::PriorityQueue *queue;
				if (DHeapBasedPQradioButton->Checked == true)
				{
					queue=new KruskalLib::DHeapBasedPriorityQueue(2,mygraph->GetNumEdges(),mygraph->GetAllEdges());
				}
				if (BinarySearchTreeBasedPQradioButton->Checked == true) 
				{
					queue=new KruskalLib::BSTbasedPQ(mygraph->GetNumEdges(),mygraph->GetAllEdges());
				}
				if (AVLTreeBasedPQradioButton->Checked == true)
				{
					queue=new KruskalLib::AVLTbasedPQ(mygraph->GetNumEdges(),mygraph->GetAllEdges());
				}
				if (queue == NULL) return;
				SpanningTree=new KruskalLib::Graph(mygraph->GetNumVerteces(), mygraph->GetNumVerteces()-1);
				KruskalLib::KruskalImplemetation::GiveMeTree(mygraph,queue,SpanningTree);
				delete queue;
			}
			if (countOfRedEdges >= SpanningTree->GetNumEdges()) return;
			KruskalLib::WeightedEdge *tmpEdge1;
			Edge ^tmpEdge2;
			int j=0;
			tmpEdge1=SpanningTree->GetEdge(countOfRedEdges);
			do 
			{
				tmpEdge2=MSAGLgraphEdges[j];
				j++;
			} while  (System::String::Compare(tmpEdge2->SourceNode->Attr->Label, Convert::ToString(tmpEdge1->GetA())) != 0 
					||  System::String::Compare(tmpEdge2->TargetNode->Attr->Label, Convert::ToString(tmpEdge1->GetB())) != 0) ;
			tmpEdge2->EdgeAttr->Color=Microsoft::Glee::Drawing::Color::Red;
			tmpEdge2->EdgeAttr->LineWidth=4;
			gViewer->Graph = MSAGLgraph;
			countOfRedEdges++;
		 }
private: System::Void TopMenuAboutToolStripMenuItem_Click(System::Object^  sender, System::EventArgs^  e) {
			 MessageBox::Show("Coded by Savichev Mikhail for UNN, 2013");
		 }
};
}

