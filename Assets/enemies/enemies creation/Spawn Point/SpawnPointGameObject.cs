using System;
using UnityEngine;
using System.Xml;
using System.Collections;

public class SpawnPointGameObject:MonoBehaviour
{
	
	private static float heightOfInvisibility;
	public ParticleCircle[] particlePoints;

	private float circleRadius;
	public SpawnPointGameObject()
	{
	}
	

	public void Start()
	{
		
		circleRadius=((CapsuleCollider)collider).radius;
		for(int i=0;i<particlePoints.Length;i++)
		{
			ParticleCircle circle=particlePoints[i];
			circle.OnCirclePosition=UnityEngine.Random.Range(0,(float)Math.PI*2);
		}
	}
	
	public void Update()
	{
		for(int i=0;i<particlePoints.Length;i++)
		{
			ParticleCircle circle=particlePoints[i];
			increaseParticlesPosition (circle);
			updateParticlesPosition(circle);
		}
	}
	
	void increaseParticlesPosition (ParticleCircle circle)
	{
		circle.OnCirclePosition+=Time.deltaTime*UnityEngine.Random.Range(1,10)*GeneralParameters.SpawnPointRotatingSpeed;
		if(circle.OnCirclePosition>(float)Math.PI*2)circle.OnCirclePosition-=(float)Math.PI*2;
	}
	
	
	public static SpawnPointGameObject createNewSPGOat(Vector3 toBePosition)
	{
		SpawnPointGameObject ret =(SpawnPointGameObject)Instantiate(TemplateList.getEnemyTemplateList().spawnPointTemplate,
			toBePosition,
			Quaternion.identity);
		
		return ret;
	}
	
	void updateParticlesPosition (ParticleCircle circle)
	{
		Vector3 newPosition=new Vector3();
		newPosition.y=circle.transform.localPosition.y;
		newPosition.x=Mathf.Sin(circle.OnCirclePosition);
		newPosition.z=Mathf.Cos(circle.OnCirclePosition);
		circle.transform.localPosition=newPosition;
	}
}


