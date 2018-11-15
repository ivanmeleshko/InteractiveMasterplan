using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour 
{

	public GameObject panel, raisingPanel, fallingPanel;


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
		RaisePanel();
	}


	public void RaisePanel()
	{
		if (raisingPanel != null)
		{
			Animator animator = raisingPanel.GetComponent<Animator>();
			if (animator != null)
			{
				bool isOpen = animator.GetBool("isOpen");
				animator.SetBool("isOpen", !isOpen);
			}
		}
		DownPanel();
	}


	public void DownPanel()
	{
		if (fallingPanel != null)
		{
			Animator animator = fallingPanel.GetComponent<Animator>();
			if (animator != null)
			{
				bool isOpen = animator.GetBool("open");
				animator.SetBool("open", !isOpen);
			}
		}
	}

}
