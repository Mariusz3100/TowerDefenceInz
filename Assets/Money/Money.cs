using System;
using UnityEngine;


public class Money:MonoBehaviour
{
	private int amount;
	
	public TextMesh amountLabel;
	private float timeLeft;
	
	public int Amount {
		get {
			return this.amount;
		}
		set {
			amount = value;
			amountLabel.text=amount.ToString();
		}
	}	
	public Money ()
	{
	}
	
	public void Update()
	{
		timeLeft-=Time.deltaTime;
		if(timeLeft<0)Destroy(this.gameObject);
	
	}
	
	
	public Money createMoneyAt(Vector3 position, int amount)
	{
		Money ret= (Money)(Instantiate(this,position,this.transform.rotation));
		ret.Amount=amount;
		ret.timeLeft=GeneralParameters.moneyLife;
		return ret;
	}
	
	
	
	public void OnTriggerEnter(Collider other)
	{
		if(other.tag==TagNames.player)
		{
			Tower.CurrentMoney+=Amount;
		
			Destroy(this.gameObject);
		}
	}
}


