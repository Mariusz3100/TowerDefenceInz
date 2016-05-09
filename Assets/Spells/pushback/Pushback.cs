using System;
using UnityEngine;

public class Pushback:Spell
{
	public PushbackTriangle triangle;
	Vector3 startingPosition;

	
	public Pushback ()
	{
	}
		
		
	public override void applyEffect ()
	{
//		Debug.Log("push spell");

		triangle.Casting=true;
	
	}
	
	public void Awake()
	{
		startingPosition=transform.position;

		
	}
	
	public override void updateAim()
	{
//		base.Update();
//		this.renderer.material.SetTextureOffset("_MainTex",renderer.material.GetTextureOffset("_MainTex")+0.01);
		
//		print (renderer);
		
		alignTargetAtPlayerCube();

		if(Input.GetKeyUp(KeyCode.Space))triangle.Casting=false;
		
		if(triangle.Casting)
		{
			
			Spell.Energy-=cost*Time.deltaTime;
			if(Spell.Energy<=0)
			{
				triangle.Casting=false;
				GameSounds.getGameSounds().noEnergy.Play();
			}
		}
		
		if(Input.GetKey(KeyCode.Z))this.transform.Rotate(0,1,0);
		if(Input.GetKey(KeyCode.C))this.transform.Rotate(0,-1,0);
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
	
}


