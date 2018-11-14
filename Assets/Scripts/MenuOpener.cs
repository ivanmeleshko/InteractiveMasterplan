using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour 
{

	public GameObject panel;


	public void OpenPanel()
	{
		if (panel != null)
		{
			Animator animator = panel.GetComponent<Animator>();
			if (animator != null)
			{
				bool isOpen = animator.GetBool("open");
				animator.SetBool("open", !isOpen);
			}
		}
	}
}
