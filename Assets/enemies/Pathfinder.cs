using System;
using UnityEngine;



public class Pathfinder:MonoBehaviour
{
	private int speed;
	
	public Pathfinder ()
	{
	}
	
	public void Start()
	{
		speed=((Enemy)(transform.parent.GetComponent("Enemy"))).speed;
	}
	
	
	
	public void OnTriggerEnter(Collider other)
	{
		print (other.gameObject.tag);
		
		
	}
	
}


