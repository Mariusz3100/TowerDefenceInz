using System;
using UnityEngine;

public class GreyBullet:Bullet
{
	public float damage;
	public GreyBullet ()
	{
	}
	
	
	
	
	#region implemented abstract members of Bullet
	public override void applyEffect (Collider enemy)
	{
		Enemy enemyHit=(Enemy)enemy.gameObject.GetComponent("Enemy");
		enemyHit.decreaseLife(damage);
		
		
//		Debug.Log("grey hit");
	
	}
	#endregion
}


