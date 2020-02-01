using System.Collections.Generic;
using UnityEngine;

public class InteractionSelector : MonoBehaviour, IControllable
{
	private List<Interactable> inRangeInteractables = new List<Interactable>();
	private Interactable currentClosest = null;

	private bool LookingUp = true;


	void Update()
	{
		Vector3 lookDir = Vector3.up * Input.GetAxis("Vertical");

		if (lookDir.y != 0)
		{

			LookingUp = lookDir.y >= 0;
			SetClosest();
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
			interacting = true;
		else if (Input.GetKeyUp(KeyCode.Alpha2))
			interacting = false;

		if (interacting && currentClosest != null)
		{
			currentClosest.Interact(new MobileInput(InputTypes.PlantInteract, Vector3.zero));
			if (!currentClosest.Active)
			{
				inRangeInteractables.Remove(currentClosest);
				currentClosest = null;
				SetClosest();
			}
		}
	}

	bool interacting = false;

	public void OnInputAcquired(MobileInput mobileInput)
	{
		if (mobileInput.InputType == InputTypes.Movement)
		{
			LookingUp = mobileInput.Value.y >= 0;
			SetClosest();
			return;
		}

		if (currentClosest == null)
			return;

		if (currentClosest.PowerType == InputTypes.PlantInteract)
		{
			interacting = mobileInput.Value.y < 0;
			Debug.Log("swapping input");
		}
	}

	public void OnTriggerEnter(Collider collider)
	{
		Interactable interactable = collider.GetComponent<Interactable>();

		if (interactable == null && interactable.Active)
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
			if (!current.Active)
				continue;

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
			interacting = false;
			if (currentClosest != null)
				currentClosest.Dehighlight();
			if (closest != null)
				closest.Highlight();
			currentClosest = closest;
		}
	}
}
