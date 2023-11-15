using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisicasAuto : MonoBehaviour
{
	[SerializeField] public List<Caracteristicas> info;
	public float maxTorque;
	public float maxAngulo;
	private void FixedUpdate()
	{
		float motor = maxTorque * Input.GetAxis("Vertical");
		float direction = maxAngulo * Input.GetAxis("Horizontal");

		foreach (Caracteristicas caract in info)
		{
			if (caract.direction)
			{
				caract.FD.steerAngle = direction;
				caract.FI.steerAngle = direction;
			}
			if (caract.motor)
			{
				caract.FD.motorTorque = motor;
				caract.FI.motorTorque = motor;
			}
		}
	}
	[System.Serializable]
	public class Caracteristicas
	{
		public WheelCollider FD;
		public WheelCollider FI;
		public bool motor;
		public bool direction;
	}


}
