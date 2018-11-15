using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSizeFitter : MonoBehaviour
{
	
	public GameObject panelMenu, panelSettings;
	
	
	void Start ()
	{
		int screenHeight = Screen.height;

		RectTransform rt = panelMenu.GetComponent<RectTransform>();
		rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, screenHeight);

		rt = panelSettings.GetComponent<RectTransform>();
		rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, screenHeight);
	}
	

	void Update () 
	{
		
	}
}
