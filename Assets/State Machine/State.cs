using System;
using UnityEngine;

public abstract class State
{
	protected StateMachine stateMachine;
	protected Menu menu;
	
	
	public abstract void update();
	public abstract void turnOn();
	public abstract void turnOff();

	public State (StateMachine sm)
	{
		stateMachine=sm;
		menu=Menu.getMenu();
	}
	
	
}

