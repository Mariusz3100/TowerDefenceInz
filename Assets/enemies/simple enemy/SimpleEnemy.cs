using System;
using System.Collections;	
using UnityEngine;


public class SimpleEnemy:Pathfinder
{


	public override Enemy createCopyAt(Vector3 position)
	{
		Pathfinder ret=(Pathfinder)(Instantiate(this,position,Quaternion.identity));
		return ret;
		
	
	}
	
	
	
	public SimpleEnemy ()
		{
//		Bounty=20;
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

//		constantForce.force=move*speed*Time.deltaTime;
	}
	
	
	
	


	
	
	
	public void Update(){
		base.Update();
//		Debug.Log(speed);

		moveTowards(currentTarget);
		UpdateEffects ();
		
		
		if(Input.GetKeyDown(KeyCode.K))
		Instantiate(StateMachine.getStateMachine().beacon,currentTarget,Quaternion.identity);
		
		
//		Debug.Log(constantForce.force.y);
	
	}

	
	
	public override void animateDead ()
	{
		
		base.animateDead();
//		Debug.Log("simple enemy died");
//		this.renderer.material.color=Color.white;
//		Destroy(this.gameObject);
		
		
		
	
	}
	
	
	
	
}


