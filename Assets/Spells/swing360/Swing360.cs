using System;
using UnityEngine;

public class Swing360:Spell
{
	
	Vector3 startingPosition;
	public GameObject circle;
	public float rotationSpeed;
	
	public ParticleSystem[] psArray;
	
	private float remaingingSpin=0;
	private float fullSpin=380;
	public float damage;

	private bool casting=false;
	private byte firstFrameOfCasting=0;

	
	public Swing360 ()
	{
		
	}
	
	
	public void Awake()
	{
		
		startingPosition=transform.position;

		
		for(int i=0;i<psArray.Length;i++)
		{
			psArray[i].enableEmission=false;
		}
	}
	
	
	void alignTargetAtPlayerCube ()
	{
		Vector3 temp=Player.getPlayer().transform.position;
		temp.y=transform.position.y;
		transform.position=temp;
		
		
	}
	
	
	

		
	public void OnTriggerStay(Collider other)
	{
		if(firstFrameOfCasting>0){
			if(other.tag==TagNames.enemyTag)
			{
				((Enemy)other.GetComponent("Enemy")).decreaseLife(damage);
			
			}
//			Debug.Log(other.tag);
		}
	}
	
	public override void applyEffect ()
	{
		
		
		
		
		if(Spell.Energy>cost)
		{
		firstFrameOfCasting=2;
		
		Spell.Energy-=cost;
		
//		Debug.Log("fire spell");
		remaingingSpin=fullSpin;
		for(int i=0;i<psArray.Length;i++)
		{
			psArray[i].enableEmission=true;
		}

		circle.renderer.enabled=false;
	
		}else{
			GameSounds.getGameSounds().noEnergy.Play();
		}
		
	
	}

	public override void updateAim ()
	{
		rotate();
		alignTargetAtPlayerCube ();
	}
	
	
	
	public override void Update ()
	{
		
		base.Update();
		
		if(firstFrameOfCasting>0)
			firstFrameOfCasting-=1;
//		
//		
//		if(firstFrameOfCasting)firstFrameOfCasting=false;
//		
//		if(casting&&!firstFrameOfCasting)
//			firstFrameOfCasting=true;
//		
//		
		if(remaingingSpin>=0){
			float deltaTime=Time.deltaTime;
			remaingingSpin+=(-10)*this.rotationSpeed*deltaTime;
			this.transform.Rotate(0,(-10)*this.rotationSpeed*deltaTime,0);
	
			if(remaingingSpin<=0)
			{
				
				for(int i=0;i<psArray.Length;i++)
				{
					psArray[i].enableEmission=false;
				}
				circle.renderer.enabled=true;
			}
			
		}
		
		

		
	}
	
	public override void activate()
	{
		base.activate();
		alignTargetAtPlayerCube ();
	}
	
	public override void deactivate()
	{
		base.deactivate();
		transform.position=startingPosition;
	}
	
	private void rotate()
	{
		if(remaingingSpin<=0)
			this.transform.Rotate(0,this.rotationSpeed*Time.deltaTime,0);
	}
	
	
}


