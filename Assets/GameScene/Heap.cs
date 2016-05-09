using System;
using UnityEngine;
using System.Collections;




public class Heap
{
	Field[] table; //elements start at index 1.

	public Field[] Table {
		get {
			return this.table;
		}
		set {
			table = value;
		}
	}

	private int heapSize=0;
	private ArrayList assessment=new ArrayList();
	
	public Heap (int initSize)
	{
		table=new Field[initSize];
	}

	public int HeapSize {
		get {
			return this.heapSize;
		}
	}	
	public void addElement(Field f)
	{
		
		if(this.table.Length<heapSize+2)extedArray();
		++heapSize;
		table[heapSize]=f;
		table[heapSize].PositionInOpenSet=heapSize;
		upHeap(heapSize);
	}
	
	
	public void upHeapFieldWithDecreasedWeight(Field f)
	{
		upHeap(f.PositionInOpenSet);
	}
			
	private void extedArray()
	{
		Field[] newTable=new Field[table.Length*2];
		
		for(int i=0;i<table.Length;i++)
			newTable[i]=table[i];
		
		table=newTable;
	}
	
	
	public void upHeap(int index)
	{
		while(index>1&&table[getParentOf(index)].TotalPathCost>table[index].TotalPathCost)
			{
				exchange(index,getParentOf(index));	
				index=getParentOf(index);
			}
	}
	
	public void exchange(int i1, int i2)
	{
		Field temp=table[i1];
		table[i1]=table[i2];
		table[i2]=temp;
		table[i2].PositionInOpenSet=i2;
		table[i1].PositionInOpenSet=i1;
	}
	public int getParentOf(int index)
	{
		if(index==1)return -1;
		return index/2;
	}
	
	public int getLeftChildOf(int index)
	{
		int ret=index*2;
		if(ret>heapSize||ret<1)return -1;
			else return ret;
		
	}
	
	
	public int getRightChildOf(int index)
	{
		int ret=1+index*2;
		if(ret>heapSize||ret<1)return -1;
			else return ret;
	}
	
	public  String toString()
	{
		String ret="";
		
		for(int i=1;i<heapSize+1;i++)ret+=table[i].TotalPathCost.ToString()+" ";
		
		return ret;
	}
	
	
	
	public void downHeap(int index)
	{
		if(table[index]==null)return;
		
		int indexOfSmallest=index;
		
		
		if(getRightChildOf(index)!=-1&&table[getRightChildOf(index)]!=null)
			if(table[getRightChildOf(index)].TotalPathCost<table[index].TotalPathCost)indexOfSmallest=getRightChildOf(index);
		
		if(getLeftChildOf(index)!=-1&&table[getLeftChildOf(index)]!=null)
			if(table[getLeftChildOf(index)].TotalPathCost<table[indexOfSmallest].TotalPathCost)indexOfSmallest=getLeftChildOf(index);
		
		
		
		
		if(indexOfSmallest!=index)
		{
			exchange(indexOfSmallest,index);
			downHeap(indexOfSmallest);
		
		
		}
					
	}
	
	
	public Field removeMin()
	{
//		assessment.Add(openSet.HeapSize);

		Field ret=table[1];
		table[1]=table[heapSize];
		table[1].PositionInOpenSet=1;
		table[heapSize]=null;
		downHeap(1);
		
		--heapSize;
		
		
		return ret;	
	}
	
	public String lispToString(int index)
	{
		if(index>heapSize||table[index]==null)return "null";
		String ret="";
		
		ret+="{"+lispToString(getLeftChildOf(index))+"+"+table[index].TotalPathCost.ToString()+"+"+lispToString(getRightChildOf(index))+"}";
		
		
		return ret;
	}
	
	
	public bool hasMoreElements()
	{
		return heapSize>0;
	}
	
	
/*	
	public String assess()
	{
		String ret="";
		for(int i=0;i<assessment
		
	}
		
		 */ 
}


