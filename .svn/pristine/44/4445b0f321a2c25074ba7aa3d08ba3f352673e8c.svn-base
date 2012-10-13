using UnityEngine;
using System;


	public class GameScene
	{
		private static GameScene singleton;
		
		public static int screenWidth=40;//0.0 to 40.0
		public static int screenHeight=40;
		
		public static Vector2 startingPoint=new Vector2(0,0);
		
		public Rect gameScene;
		
	private GameScene ()
	{
					gameScene=new Rect(startingPoint.x,startingPoint.y,screenWidth,screenHeight);

	}
	
	
	
	
		public static GameScene getGameScene(){
		if(singleton==null)
				singleton=new GameScene();
			
			return singleton;
		
		}		
	
}

