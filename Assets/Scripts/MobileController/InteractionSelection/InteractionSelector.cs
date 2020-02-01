﻿using System.Collections.Generic;
using UnityEngine;

public class InteractionSelector : MonoBehaviour, IControllable
{
	private List<Interactable> inRangeInteractables = new List<Interactable>();
	private Interactable currentClosest = null;

	private bool LookingUp = true;





	public void OnInputAcquired(MobileInput mobileInput)
	{
		if (mobileInput.InputType == InputTypes.Movement)
		{
			LookingUp = mobileInput.Value.y >= 0;
			return;
		}

		if (currentClosest == null)
			return;

		if (currentClosest.PowerType == mobileInput.InputType)
		{
			currentClosest.Interact(mobileInput);
		}
	}

	public void OnTriggerEnter(Collider collider)
	{
		Interactable interactable = collider.GetComponent<Interactable>();

		if (interactable == null)
			return;

		if (!inRangeInteractables.Contains(interactable))
		{
			inRangeInteractables.Add(interactable);
		}

		SetClosest();
	}

	public void OnTriggerExit(Collider collider)
	{
		Interactable interactable = collider.GetComponent<Interactable>();

		if (interactable == null)
			return;

		int i = inRangeInteractables.IndexOf(interactable);

		if (i != -1)
		{
			inRangeInteractables.RemoveAt(i);
		}

		SetClosest();
	}


	public void SetClosest()
	{
		Interactable closestTop = null;
		float minDistTop = Mathf.Infinity;

		Interactable closestBottom = null;
		float minDistBottom = Mathf.Infinity;


		foreach (Interactable current in inRangeInteractables)
		{
			float distance = Vector3.Distance(current.transform.position, transform.position);

			Vector3 delta = current.transform.position - transform.position;

			if (delta.y >= 0 && distance < minDistTop)
			{
				minDistTop = distance;
				closestTop = current;
				if (closestBottom == null)
				{
					closestBottom = current;
				}
			}
			else if (delta.y < 0 && distance < minDistBottom)
			{
				minDistBottom = distance;
				closestBottom = current;
				if (closestTop == null)
				{
					closestTop = current;
				}
			}
		}

		Interactable closest = LookingUp ? closestTop : closestBottom;
		if (currentClosest != closest)
		{
			if (currentClosest != null)
				currentClosest.Dehighlight();
			closest.Highlight();
		}
	}
}
