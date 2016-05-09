using System;
using System.Collections;	
using UnityEngine;


	public abstract class Enemy: LivingObject
	{
	private int deathHeight=-100;
	public int speed= 4;
	public float speedMultiplier=1;
	public float maxLightChange;
	public int maxSpeed=7;
	public float maxDeltaTime=0.1f;
	private Wave createdFor;
	public int bounty;
	public float damage;
	
	
	private bool dead=false;

	public float SpeedMultiplier {
		get {
			return this.speedMultiplier;
		}
		set {
			speedMultiplier = value;
		}
	}
	public bool Dead {
		get {
			return this.dead;
		}
		set {
			dead = value;
		}
	}	
	public Material transparentMaterial;
	
	public Wave CreatedFor {
		get {
			return this.createdFor;
		}
		set {
			createdFor = value;
		}
	}

		public Enemy ()
		{
			
			
			
		}
		
	public void Update(){
		base.Update();
		
		if(transform.position.y<deathHeight){
			Tower.CurrentMoney+=bounty;
				
			Destroy(this.gameObject);
		}
		
		
		if(dead)
			animateDead();
		else 
			base.Update();
		
	
	}
	
	public virtual Enemy createCopyAt(Vector3 position)
	{
		return (Enemy)(Instantiate(this,position,Quaternion.identity));
	}
	
	public float MaxDeltaTime {
		get {
			return this.maxDeltaTime;
		}
		set {
			maxDeltaTime = value;
		}
	}

	public int MaxSpeed {
		get {
			return this.maxSpeed;
		}
		set {
			maxSpeed = value;
		}
	}

	public int Speed {
		get {
			return this.speed;
		}
		set {
			speed = value;
		}
	}


	
	public void decreaseLife(float amount)
	{
		base.decreaseLife(amount);		
		
		float lightDecreased=(amount/maxLife)*1.7f;//100;
//		Color temp=this.renderer.material.color;
//		temp.r-=colorDecreased;
//		temp.b-=colorDecreased;
//		temp.g-=colorDecreased;
//		temp.a-=colorDecreased;
		light.range-=lightDecreased;
		
//		renderer.material.color=temp;
	}
	
	public override void handleDying ()
	{
		dead=true;
//		rigidbody.mass=0.01f;
		//collider.isTrigger=true;
		light.enabled=false;
 		createdFor.NumberOfNotKilledEnemies--;
		this.renderer.material=transparentMaterial;
		this.rigidbody.constraints=RigidbodyConstraints.FreezeAll;
		
		collider.isTrigger=true;
		this.tag=TagNames.deadEnemyTag;
		
		Vector3 moneyPos=this.transform.position;
		moneyPos.y=Player.getPlayer().transform.position.y;
		
		TemplateList.getEnemyTemplateList().moneyTemplate.createMoneyAt(moneyPos,bounty);
		
		
	
	}
	
	
	public virtual void animateDead ()
	{
		Color temp=this.renderer.material.color;
		temp.a-=0.05f;
		transform.localScale*=1.01f;
		renderer.material.color=temp;

		
		if(temp.a<0)Destroy(this.gameObject);
	}
	
	
	public void OnCollisionStay(Collision collision)
	{
		
 		if(collision.collider.tag==TagNames.player)
			((Player)(collision.collider.GetComponent("Player"))).decreaseLife(damage*Time.deltaTime);
	}
	
}

