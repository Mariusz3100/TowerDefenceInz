using System;
using UnityEngine;

public abstract class Bullet:MonoBehaviour
{
	private Enemy target;
	public float speed;
	public Enemy Target {
		get {
			return this.target;
		}
		set {
			target = value;
		}
	}
	
	
	public Bullet ()
	{
	}
	
	public void Update()
	{
		if(target.Dead)Destroy(this.gameObject);
		moveTowards(target.transform.position);
	}
	
	
	
	void moveTowards (Vector3 target)
	{
		
		
		Vector3 move=-(transform.position-target);
		
		Vector3 moveNorm=move.normalized;
		transform.Translate(moveNorm*speed/100);
	}
	
	
	public void OnTriggerEnter(Collider other)
	{
		if(other.tag==TagNames.enemyTag)
		{
			applyEffect(other);
		
			Destroy(this.gameObject);
		}
	}
	
	
	public abstract void applyEffect(Collider enemy);
	
	
	
	
	
}


