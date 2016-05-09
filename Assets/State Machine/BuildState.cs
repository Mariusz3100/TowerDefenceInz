using System;
using UnityEngine;

public class BuildState:State
{
	float timeLeft;

	public float TimeLeft {
		get {
			return this.timeLeft;
		}
		set {
			timeLeft = value;
		}
	}	
	public BuildState(StateMachine sm):base(sm)
	{
		menu.buildPhaseLabel.renderer.enabled=false;
	}

	public override void update()
	{
		menu.UpdateBuildPhase(this);
		TimeLeft-=Time.deltaTime;
		if(TimeLeft<0)stateMachine.changeState();
	}
	
	
	
	
	public override void turnOn()
	{
		
		menu.SwitchToBuildPhase();
		TimeLeft=GeneralParameters.buildStateLength;
		
	}
	
	public override void turnOff()
	{
//		Debug.Log("switch to fight");
	}

}


