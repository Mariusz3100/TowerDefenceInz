using System;
using System.Collections;

public struct Path
{
//	ArrayList nodes; //ordered list of Fields
	Field pathStart;

	public Field PathStart {
		get {
			return this.pathStart;
		}
		set {
			pathStart = value;
		}
	}
	
	float cost;

	public float Cost {
		get {
			return this.cost;
		}
		set {
			cost = value;
		}
	}	
	public Path(Field destination)
	{
		this.pathStart=destination;
		cost=destination.TotalPathCost;
	}
	
	
}


