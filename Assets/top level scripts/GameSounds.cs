using System;
using UnityEngine;

public class GameSounds:MonoBehaviour
{
	private static GameSounds singleton;
	
	public GameSounds ()
	{
	}
	
	void Start()
	{
		
		if(singleton==null)singleton=this;
		else throw new Exception("Singleton rules vialotation at GameSounds");
	}
	
	
	
	public static GameSounds getGameSounds()
	{
	
		return singleton;
	}
	
	public AudioSource noEnergy;
	public AudioSource noMoney;
	public AudioSource towerBuilt;
	
}


