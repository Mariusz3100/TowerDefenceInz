using System;
using System.Xml;
using UnityEngine;
using System.Collections;

public class EnemyGroup
{
	private Enemy enemySpawned;
	private int numberOfEnemies;
	private int numberOfEnemiesSpawned;

	private SpawnPointController[] spawnPoints;
	private XmlNode XMLdescription;
	
	public int numberOfEnemiesLeftToSpawn()
	{
		return numberOfEnemies-numberOfEnemiesSpawned;
	
	}
	
	
	
	public SpawnPointController[] SpawnPoints {
		get {
			return this.spawnPoints;
		}
		set {
			spawnPoints = value;
		}
	}	
	
	public EnemyGroup (XmlNode xmlData)
	{
		getDatafromXml(xmlData);
		NumberOfEnemiesSpawned=0;
	}
	
	public Enemy spawnMonster()
	{
		int rand=UnityEngine.Random.Range(0,spawnPoints.Length);
		Vector3 positionToSpawn=SpawnPoints[rand].SpawnPointSceneObject.transform.position;
		Enemy newEnemy=enemySpawned.createCopyAt(positionToSpawn);
		
		numberOfEnemiesSpawned++;
		return newEnemy;
	}
	
	
	public void getDatafromXml (XmlNode xmlDescritpionOfGroup)
	{
		XMLdescription=xmlDescritpionOfGroup;
		int enemyTypeIndex=Int32.Parse(xmlDescritpionOfGroup.Attributes.GetNamedItem("enemyType").Value);
		EnemySpawned=TemplateList.getEnemyTemplateList().enemyTemplates[enemyTypeIndex];
		
		
		int enemyAmount=Int32.Parse(xmlDescritpionOfGroup.Attributes.GetNamedItem("amount").Value);
		NumberOfEnemies=enemyAmount;
		
		

		
		XMLhelper helpMe=new XMLhelper(xmlDescritpionOfGroup);
		XmlNode[] listOFSpawnPointsDescriptions=helpMe.getAllChildrenNamed("SpawnPoint");
		spawnPoints=new SpawnPointController[listOFSpawnPointsDescriptions.Length];
		for(int i=0;i<listOFSpawnPointsDescriptions.Length;i++)
		{
			spawnPoints[i]=new SpawnPointController(listOFSpawnPointsDescriptions[i]);
				
		}
			
	}
	
	
	
	
	
	public Enemy EnemySpawned {
		get {
			return this.enemySpawned;
		}
		set {
			enemySpawned = value;
		}
	}

	public int NumberOfEnemies {
		get {
			return this.numberOfEnemies;
		}
		set {
			numberOfEnemies = value;
		}
	}

	
	public int NumberOfEnemiesSpawned {
		get {
			return this.numberOfEnemiesSpawned;
		}
		set {
			numberOfEnemiesSpawned = value;
		}
	}
}


