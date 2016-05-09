using System;
using UnityEngine;
using System.Xml;

public class SpawnPointController
{
	private SpawnPointGameObject spawnPointSceneObject;
	private Vector3 toBePosition;

	public SpawnPointGameObject SpawnPointSceneObject {
		get {
			return this.spawnPointSceneObject;
		}
		set {
			spawnPointSceneObject = value;
		}
	}

	public Vector3 ToBePosition {
		get {
			return this.toBePosition;
		}
		set {
			toBePosition = value;
		}
	}	
	public SpawnPointController (XmlNode spawnPointDescription)
	{
		
		float x=Int32.Parse(spawnPointDescription.Attributes.GetNamedItem("x").Value);
		float z=Int32.Parse(spawnPointDescription.Attributes.GetNamedItem("y").Value);
		float y=TemplateList.getEnemyTemplateList().spawnPointTemplate.transform.position.y;
		toBePosition=new Vector3(x,y,z);
		
	}
	
	public void beginWork()
	{
		spawnPointSceneObject=SpawnPointGameObject.createNewSPGOat(toBePosition);
		
	}
	
}
