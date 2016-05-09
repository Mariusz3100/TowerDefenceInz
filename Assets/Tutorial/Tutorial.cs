using UnityEngine;
using System.Collections;

public class Tutorial: MonoBehaviour {

	private Vector3 inactivePosition=new Vector3(20,0,0);
	private Vector3 activePosition;
	private Color initialLensColor;
	
	
	void Start () {
		activePosition=transform.position;
		transform.position=inactivePosition;

		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		StateMachine sm=StateMachine.getStateMachine();
		
		if(initialLensColor==null)initialLensColor=sm.cam.lens.renderer.material.color;

		if(!(sm.GameLost||sm.GameWon))
			if(sm.GamePaused)
			{
				transform.position=activePosition;
				
				
				
				
				
				
				Color temp=Color.gray;
				temp.a=0.8f;
			
				
				
				sm.cam.lens.renderer.material.color=temp;
				
			}else{
				transform.position=inactivePosition;
				
				
				sm.cam.lens.renderer.material.color=initialLensColor;
				
				
			}
	}
}
