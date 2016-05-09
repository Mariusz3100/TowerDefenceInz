using System;
using UnityEngine;
using System.Collections;
using System.IO;

	public abstract class Pathfinder:Enemy
	{
	private float pathRecalculatingDistance=10;
	protected Vector3 currentTarget;
	private Vector3 currentAStarTarget;

	private MovementAdviceSystem movementAdviceSystem ;

	public MovementAdviceSystem MovementAdviceSystem {
		get {
			return this.movementAdviceSystem;
		}
		set {
			movementAdviceSystem = value;
		}
	}
	
	public Pathfinder ()
	{
	
	}
	
	public void Update(){
		base.Update();
		
		if(checkIfPathIsEmpty())
		{
			currentTarget=Player.getPlayer().transform.position;
//			Debug.Log("straight Line");
		}
		else
		{
			if(Vector3.Distance(this.transform.position,currentAStarTarget)<pathRecalculatingDistance)
				findPath();
			currentTarget=currentAStarTarget;
		
		
		
		
		}
//		
//		if(Input.GetKey(KeyCode.C))
//		{
//			movementAdviceSystem.updatePath();
//			Instantiate(GameScene.getGameScene().redBeacon,currentTarget,Quaternion.identity);	
//			
//		}
//		
//		
		if(Input.GetKey(KeyCode.G))
		{
			movementAdviceSystem.updatePath();
			findAndMarkPath();
			
		}
		
		if(Input.GetKey(KeyCode.K))
		{
			RaycastToLinecast(transform.position,Player.getPlayer().transform.position);
			
		}
		
		
	
	}

	public void Start()
	{
		base.Start();
		GameScene.getGameScene().registerGameTableUser(this);
		currentAStarTarget=Player.getPlayer().transform.position;
	}
	
	public void findPath()
	{
			Player target=Player.getPlayer();
			Vector2 coordinates=movementAdviceSystem.getTableCoordinates(transform.position);
			Field field=movementAdviceSystem.getFieldAt(transform.position);
			Field lastKnownClearilyArrivable=field,fieldChecked=field.next;
			
			while(true)
			{
				Vector3 temp= this.transform.position;
				if(fieldChecked==null)break;
		
				if(!checkIfPathToPointIsEmpty(fieldChecked.coordinates))
				{
					break;    //obstacle found on the way, thus, we can't simplify path any longer...	
				}
				lastKnownClearilyArrivable=fieldChecked;
				fieldChecked=fieldChecked.next;
				
			}
			currentAStarTarget=lastKnownClearilyArrivable.coordinates;
	}
	
	
	private RaycastHit[] RaycastToLinecast(Vector3 start, Vector3 end)
		{
			RaycastHit[] ret;
			Vector3 relativeVector=end-start;
			
			ret=Physics.RaycastAll(start,relativeVector,relativeVector.magnitude
			/*LayerMask.NameToLayer(LayerNames.CollisionIgnoreLayers.range)*/);
/*			if(ret.Length==0)
//				Debug.Log("no colliders found");
		
//			else
//			{
				Debug.Log(ret.Length);
				for(int i=0;i<ret.Length;i++)
					Debug.Log(" "+ret[i].collider.tag);
			}
*/
		return ret;
			
		}
			
	public void findAndMarkPath()
	{
		
		
			Player target=Player.getPlayer();
			Vector2 coordinates=movementAdviceSystem.getTableCoordinates(transform.position);
			Field field=movementAdviceSystem.getFieldAt(transform.position);
			Field lastKnownClearilyArrivable=field,fieldChecked=field.next;
			
			while(fieldChecked!=null)
			{
				Instantiate(GameScene.getGameScene().beacon,fieldChecked.coordinates,Quaternion.identity);

				fieldChecked=fieldChecked.next;
				
			
			}
			
			
		
	}
	
	
	public void handleNonGuidedField(Field fieldWithoutPathSpecified)
	{
		Debug.Log("Non-guided field found");
		
	
	}
	
	

	
	bool checkIfPathToPointIsEmpty (Vector3 destination)
	{
		RaycastHit[] hits=RaycastToLinecast(this.transform.position,destination);
		
		foreach(RaycastHit x in hits)
		{
			if(x.collider.tag.StartsWith(TagNames.ignoreCollisionTagStart)
				||
				x.collider.gameObject==Player.getPlayer().gameObject)continue;
			
			else
			{
//				Debug.Log("break caused by:"+x.collider.tag);
				return false;
			}
			
		}
		
//		Debug.Log("no breaking");
/*		
		inputC=inputC||Input.GetKey(KeyCode.C);
		if(inputC)
		{
			inputC=false;
			movementAdviceSystem.updatePath();
			Vector3 delta=transform.position-destination;
			for(int i=0;i<100;i++)
			Instantiate(GameScene.getGameScene().redBeacon,transform.position+i*0.1f*delta,Quaternion.identity);	
			
		}
*/
		return true;
		
	}
	
	bool checkIfPathIsEmpty ()
	{
		Player target=Player.getPlayer();
		bool straightPath=false;
		//check if there are colliders between centers of this and target:
		straightPath= !lineCastExcludingThisAndTargetObjects(transform.position,target.transform.position);
		//check for colliders on the sides of the path
		Vector3 difference=target.transform.position-transform.position;
		
		difference.y=0;
		difference.Normalize();
		//just switch x and z... and add minus to one
		Vector3 orthoToDifference=new Vector3(difference.z,0,difference.x*(-1));
		
		Vector3 tempStart=transform.position+orthoToDifference*((SphereCollider)collider).radius*transform.lossyScale.y;
		Vector3 tempTarget=target.transform.position+orthoToDifference*((SphereCollider)collider).radius*transform.lossyScale.y;
		
		bool straightPath2=!lineCastExcludingThisAndTargetObjects(tempStart,tempTarget);
				
		orthoToDifference=new Vector3(difference.z*(-1),0,difference.x);
		
		tempStart=transform.position+orthoToDifference*((SphereCollider)collider).radius*transform.lossyScale.y;
		tempTarget=target.transform.position+orthoToDifference*((SphereCollider)collider).radius*transform.lossyScale.y;
		
		bool straightPath3=!lineCastExcludingThisAndTargetObjects(tempStart,tempTarget);
		
		
		
		return straightPath&&straightPath2&&straightPath3;
	}
	

	
	bool lineCastExcludingThisAndTargetObjects (Vector3 start, Vector3 destination)
	{
		if(Physics.Linecast(start,destination/*,LayerMask.NameToLayer(LayerNames.CollisionIgnoreLayers.range)*/))
			{
			
			foreach(RaycastHit info in RaycastToLinecast(start,destination))
			{
				if(
						!info.collider.Equals(Player.getPlayer().collider)
						&&!info.collider.Equals(this.collider)
						&&!info.collider.tag.StartsWith(TagNames.ignoreCollisionTagStart)
					){
					return true;
					
			
			
				}
			}
			
			return false;
		
		}else{
			
		return false;
			
		}
		
		
//		return straightPathObstructed;
	}
	
	
	
	void moveTowards (Vector3 target)
	{
		
		
		Vector3 move=-(transform.position-target);
		
		move.y=0;
		Vector3 moveNorm=move.normalized;
		this.constantForce.force=move*Speed*(float)(SpeedMultiplier)
			*Math.Max(0,(MaxSpeed-rigidbody.velocity.magnitude))
			*Math.Max(Time.deltaTime,MaxDeltaTime);

//		constantForce.force=move*speed*Time.deltaTime;
	}
	
	
	
	public override void handleDying ()
	{
		MovementAdviceSystem.removeGameTableUser(this);
		base.handleDying();
		
	}
}