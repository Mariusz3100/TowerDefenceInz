using UnityEngine;
using System;
using System.Collections;
using System.IO;
public class MovementAdviceSystem
	{
	private float guidedObjectRadius;
	
	private float pathfindingHeight;
	
	private ArrayList gameTableUsers;
	
	public static Rect gameBounds;
	
	
	
	String output="";
	Field[][] gameTable;
	Field[][] newGameTable;

	CompoundHeap openSet;
	ArrayList closedSet;
	private float step=0.4f;
	MonoBehaviour target;
	float cooldown=0;
	float totalCooldown=10;
	bool preparingNewGameTable=false;
	
	
	
	
	public float GuidedObjectRadius {
		get {
			return this.guidedObjectRadius;
		}
		set {
			guidedObjectRadius = value;
		}
	}

	public float PathfindingHeight {
		get {
			return this.pathfindingHeight;
		}
		set {
			pathfindingHeight = value;
		}
	}
	public MovementAdviceSystem (float radius,Rect environmentBounds, float heightOfPathfinding)
	{
		guidedObjectRadius=radius;
		gameBounds=environmentBounds;
		pathfindingHeight=heightOfPathfinding;
		gameTableUsers=new ArrayList(10);
		target=Player.getPlayer();
//		target.StartCoroutine(processMap());
		initializeSets();
//		gameTableUsersCount=0;
	}
	
	
	
	public void registerGameTableUser(Pathfinder pf)
	{
//		checkGameTableUsersTableSize();
		gameTableUsers.Add(pf);
//		gameTableUsersCount++;
		pf.MovementAdviceSystem=this;
	}
		
	public void removeGameTableUser(Pathfinder pf)
	{
		gameTableUsers.Remove(pf);
//		pf.MovementAdviceSystem=null;
	}
	
	public Field getFieldAt(Vector3 coordinates)
	{
//		try{
			return gameTable
			[(int)Math.Round((coordinates.z-MovementAdviceSystem.gameBounds.y)/step)]
			[(int)Math.Round((coordinates.x-MovementAdviceSystem.gameBounds.x)/step)];
/*		}catch(IndexOutOfRangeException e)
		{
			Debug.Log("except");
			
			for(int i=0;i<500;i++)Debug.Log("xxx");
			return null;
		}
*/
		}
	
		
		
	public Field getFieldAtTableIndexes(int x,int y)
	{
		return gameTable
			[y]
			[x];
	}
	
	
	
/*	
	private void checkGameTableUsersTableSize()
	{
		if(this.gameTableUsersCount>=gameTableUsers.Count)extendGameTableUsersTable();
	}

/*	
	private void extendGameTableUsersTable()
	{
		Pathfinder[] newTable=new Pathfinder[gameTableUsers.Count*2];
		
		for(int i=0;i<gameTableUsers.Count;i++)
			newTable[i]=gameTableUsers[i];
		
		gameTableUsers=newTable;
	}
*/	
	 void initializeSets ()
	{
		openSet=new CompoundHeap(50);
		closedSet=new ArrayList((int)gameBounds.width);
	}
	
	
	void setUpSingleField (int i, int j)
	{
		Vector3 vector3 = new Vector3 (MovementAdviceSystem.gameBounds.x+j* step,PathfindingHeight,MovementAdviceSystem.gameBounds.y+i*step);
		
		
			Field temp=new Field(vector3,null,PathfinderWeights.predictionWeight*
			calculateManhattanDistance(vector3,target.transform.position)/step,
				0,float.MaxValue);
			temp.xIndex=j;
			temp.zIndex=i;
		
			
		setFieldCost (vector3, temp, guidedObjectRadius);
		
//		Debug.Log(temp.fieldCost);
		newGameTable[i][j]=temp;
	}

	void setFieldCost (Vector3 vector3, Field temp, float r)
	{
		temp.fieldCost=PathfinderWeights.emptyFieldWeight;

		foreach(Collider other in Physics.OverlapSphere(
			vector3,r/*,LayerMask.NameToLayer(LayerNames.CollisionIgnoreLayers.range)*/))
		{
			
		if(other.gameObject.tag.StartsWith("S "))temp.fieldCost=PathfinderWeights.staticWeight;
			else
			if(other.gameObject.tag.StartsWith("D ")&&
					temp.fieldCost!=PathfinderWeights.staticWeight&&
					other.collider!=target.collider){
						temp.fieldCost=PathfinderWeights.dynamicWeight;
						temp.OccupiedBy=other;
			}
				 
			if(other.gameObject.tag=="S Border")
				temp.fieldCost=PathfinderWeights.borderWeight;
			
		}
	}

	
	public Vector2 getTableCoordinates(Vector3 globalCoordinates)
	{
		Vector2 ret=new Vector2(
			(int)Math.Round((globalCoordinates.x-MovementAdviceSystem.gameBounds.x)/step),
			(int)Math.Round((globalCoordinates.z-MovementAdviceSystem.gameBounds.y)/step));
		return ret;

	}
	
	void setUpGoalField ()
	{
		Field goalField=newGameTable[(int)Math.Round((target.transform.position.z-MovementAdviceSystem.gameBounds.y)/step)][(int)Math.Round((target.transform.position.x-MovementAdviceSystem.gameBounds.x)/step)];
		goalField.fieldCost=PathfinderWeights.goalPointCost;
		goalField.TotalPathCost=goalField.fieldCost;
		
		
		openSet.safeAddElement(goalField);
	}

	void writeFieldCostsMap ()
	{
		writeln("field cost:");

		for(int i=0;i<gameTable.Length;i++){
			for(int j=0;j<gameTable[i].Length;j++){
				if(gameTable[i][j].fieldCost==float.MaxValue)
					write("X ");
				else
				write(gameTable[i][j].fieldCost+" ");
			}
			writeln("");
		}
		
		writeln("");
		writeln("");
	}
	
	
	
	public void writeTotalCostsMap ()
	{
		
		writeln("total cost:");
		
		for(int i=0;i<gameTable.Length;i++){
			for(int j=0;j<gameTable[i].Length;j++){
				if(gameTable[i][j].TotalPathCost>=float.MaxValue)
					write("X ");
				else
				write(gameTable[i][j].TotalPathCost+" ");
			}
			writeln("");
		}
		
		writeln("");
		writeln("");
	}
	
	public void writePreviousesMap ()
	{
		
		writeln("previouses:");
		
		for(int i=0;i<gameTable.Length;i++){
			for(int j=0;j<gameTable[i].Length;j++){
				Field current=gameTable[i][j];
				Field next=gameTable[i][j].next;
				if(next==null){
					write("0 ");
				
				
				}
				else
				{
					
					
					if(current.xIndex>next.xIndex)write("<");
					if(current.xIndex<next.xIndex)write(">");
					if(current.zIndex>next.zIndex)write("^");
					if(current.zIndex<next.zIndex)write("v");
					write(" ");
				}
				
				
				
				
			}
			writeln("");
		}
		
		writeln("");
		writeln("");
	}
	
	
	public void updatePath()
	{
		
			
		
		
		
		if(preparingNewGameTable)
		
		
		processMap();
		
		
		
	}
	
	public void notifyRecipients()
	{
//		Debug.Log("notify recipients");
		for(int i=0;i<gameTableUsers.Count;i++)
		{
			((Pathfinder)gameTableUsers[i]).findPath();
		}	
	
	}
	
	
	
	
	public void Update()
	{
		cooldown-=Time.deltaTime;
		
		if(cooldown<0&&GameScene.getGameScene().permissionToCalculatePaths())
		{
//			preparingNewGameTable=true;
			
			target.StartCoroutine(processMap());
			cooldown=totalCooldown;
			
			
		}
		
		if(Input.GetKey(KeyCode.B))
		{
			updatePath();
			writePreviousesMap();
			
		}
		
		if(Input.GetKey(KeyCode.T))
		{
			updatePath();
			writeTotalCostsMap();
		}

		
	}
	
	void processField ()
	{
			Field minimalCostField=openSet.removeMin();
			
			
		
			for(int i=-1;i<2;i++)
				for(int j=-1;j<2;j++){
				
			
			Field current=null;
			
				int x = minimalCostField.zIndex + i;
				int y = minimalCostField.xIndex + j;
				
			
			
			try{
				current=newGameTable[x][y];
			}catch(IndexOutOfRangeException e){
				Debug.Log("XXX");
			}
			
		
			
				if(current.fieldCost==PathfinderWeights.borderWeight)
					{
						continue;
					}
					
					if(current.fieldCost+minimalCostField.TotalPathCost<current.TotalPathCost)
					{
						current.next=minimalCostField;
						current.TotalPathCost=minimalCostField.TotalPathCost+current.fieldCost;
						
						openSet.safeAddElement(current);
				
				}
			
			
			}
			closedSet.Add(minimalCostField);
		
	}
	
	String changeMaxValueToX(float fieldCost)
	{
		if(fieldCost==float.MaxValue)return "X";
		else return fieldCost.ToString();
	}
	
	
	
	IEnumerator processMap()
	{
		yield return 0;
		
//		while(true){
		
//		Debug.Log("start");
		
		
		int counter=0;
			int xRange=(int)(MovementAdviceSystem.gameBounds.width/step)+1;	
				int zRange=(int)(MovementAdviceSystem.gameBounds.height/step)+1;	
//				Debug.Log("yield");

				newGameTable=new Field[zRange][];
				for(int i=0;i<zRange;i++)
				{
				newGameTable[i]=new Field[xRange];
					
					
				
				for(int j=0;j<xRange;j++)
				{
					setUpSingleField (i, j);
					counter++;
					if(counter>1000){
						counter=0;
//						Debug.Log("breaking");
						yield return 0;

					}
				}
				
		}		
	
		initializeSets();
		
		setUpGoalField ();
		
//		Debug.Log("XxxxxX");
		for(int i=0;openSet.HeapSize>0;i++)
		{
			
		
			processField ();
			
	
		}
		
		preparingNewGameTable=false;
		gameTable=newGameTable;
		
			
//		writeFieldCostsMap();
		
//		writePreviousesMap();
			
//		writeTotalCostsMap();
			
//		Debug.Log("Done");
		notifyRecipients();
//		}
		
		
	}
	
	
	public void write(String x)
	{
		File.AppendAllText("output.csv",x);
		
	}
	
	public void writeln(String x)
	{
		write(x+System.Environment.NewLine);
	}
	
	
	
	public static float calculateManhattanDistance(Vector3 vec1,Vector3 vec2)
	{
	return Math.Abs(vec1.x-vec2.x)+Math.Abs(vec1.z-vec2.z);
	}
	
	public static float sqDist(Vector3 vec1,Vector3 vec2)
	{
		return (float)(Math.Pow(vec1.x-vec2.x,2f)+Math.Pow(vec1.z-vec2.z,2f));
	}
	

	
}

