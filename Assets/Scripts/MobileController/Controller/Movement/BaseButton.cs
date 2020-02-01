﻿using UnityEngine;

public abstract class BaseButton : MonoBehaviour
{
	public MobileController controller;

	public RectTransform centre;
	public float MaxDistance;

	public string MobileInputType;


	protected void Start()
	{
		Input.multiTouchEnabled = true;
	}


	protected void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Vector3 delta = Input.mousePosition - centre.position;
			float distance = Mathf.Abs(delta.magnitude);

			if (distance <= MaxDistance)
			{
				HandleInput(delta, distance);
				return;
			}

			HandleFailedInput();
		}
		else
		{
			Vector3 delta = Input.mousePosition - centre.position;
			float distance = Mathf.Abs(delta.magnitude);

			if (distance <= MaxDistance)
			{
				HandleFailedInput();
			}
		}

		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Ended)
				continue;

			Vector3 delta = (Vector3)touch.position - centre.position;
			float distance = Mathf.Abs(delta.magnitude);
			
			if (distance <= MaxDistance)
			{
				HandleInput(delta, distance);
				break;
			}
		}

		HandleFailedInput();
	}

	protected abstract void HandleInput(Vector3 delta, float distance);
	protected abstract void HandleFailedInput();
}