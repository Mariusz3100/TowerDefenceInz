using System;
using UnityEngine;



	public class Tower:MGameObject
	{
	public float topRotation=4;
		public Tower ():base()
		{
			
		}
		
		public void Update()
		{
		rotateTop ();
			
			
			
		}
		
	void rotateTop ()
	{
		this.transform.Find("top").transform.Rotate(new Vector3(0,0,2));
	}
	
	
	}


