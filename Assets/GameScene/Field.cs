using System;
using UnityEngine;


	public class Field
	{
		public Vector3 coordinates;
		public Field next;
		public float fieldCost;
		public float predictedLeftCost;
		private float totalPathCost;
		public int xIndex;
		public int zIndex;
		private int positionInOpenSet;
		
	public float TotalPathCost {
		get {
			readCounter++;
			return this.totalPathCost;
		}
		set {
			writeCounter++;
			totalPathCost = value;
		}
	}

	public int PositionInOpenSet {
		get {
			return this.positionInOpenSet;
		}
		set {
			positionInOpenSet = value;
		}
	}	
	
	public Vector3 Coordinates {
		get {
			return this.coordinates;
		}
		set {
			coordinates = value;
		}
	}

	
	public float FieldCost {
		get {
			return this.fieldCost;
		}
		set {
			fieldCost = value;
		}
	}

	public float PredictedLeftCost {
		get {
			return this.predictedLeftCost;
		}
		set {
			predictedLeftCost = value;
		}
	}

	public Field Previous {
		get {
			return this.next;
		}
		set {
			next = value;
		}
	}
	
	public static void beginAssessment()
	{
		writeCounter=0;
		readCounter=0;
	}
	
	public static String endAssessment()
	{
		return writeCounter+" times written and "+readCounter+" times read.";
	}
		
	private static int writeCounter=0;
	private static int readCounter=0;
	

	public int XIndex {
		get {
			return this.xIndex;
		}
		set {
			xIndex = value;
		}
	}

	public int ZIndex {
		get {
			return this.zIndex;
		}
		set {
			zIndex = value;
		}
	}
		private float sumOfCosts;
		
		private Collider occupiedBy;

	public Collider OccupiedBy {
		get {
			return this.occupiedBy;
		}
		set {
			occupiedBy = value;
		}
	}

	public float SumOfCosts {
		get {
			sumOfCosts=Math.Min(TotalPathCost+predictedLeftCost,float.MaxValue);
				
			return this.sumOfCosts;
		}
		set {
			sumOfCosts = value;
		}
	}	
	public Field(Vector3 coordinates, Field previous,float predictedLeftCost,
		float fieldCost , float TotalPathCost)
	{
		this.coordinates=coordinates;
		this.next=previous;
		this.fieldCost=fieldCost;
		this.predictedLeftCost=predictedLeftCost;
		this.TotalPathCost=TotalPathCost;
		this.PositionInOpenSet=-1;
	
	}
	
	public Field()
	{
		this.PositionInOpenSet=-1;
	}
		
	}


