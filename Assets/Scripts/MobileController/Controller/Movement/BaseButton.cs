using UnityEngine;

public abstract class BaseButton : MonoBehaviour
{
	public MobileController controller;

	public RectTransform centre;
	public float MaxDistance;

	public string MobileInputType;


	private int myTouchIndex = -1;


	protected void Start()
	{
		Input.multiTouchEnabled = true;
	}

	protected void Update()
	{
		//if (Input.GetMouseButton(0))
		//{
		//	Vector3 delta = Input.mousePosition - centre.position;
		//	float distance = Mathf.Abs(delta.magnitude);

		//	if (distance <= MaxDistance)
		//	{
		//		HandleInput(delta, distance);
		//		return;
		//	}

		//	HandleFailedInput();
		//}
		//else
		//{
		//	Vector3 delta = Input.mousePosition - centre.position;
		//	float distance = Mathf.Abs(delta.magnitude);

		//	if (distance <= MaxDistance)
		//	{
		//		HandleFailedInput();
		//	}
		//}


		for(int i = 0; i < Input.touches.Length; i++)
		{
			Touch touch = Input.touches[i];
			if (touch.phase == TouchPhase.Began)
			{
				float d = Mathf.Abs(((Vector3)touch.position - centre.position).magnitude);
				if (d < MaxDistance)
				{
					myTouchIndex = i;
				}
			}
			else if (touch.phase == TouchPhase.Ended && i == myTouchIndex)
			{
				myTouchIndex = -1;
				HandleFailedInput();
			}
		}

		if (myTouchIndex > -1)
		{
			Touch touch = Input.GetTouch(myTouchIndex);

			Vector3 delta = (Vector3)touch.position - centre.position;
			float distance = delta.magnitude;

			HandleInput(delta, distance);
		}
	}

	protected abstract void HandleInput(Vector3 delta, float distance);
	protected abstract void HandleFailedInput();
}