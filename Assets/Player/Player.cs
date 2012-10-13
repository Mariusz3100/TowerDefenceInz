using System;
using UnityEngine;


	public class Player:MGameObject
	{
		
		private static Player singleton;
		
		public static Player getPlayer(){
		if(singleton==null)
				singleton=new Player();
			return singleton;
		
		}
		
	
	private static GameScene gameScene; 
	
	
	public int speed=5;
		
		
		
	
	public void Update(){
		hadleInput ();
	}

	void hadleInput ()
	{
			Vector3 temp=this.transform.position;
			
				temp.z+=Input.GetAxisRaw("Vertical")*Time.deltaTime*speed;		
				temp.x+=Input.GetAxisRaw("Horizontal")*Time.deltaTime*speed;

			this.transform.position=temp;
		
	
	
	}
	
	
	public void Awake()
	{
	gameScene=GameScene.getGameScene();
	}
	public Player ()
		{
			
		}
}

