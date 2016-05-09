using System;
using UnityEngine;


public abstract class LivingObject:MonoBehaviour
{
	private Vector3 forceAddition;
	private float life;
	public float maxLife;
	
	OvertimeEffect[] activeEffects=new OvertimeEffect[2];
	int effectsCount=0;
	
	public LivingObject ()
	{
	}

	public Vector3 ForceAddition {
		get {
			return this.forceAddition;
		}
		set {
			forceAddition = value;
		}
	}
	public float Life {
		get {
			return this.life;
		}
		set {
			life = value;
		}
	}
	
	public void decreaseLife(float amount)
	{
		Life-=amount;
		
		if(Life<0)
			handleDying();
		
//		Debug.Log("life lowered to"+Life);
	}

	protected void UpdateEffects ()
	{
		for(int i=0;i<effectsCount;i++)
		{
			if(activeEffects[i]==null)continue;
			bool toDelete=!activeEffects[i].updateEffect();
			if(toDelete)activeEffects[i]=null;
		}
	}
			
	public void Update(){
	
	}
	
	
	public void Start()
	{
		Life=maxLife;
		
	}
	
	public abstract void handleDying();
	
	public void addEffect(OvertimeEffect oe)
	{
		
		for(int i=0;i<effectsCount;i++)
		{
			if(activeEffects[i]==null)
			{
				if(i<activeEffects.Length-1){
					activeEffects[i]=activeEffects[i+1];
					activeEffects[i+1]=null;
					continue;
				}else{
					activeEffects[i]=oe;
				}
			}
			if(activeEffects[i].GetType()==oe.GetType())
			{
				activeEffects[i]=oe;
				return;
			}
		}
		
		activeEffects[effectsCount]=oe;
		++effectsCount;
	}
}


