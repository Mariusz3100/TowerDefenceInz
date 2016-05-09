using System;
using UnityEngine;

public class Sniper:Spell
{
	Vector3 startingPosition;
	public float speed;
	public SniperShot bulletObject;
	public float damage;
	private float currentFlightPhase=0;
	private bool shotPerformed=false;
	
	public Sniper ()
	{
	}
	void Start()
	{
		startingPosition=transform.position;
	}
	
	public override void applyEffect ()
	{
		if(Spell.Energy>cost)
		{
		Spell.Energy-=cost;	
			
		shotPerformed=true;
		bulletObject.createShotAt(transform.position,damage);
		
		}else{
			GameSounds.getGameSounds().noEnergy.Play();
		}
		
		
//		Debug.Log("sniper spell");
	}

	public override void updateAim ()
	{
		
		
		
		
		if(Input.GetKey(KeyCode.Z))
		{
			Player.getPlayer().MovementBlocked=true;
			Player.getPlayer().stopMovement();
			
			Vector3 temp=new Vector3(transform.position.x+Input.GetAxisRaw("Horizontal") * speed,
				transform.position.y,
				transform.position.z+Input.GetAxisRaw ("Vertical") * speed);
			transform.position=temp;
		
		}else{
			alignTargetAtPlayerCube();

		}
		
		if(Input.GetKeyUp(KeyCode.Z)){
			Player.getPlayer().MovementBlocked=false;
		}
	}
	
	public override void activate()
	{
		base.activate();
		alignTargetAtPlayerCube ();
	}

	void alignTargetAtPlayerCube ()
	{
		Vector3 temp=Player.getPlayer().transform.position;
		temp.y=transform.position.y;
		transform.position=temp;

	}
	
	public override void deactivate()
	{
		base.deactivate();
		transform.position=startingPosition;
	}
	
	public void updateEffect()
	{
		
	}
	
}