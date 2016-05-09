using System;
using UnityEngine;

public class CameraScript:MonoBehaviour
{
	public float speedOfZoom;
	public float minCameraHeight;
	public float maxCameraHeight;
	
		public float leftAngleMultiplier;

		public float rightAngleMultiplier;

		public float topAngleMultiplier;

		public float bottomAngleMultiplier;

	public GameObject lens;
	private bool gameLost=false;
	private bool gameWon=false;

	public bool GameLost {
		get {
			return this.gameLost;
		}
		set {
			gameLost = value;
		}
	}

	public bool GameWon {
		get {
			return this.gameWon;
		}
		set {
			gameWon = value;
		}
	}	
		
	public float guiShift;//necessary to shift zone camera moves in to the right, so GUI won't overlap gameworld
	public float topShift;
	
	
	public float baseMoveThreshold;
	private float adjustedMoveThreshold;
	public CameraScript ()
	{
	}
	
	
	public void Update()
	{
		adjustedMoveThreshold=baseMoveThreshold+Mathf.Tan(camera.fieldOfView/2)*(transform.position.y-Player.getPlayer().transform.position.y)*2;
		
		Vector3 temp=transform.position;
			
		
/*		
		float speed=5;
		
			temp=this.transform.position;
			
				temp.z+=Input.GetAxisRaw("Vertical")*Time.deltaTime*speed;		
				temp.x+=Input.GetAxisRaw("Horizontal")*Time.deltaTime*speed;
		
		*/
		
		
		followPlayer (ref temp);
		
		
		verticalPositionControl(ref temp);
		
			
		adjustToVerticalRestrictions (ref temp);
			
		adjustingToHorizontalRestrictions (ref temp);
			
						

	
		

		this.transform.position=temp;
		
		
		
		if(gameLost)gameLostUpdate();
		else
			if(gameWon)gameWonUpdate();
		
	}

	void followPlayer (ref Vector3 temp)
	{
		Vector3 difference=Player.getPlayer().transform.position-temp;
		difference.y=0;
		
		//add 1/4 of the visible gamespace to x coordinate, so the gui will not cover the player
		
//		difference.x-=Mathf.Tan(camera.fieldOfView/2)*(transform.position.y-Player.getPlayer().transform.position.y)/4;	
		
		difference.x+=guiShift;	
		Vector3 newPosition=transform.position+0.02f*difference;
		if(difference.magnitude>adjustedMoveThreshold) 
			temp=newPosition;
		
	}

	void adjustToVerticalRestrictions (ref Vector3 temp)
	{
		if(temp.y>maxCameraHeight)temp.y=maxCameraHeight;
					if(temp.y<minCameraHeight)temp.y=minCameraHeight;
	}

	void adjustingToHorizontalRestrictions (ref Vector3 temp)
	{
		float fieldOfView=this.camera.fieldOfView;
		float height=(transform.position.y-Player.getPlayer().transform.position.y);
		
		float maxDistanceFromBorderOnCurrentY=(float)Mathf.Tan(Mathf.Deg2Rad*fieldOfView/3f)*height;
		float xmin=GameScene.getGameScene().X_min_Border.position.x+(float)Mathf.Tan(Mathf.Deg2Rad*fieldOfView/leftAngleMultiplier)*height;
		float xmax=GameScene.getGameScene().X_max_Border.position.x-(float)Mathf.Tan(Mathf.Deg2Rad*fieldOfView/rightAngleMultiplier)*height;
		float zmin=GameScene.getGameScene().Z_min_Border.position.z+(float)Mathf.Tan(Mathf.Deg2Rad*fieldOfView/bottomAngleMultiplier)*height;
		float zmax=GameScene.getGameScene().Z_max_Border.position.z-(float)Mathf.Tan(Mathf.Deg2Rad*fieldOfView/topAngleMultiplier)*height-topShift;
		
		
		
					if(temp.x<xmin)temp.x=xmin;
					if(temp.x>xmax)temp.x=xmax;
					if(temp.z>zmax)temp.z=zmax;
					if(temp.z<zmin)temp.z=zmin;
	}
	
	
	void verticalPositionControl (ref Vector3 temp)
	{
		if(Input.GetKey(KeyCode.PageUp))temp.y-=speedOfZoom;
		if(Input.GetKey(KeyCode.PageDown))temp.y+=speedOfZoom;
	}
	
	public void gameLostUpdate()
	{
		
	
		Color temp=lens.renderer.material.color;
		
		if(temp.a<0.8){
			temp.a+=0.002f;
			lens.renderer.material.color=temp;
		}
	}
	
	public void gameWonUpdate()
	{
//		Player.getPlayer().playerParticleSystem.transform.Translate(0,0.1f,0);
		Player.getPlayer().playerParticleSystem.light.range+=0.2f;
		Player.getPlayer().playerParticleSystem.particleSystem.startLifetime++;
		Player.getPlayer().playerParticleSystem.particleSystem.emissionRate++;
		Player.getPlayer().playWinAnimation();
		
		Color temp=Color.white;
			temp.a=lens.renderer.material.color.a;
		
		if(temp.a<0.8){
			temp.a+=0.002f;
			lens.renderer.material.color=temp;
		}
		
		
	}
	
}


