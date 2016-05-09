using System;
using System.Collections;
using UnityEngine;


public class Menu:MonoBehaviour
	{
		public TextMesh moneyAmount;
		public TextMesh energyAmount;
		public TextMesh fightPhaseLabel;
		public TextMesh buildPhaseLabel;
		public TextMesh timeOrMonstersLeftLabel;
		public TextMesh congratulationsLabel;
		public TextMesh gameOverLabel;
		private String timeLeftBegin="Time Left: ";
		private String monstersLeftBegin="Monsters left: 1";

		private static Menu singleton;
		public GameObject selectionBox;
	
		public SpellGuiElement[] spellGuiElements;
		private int selectedTower=0;
		public TowerGuiElement[] towerGuiElements;
		private int selectedSpell=0;
		public GameObject highlightingShape;
		private int drawTowerPosition_Index=0;
		private bool targetingHidden;
	
		private Menu()
	{
			singleton=this;		
		

	}
	
	public void announceVictory()
	{
		congratulationsLabel.renderer.enabled=true;
		Player.getPlayer().MovementBlocked=true;
	}
	
	
	public void GameLost()
	{
		gameOverLabel.renderer.enabled=true;
		
		
	}
	
	
	public void GameWon()
	{
		congratulationsLabel.renderer.enabled=true;
		
		
	}
	
	
		
		public static Menu getMenu()
	{
		return singleton;
	}

	public GameObject SelectionBox {
		get {
			return this.selectionBox;
		}
		set {
			selectionBox = value;
		}
	}
	
		
	public void Start()
		{
			moneyAmount.text=((int)GeneralParameters.startingGold).ToString();
			energyAmount.text=((int)GeneralParameters.startingEnergy).ToString();

			highlightingShape.transform.Find("spells").renderer.enabled=false;
			
			congratulationsLabel.renderer.enabled=false;
			gameOverLabel.renderer.enabled=false;
		}
		
	
	
	public void UpdateBuildPhase(BuildState currentBS)
	{
		
		
		generalUpdate();
		if(Input.GetKey(KeyCode.LeftShift))
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{	
				
				changeTowerSelected ();
			
			}
		}else{
			if(Input.GetKeyDown(KeyCode.Z))
			{	
				
				changeTowerSketchRelativePosition(1);
			}
			
			if(Input.GetKeyDown(KeyCode.C))
			{	
				
				changeTowerSketchRelativePosition(-1);
			}
			
			if(Input.GetKeyDown(KeyCode.X))
			{	
				
				buildTowerAtCurrentPosition();
				
			}
		
		
		}
		
		
		timeOrMonstersLeftLabel.text=timeLeftBegin+((int)currentBS.TimeLeft).ToString();
	
	}
	
	
	void buildTowerAtCurrentPosition ()
	{
		TowerSketch towerToBuild=towerGuiElements[selectedTower].intangibleTowerTemplate;
				
		towerToBuild.tryBuildingTower();
	}
	
	void changeTowerSketchRelativePosition (int change)
	{
		TowerSketch towerToBuild=towerGuiElements[selectedTower].intangibleTowerTemplate;
				
		towerToBuild.changePosition(change);
	}

	
	
	public void UpdateFightPhase()
	{
		generalUpdate ();
		if(Input.GetKey(KeyCode.LeftShift))
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				changeSpellSelected();
				
			
			
			
			}
		}else{
		
		
			if(Input.GetKeyDown(KeyCode.Space))
			{
				spellGuiElements[selectedSpell].actualSpell.applyEffect();
			}
		}
	
	}

	void generalUpdate ()
	{
		energyAmount.text=((int)Spell.Energy).ToString();
		moneyAmount.text=((int)Tower.CurrentMoney).ToString();
		
		if(Input.GetKeyDown(KeyCode.H))targetingHidden=!targetingHidden;
		if(Input.GetKeyDown(KeyCode.R))toggleTowerRangeVisibility();
	}
	
	void toggleTowerRangeVisibility()
	{
		Tower.toggleRangeVisibility();
//		Debug.Log("xxx");
	}
	
	
	void changeTowerSelected ()
	{
		towerGuiElements[selectedTower].intangibleTowerTemplate.deactivate();
		dimPreviousTowerButton();
		selectedTower=(selectedTower+1)%towerGuiElements.Length;
		highlightCurrentTower();
		towerGuiElements[selectedTower].intangibleTowerTemplate.activate();

	}
	
	void changeSpellSelected ()
	{
		
		
		
		dimPreviousSpell();
		spellGuiElements[selectedSpell].setInactive();
		selectedSpell=(selectedSpell+1)%spellGuiElements.Length;
		highlightCurrentSpell ();
		spellGuiElements[selectedSpell].setActive();

	}
	
	
	
	public void SwitchToBuildPhase()
	{
		dimPreviousSpell();
		spellGuiElements[selectedSpell].setInactive();

		highlightCurrentTower();
		highlightingShape.transform.Find("spells").renderer.enabled=false;
		highlightingShape.transform.Find("towers").renderer.enabled=true;
		fightPhaseLabel.renderer.enabled=false;
		buildPhaseLabel.renderer.enabled=true;
		towerGuiElements[selectedTower].intangibleTowerTemplate.activate();

	}

	
	
	public void SwitchToFightPhase()
	{
		
		dimPreviousTowerButton();
		towerGuiElements[selectedTower].intangibleTowerTemplate.deactivate();

		highlightCurrentSpell();
		highlightingShape.transform.Find("towers").renderer.enabled=false;
		highlightingShape.transform.Find("spells").renderer.enabled=true;
		fightPhaseLabel.renderer.enabled=true;
		buildPhaseLabel.renderer.enabled=false;
		Wave currentWave=StateMachine.getStateMachine().getCurrentWave();
		
		timeOrMonstersLeftLabel.text=monstersLeftBegin;//currentWave.NumberOfNotKilledEnemies;
		spellGuiElements[selectedSpell].setActive();

		
	}

	void highlightCurrentTower ()
	{
		GameObject buttonsPlace=towerGuiElements[selectedTower].inactiveButton;
		buttonsPlace.renderer.enabled=false;
		
		Vector3 temp=new Vector3(buttonsPlace.transform.position.x,selectionBox.transform.position.y,buttonsPlace.transform.position.z);
		selectionBox.transform.position=temp;
	}

	void dimPreviousTowerButton ()
	{
		towerGuiElements[selectedTower].inactiveButton.renderer.enabled=true;
	}

	void highlightCurrentSpell ()
	{
		GameObject buttonsPlace=spellGuiElements[selectedSpell].inactiveButton;
		buttonsPlace.renderer.enabled=false;
		
		Vector3 temp=new Vector3(buttonsPlace.transform.position.x,selectionBox.transform.position.y,buttonsPlace.transform.position.z);
		selectionBox.transform.position=temp;
	}

	void dimPreviousSpell ()
	{
		spellGuiElements[selectedSpell].inactiveButton.renderer.enabled=true;
	}
}



