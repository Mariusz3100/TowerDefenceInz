using System;
using UnityEngine;

public class TemplateList:MonoBehaviour
{
	public SpawnPointGameObject spawnPointTemplate;

	public Enemy[] enemyTemplates;
	public Money moneyTemplate;
	
	private static TemplateList singleton;
	
	public TemplateList ()
	{
		
	}

	public SpawnPointGameObject SpawnPointTemplate {
		get {
			return this.spawnPointTemplate;
		}
		set {
			spawnPointTemplate = value;
		}
	}	
	public void Start()
	{
		if(singleton!=null)print("error, violating singleton rules");
		singleton=this;
	}
	
	public static TemplateList getEnemyTemplateList()
	{
		return singleton;
	}
	
}


