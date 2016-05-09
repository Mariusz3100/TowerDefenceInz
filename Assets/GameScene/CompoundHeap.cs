using System;
using UnityEngine;
using System.Collections;

public class CompoundHeap
{

	
	private Heap primaryHeap;
	private Heap secondaryHeap;
	private int compoundHeapSize=0;
	
	public CompoundHeap (int initSize)
	{
		primaryHeap=new Heap(initSize);
		secondaryHeap=new Heap(initSize);
	}

	public int HeapSize {
		get {
			return this.compoundHeapSize;
		}
	}	
	public void safeAddElement(Field f)
	{
		
		
		if(f.TotalPathCost<PathfinderWeights.staticWeight)
			{
				if(f.PositionInOpenSet>0)
					primaryHeap.upHeapFieldWithDecreasedWeight(f);
			else{
				primaryHeap.addElement(f);
				++compoundHeapSize;

				}
			}
		else
			{
				if(f.PositionInOpenSet>0)
					secondaryHeap.upHeapFieldWithDecreasedWeight(f);
			else{
				++compoundHeapSize;
				secondaryHeap.addElement(f);
				}
				
			}
		
	}
	
	
	public bool hasMoreElements()
	{
		return compoundHeapSize>0;
	}
	
	public Field removeMin()
	{
		--compoundHeapSize;
		if(primaryHeap.hasMoreElements())
		return primaryHeap.removeMin();
		
		if(secondaryHeap.hasMoreElements())
		return secondaryHeap.removeMin();
		
		
		
		return null;	
	}
	
	public void checkHeap()
	{
					Debug.Log("checking...");

		
		for(int i=0;primaryHeap.Table[i]!=null;i++)
		{
			Field x=primaryHeap.Table[i];
			for(int j=i;primaryHeap.Table[j]!=null;j++)
			{
				if(x==primaryHeap.Table[j])Debug.Log("woooops!");
			}
		}
	}
	
}


