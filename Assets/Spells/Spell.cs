using System;
using UnityEngine;


public abstract class Spell:MonoBehaviour
{
	private static float energy;

	public static float Energy {
		get {
			return energy;
		}
		set {
			energy = value;
		}
	}	
	
	public Spell ()
	{
	}
	public float cost;
	public abstract void applyEffect();
	public abstract void updateAim();
	
	
	bool active;
	
	public virtual void activate()
	{
  		active=true;
	}
	
	public virtual void deactivate()
	{
		active=false;
	}
	
	public virtual void Update()
	{
		if(active)
		{
			updateAim();
		}
	}
}


