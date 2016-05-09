using System;
using UnityEngine;

public class TowerSketch:MonoBehaviour
{
	private bool active=false;
	private Vector2 positionRelativeToPlayer;
	public static float drawingTowerSketchReserve=1.08f;
	private int drawTowerPosition_Index=0;
	private Vector3 nonActivePosition;
	private bool obstructed;
	public Tower towerTemplate;
	public void activate()
	{
		active=true;
		adjustToCurrentPosition();
	}
	
	public void deactivate()
	{
		active=false;
		transform.position=nonActivePosition;
	}

	public bool Obstructed {
		get {
			return this.obstructed;
		}
		set {
			obstructed = value;
		}
	}
	public Vector3 getPosition()
	{
		return new Vector3(this.positionRelativeToPlayer.x+Player.getPlayer().transform.position.x,
			Player.getPlayer().transform.position.y,
			this.positionRelativeToPlayer.y+Player.getPlayer().transform.position.z);
		
	}	
	public TowerSketch ()
	{
	}
	
	
	public void Start()
	{
		positionRelativeToPlayer=new Vector2(0,0);
		nonActivePosition=transform.position;
	}

	public void Update()
	{
		if(active)
		{
			Vector3 playerPosition=Player.getPlayer().transform.position;
			Vector3 newCoordinates=new Vector3(playerPosition.x,transform.position.y,playerPosition.z); // y stayes the same, x and z has to get calculated
			transform.position=newCoordinates;
			transform.Translate(positionRelativeToPlayer.x,0,positionRelativeToPlayer.y);
		}
		
		if(obstructed==false)transform.Find("base").renderer.material.color=Color.gray;
		

	}
	
	public void tryBuildingTower()
	{
		if(!obstructed)
		{
			if(Tower.CurrentMoney>=towerTemplate.cost)
			{
				Tower.CurrentMoney-=towerTemplate.cost;
				Instantiate(towerTemplate,transform.position,Quaternion.identity);
				GameSounds.getGameSounds().towerBuilt.Play();
			}
			else
			{
				GameSounds.getGameSounds().noMoney.Play();
			}
		}
	}
	
	public void changePosition(int change){
		
		drawTowerPosition_Index=(drawTowerPosition_Index+change+8)%8;

		
		adjustToCurrentPosition ();

		
		
	}

	void adjustToCurrentPosition ()
	{
		Vector3 playerPosition=Player.getPlayer().transform.position;
		Vector3 towerPosition=	transform.position;
		float absoluteOffset=
				(((BoxCollider)Player.getPlayer().collider).bounds.size.x/2+
					((BoxCollider)collider).bounds.size.x/2)*drawingTowerSketchReserve;
			
			float Xoffset=0,Yoffset=0;
		switch(drawTowerPosition_Index){
			case 0:Xoffset=-absoluteOffset;Yoffset=-absoluteOffset;break;
			case 1:Xoffset=0;Yoffset=-absoluteOffset;break;
			case 2:Xoffset=absoluteOffset;Yoffset=-absoluteOffset;break;
			case 3:Xoffset=absoluteOffset;Yoffset=0;break;
			case 4:Xoffset=absoluteOffset;Yoffset=absoluteOffset;break;
			case 5:Xoffset=0;Yoffset=absoluteOffset;break;
			case 6:Xoffset=-absoluteOffset;Yoffset=absoluteOffset;break;
			case 7:Xoffset=-absoluteOffset;Yoffset=0;break;
		
		
		}
		
		positionRelativeToPlayer.Set(Xoffset,Yoffset);
	}
	
	void OnTriggerEnter(Collider other) {
//		if(other.gameObject.tag!="Player"&&other.gameObject.tag!="Ignore")
 //   	transform.Find("base").renderer.material.color=Color.red;
	}
	
	
	void OnTriggerStay(Collider other) {
		if(other.gameObject.tag!="Player"&&!other.gameObject.tag.StartsWith(TagNames.ignoreCollisionTagStart))
		{
		obstructed=true;
		transform.Find("base").renderer.material.color=Color.red;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag!="Player"&&!other.gameObject.tag.StartsWith(TagNames.ignoreCollisionTagStart))
		obstructed=false;
		
	}
	
	
	
}