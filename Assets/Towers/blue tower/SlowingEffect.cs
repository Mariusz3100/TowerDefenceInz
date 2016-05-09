using System;
using UnityEngine;

public class SlowingEffect:OvertimeEffect
{
	int originalSpeed=-1;
	float slowSpeed;
	Enemy target;
	float timeLeft;
	
	
	public SlowingEffect (Enemy target):base()
	{
		this.target=target;
		timeLeft=6;
	}
	
	

	public override bool updateEffect ()
	{
		target.speedMultiplier=target.speedMultiplier/2;		
		
		timeLeft-=Time.deltaTime;
		if(timeLeft<0)
		{
			target.speedMultiplier=1;
			return false;
		}else{
			return true;
		}
		
		
	
	}
}


