using System;
using UnityEngine;

public class PushbackEffect:OvertimeEffect
{
	Vector3 forceShift;
	Enemy enemy;
	private float timeLeft;
	public PushbackEffect (Enemy target, Vector3 forceShift)
	{
		enemy=target;
		this.forceShift=forceShift;
		timeLeft=0.1f;
	}
	
	
	
	
	
	
	
	public override bool updateEffect ()
	{
		enemy.ForceAddition+=forceShift;
		
		timeLeft-=Time.deltaTime;
		if(timeLeft<0)
		{
			enemy.ForceAddition=Vector3.zero;//-=forceShift;
			return false;

		}
		else return true;
	}
}


