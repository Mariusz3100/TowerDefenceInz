using System;
using UnityEngine;
using System.Collections;


public class GuiSelectableElement:MonoBehaviour
{

	public static GameObject selectionBox;
	public GameObject inactiveButton;
	public GameObject activeButton;
	
	public void Start()
	{
		selectionBox=Menu.getMenu().selectionBox;
		
		
	}

	public GuiSelectableElement ()
	{
	}
	
	public void setActive()
	{
		activeButton.renderer.enabled=true;
		inactiveButton.renderer.enabled=false;
		selectionBox.transform.position=transform.position;
	}
	
	
	public void setInactive()
	{
		activeButton.renderer.enabled=false;
		inactiveButton.renderer.enabled=true;
	}
	
}


