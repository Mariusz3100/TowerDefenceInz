using System;
using UnityEngine;

public class SniperShot:MonoBehaviour
{
	
	public float flightHeight;
	public float flightTime;
	private float remainingFlightTime;
	private float damage;
	
	public void Start()
	{
		remainingFlightTime=flightTime;
	}
	
	
	public void Update()
	{
		if(remainingFlightTime>0)
		{
			float currentMove=flightHeight*Time.deltaTime/flightTime;
			transform.Translate(0,currentMove,0);
			remainingFlightTime-=Time.deltaTime;
		}else{
			Destroy(this.gameObject);
		}
	}
	
	
	
	
	public SniperShot createShotAt(Vector3 position,float damageToDeal)
	{
		position.y=transform.position.y;
		
		SniperShot ret=(SniperShot)Instantiate(this,position,Quaternion.identity);
		ret.damage=damageToDeal;
		return ret;
		
		
	}
	
	
	public void OnTriggerEnter(Collider other)
	{
		if(other.tag==TagNames.enemyTag)
		{
			((Enemy)other.GetComponent("Enemy")).decreaseLife(damage);
		
		}
	}
	
	
}


