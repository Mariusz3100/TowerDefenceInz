using System;
using System.Xml;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class Wave
{
	EnemyGroup[] enemyGroups;
	int numberOfAllEnemies=0;
	
	int numberOfEnemiesLeftToSpawn=0;
	int numberOfNotKilledEnemies=0;

	
	private XmlNode XMLdescription;
	private SpawnPointController[] listOfSpawnPoints;
	private int listOfSpawnPointsCount;
	private int indexNumber=0;

	public EnemyGroup[] EnemyGroups {
		get {
			return this.enemyGroups;
		}
		set {
			enemyGroups = value;
		}
	}	

	
	public int IndexNumber {
		get {
			return this.indexNumber;
		}
	}

	public int NumberOfNotKilledEnemies {
		get {
			return this.numberOfNotKilledEnemies;
		}
		set {
			numberOfNotKilledEnemies = value;
		}
	}
	public int NumberOfEnemiesLeftToSpawn {
		get {
			return this.numberOfEnemiesLeftToSpawn;
		}
		set {
			numberOfEnemiesLeftToSpawn = value;
		}
	}
	public Wave (XmlNode waveDescription)
	{
		listOfSpawnPoints=new SpawnPointController[10];
		listOfSpawnPointsCount=0;
		GetDataFromXML (waveDescription);
		numberOfEnemiesLeftToSpawn=numberOfAllEnemies;
		numberOfNotKilledEnemies=numberOfAllEnemies;
	}

	public void GetDataFromXML (XmlNode waveDescription)
	{
		
		waveDescription=waveDescription;
		
		indexNumber=Int32.Parse(waveDescription.Attributes.GetNamedItem("lp").Value);
		
		XmlNode[] xmlDescritpionOfGroups = new XMLhelper(waveDescription).getAllChildrenNamed("EnemyGroup");
		
		
		enemyGroups=new EnemyGroup[xmlDescritpionOfGroups.Length];
			
		for(int i=0;i<xmlDescritpionOfGroups.Length;i++)
		{
			EnemyGroup eg=new EnemyGroup(xmlDescritpionOfGroups[i]);
			enemyGroups[i]=eg;
			SpawnPointController[] refParameter=enemyGroups[i].SpawnPoints;
			addSpawnPoints(ref refParameter);
			numberOfAllEnemies+=enemyGroups[i].NumberOfEnemies;
		}
		
	}
	
	
		
	public Enemy spawnMonster()
	{
		EnemyGroup eg= chooseRandomEnemyGroupConsideringNumberOfEnemiesToSpawn();
			
		while(eg.numberOfEnemiesLeftToSpawn()<1)
			eg=chooseRandomEnemyGroupConsideringNumberOfEnemiesToSpawn();
			
			
		Enemy newEnemy=eg.spawnMonster();
		newEnemy.CreatedFor=this;
		--numberOfEnemiesLeftToSpawn;
		return newEnemy;
	
	}	

	
	public void placeSpawnPoints()
	{
		foreach(SpawnPointController x in listOfSpawnPoints)
		{
			x.beginWork();
		}
	
	}

	EnemyGroup chooseRandomEnemyGroupConsideringNumberOfEnemiesToSpawn ()
	{
		//takes into account to create monsters from big groups more often than those from small groups,
		//to make creation of monsters more balanced
		
		int rand=UnityEngine.Random.Range(0,numberOfAllEnemies);
		
		foreach(EnemyGroup eg in enemyGroups)
		{
			rand-=eg.NumberOfEnemies;
			if(rand<0)return eg;
		}
		
		return null;
	}
	
	private void expandArray()
	{
		SpawnPointController[] newOne=new SpawnPointController[listOfSpawnPointsCount*2];
		for(int i=0;i<listOfSpawnPointsCount;i++)
			newOne[i]=listOfSpawnPoints[i];
		
		listOfSpawnPoints=newOne;
		
	}
	
	
	private void cutArrayToSize()
	{
		SpawnPointController[] newOne=new SpawnPointController[listOfSpawnPointsCount];
		for(int i=0;i<listOfSpawnPointsCount;i++)
			newOne[i]=listOfSpawnPoints[i];
		
		listOfSpawnPoints=newOne;
		
	}
		
		
		
	public void addSpawnPoints(ref SpawnPointController[] newSpawnPoints)
	{
		bool collision=false;
		for(int j=0;j<newSpawnPoints.Length;j++)
		{
			if(listOfSpawnPointsCount>=listOfSpawnPoints.Length-1)expandArray();

			for(int i=0;i<listOfSpawnPointsCount;i++)
			{
				Vector3 pos1=listOfSpawnPoints[i].ToBePosition;
				Vector3	pos2=newSpawnPoints[j].ToBePosition;

				float distanceBetweenSPs=Vector3.Distance(pos1,pos2);
				float spawnPointsRadius=((CapsuleCollider)TemplateList.getEnemyTemplateList().spawnPointTemplate.collider)
				.radius;
				
				if(distanceBetweenSPs<2*spawnPointsRadius)
				{
					
					Vector3 newPositionForOldSP=(pos1+pos2)/2;
					listOfSpawnPoints[i].ToBePosition=newPositionForOldSP;
									
					newSpawnPoints[j]=listOfSpawnPoints[i];
					collision=true;
					break;
				}
				
					
			}
			
			if(!collision)listOfSpawnPoints[listOfSpawnPointsCount++]=newSpawnPoints[j];
			
			
		}
		
		cutArrayToSize();
		
	}
	
	public void StartWave()
	{
	}
	
	public void StartPreceedingBuildPhase()
	{
		placeSpawnPoints();

	}
		
}


