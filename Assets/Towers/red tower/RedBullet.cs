using System;
using UnityEngine;

public class RedBullet:Bullet
{
	
	
	public RedBullet()
	{
	}

	#region implemented abstract members of Bullet
	public override void applyEffect (UnityEngine.Collider enemy)
	{
		
		Debug.Log("red bullet hit");
	}
	#endregion
}


