using System;
using UnityEngine;
using System.Collections;


public class RedTower:Tower
{
	public ParticleSystem ps;
	public float damage;
		
	public RedTower ()
	{
	}
	
	
	
	Enemy[] getEnemies (float r)
	{
		ArrayList list=new ArrayList();
		Vector3 thisPosition=transform.position;
		int enemyLayer=~LayerMask.NameToLayer(LayerNames.enemyLayer);
		
		foreach(Collider other in Physics.OverlapSphere(transform.position,r,enemyLayer))
		{
			
			if(other.tag==TagNames.enemyTag){
				Enemy x=(Enemy)other.gameObject.GetComponent("Enemy");
				
				list.Add(x);
			}
		}
		
		object[] objects=list.ToArray();
		Enemy[] ret=new Enemy[list.Count];
			
		for(int i=0;i<list.Count;i++)
		{
			ret[i]=(Enemy)objects[i];
		}
			
		return ret;
	}
	
	public override void UpdateShooting ()
	{
		
		
		if(shootingCooldown<baseShootingCooldown*0.9)
			ps.enableEmission=false;
		
		shootingCooldown-=Time.deltaTime;
		
		
		float r=((MeshRenderer)range.renderer).bounds.extents.x;

		Enemy[] enemies=getEnemies(r);
		
		if(enemies.Length>0&&shootingCooldown<=0)
		{
			ps.enableEmission=true;
			foreach(Enemy x in enemies)
			{
				x.decreaseLife(damage);
			}
		
			
			shootingCooldown=baseShootingCooldown;
		}
		
		

	}
	public void Start()
	{
		ps.enableEmission=false;
	}
	
	
	
}


