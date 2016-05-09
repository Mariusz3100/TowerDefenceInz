using System;
using UnityEngine;


public class AntiTowerEnemy:Enemy
{

	public bool enableMovement=false;
	public float towerDamage;
	
	
	public AntiTowerEnemy ()
	{
	}
	
	
	
	
	void moveTowards (Vector3 target)
	{
	
	
	Vector3 move=-(transform.position-target);
	
	move.y=0;
	Vector3 moveNorm=move.normalized;
	this.constantForce.force=move*Speed*(float)(SpeedMultiplier)
		*Math.Max(0,(MaxSpeed-rigidbody.velocity.magnitude))
		*Math.Max(Time.deltaTime,MaxDeltaTime);
		
	this.constantForce.force+=ForceAddition;


	}
	
	public override Enemy createCopyAt(Vector3 position)
	{
		AntiTowerEnemy ret=(AntiTowerEnemy)(Instantiate(this,position,Quaternion.identity));
		ret.enableMovement=true;
		return ret;
	
	}
	
	public void Update()
	{
		base.Update();
		
		if(enableMovement)
		moveTowards(Player.getPlayer().transform.position);
		
		UpdateEffects ();

	}

	public override void animateDead ()
	{
		base.animateDead();
//		Debug.Log("anti-tower enemy died");
	
	}
/*	
	public void OnCollisionStay(Collision collision)
	{
		base.OnCollisionStay(collision);
 		if(collision.collider.tag==TagNames.tower)
			((Tower)(collision.collider.GetComponent("Tower"))).decreaseLife(damage*Time.deltaTime);
	}
*/
	
}
