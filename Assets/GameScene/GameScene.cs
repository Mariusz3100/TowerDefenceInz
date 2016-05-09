using UnityEngine;
using System;
using System.Collections;
using System.IO;
public class GameScene:MonoBehaviour
{
	private static GameScene singleton;

	public Transform X_min_Border;
	public Transform X_max_Border;
	public Transform Z_min_Border;
	public Transform Z_max_Border;
	
	private Pathfinder[] gameTableUsers;
	private int gameTableUsersCount;
	private MovementAdviceSystem[] storedSystems;
	private int storedSystemsCount;
	private bool noAStarPerformedThisFrame;
	
	
	public float pathfindingHeight;
	public GameObject beacon;
	public GameObject redBeacon;

	
	public bool permissionToCalculatePaths()
	{
		if(noAStarPerformedThisFrame){
			noAStarPerformedThisFrame=false;
			return true;
		}
		else
		return noAStarPerformedThisFrame;
	}
	
	public static Rect gameScene;
	
	

	private GameScene ()
	{

	}
	
	public static GameScene getGameScene()
	{
		return singleton;
	}
	
	
	public void registerGameTableUser(Pathfinder pf)
	{
		bool newTypeOfObjectFound=true;
		float pfRadius=((SphereCollider)pf.collider).radius*pf.transform.localScale.y;

		for(int i=0;i<storedSystemsCount;i++)
		{
			if(storedSystems[i].GuidedObjectRadius==pfRadius)
			{
				storedSystems[i].registerGameTableUser(pf);
				newTypeOfObjectFound=false;
			}
			
		}
		
		if(newTypeOfObjectFound)
			{
				checkAdviceSystemTableSize();
				
				storedSystems[storedSystemsCount]=new MovementAdviceSystem(pfRadius,gameScene,pathfindingHeight);
				++storedSystemsCount;
			
				storedSystems[storedSystemsCount-1].registerGameTableUser(pf);

				
			}
		
		
	}
	
	private void checkAdviceSystemTableSize()
	{
		if(this.storedSystemsCount>=storedSystems.Length)extendAdviceSystemTable();
	}
	
	private void extendAdviceSystemTable()
	{
		MovementAdviceSystem[] newTable=new MovementAdviceSystem[storedSystems.Length*2];
		
		for(int i=0;i<storedSystems.Length;i++)
			newTable[i]=storedSystems[i];
		
		storedSystems=newTable;
	}
	
	public void Awake()
	{
		gameScene=new Rect(X_min_Border.position.x,Z_min_Border.position.z,
				X_max_Border.position.x-X_min_Border.position.x,Z_max_Border.position.z-Z_min_Border.position.z);
//		gameTableUsers=new ArrayList(1);
		singleton=this;
		storedSystems=new MovementAdviceSystem[2];

		File.WriteAllText("output.csv","");

	}


	
	public void Start()
	{
		
		
	}
	
	


	
	/*
	
	public static RaycastHit myLineCast(Vector3 start, Vector3 target)
	{
		RaycastHit ret=new RaycastHit();
		int 
	}

	
	*/
	public void Update()
	{
		noAStarPerformedThisFrame=true;
		foreach(MovementAdviceSystem x in storedSystems)
			if(x!=null)
			x.Update();
			
	
	}
	
	
}

