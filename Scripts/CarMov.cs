using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CarMov : MonoBehaviour
{
	//!cambiar por un gameObject
	public Component[] componentes;
	public Component componentTemp;
	public GameObject x;
	public float velActual;
	[SerializeField] private float aceleracion = 20f;
	[SerializeField] private float sensitive = 1f;
	[SerializeField] private float angulo = 45f;
	[SerializeField] private float velMax = 200f;
	[SerializeField] private float frenar = 700f;
	[SerializeField] private string intItem;
	public bool compItem = false;
	public List<InfoEje> infoWheel = new();
	private Rigidbody rb;
	public ItemBox item;
	/*
	public enum X
	{
		Front,
		Back
	}
	public struct Wheel
	{
		public GameObject Model;
		public WheelCollider wheelCollider;
		public X x;
	}
	*/
	[System.Serializable]
	public class InfoEje
	{
		public WheelCollider rightWhell;
		public WheelCollider leftWhell;
		public Transform rightvisual;
		public Transform leftvisual;
		public bool isBack;
		public bool isFront;
	}
	private void Awake()
	{
		if (this.gameObject.GetComponent<Rigidbody>() != null)
		{
			//Debug.Log(this.name);
			rb = GetComponent<Rigidbody>();
			rb.centerOfMass = new(0, -0.9f, 0);
		}
	}
	//----------------------------controlles de disparo-----------------------------//
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			//componentes = x.GetComponents<Component>();
			componentTemp = x.GetComponents<Component>()[x.GetComponents<Component>().Length - 1];
			//Debug.Log(x.GetComponents<Component>()[componente.Length - 1].GetType()) ;
			//Debug.Log(componentTemp);
		}
	}

	private void FixedUpdate()
	{

		float movVer = Input.GetAxis("Vertical");
		float movHor = Input.GetAxis("Horizontal");

		//--------------------------mov de las ruedas, animacion de la misma--------------------------------------------------------//
		foreach (InfoEje info in infoWheel)
		{
			if (info.isBack)
			{
				//velActual = movVer > 0 ? rb.velocity.magnitude + movVer * aceleracion * 500 * Time.deltaTime : velActual - frenar * Time.deltaTime;
				velActual = rb.velocity.magnitude + movVer * aceleracion * 500 * Time.deltaTime;
				//---------------Freno---------------------//
				if (movVer == 0)
				{
					info.rightWhell.brakeTorque = 40000 * 2;
					info.rightWhell.brakeTorque = 40000 * 2;
					rb.AddForce(500 * aceleracion * -transform.forward);
					if (rb.velocity.magnitude < 1)
						rb.velocity = Vector3.zero;
				}
				else
				{
					info.rightWhell.brakeTorque = 0;
					info.leftWhell.brakeTorque = 0;
				}
				//---------------Aceleracion---------------------//
				if (movVer > 0)
				{

					Debug.Log(rb.velocity);
					rb.AddForce(200 * aceleracion * movVer * transform.forward);
				}
				if (velActual < velMax)
				{
					info.rightWhell.motorTorque = movVer * aceleracion * 500;
					info.leftWhell.motorTorque = movVer * aceleracion * 500;
				}
			}

			if (info.isFront)
			{
				var angle = movHor * sensitive * angulo;

				info.rightWhell.steerAngle = Mathf.Lerp(info.rightWhell.steerAngle, angle, 0.5f);
				info.leftWhell.steerAngle = Mathf.Lerp(info.leftWhell.steerAngle, angle, 0.5f);
				if (movVer == 0)
				{
					info.rightWhell.brakeTorque = frenar * rb.velocity.magnitude;
					info.leftWhell.brakeTorque = frenar * rb.velocity.magnitude;
				}
				else
				{
					info.rightWhell.brakeTorque = 0;
					info.leftWhell.brakeTorque = 0;
				}
			}
			AnimateWheel(info.leftvisual, info.leftWhell);
			AnimateWheel(info.rightvisual, info.rightWhell);
		}
	}
	private void AnimateWheel(Transform animatewheel, WheelCollider wheel)
	{
		Quaternion rot;
		Vector3 pos;
		//Vector3 rotate = Vector3.zero;

		wheel.GetWorldPose(out pos, out rot);
		animatewheel.transform.rotation = rot;
		animatewheel.transform.position = pos;
	}
	//----------------colisiona con el objeto y lo desactiva------------------//
	private void OnTriggerEnter(Collider other)
	{
		item = other.gameObject.GetComponent<ItemBox>();
		compItem = true;
		other.gameObject.SetActive(false);
		StartCoroutine(Inst(other.gameObject));
	}
	IEnumerator Inst(GameObject itemBox)
	{
		Invoke(nameof(GenObject), 2);
		yield return new WaitForSeconds(5);
		itemBox.SetActive(true);
	}
	public void GenObject()
	{
		Instantiate(x, transform.position + new Vector3(0, 5, 0), Quaternion.identity, transform);
		//Debug.Log(x);
	}
}
