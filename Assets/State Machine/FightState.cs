using System;
using UnityEngine;
using System.Xml;
public class FightState:State
{
	private Wave[] waves;
	private int waveCounter; //index of current wave (the one player is preparing for or fighting with)
	private float monsterSpawningCD;
	private float monsterSpawningMaxCD;
	
	public FightState (StateMachine sm):base(sm)
	{
		menu.fightPhaseLabel.renderer.enabled=false;
		monsterSpawningMaxCD=GeneralParameters.cooldownOfSpawningMonsters;
		initializeWaves();
		waveCounter=0;
		waves[waveCounter].StartPreceedingBuildPhase();

	}

	void initializeWaves ()
	{
		TextAsset input=(TextAsset)Resources.Load("EnemiesParameters");
		
		XmlDocument doc=new XmlDocument();
		doc.LoadXml(input.text);
		String ret="";
		XmlNode fightState=doc["FightState"];
		
		
		
		XmlNode[] wavesDescriptions=new XMLhelper(fightState).getAllChildrenNamed("Wave");
		
		
		this.waves=new Wave[wavesDescriptions.Length];
		
		for(int i=0;i<wavesDescriptions.Length;i++)
		{
			Wave temp=new Wave(wavesDescriptions[i]);
			waves[temp.IndexNumber]=temp;
		}
		
		
	
	}

		
	
	public override void update()
	{
		menu.UpdateFightPhase();
		regenerateEnergy ();
		Wave current=waves[waveCounter];

		
		monsterSpawningCD-=Time.deltaTime;
		
		if(monsterSpawningCD<0)
			{
				if(current.NumberOfEnemiesLeftToSpawn>0)
					current.spawnMonster();
					
						
				
				monsterSpawningCD=monsterSpawningMaxCD;
			}
			
		
		if(current.NumberOfNotKilledEnemies<=0)
			stateMachine.changeState();
		
		
		
		

	}
	

		

	void regenerateEnergy ()
	{
		Spell.Energy+=GeneralParameters.manaPerSecond*Time.deltaTime;
	}
	
	public override void turnOn()
	{
		menu.SwitchToFightPhase();
		
	}
	
	public override void turnOff()
	{
		waveCounter++;
		if(waveCounter>=waves.Length)stateMachine.GameWon=true;
		else
		waves[waveCounter].StartPreceedingBuildPhase();
	}
	
	
	public Wave getCurrentWave()
	{
		return waves[waveCounter];
	}
	
	
	
}


