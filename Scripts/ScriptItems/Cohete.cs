using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cohete : MonoBehaviour, INterface
{
	GameObject objCohete;

	public void Action()
	{
		Lanzar();
	}
	void Lanzar()
	{
		Instantiate(objCohete, transform.position, Quaternion.identity, transform);
	}
}
