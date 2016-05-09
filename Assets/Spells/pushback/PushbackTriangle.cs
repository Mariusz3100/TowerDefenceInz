using System;
using UnityEngine;

public class PushbackTriangle:MonoBehaviour
{
	private bool casting;
	private float forceStrength=5;
	
	
	public bool Casting {
		get {
			return this.casting;
		}
		set {
			casting = value;
		}
	}	
	
	public PushbackTriangle ()
	{
	}
	
	public void Update()
	{
		Vector2 temp=renderer.material.mainTextureOffset;
		temp.x+=0.003f;
		renderer.material.mainTextureOffset=temp;
		
//		Debug.Log("casting: "+casting);
	}
	
	public void OnTriggerStay(Collider other)
	{
//		Debug.Log(other.tag);
		if(other.tag==TagNames.enemyTag&&casting)
		{
   			Enemy e=((Enemy)other.GetComponent("Enemy"));
			Vector3 diff=e.transform.position-Player.getPlayer().transform.position;
			diff.y=0;
//			diff.Normalize();
			
			//Debug.Log(diff);
			e.addEffect(new PushbackEffect(e, forceStrength*diff));
			
		
		}
	}
	
	
	
}

