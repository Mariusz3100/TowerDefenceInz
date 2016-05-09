using System;
using UnityEngine;


public class ParticleCircle:MonoBehaviour
{
	private float onCirclePosition=0;

	public float OnCirclePosition {
		get {
			return this.onCirclePosition;
		}
		set {
			onCirclePosition = value;
		}
	}

}