using System;
using UnityEngine;


	public class Player:LivingObject
	{
		
		private static Player singleton;
		
		public static Player getPlayer(){
		
			return singleton;
		
		}
		
	public GameObject healthBar;
	public float healBarMaxMovement;
	private Light playerLight;
	public GameObject playerParticleSystem;
	public int speed=5;
	public int speedMultiplier=400;
	public float maxDeltaTime=0.02f;
	public int maxSpeed=10;
	private bool movementBlocked;	
	
	public void Update()
	{
		if(!movementBlocked)
		hadleMovementInput ();
//		Debug.Log(Life);
		
///		if(Input.GetKey(KeyCode.Y))animation.Play("finish");

		
	}

	public bool MovementBlocked {
		get {
			return this.movementBlocked;
		}
		set {
			movementBlocked = value;
		}
	}	
	
		
	public void stopMovement()
	{
	constantForce.relativeForce=new Vector3(0,0,0);
	}
	
	void hadleMovementInput ()
	{
		
		Vector3 temp=new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
		this.constantForce.relativeForce=temp*speed*(float)(speedMultiplier)
				*Math.Max(0,(maxSpeed-rigidbody.velocity.magnitude))
				*Math.Max(Time.deltaTime,maxDeltaTime);
		
		
	
		
		
		
		
		/*
		
		
			Vector3 temp=this.transform.position;
			
				temp.z+=Input.GetAxisRaw("Vertical")*Time.deltaTime*speed;		
				temp.x+=Input.GetAxisRaw("Horizontal")*Time.deltaTime*speed;

			this.transform.position=temp;
	*/	
		
	}
	
	
	public void Awake()
	{
		singleton=this;

	}
	
	
	public void Start()
	{
		playerLight=playerParticleSystem.light;
		singleton=this;
		Life=maxLife;
	}
	
	
	private Player ()
		{
			singleton=this;
		}

	#region implemented abstract members of LivingObject
	public override void handleDying ()
	{
//		transform.localScale*=0.1f;
		collider.isTrigger=true;
		animation.Play("lost");
		playerParticleSystem.particleSystem.enableEmission=false;
		playerLight.enabled=false;
		movementBlocked=true;
		StateMachine.getStateMachine().GameLost=true;
	}
	#endregion
	
	
	
	public void decreaseLife(float amount)
	{
		base.decreaseLife(amount);		
		
		float colorDecreased=(amount/maxLife)*0.9f;//100;
		playerLight.range-=colorDecreased;
		
		
		healthBar.transform.Translate(0,(-1)*colorDecreased*healBarMaxMovement,0);
		
	}
	
	
	public void playWinAnimation()
	{
		animation.Play("finish");

	}
}

