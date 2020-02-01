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
		for(int i = 0; i < Input.touches.Length; i++)
		{
			Touch touch = Input.touches[i];
			if (touch.phase == TouchPhase.Began)
			{
				if (InRange((Vector3)touch.position))
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


	protected virtual bool InRange(Vector3 position)
	{
		float d = Mathf.Abs((position - centre.position).magnitude);
		return d < MaxDistance;
	}


	protected abstract void HandleInput(Vector3 delta, float distance);
	protected abstract void HandleFailedInput();
}