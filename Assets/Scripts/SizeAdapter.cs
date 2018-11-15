using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeAdapter : MonoBehaviour 
{

	public GameObject[] buttons = new GameObject[7];


	void Start () 
	{
		foreach (GameObject btn in buttons)
		{
			RectTransform rt = btn.GetComponent<RectTransform>();
			rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height / buttons.Length + 2);
		}
	}
	

	void Update () 
	{
		
	}
}
