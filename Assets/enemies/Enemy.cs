using System;
using System.Collections;	
using UnityEngine;


	public class Enemy:MGameObject
	{
	
	public int speed= 4;
	
	public Enemy ()
		{
		}
	
	
	
	
	
	public void Update(){
	
	Vector3 temp=this.transform.position;
			
				temp.z+=Input.GetAxisRaw("Vertical")*Time.deltaTime*speed;		
				temp.x+=Input.GetAxisRaw("Horizontal")*Time.deltaTime*speed;

			this.transform.position=temp;
		
//		Transform children=transform.Find("Sphere");
		
//			children.Translate(new Vector3(0,1,0));
				
		
	
	}
	
	
	
	
	
	
	}


