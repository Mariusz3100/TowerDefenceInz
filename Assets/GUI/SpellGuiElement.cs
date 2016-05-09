using System;


public class SpellGuiElement:GuiSelectableElement
{
	public Spell actualSpell;	
	
	
	
	public void Update()
	{
		
	}
	
	
	
	
	public new void setActive()
	{
		actualSpell.activate();
	}
	
	
	public new void setInactive()
	{
		actualSpell.deactivate();
	}
	
}


