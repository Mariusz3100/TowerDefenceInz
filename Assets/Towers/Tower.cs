using System;
using UnityEngine;
using System.Collections;


public class Tower:LivingObject
{
	public int topRotation=4;
	public GameObject range;
	private static bool allRangesAreVisible=false;
	private bool rangeIsVisible=true;
	public Bullet bulletTemplate;
	private float lightRangeDifference;
	private float minimalLightRange=1;
	protected float shootingCooldown=0;
	public float baseShootingCooldown=0.5f;
	public int cost;
	
	private static int currentMoney;

	public static int CurrentMoney {
		get {
			return currentMoney;
		}
		set {
			currentMoney = value;
		}
	}
	
	public Tower ():base()
	{
	}

	public static bool AllRangesAreVisible {
		get {
			return allRangesAreVisible;
		}
		set {
			allRangesAreVisible = value;
		}
	}
	
	public static void toggleRangeVisibility()
	{
		AllRangesAreVisible=!AllRangesAreVisible;
	}
	public bool RangeIsVisible {
		get {
			return this.rangeIsVisible;
		}
		set {
			rangeIsVisible = value;
				
	
		}
	}	
	
	public void Start()
	{
//		range.renderer.enabled=false;
		base.Start();
		lightRangeDifference=this.transform.Find("top").light.range-minimalLightRange;
		
		rangeIsVisible=allRangesAreVisible;
		
		if(allRangesAreVisible)
					displayRange();
				else
					hideRange();
//		currentMoney=GeneralParameters.startingGold;
	}
	
	public void Update()
	{
	rotateTop ();
		
	adjustLightTop ();	
		
	UpdateShooting();
		
		
		checkRangeVisibility ();
		
		
//		checkCasting();
	}
	
/*	
	private void checkCasting()
	{
		float r=((MeshRenderer)range.renderer).bounds.extents.y;

		
		if(Input.GetKey(KeyCode.V))
		{
			foreach(Collider x in Physics.OverlapSphere(transform.position,r))
				if(x.tag=="D Enemy")
				Debug.Log(x.tag);
		}
	}
	 
*/
	
	void checkRangeVisibility ()
	{
		if(rangeIsVisible!=allRangesAreVisible)
		{
			rangeIsVisible=allRangesAreVisible;
			if(allRangesAreVisible)
					displayRange();
				else
					hideRange();
		}
	}
	

	
	public virtual void UpdateShooting()
	{
		float r=((MeshRenderer)range.renderer).bounds.extents.y;
		
		Enemy closest=getClosestEnemy (r);
		
		shootingCooldown-=Time.deltaTime;
		
		if(closest!=null&&shootingCooldown<0)
		{
			shootingCooldown=baseShootingCooldown;
			
			Bullet newBullet=(Bullet)Instantiate(bulletTemplate,transform.position,Quaternion.identity);
			newBullet.Target=closest;
		}
		

		
	}

	Enemy getClosestEnemy (float r)
	{
		Vector3 thisPosition=transform.position;
		Enemy ret=null;
		int enemyLayer=~LayerMask.NameToLayer(LayerNames.enemyLayer);
		
		foreach(Collider other in Physics.OverlapSphere(transform.position,r,enemyLayer))
		{
			
			if(other.tag==TagNames.enemyTag){
				Enemy x=(Enemy)other.gameObject.GetComponent("Enemy");
				
				if(ret==null||
					MovementAdviceSystem.sqDist(transform.position,x.transform.position)<MovementAdviceSystem.sqDist(transform.position,ret.transform.position))
					ret=x;
			}
		}
		
//		if(ret!=null)Debug.Log(ret.tag);

		return ret;
	}
	
	
	
	void rotateTop ()
	{
		this.transform.Find("top").transform.Rotate(new Vector3(0,0,2));
	}
	
	void adjustLightTop ()
	{
		GameObject top=this.transform.Find("top").gameObject;
		
		top.light.range=lightRangeDifference*(Life/maxLife)+minimalLightRange;
	}
	
	public void displayRange()
	{
		range.renderer.enabled=true;
	}
	
	
	public void hideRange()
	{
		range.renderer.enabled=false;
	}
	
	#region implemented abstract members of LivingObject
	public override void handleDying ()
	{
//		this.transform.Find("top").localScale*=0/2;
		
		Destroy(this.gameObject);
		
	}
	#endregion
	
	
	
	public void OnCollisionStay(Collision collision)
	{
 		if(collision.collider.tag==TagNames.enemyTag)
		{
			try{
				AntiTowerEnemy e=(AntiTowerEnemy)(collision.collider.GetComponent("Enemy"));
			
				decreaseLife(e.towerDamage*Time.deltaTime);
			}catch(InvalidCastException e){}
				

			
		}
	}
	
	
}


