using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
	public GameObject cohete;
	public GameObject plus;
	public GameObject obstaculo;
	public GameObject turbo;
	private List<GameObject> items = new();
	public GameObject itemObject;


	private void Awake()
	{
		List_items();
	}
	private void OnTriggerEnter(Collider other)
	{
		int x;
		if (other.CompareTag("Carro"))
		{
			x = Random.Range(0, items.Count);
			itemObject = items[x];
		}
	}
	void List_items()
	{
		items.Add(turbo);
		items.Add(plus);
		items.Add(cohete);
		items.Add(obstaculo);
	}
	
	public void GenItem(GameObject item)
	{
		Instantiate(item, GetComponentInParent<Transform>().position + new Vector3(0, 5, 0), Quaternion.identity);
	}

}
