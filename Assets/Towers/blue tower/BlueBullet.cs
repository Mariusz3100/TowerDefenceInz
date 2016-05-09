using System;
using UnityEngine;

public class BlueBullet:Bullet
{
	
	
	public BlueBullet ()
	{
	}

	#region implemented abstract members of Bullet
	public override void applyEffect (UnityEngine.Collider enemy)
	{
//		Debug.Log("blue bullethit");
		Enemy e=((Enemy)enemy.GetComponent("Enemy"));
		
		
		SlowingEffect se=new SlowingEffect(e);
		
		e.addEffect(se);
	}
	#endregion
}


