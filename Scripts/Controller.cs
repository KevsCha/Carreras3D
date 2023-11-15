using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField] MonoBehaviour[] scripts;
	[SerializeField] CarMov car1;
	[SerializeField] CarMov car2;
	[SerializeField] GameObject[] itemBox;
	[SerializeField] ItemBox script;
	//-----------------------------GameObject del Item(scripts)-------------//
	[SerializeField] Cohete cohete;
	[SerializeField] Obstaculo obstaculo;
	[SerializeField] Plus plus;
	[SerializeField] Turbo turbo;

	[SerializeField] private int indice;
	private bool activate = true;
	private void Awake()
	{
		itemBox = GameObject.FindGameObjectsWithTag("ItemBox");
		Debug.Log(cohete.GetType());
	}

	private void Update()
	{
		if (car1.componentTemp != null)
		{
			if (cohete.GetType() == car1.componentTemp.GetType() && activate)
				cohete.Action();
			if (plus.GetType() == car1.componentTemp.GetType() && activate)
				plus.Action();
			if (turbo.GetType() == car1.componentTemp.GetType() && activate)
				turbo.Action();
			if (obstaculo.GetType() == car1.componentTemp.GetType() && activate)
				obstaculo.Action();
			activate = false;
			//car1.componentTemp = null;
		}
		//-------------------------------Cuando se desactiva un componente Item le obtiene el nombre del item en concreto-----------------------//
		if (car1.item != null && car1.compItem == true)
		{
			int i = 0;
			while (car1.item.name != itemBox[i].GetComponent<ItemBox>().name)
				i++;
			script = itemBox[i].GetComponent<ItemBox>();
			indice = i;
			car1.compItem = false;
		}
		if (!itemBox[indice].activeSelf)
		{
			//Debug.Log();
			car1.x = script.itemObject;
			//car1.GenObject();
		}
	}
}
