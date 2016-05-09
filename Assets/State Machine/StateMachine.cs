using System;
using System.Collections;	
using UnityEngine;

public class StateMachine:MonoBehaviour
	{
		Menu menu;
		private State currentState;
		private State nextState;
		private State fightState;
		private State buildState;
		private bool gamePaused=false;
	public GameObject beacon;
	public bool GamePaused {
		get {
			return this.gamePaused;
		}
		set {
			gamePaused = value;
		}
	}

		private static StateMachine singleton;
	public CameraScript cam;
	
	public static StateMachine getStateMachine()
	{
		if(singleton!=null)return singleton;
		else throw new NullReferenceException("State Machine singleton not set");
		
	}
	
	private bool gameLost;
	private bool gameWon;

	public bool GameLost {
		get {
			return this.gameLost;
		}
		set {
			gameLost = value;
			if(value)startGameLost();
		}
	}

	public bool GameWon {
		get {
			return this.gameWon;
		}
		set {
			gameWon = value;
			if(value)startGameWon();

		}
	}	
	
		public StateMachine ()
		{
		menu=Menu.getMenu();
		}
		
		
	public void Update(){
		
//		if(Input.GetKeyDown(KeyCode.Tab))
//			changeState();
		
		if(!(gameWon||gameLost))currentState.update();
		
		if(gamePaused)
		{
			if(Input.GetKeyDown(KeyCode.P)){
				Time.timeScale=1;
				gamePaused=false;

			}
		}
		else
		{
			if(Input.GetKeyDown(KeyCode.P)){
				Time.timeScale=0;
				gamePaused=true;

			}
			
		}
	}
		
	
	
		public void Start()
		{
			singleton=this;
			
			fightState=new FightState(this);
			
			buildState=new BuildState(this);
			currentState=buildState;
			nextState=fightState;
			
			Tower.CurrentMoney=GeneralParameters.startingGold;
			Spell.Energy=GeneralParameters.startingEnergy;
		
		
		
			currentState.turnOn();
			menu.Start();
		
		}

		public void changeState()
		{
			currentState.turnOff();
			nextState.turnOn();
			State temp=currentState;
			currentState=nextState;
			nextState=temp;
			
		}
		
	
	public Wave getCurrentWave()
	{
		if(currentState==fightState)return ((FightState)fightState).getCurrentWave();
		else
			return null;
	}
	public void startGameWon()
	{
		menu.GameWon();
		cam.GameWon=true;
		Player.getPlayer().MovementBlocked=true;
	}
	
	
	public void startGameLost()
	{
//		camera.
		menu.GameLost();
		cam.GameLost=true;
		
	}
	
	
	
	
}

