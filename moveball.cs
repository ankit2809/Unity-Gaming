using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Moveball : MonoBehaviour {

	private bool isPressed = false;
	public Rigidbody2D rb;
	public Rigidbody2D hook;
	public float releaseTime = 0.15f;
	public float maxDragDistance = 1f;
	public GameObject nextChance;

	void OnMouseDown ()
	{
		isPressed = true;
		rb.isKinematic = true;

	}

	void OnMouseUp ()
	{
		isPressed = false;
		rb.isKinematic = false;

		StartCoroutine(Release());
	}

	void Update ()
	{	
		if(isPressed)
		{	
			Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if(Vector3.Distance(mousepos,hook.position) > maxDragDistance)
			{
				rb.position = hook.position + (mousepos-hook.position).normalized*maxDragDistance;
			}
			else
			{
				rb.position = mousepos;
			}
			 
		}
	}

	IEnumerator Release()
	{
		yield return new WaitForSeconds(releaseTime);
		GetComponent<SpringJoint2D>().enabled = false;
		this.enabled = false;

		yield return new WaitForSeconds(2f);
		
		if (nextChance != null)
		{	
			nextChance.SetActive(true);
		}
		else
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

	}
}
